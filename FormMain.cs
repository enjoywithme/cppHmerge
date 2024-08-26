using cppHmerge;

namespace cppHmerge
{
    public partial class FormMain : Form
    {
        private OpenFileDialog? _openFileDialog;
        private SaveFileDialog? _saveFileDialog;
        private FolderBrowserDialog? _folderBrowserDialog;
        private CppHeaderMerge? _merge;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            btSelectInput.Click += BtSelectInput_Click;
            btSelectOutput.Click += BtSelectOutput_Click;
            btAddIncludeDir.Click += BtAddIncludeDir_Click;
            btRemoveInlcudeDir.Click += BtRemoveInlcudeDir_Click;
            btExecute.Click += BtExecute_Click;

        }

        private void BtExecute_Click(object? sender, EventArgs e)
        {
            if (_merge == null)
            {
                tbLog.Clear();

                _merge = new CppHeaderMerge()
                {
                    InputFile = tbInputFile.Text,
                    OutputFile = tbOutputFile.Text
                };
                _merge.IncludeDirs.AddRange(lsIncludeDirs.Items.OfType<string>().ToList());

                if (!_merge.Validate(out var message))
                {
                    tbLog.AppendText(message);
                    return;
                }
                _merge.Stopped += _merge_Stopped;
                _merge.FileProcessed += _merge_FileProcessed;
                _merge.FileAdded += _merge_FileAdded;
                _merge.Start();
                btExecute.Text = "Stop";
                LogMessage("Started");
            }
            else
            {
                _merge.Stop();
                btExecute.Enabled = false;
            }

            

        }

        private void _merge_FileAdded(object? sender, HeadFile e)
        {
            LogMessage($"File added:{e.Id} {e.FullPath}");

        }

        private void _merge_FileProcessed(object? sender, HeadFile e)
        {
            LogMessage($"File processed:{e.Id} {e.FullPath}");
        }

        private void _merge_Stopped(object? sender, EventArgs e)
        {
            Invoke(() =>
            {
                btExecute.Text = "Start";
                btExecute.Enabled = true;
                LogMessage("Stopped");
                if(!string.IsNullOrEmpty(_merge?.LastError))
                    LogMessage($"Error:{_merge.LastError}");
            });
            _merge = null;
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    tbLog.AppendText($"{message}\r\n");
                });

                return;
            }

            tbLog.AppendText($"{message}\r\n");

        }

        private void BtRemoveInlcudeDir_Click(object? sender, EventArgs e)
        {
            var selectedItems = lsIncludeDirs.SelectedItems;
            if(selectedItems.Count==0)return;
            for (var i = selectedItems.Count - 1; i >= 0; i--)
                lsIncludeDirs.Items.Remove(selectedItems[i]);
        }

        private void BtAddIncludeDir_Click(object? sender, EventArgs e)
        {
            _folderBrowserDialog ??=new FolderBrowserDialog();
            if (_folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                var dirs = lsIncludeDirs.Items.OfType<string>().ToList();
                if(dirs.Any(x=>string.Compare(x,_folderBrowserDialog.SelectedPath,StringComparison.InvariantCultureIgnoreCase)==0))
                    return;
                lsIncludeDirs.Items.Add(_folderBrowserDialog.SelectedPath);
            }
        }

        private void BtSelectOutput_Click(object? sender, EventArgs e)
        {
            _saveFileDialog??=new SaveFileDialog();
            _saveFileDialog.Filter = "Header files (*.h)|*.h";
            if (_saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                tbOutputFile.Text = _saveFileDialog.FileName;
            }
        }

        private void BtSelectInput_Click(object? sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();
            _openFileDialog.Filter = "Header files (*.h)|*.h";
            if (_openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                tbInputFile.Text = _openFileDialog.FileName;
            }
        }
    }
}
