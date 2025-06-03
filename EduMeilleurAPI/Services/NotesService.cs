using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class NotesService
    {
        public readonly EduMeilleurAPIContext _context;

        public NotesService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Subject != null;
        }

        public async Task<List<Notes>?> GetAllAsync(int subjectId)
        {
            if (!IsConstextValid()) return null;

            return await _context.Notes.Where(n => n.Subject.Id == subjectId).ToListAsync();
        }

        public async Task<Notes?> GetAsync(int id)
        {
            if (!IsConstextValid()) return null;

            return await _context.Notes.FindAsync(id);
        }
    }
}
