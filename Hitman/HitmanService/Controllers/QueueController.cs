using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HitmanService.Services.Queue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HitmanService.Controllers
{
    [Produces("application/json")]
    [Route("api/Queue")]
    public class QueueController : Controller
    {
        private IQueueClient _queueClient;

        public QueueController(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        [HttpGet("{queueName?}")]
        public async Task<IActionResult> Get(string queueName)
        {
            IQueue queue = await _queueClient.GetQueueAsync(queueName);
            try
            {
                if (queue == null)
                {
                    return NotFound();
                }
                // Async dequeue the message
                string retrievedMessage = await queue.GetMessageAsync();
                IActionResult result = Content(retrievedMessage);
                return result;
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}