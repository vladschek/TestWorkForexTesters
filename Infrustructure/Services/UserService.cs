using Amazon.Util;
using AutoMapper;
using Common.DTOs;
using Common.DTOs.Subscription;
using Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Interfaces.Repositories.Postgre;
using Core.Interfaces.Services;
using System.Net.Http;
using System.Text.Json;

namespace Infrustructure.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("ProjectService");
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _httpClient.DeleteAsync($"/api/projects/{id}");
            await _httpClient.DeleteAsync($"/api/usersettigs/{id}");
        }

        public async Task<IEnumerable<ReadUserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }

        public async Task<ReadUserDTO> GetUserById(int id)
        {
            var user = await TryGetUserById(id, false);
            var userDto = _mapper.Map<ReadUserDTO>(user);

            userDto.Projects = await GetUserProjects(user.Id);
            userDto.UserSettings = await GetUserSettings(user.Id);
            return userDto;
        }

        private async Task<UserSettingsDTO> GetUserSettings(int id)
        {
            var response = await _httpClient.GetAsync($"/api/usersettings/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserSettingsDTO>(jsonContent);
            }
            return new UserSettingsDTO { UserId = id, Language = Language.English.ToString(), Theme = Theme.Light.ToString()};
        }

        private async Task<List<ProjectResponseDTO>> GetUserProjects(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/projects/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ProjectResponseDTO>>(jsonContent);
            }
            return new List<ProjectResponseDTO>();
        }
        public async Task UpdateUser(int id, UpdateUserDTO updateDto)
        {
            var user = await TryGetUserById(id, true);
            _mapper.Map(updateDto, user);

            await _userRepository.SaveChangeAsync();
        }

        private async Task<User> TryGetUserById(int id, bool trackChanges)
        {
            var user = await _userRepository.GetById(id, trackChanges);
            if (user == null)
            {
                throw new NotFoundException($"User with id:{id} doesn't exist.");
            }
            return user;
        }

        public async Task<int> CraeteUser(UserCreateDTO createUserDto)
        {
            if (createUserDto.Subscription == null)
            {
                createUserDto.Subscription = new SubscriptionDTO
                {
                    Type = SubscriptionType.Free.ToString(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(100),
                };

            }
            var user = _mapper.Map<User>(createUserDto);
            await _userRepository.Create(user);

            return user.Id;
        }
        public async Task UpdateUserSubscription(int id, SubscriptionDTO updateSubscriptionDto)
        {
            var user = await TryGetUserById(id, true);

            user.Subscription = _mapper.Map<Subscription>(updateSubscriptionDto);

            await _userRepository.SaveChangeAsync();
        }

        public async Task IsUserExist(int userId)
        {
            await TryGetUserById(userId, false);
        }

        public async Task<IEnumerable<int>> GetUsersBySubscription(string subscriptionType)
        {
            bool success = Enum.TryParse<SubscriptionType>(subscriptionType, out SubscriptionType type);
            if (!success)
            {
                throw new NotFoundException($"SubcriptionType: {subscriptionType} doesn't exist.");
            }

            var result = await _userRepository.GetUsersWithSubscription(type);
            return result;
        }
    }
}
