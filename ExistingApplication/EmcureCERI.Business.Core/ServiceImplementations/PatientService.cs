namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;

    public class PatientService : IPatientService
    {
        private readonly IEntityBaseRepository<PatientDetails> _patient;

        #region Default Construtor

        public PatientService(IEntityBaseRepository<PatientDetails> patient)
        {
            _patient = patient;
        }

        #endregion

        public ServiceResponseList<PatientDetails> GetAllPatients()
        {
            ServiceResponseList<PatientDetails> response = new ServiceResponseList<PatientDetails>() { Success = true };
            response.Results = _patient.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Patient found", Status = MessageType.Error });
            }
            return response;
        }

        public ServiceResponseList<PatientDetails> GetAllPatientsByPrescriber(int Id)
        {
            ServiceResponseList<PatientDetails> response = new ServiceResponseList<PatientDetails>() { Success = true };
            response.Results = _patient.AllIncluding().Where(o => o.AspNetUserId == Id).ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Patient found", Status = MessageType.Error });
            }
            return response;
        }

        public PatientDetails GetPatient(int Id)
        {
            return _patient.GetSingle(o => o.Id == Id);
        }

        public void AddPatientDetails(PatientDetails entity)
        {
            _patient.Add(entity);
            _patient.Commit();
        }

        public void UpdatePatientDetails(PatientDetails entity)
        {
            _patient.Edit(entity);
            _patient.Commit();
        }
    }
}
