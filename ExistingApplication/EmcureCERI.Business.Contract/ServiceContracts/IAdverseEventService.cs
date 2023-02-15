namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    public interface IAdverseEventService
    {
        ServiceResponseList<StudyDrug> GetAllStudyDrug();

        ServiceResponseList<Outcome> GetAllOutcome();

        ServiceResponseList<RelaStudyDrug> GetAllRelaStudyDrug();
    }
}

