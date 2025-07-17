using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class VideoService
    {
        public readonly EduMeilleurAPIContext _context;

        public VideoService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Video != null;
        }

        public async Task<List<Video>?> GetAllAsync(int subjectid)
        {
            if (!IsConstextValid()) return null;

            return await _context.Video.Where(v => v.Chapter.SubjectId == subjectid).ToListAsync();
        }

        public async Task<Video?> GetAsync(int id)
        {
            if (!IsConstextValid()) return null;

            return await _context.Video.FindAsync(id);
        }
    }
}
