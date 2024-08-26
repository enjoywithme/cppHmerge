using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace cppHmerge
{

    internal class HeadFile
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? FullPath { get; set; }
        public int Level { get; set; }
        public bool Processed { get; set; }
        public List<string> ReplacedHeaders { get; set; } = [];
        public List<HeadFile> IncludeFiles { get; set; } = [];

        public void IncreaseLevel(int levelUp)
        {

            Level += levelUp;
            foreach (var includeFile in IncludeFiles)
            {
                includeFile.IncreaseLevel(levelUp);
            }
        }
    }

    internal class CppHeaderMerge
    {
        public string? InputFile { get; set; }
        public string? OutputFile { get; set; }
        public List<string> IncludeDirs { get; set; } = [];
        private bool _stopPending;
        public string? LastError { get; set; }


        private readonly List<HeadFile> _headFiles = [];

        public event EventHandler? Stopped;
        public event EventHandler<HeadFile>? FileProcessed;
        public event EventHandler<HeadFile>? FileAdded;


        public bool Validate(out string message)
        {
            var sb = new StringBuilder();
            var ok = true;
            if (string.IsNullOrEmpty(InputFile) || !File.Exists(InputFile))
            {
                sb.AppendLine("The input header file must be specified.");
                ok = false;
            }

            if (string.IsNullOrEmpty(OutputFile))
            {
                sb.AppendLine("The output header file must be specified.");
                ok = false;
            }

            if (IncludeDirs.Count == 0)
            {
                sb.AppendLine("At least one include directory must be specified.");
                ok = false;
            }

            message = sb.ToString();
            return ok;
        }

        public void Start()
        {
            _stopPending = false;
            _headFiles.Clear();
            AddHeaderFile(new HeadFile()
            {
                Name = Path.GetFileName(InputFile),
                FullPath = InputFile,
                Level = 0,
                Processed = false
            });
            Task.Run(Execute);
        }

        public void Stop()
        {
            _stopPending = true;
        }

        private void Execute()
        {
            try
            {
                LastError = null;
                while (!_stopPending)
                {
                    var files = _headFiles.Where(x => x.Processed == false).ToList();
                    if (files.Count == 0)
                        break;

                    foreach (var file in files)
                    {
                        if (_stopPending)
                            break;
                        ProcessFile(file);
                    }

                }

                if (!_stopPending)
                {
                    WriteOutputFile();
                }
            }
            catch (Exception e)
            {
                LastError = e.Message;
            }
            

            
            Stopped?.Invoke(this,EventArgs.Empty);
        }

        private void WriteOutputFile()
        {
            var headFiles = _headFiles.Where(x => x.Level > 0).OrderByDescending(x => x.Level).ThenByDescending(x => x.Id).ToArray();
            using var outFile = new FileStream(OutputFile, FileMode.OpenOrCreate);
            outFile.SetLength(0);
            using var textWriter = new StreamWriter(outFile);

            var sb = new StringBuilder();
            for (var i = 0; i < headFiles.Length; i++)
            {
                var headFile = headFiles[i];

                sb.AppendLine($"//---{headFile.Id} {headFile.Level} {headFile.Name}");
            }

            for (var i = 0; i < headFiles.Length; i++)
            {
                var headFile = headFiles[i];
                if(string.IsNullOrEmpty(headFile.FullPath))
                    continue;

                sb.AppendLine($"//-----------{headFile.Name}-------------start\r\n");
                sb.AppendLine(File.ReadAllText(headFile.FullPath));
                
                foreach (var replacedHeader in headFile.ReplacedHeaders)
                {
                    sb.Replace(replacedHeader, "");
                }

                sb.AppendLine($"//-----------{headFile.Name}-------------end");
                sb.AppendLine();
                sb.AppendLine();

            }

            textWriter.Write(sb);

            textWriter.Close();
            outFile.Close();
        }

        private HeadFile? AddHeaderFile(HeadFile headFile)
        {
            if(string.IsNullOrEmpty(headFile.FullPath))
                return null;


            var file = _headFiles.FirstOrDefault(x =>
                string.Compare(x.FullPath, headFile.FullPath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (file == null)
            {
                headFile.Id = _headFiles.Count + 1;
                _headFiles.Add(headFile);
                FileAdded?.Invoke(this, headFile);

                return headFile;
            }

            if(headFile.Level > file.Level)
                file.IncreaseLevel(headFile.Level - file.Level);

            //System.Diagnostics.Debug.WriteLine($"File added:{file.FullPath}");
            return file;
        }

        private void ProcessFile(HeadFile headFile)
        {
            if (!File.Exists(headFile.FullPath))
            {
                Debug.WriteLine($"NOT FOUND:{headFile}");
                headFile.Processed = true;
                return;
            }
            var text = File.ReadAllText(headFile.FullPath);

            
            //Match #include <xxx.h>
            MatchIncludeFile(headFile,text, "#include\\s+<([\\S]*)>",true);

            //match #include "xxx.h"
            MatchIncludeFile(headFile,text, "#include\\s+\"([\\S]*)\"");


            //Mark as processed
            headFile.Processed = true;
            FileProcessed?.Invoke(this,headFile);
        }

        private void MatchIncludeFile(HeadFile headFile, string text, string matchRule,bool standardInclude = false)
        {
            var matches = Regex.Matches(text, matchRule, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                //Search header file
                var name = match.Groups[1].Value;
                var filePath = SearchHeadFile(name, standardInclude?null:Path.GetDirectoryName(headFile.FullPath));
                if (filePath == null)
                    continue;


                var headerFile = new HeadFile()
                {
                    Name = name,
                    Level = headFile.Level + 1,
                    FullPath = filePath,
                    Processed = false
                };
                headFile.ReplacedHeaders.Add(match.Groups[0].Value);

                var addedFile = AddHeaderFile(headerFile);
                if (addedFile != null)
                {
                    headFile.IncludeFiles.Add(addedFile);
                }

            }
        }

        private string? SearchHeadFile(string name,string? startDir = null)
        {
            if (!string.IsNullOrEmpty(startDir) && Directory.Exists(startDir))
            {
                var path = Path.Combine(startDir, name);
                if (File.Exists(path))
                {
                    path = new FileInfo(path).FullName;
                    return path;
                }
            }
            else
            {
                //Search in top folders
                foreach (var includeDir in IncludeDirs)
                {
                    if (!Directory.Exists(includeDir))
                        continue;

                    var files = Directory.GetFiles(includeDir);
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file);
                        if (fileName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                            return file;
                    }


                }
            }

            System.Diagnostics.Debug.WriteLine($"Not found:{name}");

            return null;
        }

    }
}
