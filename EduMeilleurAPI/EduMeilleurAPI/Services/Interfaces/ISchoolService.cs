using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<bool?> IsSchoolIdValid(int? id);

        Task<School?> GetSchool(int id);
    }
}
