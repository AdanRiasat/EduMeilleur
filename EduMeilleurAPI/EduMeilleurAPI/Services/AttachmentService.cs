using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;

namespace EduMeilleurAPI.Services
{
    public class AttachmentService
    {
        private readonly EduMeilleurAPIContext _context;

        public AttachmentService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.Attachments != null;
        }

        public async Task<Attachment?> CreateAttachment(Attachment attachement)
        {
            if (!IsConstextValid()) return null;

            _context.Attachments.Add(attachement);
            await _context.SaveChangesAsync();

            return attachement;
        }
    }
}
