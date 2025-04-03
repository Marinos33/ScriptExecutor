using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_AddGame : Form
    {
        /// <summary>
        /// the game to add or edit
        /// </summary>
        public Game Game { get; set; }

        private readonly IScriptRunner _scriptRunner;

        /// <summary>
        /// when click on AddGame on the main form
        /// </summary>
        public Form_AddGame(IScriptRunner scriptRunner)
        {
            InitializeComponent();
            _scriptRunner = scriptRunner;
        }

        /// <summary>
        /// when click on modify on the main form
        /// </summary>
        /// <param name="oldGame">the data of the game to edit if click on edit button in the grid of main form</param>
        public Form_AddGame(Game oldGame, IScriptRunner scriptRunner)
        {
            InitializeComponent();
            Game = oldGame;

            //set the text field to match the one which is currently modifying
            tbName.Text = Game.Name;
            tbExeFile.Text = Game.ExecutableFile;
            tbScript.Text = Game.Script;
            cbAfterShutdown.Checked = Game.RunAfterShutdown;
            cbOnLaunch.Checked = Game.RunOnStart;

            openFileExe.FileName = Game.ExecutableFile;

            _scriptRunner = scriptRunner;
        }

        private void PbExePathDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get its name
            if (openFileExe.ShowDialog() == DialogResult.OK)
            {
                tbExeFile.Text = openFileExe.SafeFileName; //get the filename and his ext
            }
        }

        private void BtValider_Click(object sender, EventArgs e)
        {
            //create the game
            if (Game == null)
            {
                Game = new Game
                {
                    Name = tbName.Text,
                    ExecutableFile = tbExeFile.Text,
                    Script = tbScript.Text,
                    RunAfterShutdown = cbAfterShutdown.Checked,
                    RunOnStart = cbOnLaunch.Checked
                };
            }
            //edit the game
            else
            {
                Game.Name = tbName.Text;
                Game.ExecutableFile = tbExeFile.Text;
                Game.Script = tbScript.Text;
                Game.RunAfterShutdown = cbAfterShutdown.Checked;
                Game.RunOnStart = cbOnLaunch.Checked;
            }
            DialogResult = DialogResult.OK; //tell the software that a game will be added
            Close();
        }

        private async void BtnRunScript_ClickAsync(object sender, EventArgs e)
        {
            bool isSciptExecuted = await _scriptRunner.RunScriptAsync(tbScript.Text).ConfigureAwait(false); //run a script
            if (isSciptExecuted)
            {
                MessageBox.Show("ScriptExecutor: script correctly runned");
            }
            else
            {
                MessageBox.Show("ScriptExecutor: unable to run the script");
            }
        }
    }
}