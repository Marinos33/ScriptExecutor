using System.Management;

namespace ScriptExecutorMAUI.Services
{
    public class ThreadsService
    {
        private readonly PeriodicTimer _timer;
        private Task _timerTask;
        private readonly CancellationTokenSource _cts = new();

        public ThreadsService(TimeSpan interval)
        {
            _timer = new PeriodicTimer(interval);
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
            var dataManager = new DataManager();
            var processes = await dataManager.ReadJson();

            foreach (var process in processes.Where(p => !string.IsNullOrEmpty(p.ExecutableFile) && !string.IsNullOrEmpty(p.Script)))
            {
                if (process.RunOnStart)
                {
                    WatchForProcessStart(process.ExecutableFile);
                }

                if (process.RunAfterShutdown)
                {
                    WatchForProcessEnd(process.ExecutableFile);
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
            Debug.WriteLine(string.Format("{0} process started", processName));
        }

        private void ProcessEnded(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            Debug.WriteLine(string.Format("{0} process ended", processName));
        }

    }
}
