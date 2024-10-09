using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Company.FunctionApp1.TimerTrigger;

public class TimerTriggerFunctionChaining(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TimerTriggerFunctionChaining>();

    [Function(nameof(TimerTriggerFunctionChaining))]
    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        try
        {
            _logger.LogInformation("C# Timer trigger function processed a request from Timer Trigger Function Chaining Process.");
            await client.ScheduleNewOrchestrationInstanceAsync("FunctionChainingOrchestration");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start Orchestration");
            throw;
        }
    }
}
