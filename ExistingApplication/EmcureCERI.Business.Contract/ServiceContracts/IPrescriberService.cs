using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IPrescriberService
    {
        ServiceResponseList<PrescriberDetails> GetAllPrescribers();

        PrescriberDetails GetPrescriber(int Id);

        PrescriberDetails GetPrescriberByAspNetUserId(int Id);
        
        void AddPrescriberDetails(PrescriberDetails entity);

        void UpdatePrescriberDetails(PrescriberDetails entity);
    }
}