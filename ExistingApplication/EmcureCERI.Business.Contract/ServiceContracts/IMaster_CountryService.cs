using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IMaster_CountryService
    {
        ServiceResponseList<Master_Country> GetAllCountry();

        ServiceResponseList<Master_Country> GetAllCountryByRegion(int Id);

        Master_Country GetCountry(int Id);

        void AddGetCountryDetails(Master_Country entity);

        void UpdateCountryDetails(Master_Country entity);


    }
}
