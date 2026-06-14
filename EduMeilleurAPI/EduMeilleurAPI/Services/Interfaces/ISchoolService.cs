using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<School?> GetSchool(int id);
    }
}
