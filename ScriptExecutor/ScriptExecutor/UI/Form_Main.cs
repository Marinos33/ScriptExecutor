using ScriptExecutor.Application;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_Main : Form
    {
        private Form_AddProcess form_AddProcess; //the form to add a process

        private readonly ILogManager _logManager; //the model from MVC pattern
        private readonly IProcessService _processService;
        private readonly IScriptRunner _scriptRunner;

        private bool isExist; //boolean to know if the app have to go minimize or completely exit, false = minimized/ true = quit

        public Form_Main(ILogManager logManager, IProcessService processService, IScriptRunner scriptRunner)
        {
            _logManager = logManager;
            _processService = processService;
            _scriptRunner = scriptRunner;

            Init();
        }

        /*event to fire before the form closing*/

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if click on the cross, the software will stay on the system tray. if click on exit, the software will really be closed
            if (!isExist)
            {
                e.Cancel = true;
            }

            Hide(); //hide the form
                    //notifyIcon.Visible = true;
        }

        private void BtAddProcess_Click(object sender, EventArgs e)
        {
            AddProcess();
        }

        private void BtExit_Click(object sender, EventArgs e)
        {
            OnExit();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnToolStripClick();
        }

        private void AddProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProcess();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExit();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnToolStripClick();
        }

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            bool isOpened = _logManager.OpenLogs();

            if (!isOpened)
            {
                MessageBox.Show("No logs file yet");
            }
        }

        private void DgvProcess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int columnIndex = e.ColumnIndex;
                int rowIndex = e.RowIndex;
                OnCellContentClick(columnIndex, rowIndex);
            }
        }

        private void Init()
        {
            try
            {
                InitializeComponent();

                FormClosing += Form1_FormClosing; //add event when click on the cross of the form

                notifyIcon.Icon = Resource.logo;

                PopulateGridView();

                _logManager.WriteLogAsync(DateTime.Now.ToString() + " > the program has been started");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateGridView()
        {
            try
            {
                var processs = _processService.GetProcesses();
                if (processs.Count > 0)
                {
                    dgvProgram.Rows.Clear();

                    foreach (Process process in processs)
                    {
                        //if the process added has been setup properly, use a green check, if not use a red cross
                        Bitmap picture;
                        if (!process.ExecutableFile.Equals("") && !process.Script.Equals(""))
                        {
                            picture = new Bitmap(Resource.check);
                        }
                        else
                        {
                            picture = new Bitmap(Resource.error);
                        }

                        Button button = new()
                        {
                            Text = "Edit",
                            AutoSize = true,
                            BackColor = Color.Gray,
                            ForeColor = Color.White
                        };

                        Button button2 = new()
                        {
                            Text = "Delete",
                            AutoSize = true,
                            BackColor = Color.Gray,
                            ForeColor = Color.White
                        };

                        dgvProgram.Rows.Add(new object[] { process.Name, picture, button, button2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddProcess()
        {
            try
            {
                form_AddProcess = new Form_AddProcess(_scriptRunner); //reset every input of the form to add process
                if (form_AddProcess.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add process
                {
                    _processService.AddProcessAsync(form_AddProcess.Process);
                    PopulateGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnModifyClick(int index)
        {
            try
            {
                //get the process to edit
                Process process = _processService.GetProcesses()[index];
                form_AddProcess = new Form_AddProcess(process, _scriptRunner);

                if (form_AddProcess.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add process
                {
                    process = null;
                    _processService.EditProcessAsync(form_AddProcess.Process, index);
                    PopulateGridView(); //recreate grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnDeleteClick(int index)
        {
            try
            {
                _processService.DeleteProcessAsync(index);
                dgvProgram.Rows.RemoveAt(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnCellContentClick(int colIndex, int rowIndex)
        {
            //check which column and row has been clicked and what to do with
            switch (colIndex)
            {
                case 2:
                    OnDeleteClick(rowIndex);
                    break;

                case 3:
                    OnModifyClick(rowIndex);
                    break;
            }
        }

        // the true exit
        private void OnExit()
        {
            try
            {
                isExist = true;
                _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the program has been shutdown");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnToolStripClick()
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}