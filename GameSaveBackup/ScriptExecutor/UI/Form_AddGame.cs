using GameSaveBackup.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_AddGame : Form
    {
        private Game game; //the game to add
        internal Game Game { get; set; }

        //when click on AddGame on the main form
        public Form_AddGame()
        {
            InitializeComponent();
        }

        //when click on modify on the main form
        public Form_AddGame(Game oldGame)
        {
            InitializeComponent();
            game = oldGame;

            //set the text field to match the one which is currently modifying
            tbName.Text = game.Name;
            tbPathExe.Text = game.ExecutablePath;
            tbPathScript.Text = game.ScriptPath;

            FileScript.FileName = game.ScriptPath;
            FileExe.FileName = game.ExecutablePath;
        }

        private void PbExePathDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get is name
            if (FileExe.ShowDialog() == DialogResult.OK)
            {
                tbPathExe.Text = FileExe.SafeFileName; //get the filename and his ext
            }
        }

        private void PbScriptFileDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get is name
            if (FileScript.ShowDialog() == DialogResult.OK)
            {
                tbPathScript.Text = FileScript.FileName; //get the full path
            }
        }

        private void BtValider_Click(object sender, EventArgs e)
        {
            //check if the script exists or not
            if (File.Exists(tbPathScript.Text) || tbPathScript.Text?.Length == 0)
            {
                FileScript.CheckFileExists = true;
            }
            else
            {
                MessageBox.Show("the file script not exists");
                FileScript.CheckFileExists = false;
            }

            if (FileScript.CheckFileExists)
            {
                //create or edit the game
                if (game == null)
                {
                    game = new Game
                    {
                        Name = tbName.Text,
                        ExecutablePath = tbPathExe.Text,
                        ScriptPath = tbPathScript.Text
                    };
                }
                else
                {
                    game.Name = tbName.Text;
                    game.ExecutablePath = tbPathExe.Text;
                    game.ScriptPath = tbPathScript.Text;
                }
                DialogResult = DialogResult.OK; //tell the software that a game will be added
                Close();
            }
        }
    }
}
