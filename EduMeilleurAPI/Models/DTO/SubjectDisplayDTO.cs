namespace EduMeilleurAPI.Models.DTO
{
    public class SubjectDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public List<string> Chapters { get; set; } = new List<string>();

        public SubjectDisplayDTO(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Description = subject.Description;
            Type = subject.Type;
            
            foreach (var item in subject.Chapters)
            {
                Chapters.Add(item.Title);
            }
        }
    }
}
