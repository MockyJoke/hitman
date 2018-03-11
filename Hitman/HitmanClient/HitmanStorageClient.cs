using HitmanModel.Client;
using HitmanModel.Storage;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace HitmanClient
{
    public class HitmanStorageClient : IHitmanClient
    {
        public HitmanStorageConfig Config { get; private set; }

        public HitmanStorageClient(HitmanStorageConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// Saves a strong-typed object to the server.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task SaveAsync<T>(T data, StorableIdentifier identifier)
            where T : class
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream memStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memStream, data);
                memStream.Seek(0, SeekOrigin.Begin);
                await callSaveAsync(memStream, typeof(T).FullName, identifier).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Loads a strong-typed object to the server.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<T> LoadAsync<T>(StorableIdentifier identifier)
            where T : class
        {
            WebResponse response = await callLoadAsync(identifier).ConfigureAwait(false);
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (Stream stream = response.GetResponseStream())
            {
                T result = jsonSerializer.ReadObject(stream) as T;
                return result;
            }
        }

        /// <summary>
        /// Saves a storable to the server.
        /// </summary>
        /// <param name="storable"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task SaveStorableAsync(IStorable storable, StorableIdentifier identifier)
        {
            await callSaveAsync(storable.DataStream, storable.Metadata, identifier).ConfigureAwait(false);
        }

        /// <summary>
        /// Loads a storable from the server.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<IStorable> LoadStorableAsync(StorableIdentifier identifier)
        {
            WebResponse response = await callLoadAsync(identifier).ConfigureAwait(false);
            Storable storable = new Storable
            {
                 DataStream = response.GetResponseStream(),
                 Metadata = response.Headers["Metadata"]
            };
            return storable;
        }

        private async Task<WebResponse> callSaveAsync(Stream dataStream, string metadata, StorableIdentifier identifier)
        {
            var request = WebRequest.Create($"{GetProtocol()}://{Config.Host}/api/store/{identifier.Category}/{identifier.UniqueName}/{Config.Key}");
            request.Headers.Add("Metadata", metadata);
            request.Method = "POST";
            using (Stream requestStream = request.GetRequestStream())
            {
                await dataStream.CopyToAsync(requestStream).ConfigureAwait(false);
            }
            return await request.GetResponseAsync().ConfigureAwait(false);
        }

        private async Task<WebResponse> callLoadAsync(StorableIdentifier identifier)
        {
            var request = WebRequest.Create($"{GetProtocol()}://{Config.Host}/api/store/{identifier.Category}/{identifier.UniqueName}/{Config.Key}");
            request.Method = "GET";
            return await request.GetResponseAsync().ConfigureAwait(false);
        }

        private string GetProtocol()
        {
            return Config.IsUnsecure ? "http" : "https";
        }
    }
}
