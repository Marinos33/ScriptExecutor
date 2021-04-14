﻿using System;
using System.IO;
using System.Windows.Forms;

/**
 * This is the form to add a game to the software
 *
 */

namespace GameSaveBackup
{
    public partial class Form_AddGame : Form
    {
        private Game game; //the game to add

        internal Game Game { get => game; set => game = value; }

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

        private void pbExePathDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get is name
            if (FileExe.ShowDialog() == DialogResult.OK)
            {
                tbPathExe.Text = FileExe.SafeFileName; //get the filename and his ext
            }
        }

        private void pbScriptFileDialog_Click(object sender, EventArgs e)
        {
            //if the file has been selected, get is name
            if (FileScript.ShowDialog() == DialogResult.OK)
            {
                tbPathScript.Text = FileScript.FileName; //get the full path
            }
        }

        private void btValider_Click(object sender, EventArgs e)
        {
            //check if the script exists or not
            if (File.Exists(tbPathScript.Text) || tbPathScript.Text == "")
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
                    game = new Game(tbName.Text, tbPathExe.Text, tbPathScript.Text);
                }
                else
                {
                    game.Name = tbName.Text;
                    game.ExecutablePath = tbPathExe.Text;
                    game.ScriptPath = tbPathScript.Text;
                }
                this.DialogResult = DialogResult.OK; //tell the software that a game will be added
                this.Close();
            }
        }
    }
}