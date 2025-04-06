using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_AddProcess : Form
    {
        /// <summary>
        /// the process to add or edit
        /// </summary>
        public Process Process { get; set; }

        private readonly IScriptRunner _scriptRunner;

        /// <summary>
        /// when click on AddProcess on the main form
        /// </summary>
        public Form_AddProcess(IScriptRunner scriptRunner)
        {
            InitializeComponent();
            _scriptRunner = scriptRunner;
        }

        /// <summary>
        /// when click on modify on the main form
        /// </summary>
        /// <param name="oldProcess">the data of the process to edit if click on edit button in the grid of main form</param>
        public Form_AddProcess(Process oldProcess, IScriptRunner scriptRunner)
        {
            InitializeComponent();
            Process = oldProcess;

            //set the text field to match the one which is currently modifying
            tbName.Text = Process.Name;
            tbExeFile.Text = Process.ExecutableFile;
            tbScript.Text = Process.Script;
            cbAfterShutdown.Checked = Process.RunAfterShutdown;
            cbOnLaunch.Checked = Process.RunOnStart;

            openFileExe.FileName = Process.ExecutableFile;

            _scriptRunner = scriptRunner;
        }

        private void PbExePathDialog_Click(object sender, EventArgs e)
        {
            try
            {
                //if the file has been selected, get its name
                if (openFileExe.ShowDialog() == DialogResult.OK)
                {
                    tbExeFile.Text = openFileExe.SafeFileName; //get the filename and his ext
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtValider_Click(object sender, EventArgs e)
        {
            try
            {
                //create the process
                if (Process == null)
                {
                    Process = new Process
                    {
                        Name = tbName.Text,
                        ExecutableFile = tbExeFile.Text,
                        Script = tbScript.Text,
                        RunAfterShutdown = cbAfterShutdown.Checked,
                        RunOnStart = cbOnLaunch.Checked
                    };
                }
                //edit the process
                else
                {
                    Process.Name = tbName.Text;
                    Process.ExecutableFile = tbExeFile.Text;
                    Process.Script = tbScript.Text;
                    Process.RunAfterShutdown = cbAfterShutdown.Checked;
                    Process.RunOnStart = cbOnLaunch.Checked;
                }
                DialogResult = DialogResult.OK; //tell the software that a process will be added
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void BtnRunScript_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                bool isSciptExecuted = await _scriptRunner.RunScriptAsync(tbScript.Text).ConfigureAwait(false); //run a script
                if (isSciptExecuted)
                {
                    MessageBox.Show("Script correctly runned");
                }
                else
                {
                    MessageBox.Show("Unable to run the script");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}