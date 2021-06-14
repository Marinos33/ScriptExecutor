
using System;

namespace ScriptExecutor.UI
{
    partial class Form_Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnAddProgram = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.systemTrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbProrgamObserved = new System.Windows.Forms.Label();
            this.dgvProgram = new System.Windows.Forms.DataGridView();
            this.programColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.deleteColBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.editColBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.enableColCheckBtn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnLogs = new System.Windows.Forms.Button();
            this.systemTrayMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgram)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTitle.Location = new System.Drawing.Point(134, 9);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(370, 65);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Script Executor";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAddProgram
            // 
            this.btnAddProgram.BackColor = System.Drawing.Color.Gray;
            this.btnAddProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddProgram.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddProgram.Location = new System.Drawing.Point(528, 102);
            this.btnAddProgram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddProgram.Name = "btnAddProgram";
            this.btnAddProgram.Size = new System.Drawing.Size(119, 40);
            this.btnAddProgram.TabIndex = 1;
            this.btnAddProgram.Text = "Add Program";
            this.btnAddProgram.UseVisualStyleBackColor = false;
            this.btnAddProgram.Click += new System.EventHandler(this.BtAddGame_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Location = new System.Drawing.Point(528, 733);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 38);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtExit_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.systemTrayMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GameSave Backup";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // systemTrayMenuStrip
            // 
            this.systemTrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.addProgramToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemTrayMenuStrip.Name = "contextMenuStrip1";
            this.systemTrayMenuStrip.Size = new System.Drawing.Size(144, 70);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openToolStripMenuItem.Text = "open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // addProgramToolStripMenuItem
            // 
            this.addProgramToolStripMenuItem.Name = "addProgramToolStripMenuItem";
            this.addProgramToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addProgramToolStripMenuItem.Text = "add program";
            this.addProgramToolStripMenuItem.Click += new System.EventHandler(this.AddGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // lbProrgamObserved
            // 
            this.lbProrgamObserved.AutoSize = true;
            this.lbProrgamObserved.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbProrgamObserved.Location = new System.Drawing.Point(15, 112);
            this.lbProrgamObserved.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProrgamObserved.Name = "lbProrgamObserved";
            this.lbProrgamObserved.Size = new System.Drawing.Size(0, 20);
            this.lbProrgamObserved.TabIndex = 4;
            // 
            // dgvProgram
            // 
            this.dgvProgram.AllowUserToAddRows = false;
            this.dgvProgram.AllowUserToDeleteRows = false;
            this.dgvProgram.AllowUserToResizeRows = false;
            this.dgvProgram.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProgram.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvProgram.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProgram.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvProgram.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgram.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.programColName,
            this.statusColImage,
            this.deleteColBtn,
            this.editColBtn,
            this.enableColCheckBtn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProgram.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProgram.Location = new System.Drawing.Point(68, 155);
            this.dgvProgram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvProgram.MultiSelect = false;
            this.dgvProgram.Name = "dgvProgram";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgram.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProgram.RowHeadersVisible = false;
            this.dgvProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvProgram.ShowCellErrors = false;
            this.dgvProgram.ShowCellToolTips = false;
            this.dgvProgram.Size = new System.Drawing.Size(518, 549);
            this.dgvProgram.TabIndex = 5;
            this.dgvProgram.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGame_CellContentClick);
            // 
            // programColName
            // 
            this.programColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.programColName.FillWeight = 200.5076F;
            this.programColName.HeaderText = "Program Name";
            this.programColName.Name = "programColName";
            this.programColName.ReadOnly = true;
            this.programColName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusColImage
            // 
            this.statusColImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusColImage.FillWeight = 74.8731F;
            this.statusColImage.HeaderText = "Status";
            this.statusColImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.statusColImage.Name = "statusColImage";
            this.statusColImage.ReadOnly = true;
            // 
            // deleteColBtn
            // 
            this.deleteColBtn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.deleteColBtn.FillWeight = 74.8731F;
            this.deleteColBtn.HeaderText = "Delete";
            this.deleteColBtn.Name = "deleteColBtn";
            this.deleteColBtn.ReadOnly = true;
            this.deleteColBtn.Text = "Delete";
            this.deleteColBtn.UseColumnTextForButtonValue = true;
            // 
            // editColBtn
            // 
            this.editColBtn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.editColBtn.FillWeight = 74.8731F;
            this.editColBtn.HeaderText = "Edit";
            this.editColBtn.Name = "editColBtn";
            this.editColBtn.ReadOnly = true;
            this.editColBtn.Text = "Edit";
            this.editColBtn.UseColumnTextForButtonValue = true;
            // 
            // enableColCheckBtn
            // 
            this.enableColCheckBtn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.enableColCheckBtn.FillWeight = 74.8731F;
            this.enableColCheckBtn.HeaderText = "Enable";
            this.enableColCheckBtn.Name = "enableColCheckBtn";
            // 
            // btnLogs
            // 
            this.btnLogs.BackColor = System.Drawing.Color.Gray;
            this.btnLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLogs.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogs.Location = new System.Drawing.Point(528, 63);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(119, 33);
            this.btnLogs.TabIndex = 6;
            this.btnLogs.Text = "See logs";
            this.btnLogs.UseVisualStyleBackColor = false;
            this.btnLogs.Click += new System.EventHandler(this.BtnLogs_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(662, 801);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.dgvProgram);
            this.Controls.Add(this.lbProrgamObserved);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddProgram);
            this.Controls.Add(this.lbTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_Main";
            this.Text = "GameSave Backup";
            this.systemTrayMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnAddProgram;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip systemTrayMenuStrip;
        private System.Windows.Forms.Label lbProrgamObserved;
        private System.Windows.Forms.DataGridView dgvProgram;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn programColName;
        private System.Windows.Forms.DataGridViewImageColumn statusColImage;
        private System.Windows.Forms.DataGridViewButtonColumn deleteColBtn;
        private System.Windows.Forms.DataGridViewButtonColumn editColBtn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enableColCheckBtn;
    }
}