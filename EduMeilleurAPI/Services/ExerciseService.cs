using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class ExerciseService
    {
        public readonly EduMeilleurAPIContext _context;

        public ExerciseService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Exercise != null;
        }

        public async Task<List<Exercise>?> GetAllAsync(int subjectid)
        {
            if (!IsConstextValid()) return null;

            return await _context.Exercise.Where(e => e.Notes.Subject.Id == subjectid).ToListAsync();
        }

        public async Task<Exercise?> GetAsync(int id)
        {
            if (!IsConstextValid()) return null;

            return await _context.Exercise.FindAsync(id);
        }
    }
}
