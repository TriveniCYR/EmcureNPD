namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Data.Repository;
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    using System.Linq;

    public class AdverseEventService : IAdverseEventService
    {
        private readonly IEntityBaseRepository<StudyDrug> _studyDrug;
        private readonly IEntityBaseRepository<Outcome> _outcome;
        private readonly IEntityBaseRepository<RelaStudyDrug> _relaStudyDrug;

        #region Default Construtor

        public AdverseEventService(IEntityBaseRepository<StudyDrug> studyDrug,
            IEntityBaseRepository<Outcome> outcome,
            IEntityBaseRepository<RelaStudyDrug> relaStudyDrug)
        {
            _studyDrug = studyDrug;
            _outcome = outcome;
            _relaStudyDrug = relaStudyDrug;
        }

        #endregion

        public ServiceResponseList<StudyDrug> GetAllStudyDrug() {
            ServiceResponseList<StudyDrug> response = new ServiceResponseList<StudyDrug>() { Success = true };
            response.Results = _studyDrug.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Study Drug Action given found", Status = MessageType.Error });
            }
            return response;
        }

        public ServiceResponseList<Outcome> GetAllOutcome() {
            ServiceResponseList<Outcome> response = new ServiceResponseList<Outcome>() { Success = true };
            response.Results = _outcome.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Outcome given found", Status = MessageType.Error });
            }
            return response;
        }

        public ServiceResponseList<RelaStudyDrug> GetAllRelaStudyDrug() {
            ServiceResponseList<RelaStudyDrug> response = new ServiceResponseList<RelaStudyDrug>() { Success = true };
            response.Results = _relaStudyDrug.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No Relationship to Study Drug given found", Status = MessageType.Error });
            }
            return response;
        }
    }
}
