using ScriptExecutor.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ScriptExecutor.Services
{
    public class ScriptRunner : IScriptRunner
    {
        public async Task<bool> RunScript(string script)
        {
            if (script != "")
            {
                /*
                 * generate a file with the script => run the script => delete the file with the script
                 * **/

                var fileName = Guid.NewGuid().ToString() + ".bat"; //generate random name for the file
                var batchPath = Path.Combine(Environment.GetEnvironmentVariable("temp"), fileName); //set the path of the file to write in the appdata/temp

                var batchCode = script; //the script
                await File.WriteAllTextAsync(batchPath, batchCode).ConfigureAwait(false); //create the file in appdata/temp with the script as content

                Process process = new();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = batchPath;
                process.StartInfo.CreateNoWindow = true;

                //run the script
                if (process.Start())
                {
                    process.WaitForExit();
                    File.Delete(batchPath); //delete the script
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}