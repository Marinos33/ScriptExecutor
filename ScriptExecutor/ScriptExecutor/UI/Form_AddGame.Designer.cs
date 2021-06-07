
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbGameName = new System.Windows.Forms.Label();
            this.lbExePath = new System.Windows.Forms.Label();
            this.lbScript = new System.Windows.Forms.Label();
            this.FileExe = new System.Windows.Forms.OpenFileDialog();
            this.tbExeFile = new System.Windows.Forms.TextBox();
            this.pbExePathDialog = new System.Windows.Forms.PictureBox();
            this.btValider = new System.Windows.Forms.Button();
            this.tbScript = new System.Windows.Forms.RichTextBox();
            this.btnRunScript = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbExePathDialog)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(79, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Game";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(20, 120);
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(241, 23);
            this.tbName.TabIndex = 1;
            // 
            // lbGameName
            // 
            this.lbGameName.AutoSize = true;
            this.lbGameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbGameName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbGameName.Location = new System.Drawing.Point(14, 88);
            this.lbGameName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbGameName.Name = "lbGameName";
            this.lbGameName.Size = new System.Drawing.Size(187, 25);
            this.lbGameName.TabIndex = 2;
            this.lbGameName.Text = "Name of the game";
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
            // FileExe
            // 
            this.FileExe.Filter = "Fichier executable|*.exe";
            this.FileExe.Title = "exe";
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
            // btValider
            // 
            this.btValider.BackColor = System.Drawing.Color.Gray;
            this.btValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btValider.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btValider.Location = new System.Drawing.Point(96, 574);
            this.btValider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btValider.Name = "btValider";
            this.btValider.Size = new System.Drawing.Size(145, 62);
            this.btValider.TabIndex = 9;
            this.btValider.Text = "Validate";
            this.btValider.UseVisualStyleBackColor = false;
            this.btValider.Click += new System.EventHandler(this.BtValider_Click);
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
            // Form_AddGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(370, 665);
            this.Controls.Add(this.btnRunScript);
            this.Controls.Add(this.tbScript);
            this.Controls.Add(this.btValider);
            this.Controls.Add(this.pbExePathDialog);
            this.Controls.Add(this.tbExeFile);
            this.Controls.Add(this.lbScript);
            this.Controls.Add(this.lbExePath);
            this.Controls.Add(this.lbGameName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_AddGame";
            this.Text = "Form_AddGame";
            ((System.ComponentModel.ISupportInitialize)(this.pbExePathDialog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbGameName;
        private System.Windows.Forms.Label lbExePath;
        private System.Windows.Forms.Label lbScript;
        private System.Windows.Forms.OpenFileDialog FileExe;
        private System.Windows.Forms.TextBox tbExeFile;
        private System.Windows.Forms.PictureBox pbExePathDialog;
        private System.Windows.Forms.Button btValider;
        private System.Windows.Forms.RichTextBox tbScript;
        private System.Windows.Forms.Button btnRunScript;
    }
}