using ScriptExecutor.Model;
using System;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_AddGame : Form
    {
        public Game Game { get; set; } //the game to add

        //when click on AddGame on the main form
        public Form_AddGame()
        {
            InitializeComponent();
        }

        //when click on modify on the main form
        public Form_AddGame(Game oldGame)
        {
            InitializeComponent();
            Game = oldGame;

            //set the text field to match the one which is currently modifying
            tbName.Text = Game.Name;
            tbExeFile.Text = Game.ExecutableFile;
            tbScript.Text = Game.Script;

            FileExe.FileName = Game.ExecutableFile;
        }

        private void PbExePathDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get is name
            if (FileExe.ShowDialog() == DialogResult.OK)
            {
                tbExeFile.Text = FileExe.SafeFileName; //get the filename and his ext
            }
        }

        private void BtValider_Click(object sender, EventArgs e)
        {
            //create or edit the game
            if (Game == null)
            {
                Game = new Game
                {
                    Name = tbName.Text,
                    ExecutableFile = tbExeFile.Text,
                    Script = tbScript.Text
                };
            }
            else
            {
                Game.Name = tbName.Text;
                Game.ExecutableFile = tbExeFile.Text;
                Game.Script = tbScript.Text;
            }
            DialogResult = DialogResult.OK; //tell the software that a game will be added
            Close();
        }
    }
}