using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.FunctionApp1;

public class SqlTrigger1(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SqlTrigger1>();

    // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
    [Function("SqlTrigger1")]
    public void Run(
        [SqlTrigger("[dbo].[ProjectComment]", "SqlConnectionString")] IReadOnlyList<SqlChange<object>> changes,
        FunctionContext context)
    {
        _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));
        
    }
}