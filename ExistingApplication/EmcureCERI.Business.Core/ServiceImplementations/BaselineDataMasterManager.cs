namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BaselineDataMasterManager : IBaselineDataMaster
    {
        private readonly IEntityBaseRepository<PatientDetails> _patient;
        private readonly IEntityBaseRepository<BaselineDataMaster> _baselinedata;
        private readonly IEntityBaseRepository<Questionnaire1> _questionnaire1;
        private readonly IEntityBaseRepository<Questionnaire2> _questionnaire2;
        private readonly IEntityBaseRepository<Questionnaire3> _questionnaire3;
        private readonly IEntityBaseRepository<Questionnaire4> _questionnaire4;

        #region Default Construtor

        public BaselineDataMasterManager
            (
                IEntityBaseRepository<PatientDetails> patient,
                IEntityBaseRepository<BaselineDataMaster> baselinedata,
                IEntityBaseRepository<Questionnaire1> questionnaire1,
                IEntityBaseRepository<Questionnaire2> questionnaire2,
                IEntityBaseRepository<Questionnaire3> questionnaire3,
                IEntityBaseRepository<Questionnaire4> questionnaire4
               )
        {
            _patient = patient;
            _baselinedata = baselinedata;
            _questionnaire1 = questionnaire1;
            _questionnaire2 = questionnaire2;
            _questionnaire3 = questionnaire3;
            _questionnaire4 = questionnaire4;
        }
        #endregion

        public ServiceResponseList<BaselineDataMaster> GetAllQuestionnaire()
        {
            ServiceResponseList<BaselineDataMaster> response = new ServiceResponseList<BaselineDataMaster>() { Success = true };
            response.Results = _baselinedata.AllIncluding().ToList();

            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Questionnaire found", Status = MessageType.Error });
            }
            return response;
        }

        public BaselineDataMaster FindQuestionnairesResult(int Id)
        {
            
                BaselineDataMaster model = _baselinedata.GetSingle(o => o.PatientId == Id);
                if (model != null)
                {
                    model.Patient = _patient.GetSingle(o => o.Id == model.PatientId);
                    model.Quest1Navigation = _questionnaire1.GetSingle(o => o.Id == model.Quest1);
                    model.Quest2Navigation = _questionnaire2.GetSingle(o => o.Id == model.Quest2);
                    model.Quest3Navigation = _questionnaire3.GetSingle(o => o.Id == model.Quest3);
                    model.Quest4Navigation = _questionnaire4.GetSingle(o => o.Id == model.Quest4);
                }
                else
                {
                    model = new BaselineDataMaster();
                }
           
            
            return model;
        }

        public BaselineDataMaster GetBaselineDataByPatientId(int Id)
        {
            BaselineDataMaster model = new BaselineDataMaster();
            if (Id != 0)
            {
                model = _baselinedata.GetSingle(o => o.PatientId == Id);
            }
            else {
                model = new BaselineDataMaster();
            }
            return model;
        }

        public void AddQuestionnaire(BaselineDataMaster entity)
        {
            _baselinedata.Add(entity);
            _baselinedata.Commit();
        }

        public void UpdateQuestionnaire(BaselineDataMaster entity)
        {
            _baselinedata.Edit(entity);
            _baselinedata.Commit();
        }

        public void DeleteQuestionnaire(BaselineDataMaster entity)
        {
            _baselinedata.Delete(entity);
            _baselinedata.Commit();
        }

    }
}
