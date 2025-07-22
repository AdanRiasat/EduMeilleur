using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Models;

namespace EduMeilleurAPI.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectDisplayDTO>?> GetAllAsync();

        Task<Subject?> GetAsync(int id);
    }
}
