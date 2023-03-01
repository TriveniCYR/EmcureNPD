namespace EmcureCERI.Business.Contract
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFollowUpFormMaster
    {
       
        FollowUpFormMaster FindFufquestionnairesResult(int Id);

        void AddFufquestionnaire(FollowUpFormMaster objFUFQuestionnaireModel);

        void UpdateFufquestionnaire(FollowUpFormMaster objFUFQuestionnaireModel);

        void DeleteFufquestionnaire(FollowUpFormMaster objFUFQuestionnaireModel);

       
    }
}

