namespace MuscleSphere.DTO.Workout
{
    public class WorkoutResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public DateTime Date { get; set; }
        public List<ExerciseResponseDto> Exercises { get; set; } = new();
    }

    public class ExerciseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
