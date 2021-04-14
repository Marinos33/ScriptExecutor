﻿using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GameSaveBackup
{
    public partial class Form_Main : Form, IObserver
    {
        /// <summary>
        /// ******************************///
        ///variable and constructor part///
        ///****************************///
        /// </summary>
        private Form_AddGame form_AddGame; //the form to add a game

        private readonly Model model = new Model(); //the model from MVC pattern

        private bool isExist = false; //boolean to know if the app have to go minimize or completely exit, false = minimized/ true = quit

        public Form_Main()
        {
            InitializeComponent();

            FormClosing += Form1_FormClosing; //add event when click on the cross of the form

            notifyIcon.Icon = Resource.logo;

            PopulateGridView();

            //launch the thread, in background, responsible to find the game to backup
            Thread myThread = new Thread(new ThreadStart(SearchProcess))
            {
                IsBackground = true
            };
            myThread.Start();

            model.WriteLog(DateTime.Now.ToString() + " > the program has been started");
        }

        /// <summary>
        /// ***************///
        ///UI input parts///
        ///*************///
        /// </summary>
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

        private void BtAddGame_Click(object sender, EventArgs e)
        {
            AddGame();
        }

        // the true exit
        private void BtExit_Click(object sender, EventArgs e)
        {
            OnExit();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnToolStripClick();
        }

        private void AddGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGame();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExit();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnToolStripClick();
        }

        private void DgvGame_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            OnCellContentClick(dgv, columnIndex, rowIndex);
        }

        /// <summary>
        /// **********************///
        ///thread background part///
        ///*********************///
        /// </summary>
        private void SearchProcess()
        {
            Thread.Sleep(2000); //wait 2 seconds

            //function to use with the thread
            bool found = false;

            int i = 0;
            while (!found) //until a game has been found
            {
                if (model.ListOfGame.Count > 0 && Process.GetProcessesByName(Path.ChangeExtension(model.ListOfGame[i].ExecutablePath, null)).Length != 0)
                {
                    found = true;
                    model.CurrentGame = (Game)model.ListOfGame[i].Clone(); //take the game actually running in memory
                    model.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern
                    model.CurrentGame.Update();
                    break; //stop the loop
                }

                i++;

                if (i >= model.ListOfGame.Count) //go back to the start of the list
                {
                    i = 0;
                }

                Thread.Sleep(2000); //wait 5 seconds
            }

            Process pname = (from p in Process.GetProcesses() where p.ProcessName == Path.ChangeExtension(model.CurrentGame.ExecutablePath, null) select p).FirstOrDefault(); //select the first process with the given name in the process running

            //update the text label inside antoher thread than the ui one, source : https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
            /* string newText = "Waiting for " + gameFound.ExecutablePath + " to close";
             lbGameObserved.Invoke((MethodInvoker)delegate
             {
                 // Running on the UI thread
                 lbGameObserved.Text = newText;
             });*/

            pname.WaitForExit(); //the thread wait until the process has stopped

            RunScript(model.CurrentGame.ScriptPath); //run a script

            model.ResetCurrentGame();
            model.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern

            SearchProcess();
        }

        private void RunScript(string scriptPath)
        {
            if (scriptPath != "")
            {
                string scriptName = scriptPath.Substring(scriptPath.LastIndexOf("\\") + 1);
                if (File.Exists(scriptPath))
                {
                    try
                    {
                        Process.Start(scriptPath);
                        model.WriteLog(DateTime.Now.ToString() + "> the script : " + scriptName + " has been launched");
                    }
                    catch
                    {
                        MessageBox.Show("GameSave_Backup: unable to run the script to backup");
                        model.WriteLog(DateTime.Now.ToString() + "> unable to run the script " + scriptName + " to backup");
                    }
                }
                else
                {
                    model.WriteLog(DateTime.Now.ToString() + "> the script " + scriptName + " is specified but cannot be found");
                }
            }
        }

        /// <summary>
        /// *******************///
        ///custom method part///
        ///*****************///
        /// </summary>
        private void AddGame()
        {
            form_AddGame = new Form_AddGame(); //reset every input of the form to add game
            if (form_AddGame.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add game
            {
                model.AddGame(form_AddGame.Game);
                PopulateGridView();
                model.WriteCsv();
                model.WriteLog(DateTime.Now.ToString() + " > the game : " + form_AddGame.Game.Name + " has been added");
            }
        }

        private void PopulateGridView()
        {
            if (model.ListOfGame.Count > 0)
            {
                dgvGame.Rows.Clear();

                foreach (Game game in model.ListOfGame)
                {
                    //if the game added has been setup properly, use a green check, if not use a red cross
                    Bitmap picture;
                    if (!game.ExecutablePath.Equals("") && !game.ScriptPath.Equals(""))
                    {
                        picture = new Bitmap(Resource.check);
                    }
                    else
                    {
                        picture = new Bitmap(Resource.error);
                    }

                    Button button = new Button
                    {
                        Text = "Edit",
                        AutoSize = true,
                        BackColor = Color.Gray,
                        ForeColor = Color.White
                    };

                    Button button2 = new Button
                    {
                        Text = "Delete",
                        AutoSize = true,
                        BackColor = Color.Gray,
                        ForeColor = Color.White
                    };

                    dgvGame.Rows.Add(new Object[] { game.Name, picture, button, button2, game.Enabled });
                }
            }
        }

        private void OnModifyClick(int index)
        {
            //get the game to edit
            Game game = model.ListOfGame[index];
            form_AddGame = new Form_AddGame(game);

            if (form_AddGame.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add game
            {
                //replace the oldGame with a new one
                model.ListOfGame[index] = form_AddGame.Game;
                model.ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name));
                PopulateGridView(); //recreate grid
                model.WriteCsv();
                model.WriteLog(DateTime.Now.ToString() + "> the game : " + game.Name + " has been modified");
            }
        }

        private void OnDeleteClick(int index)
        {
            string oldGame = model.ListOfGame[index].Name; //get the game name to delete for the log
            //remove the game
            model.ListOfGame.RemoveAt(index);
            dgvGame.Rows.RemoveAt(index);
            //write files
            model.WriteCsv();
            model.WriteLog(DateTime.Now.ToString() + "> the game : " + oldGame + " has been deleted");
        }

        private void OnCheck(int index, bool c)
        {
            model.ListOfGame[index].Enabled = c;
            model.WriteCsv();
        }

        private void OnCellContentClick(DataGridView dgv, int colIndex, int rowIndex)
        {
            //check which column and row has been clicked and what to do with
            switch (colIndex)
            {
                case 2:
                    OnModifyClick(rowIndex);
                    break;

                case 3:
                    OnDeleteClick(rowIndex);
                    break;

                case 4:
                    OnCheck(rowIndex, (bool)((DataGridViewCheckBoxCell)dgv.Rows[rowIndex].Cells[colIndex]).EditedFormattedValue); //the second arg is to get the status of the checkbox as a boolean
                    break;
            }
        }

        private void OnExit()
        {
            isExist = true;
            model.WriteLog(DateTime.Now.ToString() + "> the program has been shutdown");
            Close();
        }

        private void OnToolStripClick()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        /*event fired when the current game change*/

        public void HandleEvent(object sender, EventArgs args)
        {
            string text;
            if (model.CurrentGame.Name == null && model.CurrentGame.ExecutablePath == null && model.CurrentGame.ScriptPath == null)
            {
                text = "";
            }
            else
            {
                text = "Waiting for " + model.CurrentGame.ExecutablePath + " to close";
            }
            //to change te text in the UI thread by another thread
            lbGameObserved.Invoke((MethodInvoker)(() => lbGameObserved.Text = text));
        }
    }
}