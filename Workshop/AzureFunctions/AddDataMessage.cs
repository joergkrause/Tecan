using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Tecan.Function
{
    public static class AddDataMessage
    {
        [FunctionName("AddDataMessage")]
        public static async Task<IActionResult> Run(
            //[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{value}/{deviceId}/{deviceType}")] HttpRequest req,
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            [Queue("%DataIngressQueue%", Connection = "AzureWebJobsStorage")] IAsyncCollector<IotData> queueItem
            )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            IotData data = JsonConvert.DeserializeObject(requestBody) as IotData;

            await queueItem.AddAsync(data);

            return new OkObjectResult("OK");
        }
    }
}
