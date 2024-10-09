using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace Company.FunctionApp1.Orchestration;

public static class FanInFanOutOrchestration
{
    [Function("FanInFanOutOrchestration")]
    public static async Task<List<string>> RunOrchestration([OrchestrationTrigger] TaskOrchestrationContext  context)
    {
        var tasks = new List<Task<string>>();
    
        for (int i = 1; i <= 5; i++)
        {
            tasks.Add(context.CallActivityAsync<string>("SingleActivity", i));
        }
    
        var result = await Task.WhenAll(tasks);
        return result.ToList();
    }
    
    [Function("SingleActivity")]
    public static string SingleActivity([ActivityTrigger] int taskId)
    {
        return $"This is result of task {taskId}";
    }
}