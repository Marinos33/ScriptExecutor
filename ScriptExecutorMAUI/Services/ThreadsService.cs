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
                while (await _timer.WaitForNextTickAsync(_cts.Token))
                {
                    Debug.WriteLine(DateTime.Now.ToString("O"));
                }
            }
            catch (OperationCanceledException ex)
            {

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

    }
}
