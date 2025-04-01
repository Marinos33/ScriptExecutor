using ScriptExecutor.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

//the file to manage the log
namespace ScriptExecutor.Infrastrucuture.Services
{
    public class LogManager : ILogManager
    {
        /// <summary>
        /// the path to the log file
        /// </summary>
        private const string LOGFILENAME = "Logs.txt";

        public async Task<string> ReadLogAsync()
        {
            if (File.Exists(LOGFILENAME))
            {
                return await File.ReadAllTextAsync(LOGFILENAME).ConfigureAwait(false);
            }
            return string.Empty;
        }

        public async Task WriteLogAsync(string text)
        {
            var previousText = ReadLogAsync();
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