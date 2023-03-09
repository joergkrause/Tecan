using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Tecan.Function
{
  public class DataEmulation
  {
    [FunctionName("DataEmulation")]
    // [return: Queue("data-emulation", Connection = "AzureWebJobsStorage")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer,
    ILogger log,
    [Queue("%DataIngressQueue%", Connection = "AzureWebJobsStorage")] IAsyncCollector<IotData> queueItem
    )
    {
      log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

      if (myTimer.IsPastDue)
      {
        log.LogInformation("Timer is running late!");
      }

      var data = new IotData
      {
        DeviceId = $"Device{Random.Shared.Next(1, 10)}",
        DeviceType = "Tecan",
        Value = Random.Shared.Next(0, 100)
      };

      await queueItem.AddAsync(data);

    }
  }
}
