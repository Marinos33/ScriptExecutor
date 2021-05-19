using GameSaveBackup.Interfaces;
using System.IO;

//the file to manage the log
namespace GameSaveBackup.Services
{
    public class LogManager : ILogManager
    {
        private const string LOGFILENAME = "log GameSave_Backup.txt"; //the path to the log file

        //read the whole log file
        public string ReadLog()
        {
            if (File.Exists(LOGFILENAME))
            {
                return File.ReadAllText(LOGFILENAME);
            }
            return null;
        }

        //(re)write the entire log file
        public void AddLog(string text)
        {
            string previousText = ReadLog();
            using StreamWriter sw = File.CreateText(LOGFILENAME);
            sw.WriteLine(text);
            sw.WriteLine(previousText);
        }
    }
}