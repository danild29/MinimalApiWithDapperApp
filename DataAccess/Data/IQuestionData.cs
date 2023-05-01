using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IQuestionData
    {
        Task<QuestionModel?> GetQuestion(int id);
        Task InsertQuestion(QuestionModel question);
        Task UpdateQuestion(QuestionModel question);
    }
}