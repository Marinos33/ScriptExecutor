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
            tbPathExe.Text = Game.ExecutablePath;
            tbPathScript.Text = Game.ScriptPath;

            FileScript.FileName = Game.ScriptPath;
            FileExe.FileName = Game.ExecutablePath;
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
                if (Game == null)
                {
                    Game = new Game
                    {
                        Name = tbName.Text,
                        ExecutablePath = tbPathExe.Text,
                        ScriptPath = tbPathScript.Text
                    };
                }
                else
                {
                    Game.Name = tbName.Text;
                    Game.ExecutablePath = tbPathExe.Text;
                    Game.ScriptPath = tbPathScript.Text;
                }
                DialogResult = DialogResult.OK; //tell the software that a game will be added
                Close();
            }
        }
    }
}
