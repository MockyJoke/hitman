using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using HitmanModel.Storage;

namespace HitmanService.Services.Storage.Sql
{
    public class StorageObject : IStorageObject
    {
        public string Category { get; set; }

        public string UniqueName { get; set; }

        public byte[] Content { get; set; }

        public string Metadata { get; set; }

        [NotMapped]
        public Stream DataStream
        {
            get
            {
                MemoryStream memoryStream = new MemoryStream(Content);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
        }
        [NotMapped]
        public StorableIdentifier Identifier
        {
            get
            {
                return new StorableIdentifier() { Category = Category, UniqueName = UniqueName };
            }
        }
    }
}
