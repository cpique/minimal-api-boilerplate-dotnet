using TickerQ.Utilities.Base;

namespace MinimalApiBoilerplate.Api.Jobs;

public class MyJobs
{
	private readonly ILogger<MyJobs> _logger;

    public MyJobs(ILogger<MyJobs> logger)
	{
		_logger = logger;
	}

    [TickerFunction(functionName: nameof(MyCronTicker), cronExpression: "* * * * *")]
    public void MyCronTicker()
    {
        _logger.LogDebug("Cron ticker fired!");
        //Console.WriteLine("Cron ticker fired!");
    }
}