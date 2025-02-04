using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuscleSphere.DataAccess.Interfaces;
using MuscleSphere.DomainModels.Entities;
using System.Security.Claims;

namespace MuscleSphere.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutsController(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var workouts = await _workoutRepository.GetUserWorkoutsAsync(userId);
            return Ok(workouts);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout([FromBody] Workout workout)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            workout.UserId = userId;
            await _workoutRepository.AddWorkoutAsync(workout);
            return Ok(workout);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            workout.UserId = userId;
            await _workoutRepository.UpdateWorkoutAsync(workout);
            return Ok(workout);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWorkout([FromBody] Workout workout)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            workout.UserId = userId;
            await _workoutRepository.DeleteWorkoutAsync(workout);
            return Ok();
        }
    }
}
