using Microsoft.AspNetCore.Http;
using UAE.Application.Models;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface IFileService
{
    Task<OperationResult<List<Photo>>> SavePictures(List<IFormFile> pictures);
}