

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;
 
        public class Master_CountryService : IMaster_CountryService
    {


        private readonly IEntityBaseRepository<Master_Country> _country;

        #region Default Construtor

        public Master_CountryService(IEntityBaseRepository<Master_Country> country)
        {
            _country = country;
        }

        #endregion

        public ServiceResponseList<Master_Country> GetAllCountry()
        {
            ServiceResponseList<Master_Country> response = new ServiceResponseList<Master_Country>() { Success = true };
            response.Results = _country.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No PIDF found", Status = MessageType.Error });
            }
            return response;
        }

        public ServiceResponseList<Master_Country> GetAllCountryByRegion(int Id)
        {
            ServiceResponseList<Master_Country> response = new ServiceResponseList<Master_Country>() { Success = true };
            response.Results = _country.AllIncluding().Where(o => o.ContinentID== Id).ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No PIDF found", Status = MessageType.Error });
            }
            return response;
        }

        public Master_Country GetCountry(int Id)
        {
            return _country.GetSingle(o => o.Id == Id);
        }

        public void AddGetCountryDetails(Master_Country entity)
        {
            _country.Add(entity);
            _country.Commit();
        }

        public void UpdateCountryDetails(Master_Country entity)
        {
            _country.Edit(entity);
            _country.Commit();
        }
    }


}
