using MuscleSphere.DataAccess.Interfaces;
using MuscleSphere.DTO.Workout;
using MuscleSphere.Services.Helpers;
using MuscleSphere.Services.Interfaces;

namespace MuscleSphere.Services.Implementation
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<List<WorkoutResponseDto>> GetUserWorkoutsAsync(string userId)
        {
            var workouts = await _workoutRepository.GetUserWorkoutsAsync(userId);
            return workouts.Select(WorkoutMapper.ToWorkoutResponseDto).ToList();
        }

        public async Task<WorkoutResponseDto> AddWorkoutAsync(string userId, WorkoutDto workoutDto)
        {
            var workout = WorkoutMapper.ToWorkout(workoutDto, userId);
            await _workoutRepository.AddWorkoutAsync(workout);
            return WorkoutMapper.ToWorkoutResponseDto(workout);
        }

        public async Task<WorkoutResponseDto> UpdateWorkoutAsync(Guid workoutId, string userId, WorkoutDto workoutDto)
        {
            var workout = await _workoutRepository.GetWorkoutByIdAsync(workoutId);
            if (workout == null || workout.UserId != userId)
                throw new Exception("Workout not found");

            WorkoutMapper.UpdateWorkout(workout, workoutDto);
            await _workoutRepository.UpdateWorkoutAsync(workout);
            return WorkoutMapper.ToWorkoutResponseDto(workout);
        }

        public async Task DeleteWorkoutAsync(Guid workoutId, string userId)
        {
            var workout = await _workoutRepository.GetWorkoutByIdAsync(workoutId);
            if (workout == null || workout.UserId != userId)
                throw new Exception("Workout not found");

            await _workoutRepository.DeleteWorkoutAsync(workout);
        }
    }
}
