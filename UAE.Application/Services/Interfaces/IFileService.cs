using Microsoft.AspNetCore.Http;
using UAE.Application.Models;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface IFileService
{
    Task<OperationResult<Picture[]>> SavePictures(List<IFormFile> pictures);
}