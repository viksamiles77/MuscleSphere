using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuscleSphere.DTO.Workout;
using MuscleSphere.Services.Interfaces;
using System.Security.Claims;

namespace MuscleSphere.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var workouts = await _workoutService.GetUserWorkoutsAsync(userId);
            return Ok(workouts);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout([FromBody] WorkoutDto workoutDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var workout = await _workoutService.AddWorkoutAsync(userId, workoutDto);
            return Ok(workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(Guid id, [FromBody] WorkoutDto workoutDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var workout = await _workoutService.UpdateWorkoutAsync(id, userId, workoutDto);
            return Ok(workout);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            await _workoutService.DeleteWorkoutAsync(id, userId);
            return NoContent();
        }
    }
}
