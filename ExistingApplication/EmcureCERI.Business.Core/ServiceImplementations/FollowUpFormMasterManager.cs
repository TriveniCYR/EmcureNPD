namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FollowUpFormMasterManager : IFollowUpFormMaster
    {
        private readonly IEntityBaseRepository<PatientDetails> _patient;
        private readonly IEntityBaseRepository<PatientFollowUpForm> _patientfollowupform;
        private readonly IEntityBaseRepository<FollowUpFormMaster> _followupform;
        private readonly IEntityBaseRepository<Fufquestionnaire1> _fufquestionnaire1;
        private readonly IEntityBaseRepository<Fufquestionnaire2> _fufquestionnaire2;
        private readonly IEntityBaseRepository<Fufquestionnaire3> _fufquestionnaire3;
        private readonly IEntityBaseRepository<Fufquestionnaire4> _fufquestionnaire4;
        private readonly IEntityBaseRepository<Fufquestionnaire5> _fufquestionnaire5;
        private readonly IEntityBaseRepository<Fufquestionnaire6> _fufquestionnaire6;
        private readonly IEntityBaseRepository<Fufquestionnaire7> _fufquestionnaire7;

        #region Default Construtor

        public FollowUpFormMasterManager
            (
                IEntityBaseRepository<PatientDetails> patient,
                 IEntityBaseRepository<PatientFollowUpForm> patientfollowupform,
                IEntityBaseRepository<FollowUpFormMaster> followupform,
                IEntityBaseRepository<Fufquestionnaire1> fufquestionnaire1,
                IEntityBaseRepository<Fufquestionnaire2> fufquestionnaire2,
                IEntityBaseRepository<Fufquestionnaire3> fufquestionnaire3,
                IEntityBaseRepository<Fufquestionnaire4> fufquestionnaire4,
                IEntityBaseRepository<Fufquestionnaire5> fufquestionnaire5,
                IEntityBaseRepository<Fufquestionnaire6> fufquestionnaire6,
                IEntityBaseRepository<Fufquestionnaire7> fufquestionnaire7
               )
        {
            _patient = patient;
            _patientfollowupform = patientfollowupform;
            _followupform = followupform;
            _fufquestionnaire1 = fufquestionnaire1;
            _fufquestionnaire2 = fufquestionnaire2;
            _fufquestionnaire3 = fufquestionnaire3;
            _fufquestionnaire4 = fufquestionnaire4;
            _fufquestionnaire5 = fufquestionnaire5;
            _fufquestionnaire6 = fufquestionnaire6;
            _fufquestionnaire7 = fufquestionnaire7;
        }
        #endregion

        

        public FollowUpFormMaster FindFufquestionnairesResult(int Id)
        {
             
            FollowUpFormMaster model = _followupform.GetSingle(o => o.Id == Id);
            if (model != null)
            {
                
                model.Fufquest1Navigation = _fufquestionnaire1.GetSingle(o => o.Id == model.Fufquest1);
                model.Fufquest2Navigation = _fufquestionnaire2.GetSingle(o => o.Id == model.Fufquest2);
                model.Fufquest3Navigation = _fufquestionnaire3.GetSingle(o => o.Id == model.Fufquest3);
                model.Fufquest4Navigation = _fufquestionnaire4.GetSingle(o => o.Id == model.Fufquest4);
                model.Fufquest5Navigation = _fufquestionnaire5.GetSingle(o => o.Id == model.Fufquest5);
                model.Fufquest6Navigation = _fufquestionnaire6.GetSingle(o => o.Id == model.Fufquest6);
                model.Fufquest7Navigation = _fufquestionnaire7.GetSingle(o => o.Id == model.Fufquest7);
            }
            else
            {
                model = new FollowUpFormMaster();
            }
            return model;
        }

        

        public void AddFufquestionnaire(FollowUpFormMaster entity)
        {
            _followupform.Add(entity);
            _followupform.Commit();
        }

        public void UpdateFufquestionnaire(FollowUpFormMaster entity)
        {
            _followupform.Edit(entity);
            _followupform.Commit();
        }

        public void DeleteFufquestionnaire(FollowUpFormMaster entity)
        {
            _followupform.Delete(entity);
            _followupform.Commit();
        }

    }
}
