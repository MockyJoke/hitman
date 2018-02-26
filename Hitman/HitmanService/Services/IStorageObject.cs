using HitmanModel.Storage;
using System.IO;

namespace HitmanService.Services
{
    public interface IStorageObject
    {
        Stream DataStream { get; }

        string Metadata { get; }

        StorableIdentifier Identifier { get; }
    }
}
