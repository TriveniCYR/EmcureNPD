using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPBFService
    {
        //      Task<DBOperation> AddUpdatePBFDetailsAnalytical(PidfPbfFormEntity pbfEntity);
        //      Task<PidfPbfFormEntity> GetPbfFormDetailsAnalytical(long pidfId, int buid, int strengthid);

        Task<dynamic> FillDropdown(int PIDFId);

        //Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid);
        //Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity);
        //Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid);

        //      // ---------------------------PBFDetails----------------------------

        //      //Task<PidfPbfFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid);
        //// ---------------------------PBFClinicalDetails----------------------------
        //Task<DBOperation> AddUpdatePBFClinicalDetails(PIDFPBFClinicalFormEntity pbfClinicalEntity);
        //Task<PIDFPBFClinicalFormEntity> GetPbfClinicalFormDetails(long pidfId, int buid, long? strengthid);

        // ---------------------------PBFDetails----------------------------
        Task<DBOperation> AddUpdatePBFDetails(PBFFormEntity pbfEntity);

        Task<DBOperation> AddUpdateRnD(PidfPbfGeneralEntity PidfPbfGeneralEntity);

        Task<PBFFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid);

        Task<dynamic> PBFAllTabDetails(int PIDFId, int BUId);
        Task<List<MasterPlantLineEntity>> GetLineByPlantId(int id);
        Task<List<PidfPbfRaEntity>> GetRa(int PidfId, int PifdPbfId);
        Task<List<MasterTypeOfSubmissionEntity>> GetTypeOfSubmission();
    }
}