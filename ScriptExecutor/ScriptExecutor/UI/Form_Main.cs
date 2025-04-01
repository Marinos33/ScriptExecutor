using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScriptExecutor.UI
{
    public partial class Form_Main : Form, IObserver
    {
        private Form_AddGame form_AddGame; //the form to add a game

        private readonly ILogManager _logManager; //the model from MVC pattern
        private readonly IGameRepository _gameRepository; //the model wihch contains the data
        private readonly IForm_MainController _form_MainController;
        private readonly IThreadSystem _threadSystem;
        private readonly IScriptRunner _scriptRunner;

        private bool isExist; //boolean to know if the app have to go minimize or completely exit, false = minimized/ true = quit

        public Form_Main(ILogManager logManager, IGameRepository gameRepository, IForm_MainController form_MainController, IThreadSystem threadSystem, IScriptRunner scriptRunner)
        {
            _logManager = logManager;
            _gameRepository = gameRepository;
            _form_MainController = form_MainController;
            _threadSystem = threadSystem;
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

        private void BtAddGame_Click(object sender, EventArgs e)
        {
            AddGame();
        }

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

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            bool isOpened = _form_MainController.OpenLogs();

            if (!isOpened)
            {
                MessageBox.Show("No logs yet");
            }
        }

        private void DgvGame_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            InitializeComponent();

            FormClosing += Form1_FormClosing; //add event when click on the cross of the form

            notifyIcon.Icon = Resource.logo;

            PopulateGridView();

            //launch the thread, in background, responsible to find the game to backup
            Thread myThread = new(() => _threadSystem.SearchProcess(HandleEvent))
            {
                IsBackground = true
            };

            myThread.Start();

            _logManager.WriteLogAsync(DateTime.Now.ToString() + " > the program has been started");
        }

        private void PopulateGridView()
        {
            var games = _gameRepository.GetGames();
            if (games.Count > 0)
            {
                dgvProgram.Rows.Clear();

                foreach (Game game in games)
                {
                    //if the game added has been setup properly, use a green check, if not use a red cross
                    Bitmap picture;
                    if (!game.ExecutableFile.Equals("") && !game.Script.Equals(""))
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

                    dgvProgram.Rows.Add(new object[] { game.Name, picture, button, button2 });
                }
            }
        }

        private void AddGame()
        {
            form_AddGame = new Form_AddGame(_scriptRunner); //reset every input of the form to add game
            if (form_AddGame.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add game
            {
                _form_MainController.AddGame(form_AddGame.Game);
                PopulateGridView();
            }
        }

        private void OnModifyClick(int index)
        {
            //get the game to edit
            Game game = _gameRepository.GetGames()[index];
            form_AddGame = new Form_AddGame(game, _scriptRunner);

            if (form_AddGame.ShowDialog() == DialogResult.OK) //if everything went fine in the form to add game
            {
                _form_MainController.OnModifyClick(form_AddGame.Game, index);
                PopulateGridView(); //recreate grid
            }
        }

        private void OnDeleteClick(int index)
        {
            _form_MainController.OnDeleteClick(index);
            dgvProgram.Rows.RemoveAt(index);
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
            isExist = true;
            _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the program has been shutdown");
            Close();
        }

        private void OnToolStripClick()
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        /*event fired when the current game change*/

        public void HandleEvent(object sender, EventArgs args)
        {
            string text;
            var game = _threadSystem.RunningGame;
            if (game.Name == null && game.ExecutableFile == null && game.Script == null)
            {
                text = "";
            }
            else
            {
                text = "Waiting for " + game.ExecutableFile + " to close";
            }

            //to change te text in the UI thread by another thread
            lbProgamObserved.Invoke((MethodInvoker)(() => lbProgamObserved.Text = text));
        }
    }
}