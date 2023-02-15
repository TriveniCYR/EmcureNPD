namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public class QAService : IQAService
    {
        private readonly IEntityBaseRepository<QuestionDetails> _question;
        private readonly IEntityBaseRepository<AnswerDetails> _answer;

        #region Default Construtor

        public QAService(IEntityBaseRepository<QuestionDetails> question, IEntityBaseRepository<AnswerDetails> answer)
        {
            _question = question;
            _answer = answer;
        }

        #endregion

        public ServiceResponseList<QuestionDetails> GetAllQuestion()
        {
            ServiceResponseList<QuestionDetails> response = new ServiceResponseList<QuestionDetails>() { Success = true };
            response.Results = _question.AllIncluding().Where(o=>o.IsDeleted == false).ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Patient found", Status = MessageType.Error });
            }
            return response;
        }

        public List<QuestionAnswer> GetAllQuesAnsByUserId(int Id, string lan)
        {
            ServiceResponseList<AnswerDetails> response = new ServiceResponseList<AnswerDetails>() { Success = true };
            response.Results = _answer.AllIncluding().Where(o => o.PrescriberId == Id).ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Answer found", Status = MessageType.Error });
            }
            List<QuestionAnswer> quesanslist = new List<QuestionAnswer>();
            foreach (var item in response.Results)
            {
                QuestionAnswer model = new QuestionAnswer();
                switch (lan)
                {
                    case "nl-BE":
                        model.Id = item.Id;
                        model.QuesId = item.QuestId;
                        model.Question = _question.GetSingle(o => o.Id == item.QuestId).QuestionBe;
                        model.Answer = item.Answer;
                        break;
                    case "de-DE":
                        model.Id = item.Id;
                        model.QuesId = item.QuestId;
                        model.Question = _question.GetSingle(o => o.Id == item.QuestId).QuestionDe;
                        model.Answer = item.Answer;
                        break;
                    case "en-GB":
                        model.Id = item.Id;
                        model.QuesId = item.QuestId;
                        model.Question = _question.GetSingle(o => o.Id == item.QuestId).QuestionGb;
                        model.Answer = item.Answer;
                        break;
                    case "es-ES":
                        model.Id = item.Id;
                        model.QuesId = item.QuestId;
                        model.Question = _question.GetSingle(o => o.Id == item.QuestId).QuestionEs;
                        model.Answer = item.Answer;
                        break;
                    case "fr-FR":
                        model.Id = item.Id;
                        model.QuesId = item.QuestId;
                        model.Question = _question.GetSingle(o => o.Id == item.QuestId).QuestionFr;
                        model.Answer = item.Answer;
                        break;
                }
                quesanslist.Add(model);
            }
            return quesanslist;
        }

        public AnswerDetails GetAnswer(int Id)
        {
            return _answer.GetSingle(o => o.Id == Id);
        }

        public void AddAnswerMap(AnswerDetails entity)
        {
            _answer.Add(entity);
            _answer.Commit();
        }

        public void UpdateAnswerMap(AnswerDetails entity)
        {
            _answer.Edit(entity);
            _answer.Commit();
        }

        public void DeleteAnswerMap(AnswerDetails entity)
        {
            _answer.Delete(entity);
            _answer.Commit();
        }
    }
}
