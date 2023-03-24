using Common.DTOs;
using Core.Domain.Entities;

namespace Projects.API.Services
{
    public interface IProjectsService
    {
        Task<IEnumerable<ProjectResponseDTO>> GetAllAsync();
        Task<IEnumerable<ProjectResponseDTO>> GetForUserAsync(int userId);
        Task<ProjectResponseDTO> GetProject(int userId, string projectName);
        Task CreateAsync(int id, string name, ProjectCreateDTO Project);
        Task UpdateAsync(int userId, string projectName, ProjectCreateDTO Project);
        Task DeleteAsync(int userId);
        Task DeleteProject(int userId, string projectName);
        Task<IEnumerable<IndicatorUsageDTO>> GetTopIndicatorBySubscription(string subscriptionType);
    }
}
