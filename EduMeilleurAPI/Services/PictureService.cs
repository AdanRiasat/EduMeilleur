using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;

namespace EduMeilleurAPI.Services
{
    public class PictureService
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
    }
}
