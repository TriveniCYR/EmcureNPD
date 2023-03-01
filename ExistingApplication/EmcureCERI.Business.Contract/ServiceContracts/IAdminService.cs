using EmcureCERI.Business.Models.DataModel;

namespace EmcureCERI.Business.Contract
{
   
    public interface IAdminService
    {
        AdminObject GetAdmin();
        PatientPrescriverObject GetPatientPrescriberByPatientId(int Id);
        PrescriverObject GetPrescriber(int Id);
    }
}

