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

        public async Task<bool> RunScriptAsync(string script)
        {
            if (string.IsNullOrEmpty(script))
            {
                return false;
            }

            string batchPath = null;

            try
            {
                var fileName = Guid.NewGuid().ToString() + ".bat";
                batchPath = Path.Combine(Path.GetTempPath(), fileName);

                await File.WriteAllTextAsync(batchPath, script).ConfigureAwait(false);

                // Create process with output capture
                using var process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = batchPath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WorkingDirectory = Path.GetTempPath(); // Set working directory

                StringBuilder output = new();
                StringBuilder error = new();

                // Set up output and error handlers
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

                if (process.Start())
                {
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Wait for process to exit with timeout
                    var exited = process.WaitForExit((int)_timeout.TotalMilliseconds);

                    if (!exited)
                    {
                        process.Kill(true);
                        Debug.WriteLine($"Script {fileName} timed out after {_timeout.TotalSeconds} seconds");
                        return false;
                    }

                    int exitCode = process.ExitCode;

                    Debug.WriteLine($"Script {fileName} completed with exit code: {exitCode}");

                    if (output.Length > 0)
                    {
                        Debug.WriteLine($"Output: {output}");
                    }

                    if (error.Length > 0)
                    {
                        Debug.WriteLine($"Errors: {error}");
                    }

                    // Return true if exit code is 0 (success)
                    return exitCode == 0;
                }
                else
                {
                    Debug.WriteLine("Failed to start the script process");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing script: {ex.Message}");
                return false;
            }
            finally
            {
                // Clean up the file in finally block to ensure it always gets deleted
                if (batchPath != null && File.Exists(batchPath))
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
}