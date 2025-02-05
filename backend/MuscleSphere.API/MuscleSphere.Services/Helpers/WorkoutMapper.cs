using MuscleSphere.DomainModels.Entities;
using MuscleSphere.DTO.Workout;

namespace MuscleSphere.Services.Helpers
{
    public static class WorkoutMapper
    {
        public static Workout ToWorkout(WorkoutDto dto, string userId)
        {
            return new Workout
            {
                Type = dto.Type,
                Day = dto.Day,
                Date = dto.Date,
                UserId = userId,
                Exercises = dto.Exercises.Select(ToExercise).ToList()
            };
        }

        public static WorkoutResponseDto ToWorkoutResponseDto(Workout workout)
        {
            return new WorkoutResponseDto
            {
                Id = workout.Id,
                Type = workout.Type,
                Day = workout.Day,
                Date = workout.Date,
                Exercises = workout.Exercises.Select(ToExerciseResponseDto).ToList()
            };
        }

        private static Exercise ToExercise(ExerciseDto dto)
        {
            return new Exercise
            {
                Name = dto.Name,
                Sets = dto.Sets,
                Reps = dto.Reps,
                Weight = dto.Weight
            };
        }

        private static ExerciseResponseDto ToExerciseResponseDto(Exercise exercise)
        {
            return new ExerciseResponseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                Weight = exercise.Weight
            };
        }

        public static void UpdateWorkout(Workout workout, WorkoutDto dto)
        {
            workout.Type = dto.Type;
            workout.Day = dto.Day;
            workout.Date = dto.Date;
            workout.Exercises = dto.Exercises.Select(ToExercise).ToList();
        }
    }
}
