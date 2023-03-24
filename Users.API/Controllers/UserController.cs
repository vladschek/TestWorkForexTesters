using Common.DTOs;
using Common.DTOs.Subscription;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using System.Text.Json;
using Users.API.Filters;
using Users.API.Validators;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, SubscriptionDTOValidator subscriptionDTOValidator)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("{userId}/exist")]
        public async Task<IActionResult> IsUserExist(int userId)
        {
            await _userService.IsUserExist(userId);
            return Ok();
        }

        [HttpGet("subscription/{subscriptionType}")]
        [ValidateSubscriptionType("subscriptionType")]
        public async Task<IActionResult> GetUsersBySubscription(string subscriptionType)
        {
            var response = await _userService.GetUsersBySubscription(subscriptionType);
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpPost]
        [ValidateUserSubscriptionAttribute("user")]
        public async Task<IActionResult> CreateUser(UserCreateDTO user)
        {
            var userId = await _userService.CraeteUser(user);
            return Ok(JsonSerializer.Serialize(userId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO user)
        {
            await _userService.UpdateUser(id, user);
            return NoContent();
        }

        [HttpPut("{userId}/subscription")]
        [ValidateSubscriptionAttribute("subscription")]
        public async Task<IActionResult> UpdateUserSubscription(int userId, [FromBody] SubscriptionDTO subscription)
        {
            await _userService.UpdateUserSubscription(userId, subscription);
            return Ok();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
