namespace cppHmerge
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            tbInputFile = new TextBox();
            tbOutputFile = new TextBox();
            label3 = new Label();
            lsIncludeDirs = new ListBox();
            btSelectInput = new Button();
            btSelectOutput = new Button();
            btAddIncludeDir = new Button();
            btRemoveInlcudeDir = new Button();
            btExecute = new Button();
            label4 = new Label();
            tbLog = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 40);
            label1.Name = "label1";
            label1.Size = new Size(19, 17);
            label1.TabIndex = 0;
            label1.Text = "In";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 79);
            label2.Name = "label2";
            label2.Size = new Size(29, 17);
            label2.TabIndex = 0;
            label2.Text = "Out";
            // 
            // tbInputFile
            // 
            tbInputFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbInputFile.Location = new Point(146, 43);
            tbInputFile.Name = "tbInputFile";
            tbInputFile.Size = new Size(320, 23);
            tbInputFile.TabIndex = 1;
            // 
            // tbOutputFile
            // 
            tbOutputFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbOutputFile.Location = new Point(146, 76);
            tbOutputFile.Name = "tbOutputFile";
            tbOutputFile.Size = new Size(320, 23);
            tbOutputFile.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 113);
            label3.Name = "label3";
            label3.Size = new Size(116, 17);
            label3.TabIndex = 0;
            label3.Text = "Include directories";
            // 
            // lsIncludeDirs
            // 
            lsIncludeDirs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lsIncludeDirs.FormattingEnabled = true;
            lsIncludeDirs.ItemHeight = 17;
            lsIncludeDirs.Location = new Point(146, 113);
            lsIncludeDirs.Name = "lsIncludeDirs";
            lsIncludeDirs.Size = new Size(320, 174);
            lsIncludeDirs.TabIndex = 2;
            // 
            // btSelectInput
            // 
            btSelectInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btSelectInput.Location = new Point(494, 43);
            btSelectInput.Name = "btSelectInput";
            btSelectInput.Size = new Size(75, 23);
            btSelectInput.TabIndex = 3;
            btSelectInput.Text = "Select";
            btSelectInput.UseVisualStyleBackColor = true;
            // 
            // btSelectOutput
            // 
            btSelectOutput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btSelectOutput.Location = new Point(494, 77);
            btSelectOutput.Name = "btSelectOutput";
            btSelectOutput.Size = new Size(75, 23);
            btSelectOutput.TabIndex = 3;
            btSelectOutput.Text = "Select";
            btSelectOutput.UseVisualStyleBackColor = true;
            // 
            // btAddIncludeDir
            // 
            btAddIncludeDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAddIncludeDir.Location = new Point(494, 111);
            btAddIncludeDir.Name = "btAddIncludeDir";
            btAddIncludeDir.Size = new Size(75, 23);
            btAddIncludeDir.TabIndex = 3;
            btAddIncludeDir.Text = "Add";
            btAddIncludeDir.UseVisualStyleBackColor = true;
            // 
            // btRemoveInlcudeDir
            // 
            btRemoveInlcudeDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRemoveInlcudeDir.Location = new Point(494, 140);
            btRemoveInlcudeDir.Name = "btRemoveInlcudeDir";
            btRemoveInlcudeDir.Size = new Size(75, 23);
            btRemoveInlcudeDir.TabIndex = 3;
            btRemoveInlcudeDir.Text = "Remove";
            btRemoveInlcudeDir.UseVisualStyleBackColor = true;
            // 
            // btExecute
            // 
            btExecute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btExecute.Location = new Point(494, 299);
            btExecute.Name = "btExecute";
            btExecute.Size = new Size(75, 94);
            btExecute.TabIndex = 3;
            btExecute.Text = "Start";
            btExecute.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(84, 299);
            label4.Name = "label4";
            label4.Size = new Size(30, 17);
            label4.TabIndex = 0;
            label4.Text = "Log";
            // 
            // tbLog
            // 
            tbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbLog.Location = new Point(146, 296);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.ScrollBars = ScrollBars.Both;
            tbLog.Size = new Size(320, 97);
            tbLog.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 405);
            Controls.Add(btExecute);
            Controls.Add(btRemoveInlcudeDir);
            Controls.Add(btAddIncludeDir);
            Controls.Add(btSelectOutput);
            Controls.Add(btSelectInput);
            Controls.Add(lsIncludeDirs);
            Controls.Add(tbLog);
            Controls.Add(tbOutputFile);
            Controls.Add(tbInputFile);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormMain";
            Text = "CPP header merge";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox tbInputFile;
        private TextBox tbOutputFile;
        private Label label3;
        private ListBox lsIncludeDirs;
        private Button btSelectInput;
        private Button btSelectOutput;
        private Button btAddIncludeDir;
        private Button btRemoveInlcudeDir;
        private Button btExecute;
        private Label label4;
        private TextBox tbLog;
    }
}
