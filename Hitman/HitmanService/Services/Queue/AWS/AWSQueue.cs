using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace HitmanService.Services.Queue.Azure
{
    public class AWSQueue : IQueue
    {
        private string _queueUrl;
        private AmazonSQSClient _amazonSQSClient;

        private bool _isFIFO;

        public AWSQueue(AmazonSQSClient amazonSQSClient, string queueUrl, bool isFifo)
        {
            _amazonSQSClient = amazonSQSClient;
            _queueUrl = queueUrl;
            _isFIFO = isFifo;
        }

        public async Task<string> GetMessageAsync()
        {
            ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
            receiveMessageRequest.QueueUrl = _queueUrl;
            ReceiveMessageResponse receiveMessageResponse = await _amazonSQSClient.ReceiveMessageAsync(receiveMessageRequest);
            Message message = receiveMessageResponse.Messages[0];

            DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest()
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = message.ReceiptHandle
            };
            await _amazonSQSClient.DeleteMessageAsync(deleteMessageRequest);

            return message.Body;
        }

        public async Task SendMessageAsync(string message)
        {
            SendMessageRequest sendMessageRequest = new SendMessageRequest();
            sendMessageRequest.QueueUrl = _queueUrl;

            sendMessageRequest.MessageBody = message;
            if (_isFIFO)
            {
                sendMessageRequest.MessageGroupId = _queueUrl;
                sendMessageRequest.MessageDeduplicationId = DateTime.Now.Ticks.ToString();
            }
            await _amazonSQSClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
