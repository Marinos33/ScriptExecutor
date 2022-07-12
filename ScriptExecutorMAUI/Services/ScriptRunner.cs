using SystemProcess = System.Diagnostics.Process;

namespace ScriptExecutorMAUI.Services
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

                await File.WriteAllTextAsync(batchPath, script).ConfigureAwait(false); //create the file in appdata/temp with the script as content

                SystemProcess process = new();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = batchPath;
                process.StartInfo.CreateNoWindow = true;

                //run the script
                process.Start();
                await Task.Delay(2000);
                process.WaitForExit();
                if (process.HasExited)
                {
                    File.Delete(batchPath); //delete the script
                    return true;
                }
            }

            return false;
        }
    }
}