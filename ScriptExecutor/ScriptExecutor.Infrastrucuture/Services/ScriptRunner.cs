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
        public async Task<bool> RunScriptAsync(string script)
        {
            if (string.IsNullOrEmpty(script))
            {
                return false;
            }

            try
            {
                // Generate a file with the script
                var fileName = Guid.NewGuid().ToString() + ".bat"; // Generate random name for the file
                var batchPath = Path.Combine(Path.GetTempPath(), fileName); // Use Path.GetTempPath() for better compatibility

                // Write the script to a temp file
                await File.WriteAllTextAsync(batchPath, script).ConfigureAwait(false);

                // Create process with output capture
                var process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = batchPath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

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

                // Start the process
                if (process.Start())
                {
                    // Begin reading outputs
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Wait for process to exit
                    process.WaitForExit();

                    // Get exit code (0 typically means success)
                    int exitCode = process.ExitCode;

                    // Log the results - you could inject ILogManager here as well
                    Debug.WriteLine($"Script {fileName} completed with exit code: {exitCode}");

                    if (output.Length > 0)
                    {
                        Debug.WriteLine($"Output: {output}");
                    }

                    if (error.Length > 0)
                    {
                        Debug.WriteLine($"Errors: {error}");
                    }

                    // Delete the script file
                    File.Delete(batchPath);

                    // Return true if exit code is 0 (success)
                    return exitCode == 0;
                }
                else
                {
                    Debug.WriteLine("Failed to start the script process");

                    // Clean up the file
                    if (File.Exists(batchPath))
                    {
                        File.Delete(batchPath);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions
                Debug.WriteLine($"Error executing script: {ex.Message}");
                return false;
            }
        }
    }
}