using UAE.Core.Entities;

namespace UAE.Infrastructure.Repositories.Base.Interfaces;

public interface ICommandBuilder
{
    string BuildUpdateAnnouncementStatement(Announcement announcement);
}