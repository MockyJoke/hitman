using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            try
            {
            IQueue queue = await _queueClient.GetQueueAsync(queueName);
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

        [HttpPost("{queueName?}")]
        public async Task<IActionResult> Post(string queueName)
        {
            try
            {
                IQueue queue = await _queueClient.GetQueueAsync(queueName);
                if (queue == null)
                {
                    return NotFound();
                }
                string message = await GetRawBodyStringAsync(Request);
                await queue.SendMessageAsync(message);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <returns></returns>
        public async Task<string> GetRawBodyStringAsync(HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            using (StreamReader reader = new StreamReader(request.Body, encoding))
                return await reader.ReadToEndAsync();
        }
    }
}