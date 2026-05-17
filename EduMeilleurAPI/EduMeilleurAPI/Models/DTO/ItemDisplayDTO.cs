using EduMeilleurAPI.Models.Interfaces;

namespace EduMeilleurAPI.Models.DTO
{
    public class ItemResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Chapter { get; set; } = null!;
        public List<int> RelatedItemIds { get; set; } = new List<int>();
        public string Type { get; set; }
        public string RelatedType { get; set; }

        public ItemResponseDTO(IEducationalItem item, ItemTypes type, ItemTypes? relatedType = null, List<int>? relatedIds = null)
        {
            Id = item.Id;
            Title = item.Title;
            Content = item.Content;
            Chapter = item.Chapter?.Title ?? string.Empty;
            RelatedItemIds = relatedIds ?? [];
            Type = type.ToString();
            RelatedType = relatedType.ToString() ?? "";
        }
    }
}
