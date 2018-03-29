
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HitmanService.Services.Queue.Azure
{
    public class AzureQueue : IQueue
    {
        private CloudQueue _cloudQueue;

        public AzureQueue(CloudQueue cloudQueue)
        {
            _cloudQueue = cloudQueue;
        }

        public async Task<string> GetMessageAsync()
        {
            CloudQueueMessage retrievedMessage = await _cloudQueue.GetMessageAsync();
            string message = retrievedMessage.AsString;
            await _cloudQueue.DeleteMessageAsync(retrievedMessage);
            return message;
        }
        
        public async Task SendMessageAsync(string message)
        {
             await _cloudQueue.AddMessageAsync(new CloudQueueMessage(message));
        }
    }
}
