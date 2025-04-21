using ScriptExecutor.Application;

namespace ScriptExecutor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IProcessService _processService;
        public string Greeting { get; } = "Welcome to Avalonia!";

        public MainWindowViewModel(IProcessService processService)
        {
            _processService = processService;
        }
    }
}
