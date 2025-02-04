using Microsoft.EntityFrameworkCore;
using MuscleSphere.DataAccess.Interfaces;
using MuscleSphere.DomainModels.Entities;

namespace MuscleSphere.DataAccess.Implementation
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Workout>> GetUserWorkoutsAsync(string userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Workout>> GetWoroutsByDayAsync(string userId, DayOfWeek day)
        {
            return await _context.Workouts
                 .Where(w => w.UserId == userId && w.Day == day)
                 .ToListAsync();
        }

        public async Task AddWorkoutAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
            await _context.SaveChangesAsync();
        }

        public Task UpdateWorkoutAsync(Workout workout)
        {
            _context.Workouts.Update(workout);
            return _context.SaveChangesAsync();
        }

        public Task DeleteWorkoutAsync(Workout workout)
        {
            _context.Workouts.Remove(workout);
            return _context.SaveChangesAsync();
        }
    }
}
