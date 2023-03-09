using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Tecan.Function
{
  public class DataProcessing
  {
    [FunctionName("DataProcessing")]
    [return: Table("data", Connection = "AzureWebJobsStorage")]
    public IotDataProcessed Run(
      [QueueTrigger("%DataIngressQueue%", Connection = "AzureWebJobsStorage")] IotData queueItem,
      ILogger log      
      )
    {
      log.LogInformation($"C# Queue trigger function processed: {queueItem.DeviceId}");

      // TODO: Add your processing logic here
      if (queueItem.Value > 1)
      {
        var tableItem = new IotDataProcessed
        {
          PartitionKey = queueItem.DeviceId,
          RowKey = Guid.NewGuid().ToString(),
          DeviceId = queueItem.DeviceId,
          DeviceType = queueItem.DeviceType,
          Value = queueItem.Value
        };
        return tableItem;
      }
      return null;
    }
  }
}
