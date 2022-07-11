using ScriptExecutorMAUI.Interfaces;
using System.Management;

namespace ScriptExecutorMAUI.Services
{
    public class ThreadsService: IThreadsService
    {
        private Task _timerTask;
        private readonly CancellationTokenSource _cts = new();
        private readonly List<ManagementEventWatcher> watcherList = new();
        private readonly IDataManager _dataManager;
        private readonly IScriptRunner _scriptRunner;

        public ThreadsService(IDataManager dataManager, IScriptRunner scriptRunner)
        {
            _dataManager = dataManager;
            _scriptRunner = scriptRunner;
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
            string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, queryString);
            watcher.EventArrived += ProcessStarted;
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
            string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, queryString);
            watcher.EventArrived += ProcessEnded;
            watcher.Start();
            return watcher;
        }

        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            Task.Run(async () =>
            {
                var process = await _dataManager.GetProcessByExecutableFileName(processName);
                await _scriptRunner.RunScript(process.Script);
            });
        }

        private void ProcessEnded(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            Task.Run(async () =>
            {
                var process = await _dataManager.GetProcessByExecutableFileName(processName);
                await _scriptRunner.RunScript(process.Script);
            });
        }

        public async Task RestartThread()
        {
            watcherList.ForEach(watcher =>
            {
                watcher.Stop();
            });

            watcherList.Clear();

            await DoWorkAsync();
        }

    }
}
