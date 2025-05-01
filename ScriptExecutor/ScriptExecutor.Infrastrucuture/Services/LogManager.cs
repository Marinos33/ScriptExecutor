using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ScriptExecutor.Application.Interfaces;

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
            if (string.IsNullOrEmpty(text))
                return;

            try
            {
                string existingContent = await ReadLogAsync().ConfigureAwait(false);
                string newContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}{Environment.NewLine}{existingContent}";

                await File.WriteAllTextAsync(LOG_FILENAME, newContent).ConfigureAwait(false);
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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening log file: {ex.Message}");
                    return false;
                }
            }

            return false;
        }
    }
}