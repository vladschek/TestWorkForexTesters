using Microsoft.AspNetCore.Mvc;
using Projects.API.Services;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class IndicatorController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public IndicatorController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet("popularIndicators/{subscriptionType}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetPopularIndicator(string subscriptionType)
        {
            var popIndicators = await _projectsService.GetTopIndicatorBySubscription(subscriptionType);
            return Ok(popIndicators);
        }
    }
}
