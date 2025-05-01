using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.ViewModels
{
    public class ProcessViewModel : ViewModelBase
    {
        private readonly Process _process;

        public ProcessViewModel(Process process)
        {
            _process = process;
        }

        public string ProcessName => _process.Name;
        public string ExecutableFile => _process.ExecutableFile;
        public string Script => _process.Script;
        public bool RunOnStart => _process.RunOnStart;
        public bool RunAfterShutdown => _process.RunAfterShutdown;
    }
}