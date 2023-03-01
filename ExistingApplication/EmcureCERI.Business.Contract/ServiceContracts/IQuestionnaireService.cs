namespace EmcureCERI.Business.Contract
{
    using EmcureCERI.Data.DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IQuestionnaireService
    {
        Questionnaire1 FindQuestionnaires1Result(int? Id);

        Questionnaire2 FindQuestionnaires2Result(int? Id);

        Questionnaire3 FindQuestionnaires3Result(int? Id);

        Questionnaire4 FindQuestionnaires4Result(int? Id);

        void AddQuestionnaire1(Questionnaire1 entity);

        void UpdateQuestionnaire1(Questionnaire1 entity);
        void UpdateQuestionnaire2(Questionnaire2 entity);
        void UpdateQuestionnaire3(Questionnaire3 entity);
        void UpdateQuestionnaire4(Questionnaire4 entity);
      
    }
}

