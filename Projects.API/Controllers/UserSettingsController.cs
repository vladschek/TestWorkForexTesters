using Common.DTOs;
using Core.Interfaces.Services;
using Infrustructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _userSettingService;

        public UserSettingsController(IUserSettingsService userSettingsService) 
        {
            _userSettingService = userSettingsService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var us = await _userSettingService.GetForUser(userId);
            return Ok(us);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateSettings(int userId, [FromBody] UserSettingsDTO usdto)
        {
            await _userSettingService.CreateAsync(userId, usdto);
            return Ok();
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateSettings(int userId, [FromBody] UserSettingsDTO usdto)
        {
            await _userSettingService.UpdateAsync(userId,usdto);
            return Ok();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteSettings(int userId)
        {
            await _userSettingService.DeleteAsync(userId);
            return Ok();
        }
    }
}
