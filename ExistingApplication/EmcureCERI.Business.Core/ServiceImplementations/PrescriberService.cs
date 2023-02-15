namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;

    public class PrescriberService : IPrescriberService
    {
        private readonly IEntityBaseRepository<PrescriberDetails> _prescriber;

        #region Default Construtor

        public PrescriberService(IEntityBaseRepository<PrescriberDetails> prescriber)
        {
            _prescriber = prescriber;
        }

        #endregion

        public ServiceResponseList<PrescriberDetails> GetAllPrescribers()
        {
            ServiceResponseList<PrescriberDetails> response = new ServiceResponseList<PrescriberDetails>() { Success = true };
            response.Results = _prescriber.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Prescriber found", Status = MessageType.Error });
            }
            return response;
        }

        public PrescriberDetails GetPrescriber(int Id)
        {
            return _prescriber.GetSingle(o => o.Id == Id);
        }

        public PrescriberDetails GetPrescriberByAspNetUserId(int Id)
        {
            return _prescriber.GetSingle(o => o.AspNetUserId == Id);
        }

        public void AddPrescriberDetails(PrescriberDetails entity)
        {
            _prescriber.Add(entity);
            _prescriber.Commit();
        }

        public void UpdatePrescriberDetails(PrescriberDetails entity)
        {
            _prescriber.Edit(entity);
            _prescriber.Commit();
        }
    }
}
