using System.Threading.Tasks;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories.Base;

namespace UAE.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var result = await DB.Find<User>()
            .Match(s => s.Email == email)
            .ExecuteSingleAsync();

        return result;
    }
}