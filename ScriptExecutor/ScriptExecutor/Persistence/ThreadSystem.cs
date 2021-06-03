﻿using ScriptExecutor.Interfaces;
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
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;

        public ThreadSystem(IData data, IScriptRunner scriptRunner, ILogManager logManager)
        {
            _data = data;
            _scriptRunner = scriptRunner;
            _logManager = logManager;
        }

        public async void SearchProcess(EventHandler HandleEvent)
        {
            Thread.Sleep(2000); //wait 2 seconds

            bool found = false; //boolean to verify if a game has been found

            //iteration through list
            int i = 0;
            while (!found) //until a game has been found
            {
                if (_data.ListOfGame.Count > 0
                    && i <= _data.ListOfGame.Count
                    && Process.GetProcessesByName(Path.ChangeExtension(_data.ListOfGame[i].ExecutableFile, null)).Length != 0) //check if the game to observe is not null or ""
                {
                    found = true;
                    _data.CurrentGame = _data.ListOfGame[i].DeepCopy(); //take the game actually running in memory
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

            Process pname = (from p in Process.GetProcesses() where p.ProcessName == Path.ChangeExtension(_data.CurrentGame.ExecutableFile, null) select p).FirstOrDefault(); //select the first process with the given name in the process running

            //update the text label inside antoher thread than the ui one, source : https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
            /* string newText = "Waiting for " + gameFound.ExecutablePath + " to close";
             lbGameObserved.Invoke((MethodInvoker)delegate
             {
                 // Running on the UI thread
                 lbGameObserved.Text = newText;
             });*/

            pname.WaitForExit(); //the thread wait until the process has stopped

            bool isSciptRunned = await _scriptRunner.RunScript(_data.CurrentGame.Script).ConfigureAwait(false); //run a script
            if (isSciptRunned)
            {
                _logManager.AddLog(DateTime.Now.ToString() + "> script for " + _data.CurrentGame.ExecutableFile + " has been launched");
            }
            else
            {
                MessageBox.Show("ScriptExecutor: unable to run the script");
                _logManager.AddLog(DateTime.Now.ToString() + "> unable to run the script for " + _data.CurrentGame.ExecutableFile);
            }

            _data.ResetCurrentGame();
            _data.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern

            SearchProcess(HandleEvent);
        }
    }
}