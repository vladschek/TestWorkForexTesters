using Core.Domain.Entities;

namespace Core.Interfaces.Repositories.Mongo
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<IEnumerable<Project>> GetForUser(int userId);
        Task<Project> GetProject(int userId, string projectName);
        Task CreateAsync(Project entity);
        Task<bool> UpdateAsync(int id, string projectName, Project entity);
        Task<bool> DeleteAsync(int userId);
        Task<bool> DeleteProject(int userId, string projectName);
        Task<List<IndicatorUsage>> GetTopUsedIndicators(List<int> userIds);
    }
}
