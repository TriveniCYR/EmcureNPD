using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations 
{
    public interface IMaster_ContinentService
    {

        ServiceResponseList<Master_Continent> GetAllRegion();

        //ServiceResponseList<Master_Continent> GetAllRegionByActive(int Id);

        Master_Continent GetRegion(int Id);

        void AddRegionDetails(Master_Continent entity);

        void UpdateRegionDetails(Master_Continent entity);



    }
}
