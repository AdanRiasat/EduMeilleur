namespace EduMeilleurAPI.Models.DTO
{
    public class RelatedItemDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";

        public RelatedItemDTO(int id, string code = "")
        {
            Id = id;
            Code = code;
        }
    }
}
