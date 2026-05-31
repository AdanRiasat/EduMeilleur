using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace EduMeilleurAPI.Services
{
    public class QuestionService
    {
        private const long MAX_SINGLE_FILE_SIZE = 4194304;
        private const long MAX_TOTAL_SIZE = 15728640;
        
        private readonly EduMeilleurAPIContext _context;
        private readonly IPictureService _pictureService;
        private readonly AttachmentService _attachmentService;

        public QuestionService(EduMeilleurAPIContext context, IPictureService pictureService, AttachmentService attachmentService)
        {
            _context = context;
            _pictureService = pictureService;
            _attachmentService = attachmentService;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.QuestionTeacher != null && _context.Feedbacks != null;
        }

        public async Task<string?> CreateQuestionTeacher(QuestionTeacher question, IFormCollection formCollection)
        {
            var (valid, error) = ValidateFileSizes(formCollection);
            if (!valid) return error;

            _context.QuestionTeacher.Add(question);
            await SaveFilesAndAttachments(formCollection, question.Pictures, question.Attachments, question);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> CreateFeedback(Feedback feedback, IFormCollection formCollection)
        {
            var (valid, error) = ValidateFileSizes(formCollection);
            if (!valid) return error;
            
            _context.Feedbacks.Add(feedback);
            await SaveFilesAndAttachments(formCollection, feedback.Pictures, feedback.Attachments, feedback);
            await _context.SaveChangesAsync();

            return null;
        }
        
        private (bool valid, string? error) ValidateFileSizes(IFormCollection collection)
        {
            long totalSize = 0;

            foreach (var file in collection.Files)
            {
                if (file.Length >= MAX_SINGLE_FILE_SIZE)
                    return (false, $"{file.FileName} exceeds the 4MB limit.");

                totalSize += file.Length;
            }

            if (totalSize > MAX_TOTAL_SIZE)
                return (false, "Total upload size exceeds the 15MB limit.");

            return (true, null);
        }

        public async Task SaveFilesAndAttachments(IFormCollection collection, List<Picture> pictures, List<Attachment> attachments, object targetEntity)
        {
            for (int i = 1; i <= collection.Files.Count; i++)
            {
                IFormFile? file = collection.Files.GetFile("file" + i);
                if (file == null) continue;
                
                var extension = Path.GetExtension(file.FileName).ToLower();
                var mimeType = file.ContentType;
                var safeFileName = Guid.NewGuid().ToString() + extension;
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", safeFileName);

                if (mimeType.StartsWith("image/"))
                {
                    try
                    { 
                        await SaveImage(file, safeFileName, targetEntity, pictures);
                    }
                    catch
                    {
                        //log errors
                    }
                }
                else
                {
                    if (!Directory.Exists(uploadsPath))
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    try
                    {
                        await SaveAttachment(uploadsPath, safeFileName, file, mimeType, targetEntity, attachments);
                    }
                    catch
                    {
                        //log errors
                    }
                }
                
            }
        }

        private async Task SaveImage(IFormFile file, string safeFileName, object targetEntity, List<Picture> pictures)
        {
            Image image = Image.Load(file.OpenReadStream());
            Picture picture = new Picture
            {
                Id = 0,
                FileName = safeFileName,
                MimeType = file.ContentType,
                            
            };

            if (targetEntity is QuestionTeacher question)
            {
                picture.QuestionTeacher = question;
            }

            if (targetEntity is Feedback feedback)
            {
                picture.Feedback = feedback;
            }

            pictures.Add(picture);
            image.Save(Directory.GetCurrentDirectory() + "/images/full/" + picture.FileName);
            await _pictureService.CreatePicture(picture);
        }

        private async Task SaveAttachment(string uploadsPath, string safeFileName, IFormFile file, string mimeType, object targetEntity, List<Attachment> attachments)
        {
            var fullPath = Path.Combine(uploadsPath, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                Id = 0,
                Filename = safeFileName,
                MimeType = mimeType,
            };

            if (targetEntity is QuestionTeacher question)
            {
                attachment.QuestionTeacher = question;
            }

            if (targetEntity is Feedback feedback)
            {
                attachment.Feedback = feedback;
            }

            attachments.Add(attachment);
            await _attachmentService.CreateAttachment(attachment);
        }
    }
}
