using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.DTO;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly EduMeilleurAPIContext _context;

        public SubjectService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Subject != null;
        }

        public async Task<List<SubjectDisplayDTO>?> GetAllAsync()
        {
            if (!IsConstextValid()) return null;

            List<SubjectDisplayDTO> result = new List<SubjectDisplayDTO>();

            foreach (var subject in await _context.Subject.ToListAsync())
            {
                result.Add(new SubjectDisplayDTO(subject));
            }

            return result;
        }

        public async Task<Subject?> GetAsync(int id)
        {
            if (!IsConstextValid()) return null;

            return await _context.Subject.FindAsync(id);
        }
    }
}
