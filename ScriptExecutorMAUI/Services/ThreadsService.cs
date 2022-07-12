using System.Management;

namespace ScriptExecutorMAUI.Services
{
    public class ThreadsService : IThreadsService
    {
        private Task _timerTask;
        private readonly CancellationTokenSource _cts = new();
        private readonly List<ManagementEventWatcher> watcherList = new();
        private readonly IDataManager _dataManager;
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;

        public ThreadsService(IDataManager dataManager, IScriptRunner scriptRunner, ILogManager logManager)
        {
            _dataManager = dataManager;
            _scriptRunner = scriptRunner;
            _logManager = logManager;
        }

        public void Start()
        {
            _timerTask = DoWorkAsync();
        }

        private async Task DoWorkAsync()
        {
            try
            {
                await RegisterProcess();
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task RegisterProcess()
        {
            var processes = await _dataManager.GetAllProcess();

            foreach (var process in processes.Where(p => !string.IsNullOrEmpty(p.ExecutableFile) && !string.IsNullOrEmpty(p.Script)))
            {
                if (process.RunOnStart)
                {
                    var watcher = WatchForProcessStart(process.ExecutableFile);
                    watcherList.Add(watcher);
                }

                if (process.RunAfterShutdown)
                {
                    var watcher = WatchForProcessEnd(process.ExecutableFile);
                    watcherList.Add(watcher);
                }
            }
        }

        public async Task StopAsync()
        {
            if (_timerTask is null)
            {
                return;
            }

            _cts.Cancel();
            await _timerTask;
            _cts.Dispose();
            Debug.WriteLine("Task stop");
        }

        private ManagementEventWatcher WatchForProcessStart(string processName)
        {
            string queryString =
                "SELECT TargetInstance" +
                "  FROM __InstanceCreationEvent " +
                "WITHIN  10 " +
                " WHERE TargetInstance ISA 'Win32_Process' " +
                "   AND TargetInstance.Name = '" + processName + "'";

            // The dot in the scope means use the current machine
            const string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            ManagementEventWatcher watcher = new(scope, queryString);
            watcher.EventArrived += OnProcessEvent;
            watcher.Start();
            return watcher;
        }

        private ManagementEventWatcher WatchForProcessEnd(string processName)
        {
            string queryString =
                "SELECT TargetInstance" +
                "  FROM __InstanceDeletionEvent " +
                "WITHIN  10 " +
                " WHERE TargetInstance ISA 'Win32_Process' " +
                "   AND TargetInstance.Name = '" + processName + "'";

            // The dot in the scope means use the current machine
            const string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            ManagementEventWatcher watcher = new(scope, queryString);
            watcher.EventArrived += OnProcessEvent;
            watcher.Start();
            return watcher;
        }

        private void OnProcessEvent(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            Task.Run(async () =>
            {
                var process = await _dataManager.GetProcessByExecutableFileName(processName);
                var isSucceed = await _scriptRunner.RunScript(process.Script);
                if (isSucceed)
                {
                    _logManager.AddLog($"{DateTime.Now}> script runned for {processName}");
                }
                else
                {
                    _logManager.AddLog($"{DateTime.Now}> failed to run script for {processName}");
                }
            });
        }

        public async Task RestartThread()
        {
            watcherList.ForEach(watcher => watcher.Stop());

            watcherList.Clear();

            await DoWorkAsync();
        }
    }
}