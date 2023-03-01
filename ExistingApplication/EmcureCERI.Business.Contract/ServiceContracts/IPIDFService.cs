using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities; 

namespace EmcureCERI.Business.Core.ServiceImplementations
{

    public interface IPIDFService
    {
        ServiceResponseList<PIDFDetails> GetAllPIDF();

        //ServiceResponseList<PIDFDetails> GetAllPIDFByProduct(int Id);

        PIDFDetails GetPIDF(int Id);

        void AddPIDFDetails(PIDFDetails entity);

        void UpdatePIDFDetails(PIDFDetails entity);
    }
}
