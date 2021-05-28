using ScriptExecutor.Interfaces;
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
                    _data.CurrentGame = _data.ListOfGame[i]; //take the game actually running in memory
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

            RunScript(_data.CurrentGame.Script); //run a script

            _data.ResetCurrentGame();
            _data.CurrentGame.SomethingHappened += HandleEvent; //event for observer pattern

            SearchProcess(HandleEvent);
        }

        /// <summary>
        /// the method to run the script associated with process curretnly observed
        /// </summary>
        /// <param name="script">the script to run</param>
        private void RunScript(string script)
        {
            if (script != "")
            {
                /*
                 * generate a file with the script => run the script => delete the file with the script
                 * **/

                var fileName = Guid.NewGuid().ToString() + ".bat"; //generate random name for the file
                var batchPath = Path.Combine(Environment.GetEnvironmentVariable("temp"), fileName); //set the path of the file to write in the appdata/temp

                var batchCode = script; //the script

                try
                {
                    File.WriteAllTextAsync(batchPath, batchCode); //create the file in appdata/temp with the script as content

                    Process.Start(batchPath).WaitForExit(); //run the script

                    File.Delete(batchPath); //delete the script
                    _logManager.AddLog(DateTime.Now.ToString() + "> script : " + fileName + " has been launched");
                }
                catch
                {
                    MessageBox.Show("ScriptExecutor: unable to run the script");
                    _logManager.AddLog(DateTime.Now.ToString() + "> unable to run the script " + fileName);
                }
            }
        }
    }
}