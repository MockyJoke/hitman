using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HitmanService.Controllers
{
    [Produces("application/json")]
    [Route("api/Queue")]
    public class QueueController : Controller
    {
        private CloudQueueClient _cloudQueueClient;

        public QueueController(CloudQueueClient cloudQueueClient)
        {
            _cloudQueueClient = cloudQueueClient;
        }

        [HttpGet("{queueName?}")]
        public async Task<IActionResult> Get(string queueName)
        {
            
            CloudQueue cloudQueue = _cloudQueueClient.GetQueueReference(queueName);
            try
            {
                if (!await cloudQueue.ExistsAsync())
                {
                    return NotFound();
                }
                // Async dequeue the message
                CloudQueueMessage retrievedMessage = await cloudQueue.GetMessageAsync();
                IActionResult result = Content(retrievedMessage.AsString);
                await cloudQueue.DeleteMessageAsync(retrievedMessage);
                return result;
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}