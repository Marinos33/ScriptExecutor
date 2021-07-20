
namespace ScriptExecutor.UI
{
    partial class Form_AddGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AddGame));
            this.lbTitleAddProgram = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbProgramName = new System.Windows.Forms.Label();
            this.lbExePath = new System.Windows.Forms.Label();
            this.lbScript = new System.Windows.Forms.Label();
            this.openFileExe = new System.Windows.Forms.OpenFileDialog();
            this.tbExeFile = new System.Windows.Forms.TextBox();
            this.pbExePathDialog = new System.Windows.Forms.PictureBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.tbScript = new System.Windows.Forms.RichTextBox();
            this.btnRunScript = new System.Windows.Forms.Button();
            this.gbChoiceRunningWhen = new System.Windows.Forms.GroupBox();
            this.cbAfterShutdown = new System.Windows.Forms.CheckBox();
            this.cbOnLaunch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbExePathDialog)).BeginInit();
            this.gbChoiceRunningWhen.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitleAddProgram
            // 
            this.lbTitleAddProgram.AutoSize = true;
            this.lbTitleAddProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbTitleAddProgram.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTitleAddProgram.Location = new System.Drawing.Point(79, 10);
            this.lbTitleAddProgram.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitleAddProgram.Name = "lbTitleAddProgram";
            this.lbTitleAddProgram.Size = new System.Drawing.Size(232, 39);
            this.lbTitleAddProgram.TabIndex = 0;
            this.lbTitleAddProgram.Text = "Add Program";
            this.lbTitleAddProgram.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(20, 120);
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(241, 23);
            this.tbName.TabIndex = 1;
            // 
            // lbProgramName
            // 
            this.lbProgramName.AutoSize = true;
            this.lbProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbProgramName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbProgramName.Location = new System.Drawing.Point(14, 88);
            this.lbProgramName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProgramName.Name = "lbProgramName";
            this.lbProgramName.Size = new System.Drawing.Size(213, 25);
            this.lbProgramName.TabIndex = 2;
            this.lbProgramName.Text = "Name of the program";
            // 
            // lbExePath
            // 
            this.lbExePath.AutoSize = true;
            this.lbExePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbExePath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbExePath.Location = new System.Drawing.Point(14, 183);
            this.lbExePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExePath.Name = "lbExePath";
            this.lbExePath.Size = new System.Drawing.Size(227, 25);
            this.lbExePath.TabIndex = 3;
            this.lbExePath.Text = "Path of the executable";
            // 
            // lbScript
            // 
            this.lbScript.AutoSize = true;
            this.lbScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbScript.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbScript.Location = new System.Drawing.Point(14, 298);
            this.lbScript.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbScript.Name = "lbScript";
            this.lbScript.Size = new System.Drawing.Size(128, 25);
            this.lbScript.TabIndex = 4;
            this.lbScript.Text = "Script to run";
            // 
            // openFileExe
            // 
            this.openFileExe.Filter = "Fichier executable|*.exe";
            this.openFileExe.Title = "exe";
            // 
            // tbExeFile
            // 
            this.tbExeFile.Location = new System.Drawing.Point(20, 217);
            this.tbExeFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbExeFile.Name = "tbExeFile";
            this.tbExeFile.Size = new System.Drawing.Size(241, 23);
            this.tbExeFile.TabIndex = 5;
            // 
            // pbExePathDialog
            // 
            this.pbExePathDialog.Image = global::ScriptExecutor.Resource.index;
            this.pbExePathDialog.Location = new System.Drawing.Point(268, 216);
            this.pbExePathDialog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbExePathDialog.Name = "pbExePathDialog";
            this.pbExePathDialog.Size = new System.Drawing.Size(46, 35);
            this.pbExePathDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExePathDialog.TabIndex = 7;
            this.pbExePathDialog.TabStop = false;
            this.pbExePathDialog.Click += new System.EventHandler(this.PbExePathDialog_Click);
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.Color.Gray;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValider.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnValider.Location = new System.Drawing.Point(96, 680);
            this.btnValider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(145, 62);
            this.btnValider.TabIndex = 9;
            this.btnValider.Text = "Validate";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.BtValider_Click);
            // 
            // tbScript
            // 
            this.tbScript.AcceptsTab = true;
            this.tbScript.DetectUrls = false;
            this.tbScript.Location = new System.Drawing.Point(20, 326);
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbScript.Size = new System.Drawing.Size(324, 186);
            this.tbScript.TabIndex = 10;
            this.tbScript.Text = "";
            // 
            // btnRunScript
            // 
            this.btnRunScript.BackColor = System.Drawing.Color.Gray;
            this.btnRunScript.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRunScript.Location = new System.Drawing.Point(268, 519);
            this.btnRunScript.Name = "btnRunScript";
            this.btnRunScript.Size = new System.Drawing.Size(75, 23);
            this.btnRunScript.TabIndex = 11;
            this.btnRunScript.Text = "run script";
            this.btnRunScript.UseVisualStyleBackColor = false;
            this.btnRunScript.Click += new System.EventHandler(this.BtnRunScript_ClickAsync);
            // 
            // gbChoiceRunningWhen
            // 
            this.gbChoiceRunningWhen.Controls.Add(this.cbOnLaunch);
            this.gbChoiceRunningWhen.Controls.Add(this.cbAfterShutdown);
            this.gbChoiceRunningWhen.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gbChoiceRunningWhen.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbChoiceRunningWhen.Location = new System.Drawing.Point(20, 556);
            this.gbChoiceRunningWhen.Name = "gbChoiceRunningWhen";
            this.gbChoiceRunningWhen.Size = new System.Drawing.Size(324, 100);
            this.gbChoiceRunningWhen.TabIndex = 12;
            this.gbChoiceRunningWhen.TabStop = false;
            this.gbChoiceRunningWhen.Text = "When to run the script";
            // 
            // cbAfterShutdown
            // 
            this.cbAfterShutdown.AutoSize = true;
            this.cbAfterShutdown.Checked = true;
            this.cbAfterShutdown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAfterShutdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbAfterShutdown.Location = new System.Drawing.Point(18, 31);
            this.cbAfterShutdown.Name = "cbAfterShutdown";
            this.cbAfterShutdown.Size = new System.Drawing.Size(128, 20);
            this.cbAfterShutdown.TabIndex = 2;
            this.cbAfterShutdown.Text = "After Shutdown";
            this.cbAfterShutdown.UseVisualStyleBackColor = true;
            // 
            // cbOnLaunch
            // 
            this.cbOnLaunch.AutoSize = true;
            this.cbOnLaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbOnLaunch.Location = new System.Drawing.Point(18, 57);
            this.cbOnLaunch.Name = "cbOnLaunch";
            this.cbOnLaunch.Size = new System.Drawing.Size(98, 20);
            this.cbOnLaunch.TabIndex = 3;
            this.cbOnLaunch.Text = "On Launch";
            this.cbOnLaunch.UseVisualStyleBackColor = true;
            // 
            // Form_AddGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(370, 754);
            this.Controls.Add(this.gbChoiceRunningWhen);
            this.Controls.Add(this.btnRunScript);
            this.Controls.Add(this.tbScript);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.pbExePathDialog);
            this.Controls.Add(this.tbExeFile);
            this.Controls.Add(this.lbScript);
            this.Controls.Add(this.lbExePath);
            this.Controls.Add(this.lbProgramName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbTitleAddProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_AddGame";
            this.Text = "Form_AddGame";
            ((System.ComponentModel.ISupportInitialize)(this.pbExePathDialog)).EndInit();
            this.gbChoiceRunningWhen.ResumeLayout(false);
            this.gbChoiceRunningWhen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitleAddProgram;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbProgramName;
        private System.Windows.Forms.Label lbExePath;
        private System.Windows.Forms.Label lbScript;
        private System.Windows.Forms.OpenFileDialog openFileExe;
        private System.Windows.Forms.TextBox tbExeFile;
        private System.Windows.Forms.PictureBox pbExePathDialog;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.RichTextBox tbScript;
        private System.Windows.Forms.Button btnRunScript;
        private System.Windows.Forms.GroupBox gbChoiceRunningWhen;
        private System.Windows.Forms.CheckBox cbOnLaunch;
        private System.Windows.Forms.CheckBox cbAfterShutdown;
    }
}