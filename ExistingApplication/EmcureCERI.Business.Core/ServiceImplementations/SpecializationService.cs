namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System.Linq;

    public class SpecializationService : ISpecializationService
    {
        private readonly IEntityBaseRepository<Specialization> _specialization;

        #region Default Construtor

        public SpecializationService(IEntityBaseRepository<Specialization> specialization)
        {
            _specialization = specialization;
        }

        #endregion

        public ServiceResponseList<Specialization> GetAllSpecialization()
        {
            ServiceResponseList<Specialization> response = new ServiceResponseList<Specialization>() { Success = true };
            response.Results = _specialization.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Specialization found", Status = MessageType.Error });
            }
            return response;
        }
    }
}
