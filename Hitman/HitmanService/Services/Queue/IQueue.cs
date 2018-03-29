using System.Threading.Tasks;

namespace HitmanService.Services.Queue{
    public interface IQueue
    {
       Task<string> GetMessageAsync();
       Task SendMessageAsync(string message);
    }
}