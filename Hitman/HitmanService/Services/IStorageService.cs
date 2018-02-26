﻿using HitmanModel.Storage;
using System.IO;
using System.Threading.Tasks;

namespace HitmanService.Services
{
    public interface IStorageService
    {
        Task SaveAsync(Stream dataStream, string metadata, StorableIdentifier identifier);

        Task<IStorageObject> LoadAsync(StorableIdentifier identifier);
    }
}
