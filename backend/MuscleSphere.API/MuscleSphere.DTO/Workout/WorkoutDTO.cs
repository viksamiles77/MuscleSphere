using System.ComponentModel.DataAnnotations;

namespace MuscleSphere.DTO.Workout
{
    public class WorkoutDto
    {
        [Required(ErrorMessage = "Workout type is required.")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Day is required.")]
        [Range(0, 6, ErrorMessage = "Day must be between 0 (Sunday) and 6 (Saturday).")]
        public DayOfWeek Day { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        public List<ExerciseDto> Exercises { get; set; } = new();
    }

    public class ExerciseDto
    {
        [Required(ErrorMessage = "Exercise name is required.")]
        public string Name { get; set; } = string.Empty;
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
    }
}
