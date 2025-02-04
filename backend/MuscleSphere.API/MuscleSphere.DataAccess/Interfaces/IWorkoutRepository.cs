using MuscleSphere.DomainModels.Entities;

namespace MuscleSphere.DataAccess.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<List<Workout>> GetUserWorkoutsAsync(string userId);
        Task<List<Workout>> GetWoroutsByDayAsync(string userId, DayOfWeek day);
        Task AddWorkoutAsync(Workout workout);
        Task UpdateWorkoutAsync(Workout workout);
        Task DeleteWorkoutAsync(Workout workout);
    }
}
