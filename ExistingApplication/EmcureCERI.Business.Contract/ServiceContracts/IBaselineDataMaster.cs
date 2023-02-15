namespace EmcureCERI.Business.Contract
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBaselineDataMaster
    {
        ServiceResponseList<BaselineDataMaster> GetAllQuestionnaire();

        BaselineDataMaster FindQuestionnairesResult(int Id);

        void AddQuestionnaire(BaselineDataMaster objQuestionnaireModel);

        void UpdateQuestionnaire(BaselineDataMaster objQuestionnaireModel);

        void DeleteQuestionnaire(BaselineDataMaster objQuestionnaireModel);

        BaselineDataMaster GetBaselineDataByPatientId(int Id);
    }
}

