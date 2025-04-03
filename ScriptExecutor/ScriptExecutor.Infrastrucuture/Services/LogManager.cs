using ScriptExecutor.Application.Interfaces;
using System;
using System.Diagnostics;
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
        private const string LOG_FILENAME = "Logs.txt";

        public async Task<string> ReadLogAsync()
        {
            if (File.Exists(LOG_FILENAME))
            {
                return await File.ReadAllTextAsync(LOG_FILENAME).ConfigureAwait(false);
            }
            return string.Empty;
        }

        public async Task WriteLogAsync(string text)
        {
            var previousText = ReadLogAsync();
            try
            {
                using StreamWriter sw = File.CreateText(LOG_FILENAME);
                await sw.WriteLineAsync(text).ConfigureAwait(false);
                await sw.WriteLineAsync(previousText.Result).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool OpenLogs()
        {
            if (File.Exists(LOG_FILENAME))
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo(LOG_FILENAME)
                    {
                        UseShellExecute = true
                    }
                };

                return p.Start();
            }

            return false;
        }
    }
}