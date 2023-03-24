using Amazon.Runtime.SharedInterfaces;
using Common.DTOs;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Projects.API.Filters;
using Projects.API.Services;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userCharts = await _projectsService.GetAllAsync();
            return Ok(userCharts);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var userCharts = await _projectsService.GetForUserAsync(userId);
            if (userCharts == null)
            {
                return NotFound();
            }
            return Ok(userCharts);
        }
        [HttpPost("{userId}/{projectName}")]
        [ValidateProjectDTO("project")]
        public async Task<IActionResult> CreateProject(int userId, string projectName, ProjectCreateDTO project)
        {
            await _projectsService.CreateAsync(userId, projectName, project);
            return Ok();
        }
        [HttpPut("{userId}/{projectName}")]
        [ValidateProjectDTO("project")]
        public async Task<IActionResult> Update(int userId, string projectName, [FromBody] ProjectCreateDTO project)
        {
            await _projectsService.UpdateAsync(userId, projectName, project);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _projectsService.DeleteAsync(userId);
            return NoContent();
        }
        [HttpDelete("{userId}/{projectName}")]
        public async Task<IActionResult> Delete(int userId, string projectName)
        {
            await _projectsService.DeleteProject(userId, projectName);
            return NoContent();
        }
    }
}
