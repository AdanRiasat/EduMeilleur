namespace EduMeilleurAPI.Models.Interfaces
{
    public interface IEducationalItem
    {
        int Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        Chapter Chapter { get; set; }
    }
}
