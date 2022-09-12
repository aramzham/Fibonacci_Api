namespace FibonacciApi.Api.Infrastructure;

public class BackgroundTask
{
    private Task _timerTask;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cts = new();

    public BackgroundTask(TimeSpan interval)
    {
        _timer = new PeriodicTimer(interval);
    }

    public void Start(Action action)
    {
        _timerTask = DoWork(action);
    }

    public async Task Stop()
    {
        if(_timerTask is null)
            return;

        _cts.Cancel();
        await _timerTask;
        _cts.Dispose();
    }

    private async Task DoWork(Action action)
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                action();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }
}