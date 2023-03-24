using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Core.Domain.Entities;
using Core.Interfaces.Repositories.Mongo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Projects.API.Services;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Infrustructure.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public ProjectsService(IProjectsRepository repository, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = clientFactory.CreateClient("UserService");
        }

        public async Task<IEnumerable<ProjectResponseDTO>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<ProjectResponseDTO>>(result);
        }

        public async Task<IEnumerable<ProjectResponseDTO>> GetForUserAsync(int userId)
        {
            var result = await _repository.GetForUser(userId);
            return _mapper.Map<List<ProjectResponseDTO>>(result);
        }

        public async Task CreateAsync(int id, string name, ProjectCreateDTO projectDto)
        {
            var response = await _httpClient.GetAsync($"/api/user/{id}/exist");
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"User with userId:{id} not found.");
            }
            var project = _mapper.Map<Project>(projectDto);

            await _repository.CreateAsync(project);
        }

        public async Task UpdateAsync(int userId, string projectName, ProjectCreateDTO projectDto)
        {
            var existingProject = await _repository.GetProject(userId, projectName);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with userId '{userId}' not found.");
            }
            var project = _mapper.Map<Project>(projectDto);
            await _repository.UpdateAsync(userId, projectName, project);
        }

        public async Task DeleteAsync(int userId)
        {
            var existingProject = await _repository.GetForUser(userId);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with userId '{userId}' not found.");
            }

            await _repository.DeleteAsync(userId);
        }

        public async Task<ProjectResponseDTO> GetProject(int userId, string projectName)
        {
            var result = await _repository.GetProject(userId, projectName);
            return _mapper.Map<ProjectResponseDTO>(result);
        }

        public async Task DeleteProject(int userId, string projectName)
        {
            await _repository.DeleteProject(userId, projectName);
        }

        public async Task<IEnumerable<IndicatorUsageDTO>> GetTopIndicatorBySubscription(string subscriptionType)
        {
            var response = await _httpClient.GetAsync($"/api/user/subscription/{subscriptionType}");
            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(await response.Content.ReadAsStringAsync());
                }
                throw new Exception(response.RequestMessage.ToString());
            }
            var jsonContent = await response.Content.ReadAsStringAsync();
            var ids = JsonSerializer.Deserialize<List<int>>(jsonContent);

            var indicators = await _repository.GetTopUsedIndicators(ids);
            return _mapper.Map<IEnumerable<IndicatorUsageDTO>>(indicators);
        }
    }
}
