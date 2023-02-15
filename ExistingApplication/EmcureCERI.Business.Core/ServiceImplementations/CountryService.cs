namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Data.Repository;
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using System.Linq;

    public class CountryService : ICountryService
    {
        private readonly IEntityBaseRepository<Country> _country;
       
        #region Default Construtor

        public CountryService(IEntityBaseRepository<Country> country)
        {
            _country = country;
        }

        #endregion

        public ServiceResponseList<Country> GetAllCounties()
        {
            ServiceResponseList<Country> response = new ServiceResponseList<Country>() { Success = true };
            response.Results = _country.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Country found", Status = MessageType.Error });
            }
            return response;
        }
    }
}
