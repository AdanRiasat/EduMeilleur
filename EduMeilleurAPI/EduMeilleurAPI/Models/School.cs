using System.Text.Json.Serialization;

namespace EduMeilleurAPI.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual List<User>? Users { get; set; }
    }
}
