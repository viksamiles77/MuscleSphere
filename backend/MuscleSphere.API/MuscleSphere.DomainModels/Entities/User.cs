using Microsoft.AspNetCore.Identity;

namespace MuscleSphere.DomainModels.Entities
{
    public class User : IdentityUser
    {
        public List<Workout> Workouts { get; set; } = new();
    }
}
