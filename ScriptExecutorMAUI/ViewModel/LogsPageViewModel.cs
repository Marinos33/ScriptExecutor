using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel
{
    public partial class LogsPageViewModel : ObservableObject
    {
        public ObservableCollection<Log> Logs { get; } = new();

        private readonly ILogManager _logManager;

        public LogsPageViewModel(ILogManager logManager)
        {
            _logManager = logManager;
        }

        [RelayCommand]
        public async Task ReadLogs()
        {
            var logs = await _logManager.ReadLog();
            var logsList = logs.Split(Environment.NewLine).ToList();
            logsList.ForEach((log) =>
            {
                Logs.Add(new Log(log));
            });
        }
    }
}
