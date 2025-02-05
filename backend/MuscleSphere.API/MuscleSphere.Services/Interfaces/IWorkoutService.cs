using MuscleSphere.DTO.Workout;

namespace MuscleSphere.Services.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<WorkoutResponseDto>> GetUserWorkoutsAsync(string userId);
        Task<WorkoutResponseDto> AddWorkoutAsync(string userId, WorkoutDto workoutDto);
        Task<WorkoutResponseDto> UpdateWorkoutAsync(Guid workoutId, string userId, WorkoutDto workoutDto);
        Task DeleteWorkoutAsync(Guid workoutId, string userId);
    }
}
