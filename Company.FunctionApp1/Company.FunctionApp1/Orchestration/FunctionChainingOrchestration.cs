using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace Company.FunctionApp1.Orchestration;

public static class FunctionChainingOrchestration
{
    [Function(nameof(FunctionChainingOrchestration))]
    public static async Task<int> RunOrchestration([OrchestrationTrigger] TaskOrchestrationContext  context)
    {
        var input = 1;
        var step1Result = await context.CallActivityAsync<int>("ActivityStep1", input);
        var step2Result = await context.CallActivityAsync<int>("ActivityStep2", step1Result);
    
        return step2Result;
    }
    
    [Function("ActivityStep1")]
    public static int ActivityStep1([ActivityTrigger] int input)
    {
        return 1 + 1;
    }
    
    [Function("ActivityStep2")]
    public static int ActivityStep2([ActivityTrigger] int input)
    {
        return input * 2;
    }
}