using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace EduMeilleurAPI.Services
{
    public class QuestionService
    {
        private readonly EduMeilleurAPIContext _context;
        private readonly PictureService _pictureService;
        private readonly AttachmentService _attachmentService;

        public QuestionService(EduMeilleurAPIContext context, PictureService pictureService, AttachmentService attachmentService)
        {
            _context = context;
            _pictureService = pictureService;
            _attachmentService = attachmentService;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.QuestionTeacher != null && _context.Feedbacks != null;
        }

        public async Task<QuestionTeacher?> CreateQuestionTeacher(QuestionTeacher question)
        {
            if (!IsConstextValid()) return null;

            _context.QuestionTeacher.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Feedback?> CreateFeedback(Feedback feedback)
        {
            if (!IsConstextValid()) return null;

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task SaveFilesAndAttachements(IFormCollection collection, List<Picture> pictures, List<Attachment> attachments, object targetEntity)
        {
            for (int i = 1; i <= collection.Files.Count; i++)
            {
                IFormFile? file = collection.Files.GetFile("file" + i);
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    var mimeType = file.ContentType;
                    var safeFileName = Guid.NewGuid().ToString() + extension;
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", safeFileName);

                    if (mimeType.StartsWith("image/"))
                    {
                        try
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
                        catch
                        {
                            //log errors
                        }
                    }
                    else
                    {
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
        }
    }
}
