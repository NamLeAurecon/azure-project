using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Company.FunctionApp1.HttpTrigger;

public class LongRunFunction(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<LongRunFunction>();

    [Function(nameof(LongRunFunction))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "long-run")] HttpRequestData req,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        try
        {
            _logger.LogInformation("C# HTTP trigger function processed a request from Long Run Process.");
            var instanceId = await client.ScheduleNewOrchestrationInstanceAsync("FanInFanOutOrchestration");
            var result = await client.CreateCheckStatusResponseAsync(req, instanceId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start Orchestration");
            throw;
        }
    }
}