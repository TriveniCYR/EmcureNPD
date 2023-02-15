namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IEntityBaseRepository<Questionnaire1> _questionnaire1;
        private readonly IEntityBaseRepository<Questionnaire2> _questionnaire2;
        private readonly IEntityBaseRepository<Questionnaire3> _questionnaire3;
        private readonly IEntityBaseRepository<Questionnaire4> _questionnaire4;
        
        
        #region Default Construtor

        public QuestionnaireService(
            IEntityBaseRepository<Questionnaire1> questionnaire1,
            IEntityBaseRepository<Questionnaire2> questionnaire2,
            IEntityBaseRepository<Questionnaire3> questionnaire3,
            IEntityBaseRepository<Questionnaire4> questionnaire4)
        {
            _questionnaire1 = questionnaire1;
            _questionnaire2 = questionnaire2;
            _questionnaire3 = questionnaire3;
            _questionnaire4 = questionnaire4;
        }
        #endregion

        public Questionnaire1 FindQuestionnaires1Result(int? Id)
        {
            if (Id != null)
            {
                return _questionnaire1.GetSingle(o => o.Id == Id); 
            }
            else {
                return new Questionnaire1();
            }
        }

        public Questionnaire2 FindQuestionnaires2Result(int? Id)
        {
            if (Id != null)
            {
                return _questionnaire2.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Questionnaire2();
            }
        }

        public Questionnaire3 FindQuestionnaires3Result(int? Id)
        {
            if (Id != null)
            {
                return _questionnaire3.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Questionnaire3();
            }
        }

        public Questionnaire4 FindQuestionnaires4Result(int? Id)
        {
            if (Id != null)
            {
                return _questionnaire4.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Questionnaire4();
            }
        }

        public void AddQuestionnaire1(Questionnaire1 entity)
        {
            _questionnaire1.Add(entity);
            _questionnaire1.Commit();
        }

        public void UpdateQuestionnaire1(Questionnaire1 entity)
        {
            entity.IsFulFill = true;
            _questionnaire1.Edit(entity);
            _questionnaire1.Commit();
        }

        public void UpdateQuestionnaire2(Questionnaire2 entity)
        {   
            entity.IsFulFill = true;
            _questionnaire2.Edit(entity);
            _questionnaire2.Commit();
        }
        public void UpdateQuestionnaire3(Questionnaire3 entity)
        {
            entity.IsFulFill = true;
            _questionnaire3.Edit(entity);
            _questionnaire3.Commit();
        }
        public void UpdateQuestionnaire4(Questionnaire4 entity)
        {
            entity.IsFulFill = true;
            _questionnaire4.Edit(entity);
            _questionnaire4.Commit();
        }
       
    }
}
