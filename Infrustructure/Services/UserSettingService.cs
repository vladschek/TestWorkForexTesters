using AutoMapper;
using Common.DTOs;
using Common.Exceptions;
using Core.Domain.Entities;
using Core.Interfaces.Repositories.Mongo;
using Core.Interfaces.Services;
using System.Net.Http;


namespace Infrustructure.Services
{
    public class UserSettingService : IUserSettingsService
    {
        private readonly IMapper _mapper;
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly HttpClient _httpClient;

        public UserSettingService(IMapper mapper, IUserSettingRepository user, IHttpClientFactory httpClientFactory) 
        {
            _mapper = mapper;
            _userSettingRepository = user;
            _httpClient = httpClientFactory.CreateClient("UserService");
        }
        public async Task CreateAsync(int userId, UserSettingsDTO entity)
        {
            var us = _mapper.Map<UserSettings>(entity);
            await _userSettingRepository.CreateAsync(userId, us);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await _userSettingRepository.DeleteAsync(userId);
        }

        public async Task<UserSettingsDTO> GetForUser(int userId)
        {
            var us = await _userSettingRepository.GetForUser(userId);
            if(us is null)
            {
                throw new NotFoundException($"User setting with userId:{userId} not found.");
            }
            return _mapper.Map<UserSettingsDTO>(us);
        }

        public async Task<bool> UpdateAsync(int id, UserSettingsDTO entity)
        {
            var existingSettings = await _userSettingRepository.GetForUser(id);
            if (existingSettings == null)
            {
                throw new KeyNotFoundException($"UserSettings with userId '{id}' not found.");
            }
            var project = _mapper.Map<UserSettings>(entity);
            return await _userSettingRepository.UpdateAsync(id, project);
        }
    }
}
