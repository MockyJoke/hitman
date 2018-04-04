using HitmanModel.Storage;
using HitmanService.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HitmanService.Services.Storage.Sql
{
    public class SqlDbStorageService : IStorageService
    {
        private ApplicationDbContext _context;

        public SqlDbStorageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Stream dataStream, string metadata, StorableIdentifier identifier)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                await dataStream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                StorageObject binObj = new StorageObject()
                {
                    Category = identifier.Category,
                    UniqueName = identifier.UniqueName,
                    Content = memStream.ToArray(),
                    Metadata = metadata
                };
                StorageObject oldStorageObj = _context.StorageObjects.Where(so => so.Category == identifier.Category && so.UniqueName == identifier.UniqueName).FirstOrDefault();
                if (oldStorageObj != null)
                {
                    _context.StorageObjects.Remove(oldStorageObj);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                await _context.StorageObjects.AddAsync(binObj).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public Task<IStorageObject> LoadAsync(StorableIdentifier identifier)
        {
            IStorageObject storageObject = _context.StorageObjects.Where(so => so.Category == identifier.Category && so.UniqueName == identifier.UniqueName).FirstOrDefault();
            if (storageObject == null)
            {
                return Task.FromResult<IStorageObject>(null);
            }

            return Task.FromResult(storageObject);
        }

        public async Task DeleteAsync(StorableIdentifier identifier)
        {
            StorageObject storageObject = _context.StorageObjects.Where(so => so.Category == identifier.Category && so.UniqueName == identifier.UniqueName).FirstOrDefault();
            if (storageObject != null)
            {
                _context.StorageObjects.Remove(storageObject);
            }
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
