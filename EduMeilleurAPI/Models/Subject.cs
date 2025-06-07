namespace EduMeilleurAPI.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual List<Chapter> Chapters { get; set; } = new List<Chapter>();
        public virtual List<Notes> Notes { get; set; } = new List<Notes>();
    }
}
