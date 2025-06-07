namespace EduMeilleurAPI.Models.DTO
{
    public class ItemDisplayDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Chapter { get; set; } = null!;

        public ItemDisplayDTO(int id, string title, string content, string chapter)
        {
            Id = id;
            Title = title;
            Content = content;
            Chapter = chapter;
        }
    }
}
