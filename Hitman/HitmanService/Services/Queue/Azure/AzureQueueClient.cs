using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Threading.Tasks;
using HitmanModel.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HitmanService.Services.Queue.Azure
{
    public class AzureQueueClient : IQueueClient
    {
        private CloudQueueClient _cloudQueueClient;

        public AzureQueueClient(CloudQueueClient cloudQueueClient)
        {
            _cloudQueueClient = cloudQueueClient;
        }
        
        public async Task<IQueue> GetQueueAsync(string queueName)
        {
            CloudQueue cloudQueue = _cloudQueueClient.GetQueueReference(queueName);
            if (!await cloudQueue.ExistsAsync())
            {
                return null;
            }
            return new AzureQueue(cloudQueue);
        }
    }
}
