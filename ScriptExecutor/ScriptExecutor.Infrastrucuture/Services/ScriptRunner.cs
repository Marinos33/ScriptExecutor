using ScriptExecutor.Application.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Services
{
    public class ScriptRunner : IScriptRunner
    {
        private readonly TimeSpan _timeout = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Runs a script asynchronously with options to show or hide the shell window
        /// </summary>
        /// <param name="script">The script content to execute</param>
        /// <param name="useShell">Whether to show a shell window (true) or run silently (false)</param>
        /// <returns>True if script executed successfully, false otherwise</returns>
        public async Task<bool> RunScriptAsync(string script, bool useShell = false)
        {
            if (string.IsNullOrEmpty(script))
            {
                return false;
            }

            var fileName = Guid.NewGuid().ToString() + ".bat";
            var batchPath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                await PrepareBatchFileAsync(script, batchPath, useShell);
                return await ExecuteProcessAsync(batchPath, useShell);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing script: {ex.Message}");
                return false;
            }
            finally
            {
                CleanupBatchFile(batchPath, useShell);
            }
        }

        private async Task PrepareBatchFileAsync(string script, string batchPath, bool useShell)
        {
            string finalScript = script;
            if (useShell)
            {
                finalScript = script + Environment.NewLine + "pause";
            }

            await File.WriteAllTextAsync(batchPath, finalScript).ConfigureAwait(false);
        }

        private async Task<bool> ExecuteProcessAsync(string batchPath, bool useShell)
        {
            using var process = new Process();
            ConfigureProcessStartInfo(process.StartInfo, batchPath, useShell);

            StringBuilder output = new();
            StringBuilder error = new();

            if (!useShell)
            {
                ConfigureOutputHandlers(process, output, error);
            }

            if (!process.Start())
            {
                Debug.WriteLine("Failed to start the script process");
                return false;
            }

            if (!useShell)
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                return await WaitForProcessExitAsync(process, output, error);
            }

            // When showing shell, we return immediately
            return true;
        }

        private void ConfigureProcessStartInfo(ProcessStartInfo startInfo, string batchPath, bool useShell)
        {
            startInfo.UseShellExecute = useShell;
            startInfo.FileName = batchPath;
            startInfo.CreateNoWindow = !useShell;
            startInfo.WorkingDirectory = Path.GetTempPath();

            // Only redirect if not using shell execution
            if (!useShell)
            {
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
            }
            else
            {
                startInfo.RedirectStandardOutput = false;
                startInfo.RedirectStandardError = false;
            }
        }

        private void ConfigureOutputHandlers(Process process, StringBuilder output, StringBuilder error)
        {
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    output.AppendLine(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    error.AppendLine(e.Data);
                }
            };
        }

        private async Task<bool> WaitForProcessExitAsync(Process process, StringBuilder output, StringBuilder error)
        {
            var exited = process.WaitForExit((int)_timeout.TotalMilliseconds);

            if (!exited)
            {
                process.Kill(true);
                Debug.WriteLine($"Process timed out after {_timeout.TotalSeconds} seconds");
                return false;
            }

            int exitCode = process.ExitCode;

            Debug.WriteLine($"Process completed with exit code: {exitCode}");

            if (output.Length > 0)
            {
                Debug.WriteLine($"Output: {output}");
            }

            if (error.Length > 0)
            {
                Debug.WriteLine($"Errors: {error}");
            }

            // Return true if exit code is 0 (success)
            return await Task.FromResult(exitCode == 0);
        }

        private void CleanupBatchFile(string batchPath, bool useShell)
        {
            if (!useShell && File.Exists(batchPath))
            {
                try
                {
                    File.Delete(batchPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to delete temporary script file: {ex.Message}");
                }
            }
        }
    }
}