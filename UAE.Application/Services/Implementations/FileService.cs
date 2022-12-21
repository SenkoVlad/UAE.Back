using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using UAE.Application.Models;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Shared.Settings;

namespace UAE.Application.Services.Implementations;

sealed class FileService : IFileService
{
    private readonly FileStorageSettings _settings;

    public FileService(IOptions<Settings> settings)
    {
        _settings = settings.Value.FileStorage;
    }
    
    public async Task<OperationResult<List<Photo>>> SavePictures(List<IFormFile> pictures)
    {
        var savedPictures = new List<Photo>();
        
        foreach (var picture in pictures)
        {
            if (string.IsNullOrWhiteSpace(picture.FileName))
            {
                return new OperationResult<List<Photo>>(IsSucceed: false, ResultMessages: new[] {"File not selected"});
            }

            var directoryPath = CreatePictureDirectoryIfNotExist();
            var path = Path.Combine(directoryPath, picture.FileName);
            await using var stream = new FileStream(path, FileMode.Create);
            await picture.CopyToAsync(stream);
            stream.Close();
            savedPictures.Add(new Photo
            {
                ID = ObjectId.GenerateNewId().ToString(),
                Name = picture.FileName,
                Path = path
            });
        }

        return new OperationResult<List<Photo>>(IsSucceed: true, ResultMessages: new[] {"Pictures are saved"}, Result: savedPictures);
    }

    private string CreatePictureDirectoryIfNotExist()
    {
        var guid = Guid.NewGuid().ToString("N");
        var directoryPath = Path.Combine(_settings.PathToAnnouncementPictures,
            guid[0].ToString(),
            guid[2].ToString(),
            guid[3].ToString(),
            guid[4].ToString());
        
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return directoryPath;
    }
}