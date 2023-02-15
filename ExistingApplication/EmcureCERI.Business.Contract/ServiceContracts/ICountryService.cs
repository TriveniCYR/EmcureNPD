namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    public interface ICountryService
    {
        ServiceResponseList<Country> GetAllCounties();
    }
}

