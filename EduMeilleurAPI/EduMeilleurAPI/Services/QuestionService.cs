using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using EduMeilleurAPI.Models.Interfaces;
using EduMeilleurAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace EduMeilleurAPI.Services
{
    public class QuestionService
    {
        private const long MAX_SINGLE_FILE_SIZE = 4194304;
        private const long MAX_TOTAL_SIZE = 15728640;
        private readonly string _imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images");
        private readonly string _attachmentsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "attachments");
        
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
            var (valid, error) = ValidateTextLength(question.Title, question.Message);
            if (!valid) return error;
            
            (valid, error) = ValidateFileSizes(formCollection);
            if (!valid) return error;
            
            _context.QuestionTeacher.Add(question);
            await SaveFilesAndAttachments(formCollection, question.Pictures, question.Attachments, question);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string?> CreateFeedback(Feedback feedback, IFormCollection formCollection)
        {
            var (valid, error) = ValidateTextLength(feedback.Title, feedback.Message);
            if (!valid) return error;
            
            (valid, error) = ValidateFileSizes(formCollection);
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
        
        private (bool valid, string? error) ValidateTextLength(string title, string message)
        {
            if (title.Length > 50)
                return (false, "Title cannot exceed 50 characters.");

            if (message.Length > 1500)
                return (false, "Message cannot exceed 1500 characters.");

            return (true, null);
        }

        public async Task SaveFilesAndAttachments(IFormCollection collection, List<Picture> pictures, List<Attachment> attachments, IQuestionFeedback targetEntity)
        {
            for (int i = 1; i <= collection.Files.Count; i++)
            {
                IFormFile? file = collection.Files.GetFile("file" + i);
                if (file == null) continue;
                
                var extension = Path.GetExtension(file.FileName).ToLower();
                var mimeType = file.ContentType;
                var safeFileName = Guid.NewGuid().ToString() + extension;

                if (mimeType.StartsWith("image/"))
                {
                    if (!Directory.Exists(_imagesPath))
                    {
                        Directory.CreateDirectory(_imagesPath);
                    }
                    
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
                    if (!Directory.Exists(_attachmentsPath))
                    {
                        Directory.CreateDirectory(_attachmentsPath);
                    }

                    try
                    {
                        await SaveAttachment(safeFileName, file, mimeType, targetEntity, attachments);
                    }
                    catch
                    {
                        //log errors
                    }
                }
                
            }
        }

        private async Task SaveImage(IFormFile file, string safeFileName, IQuestionFeedback targetEntity, List<Picture> pictures)
        {
            var fullPath = Path.Combine(_imagesPath, safeFileName);
            Image image = await Image.LoadAsync(file.OpenReadStream());
            Picture picture = new Picture
            {
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
            await image.SaveAsync(fullPath);
            await _pictureService.CreatePicture(picture);
        }

        private async Task SaveAttachment(string safeFileName, IFormFile file, string mimeType, IQuestionFeedback targetEntity, List<Attachment> attachments)
        {
            var fullPath = Path.Combine(_attachmentsPath, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                FileName = safeFileName,
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
        
        public List<string> GetFilePaths(IQuestionFeedback questionFeedback)
        {
            return questionFeedback.Pictures
                .Select(p => Path.Combine(_imagesPath, p.FileName))
                .Concat(questionFeedback.Attachments.Select(a => Path.Combine(_attachmentsPath, a.FileName)))
                .ToList();
        }
    }
}
