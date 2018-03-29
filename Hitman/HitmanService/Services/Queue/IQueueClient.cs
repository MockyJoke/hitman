using System.Threading.Tasks;

namespace HitmanService.Services.Queue{
    public interface IQueueClient
    {
       Task<IQueue> GetQueueAsync(string queueName);
    }
}