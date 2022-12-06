using System.Text;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories.Base.Implementation;
using UAE.Infrastructure.Repositories.Base.Interfaces;

namespace UAE.Infrastructure.Repositories;

public class AnnouncementRepository :  RepositoryBase<Announcement>, IAnnouncementRepository
{
    private readonly ICommandBuilder _commandBuilder;

    public AnnouncementRepository(ICommandBuilder commandBuilder)
    {
        _commandBuilder = commandBuilder;
    }

    public async Task UpdateFieldsAsync(Announcement announcement)
    {
        var updateCommand = DB.Update<Announcement>()
            .MatchID(announcement.ID);

        var updateStatement = _commandBuilder.BuildUpdateAnnouncementStatement(announcement);
        updateCommand.WithPipelineStage(updateStatement);
        
        await updateCommand.ExecutePipelineAsync();
        }

}