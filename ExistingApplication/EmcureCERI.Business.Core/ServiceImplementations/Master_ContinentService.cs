 
namespace EmcureCERI.Business.Core.ServiceImplementations
{

    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;
    public class Master_ContinentService : IMaster_ContinentService
    {


        private readonly IEntityBaseRepository<Master_Continent> _region;

        #region Default Construtor

        public Master_ContinentService(IEntityBaseRepository<Master_Continent> region)
        {
            _region = region;
        }

        #endregion

        public ServiceResponseList<Master_Continent> GetAllRegion()
        {
            ServiceResponseList<Master_Continent> response = new ServiceResponseList<Master_Continent>() { Success = true };
            response.Results = _region.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No PIDF found", Status = MessageType.Error });
            }
            return response;
        }

        //public ServiceResponseList<PIDFDetails> GetAllRegionByActive(int Id)
        //{
        //    ServiceResponseList<PIDFDetails> response = new ServiceResponseList<PIDFDetails>() { Success = true };
        //    response.Results = _pidf.AllIncluding().Where(o => o.ProductId == Id).ToList();
        //    if (null == response.Results)
        //    {
        //        response.Success = false;
        //        response.Messages.Add(new Message() { Detail = "No PIDF found", Status = MessageType.Error });
        //    }
        //    return response;
        //}

        public Master_Continent GetRegion(int Id)
        {
            return _region.GetSingle(o => o.Id == Id);
        }

        public void AddRegionDetails(Master_Continent entity)
        {
            _region.Add(entity);
            _region.Commit();
        }

        public void UpdateRegionDetails(Master_Continent entity)
        {
            _region.Edit(entity);
            _region.Commit();
        }
    }

}
