using ScriptExecutor.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

//the file to manage the log
namespace ScriptExecutor.Services
{
    public class LogManager : ILogManager
    {
        private const string LOGFILENAME = "log GameSave_Backup.txt"; //the path to the log file

        //read the whole log file
        public async Task<string> ReadLog()
        {
            if (File.Exists(LOGFILENAME))
            {
                return await File.ReadAllTextAsync(LOGFILENAME).ConfigureAwait(false);
            }
            return "";
        }

        //(re)write the entire log file
        public async void AddLog(string text)
        {
            var previousText = ReadLog();
            try
            {
                using StreamWriter sw = File.CreateText(LOGFILENAME);
                await sw.WriteLineAsync(text).ConfigureAwait(false);
                await sw.WriteLineAsync(previousText.Result).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}