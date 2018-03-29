using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using HitmanModel.Storage;

namespace HitmanService.Services.Queue.Azure
{
    public class AWSQueueClient : IQueueClient
    {
        private AmazonSQSClient _amazonSQSClient;

        public AWSQueueClient(AmazonSQSClient amazonSQSClient)
        {
            _amazonSQSClient = amazonSQSClient;
        }

        public async Task<IQueue> GetQueueAsync(string queueName)
        {
            try
            {
                // Only supports fifo queue
                queueName = queueName.EndsWith(".fifo") ? queueName : queueName + ".fifo";
                GetQueueUrlResponse getQueueUrlResponse = await _amazonSQSClient.GetQueueUrlAsync(queueName);
                return new AWSQueue(_amazonSQSClient, getQueueUrlResponse.QueueUrl, true);
            }
            catch
            {
                return null;
            }
        }
    }
}
