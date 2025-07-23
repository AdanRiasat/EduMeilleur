using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services
{
    public class PictureService : IPictureService
    {
        public readonly EduMeilleurAPIContext _context;

        public PictureService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Picture != null;
        }

        public async Task<Picture?> CreatePicture(Picture picture)
        {
            if (!IsConstextValid()) return null;

            _context.Picture.Add(picture);
            await _context.SaveChangesAsync();

            return picture;
        }

        public async Task<Picture?> Delete(Picture picture)
        {
            if (!IsConstextValid()) return null;

            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();

            return picture;
        }

        public async Task<Picture?> GetAsync(User user)
        {
            if (!IsConstextValid()) return null;

            Picture? picture = await _context.Picture.Where(p => p.FileName == user.FileName).FirstOrDefaultAsync();
            if (picture == null) return null;

            return picture;
        }
    }
}
