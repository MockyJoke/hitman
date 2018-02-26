using System.IO;

namespace HitmanModel.Storage
{
    /// <summary>
    /// Storable by the Hitmain Storage Service.
    /// </summary>
    public interface IStorable
    {
        /// <summary>
        /// Get object as stream for storage
        /// </summary>
        Stream DataStream { get; }

        /// <summary>
        /// Get the metadata for the stroage.
        /// </summary>
        string Metadata { get; }
    }
}
