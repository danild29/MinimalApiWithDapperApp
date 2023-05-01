using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class QuestionData : IQuestionData
{
    private readonly ISqlDataAccess _db;

    public QuestionData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<QuestionModel?> GetQuestion(int id)
    {
        var res = await _db.LoadData<QuestionModel, dynamic>("dbo.spQuestion_Get", new { Id = id });

        return res.FirstOrDefault();
    }

    public Task InsertQuestion(QuestionModel question) =>
        _db.SaveData("dbo.spQuestion_Insert", new { question.Question, question.Answer, question.Content, question.ContentType });

    public Task UpdateQuestion(QuestionModel question) =>
        _db.SaveData("dbo.spQuestion_Update", question);

    //public Task DeleteUser(int id) =>
    //    _db.SaveData("dbo.spUser_Delete", new { Id = id });
}
