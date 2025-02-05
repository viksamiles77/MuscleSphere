namespace MuscleSphere.DomainModels.Entities
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public DateTime Date { get; set; }
        public List<Exercise> Exercises { get; set; } = new();
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
