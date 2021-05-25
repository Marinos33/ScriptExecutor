using ScriptExecutor.Interfaces;
using ScriptExecutor.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ScriptExecutor.Persistence
{
    public class ThreadSystem : IThreadSystem
    {
        private readonly IData _data;
        private readonly ILogManager _logManager;

        public ThreadSystem(IData data, ILogManager logManager)
        {
            _data = data;
            _logManager = logManager;
        }

        public void SearchProcess(EventHandler HandleEvent)
        {
            Thread.Sleep(2000); //wait 2 seconds

            //function to use with the thread
            bool found = false;

            int i = 0;
            while (!found) //until a game has been found
            {
                if (_data.ListOfGame.Count > 0 && i <= _data.ListOfGame.Count && Process.GetProcessesByName(Path.ChangeExtension(_data.ListOfGame[i].ExecutablePath, null)).Length != 0)
                {
                    found = true;
                    _data.CurrentGame = (Game)_data.ListOfGame[i].Clone();//take the game actually running in memory
                    _data.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern
                    _data.CurrentGame.Update();
                    break; //stop the loop
                }

                i++;

                if (i >= _data.ListOfGame.Count) //go back to the start of the list
                {
                    i = 0;
                }

                Thread.Sleep(2000); //wait 2 seconds
            }

            Process pname = (from p in Process.GetProcesses() where p.ProcessName == Path.ChangeExtension(_data.CurrentGame.ExecutablePath, null) select p).FirstOrDefault(); //select the first process with the given name in the process running

            //update the text label inside antoher thread than the ui one, source : https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
            /* string newText = "Waiting for " + gameFound.ExecutablePath + " to close";
             lbGameObserved.Invoke((MethodInvoker)delegate
             {
                 // Running on the UI thread
                 lbGameObserved.Text = newText;
             });*/

            pname.WaitForExit(); //the thread wait until the process has stopped

            RunScript(_data.CurrentGame.ScriptPath); //run a script

            _data.ResetCurrentGame();
            _data.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern

            SearchProcess(HandleEvent);
        }

        private void RunScript(string scriptPath)
        {
            if (scriptPath != "")
            {
                string scriptName = scriptPath[(scriptPath.LastIndexOf("\\") + 1)..];
                if (File.Exists(scriptPath))
                {
                    try
                    {
                        Process.Start(scriptPath);
                        _logManager.AddLog(DateTime.Now.ToString() + "> the script : " + scriptName + " has been launched");
                    }
                    catch
                    {
                        MessageBox.Show("GameSave_Backup: unable to run the script");
                        _logManager.AddLog(DateTime.Now.ToString() + "> unable to run the script " + scriptName);
                    }
                }
                else
                {
                    _logManager.AddLog(DateTime.Now.ToString() + "> the script " + scriptName + " is specified but cannot be found");
                }
            }
        }
    }
}