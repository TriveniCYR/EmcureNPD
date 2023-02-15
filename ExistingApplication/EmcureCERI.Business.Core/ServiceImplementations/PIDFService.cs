 
namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;

    public class PIDFService : IPIDFService
    {
        private readonly IEntityBaseRepository<PIDFDetails> _pidf;

        #region Default Construtor

        public PIDFService(IEntityBaseRepository<PIDFDetails> pidf)
        {
            _pidf = pidf;
        }

        #endregion

        public ServiceResponseList<PIDFDetails> GetAllPIDF()
        {
            ServiceResponseList<PIDFDetails> response = new ServiceResponseList<PIDFDetails>() { Success = true };
            response.Results = _pidf.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No PIDF found", Status = MessageType.Error });
            }
            return response;
        }

        //public ServiceResponseList<PIDFDetails> GetAllPatientsByPrescriber(int Id)
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

        public PIDFDetails GetPIDF(int Id)
        {
            return _pidf.GetSingle(o => o.Id == Id);
        }

        public void AddPIDFDetails(PIDFDetails entity)
        {
            _pidf.Add(entity);
            _pidf.Commit();
        }

        public void UpdatePIDFDetails(PIDFDetails entity)
        {
            _pidf.Edit(entity);
            _pidf.Commit();
        }
    }
}
