using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;
using System.Collections.Generic;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IQAService
    {
        ServiceResponseList<QuestionDetails> GetAllQuestion();

        List<QuestionAnswer> GetAllQuesAnsByUserId(int Id, string lan);

        AnswerDetails GetAnswer(int Id);

        void AddAnswerMap(AnswerDetails entity);

        void UpdateAnswerMap(AnswerDetails entity);

        void DeleteAnswerMap(AnswerDetails entity);
    }
}