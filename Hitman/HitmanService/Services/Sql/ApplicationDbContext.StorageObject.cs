using HitmanService.Services.Sql;
using Microsoft.EntityFrameworkCore;

namespace HitmanService.Data
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<StorageObject> StorageObjects { get; set; }
        private void CreateStorageObjectModels(ModelBuilder builder)
        {
            builder.Entity<StorageObject>().HasKey(so => new { so.Category, so.UniqueName });
        }
    }
}
