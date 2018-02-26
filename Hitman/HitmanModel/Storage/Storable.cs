using System.IO;

namespace HitmanModel.Storage
{
    /// <summary>
    /// Storable by the Hitmain Storage Service.
    /// </summary>
    public class Storable : IStorable
    {
        /// <summary>
        /// Get object as stream for storage
        /// </summary>
        public Stream DataStream { get; set; }

        /// <summary>
        /// Get the metadata for the stroage.
        /// </summary>
        public string Metadata { get; set; }
    }
}
