using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IPatientService
    {
        ServiceResponseList<PatientDetails> GetAllPatients();

        ServiceResponseList<PatientDetails> GetAllPatientsByPrescriber(int Id);

        PatientDetails GetPatient(int Id);

        void AddPatientDetails(PatientDetails entity);

        void UpdatePatientDetails(PatientDetails entity);
    }
}