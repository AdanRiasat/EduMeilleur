using EduMeilleurAPI.Data;
using EduMeilleurAPI.Models;

namespace EduMeilleurAPI.Services
{
    public class QuestionService
    {
        public readonly EduMeilleurAPIContext _context;

        public QuestionService(EduMeilleurAPIContext context)
        {
            _context = context;
        }

        private bool IsConstextValid()
        {
            return _context != null && _context.QuestionTeacher != null;
        }

        public async Task<QuestionTeacher?> CreateQuestionTeacher(QuestionTeacher question)
        {
            if (!IsConstextValid()) return null;

            _context.QuestionTeacher.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }
    }
}
