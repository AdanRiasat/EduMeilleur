using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class SchoolService: ISchoolService
    {
        private readonly EduMeilleurAPIContext _context;

        public SchoolService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Chat != null && _context.ChatMessages != null;
        }

        public async Task<School?> GetSchool(int id)
        {
            if (!IsConstextValid()) return null;

            return await _context.Schools.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
