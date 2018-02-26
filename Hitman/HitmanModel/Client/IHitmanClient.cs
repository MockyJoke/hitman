using HitmanModel.Storage;
using System.Threading.Tasks;

namespace HitmanModel.Client
{
    public interface IHitmanClient
    {
        /// <summary>
        /// Saves a strong-typed object to the server.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task SaveAsync<T>(T data, StorableIdentifier identifier) where T : class;

        /// <summary>
        /// Loads a strong-typed object from the server.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<T> LoadAsync<T>(StorableIdentifier identifier) where T : class;

        /// <summary>
        /// Saves a storable to the server.
        /// </summary>
        /// <param name="storable"></param>
        /// <returns></returns>
        Task SaveStorableAsync(IStorable storable, StorableIdentifier identifier);

        /// <summary>
        /// Loads a storable from the server.
        /// </summary>
        /// <returns></returns>
        Task<IStorable> LoadStorableAsync(StorableIdentifier identifier);
    }
}
