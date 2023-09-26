using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPBFService
    {
        //      Task<DBOperation> AddUpdatePBFDetailsAnalytical(PidfPbfFormEntity pbfEntity);
        //      Task<PidfPbfFormEntity> GetPbfFormDetailsAnalytical(long pidfId, int buid, int strengthid);

        Task<dynamic> FillDropdown(int PIDFId,int selectedbusinessunit);

        //Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid);
        //Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity);
        //Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid);

        //      // ---------------------------PBFDetails----------------------------

        //      //Task<PidfPbfFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid);
        //// ---------------------------PBFClinicalDetails----------------------------
        //Task<DBOperation> AddUpdatePBFClinicalDetails(PIDFPBFClinicalFormEntity pbfClinicalEntity);
        //Task<PIDFPBFClinicalFormEntity> GetPbfClinicalFormDetails(long pidfId, int buid, long? strengthid);

        // ---------------------------PBFDetails----------------------------
        Task<DBOperation> AddUpdatePBFDetails(PBFFormEntity pbfEntity, IFormFileCollection files, string webRootPath);

        Task<DBOperation> AddUpdateRnD(PidfPbfGeneralEntity PidfPbfGeneralEntity);

        Task<PBFFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid);

        Task<dynamic> PBFAllTabDetails(int PIDFId, int BUId, int pbfId = 0, int PbfRndDetailsId = 0,string APIurl=null);
        Task<List<MasterPlantLineEntity>> GetLineByPlantId(int id);
        Task<List<PidfPbfRaEntity>> GetRa(int PidfId, int PifdPbfId, int BuId);
        Task<List<MasterTypeOfSubmissionEntity>> GetTypeOfSubmission();
        Task<List<MasterNationApprovalEntity>> GetNationApprovals();
        Task<dynamic> GetPBFRADates(RaCalculatedDates calculatedDates);
        Task<dynamic> FileUpload(IFormFile files, string path, string uniqueFileName);
        Task<PidfProductStrengthGeneralRanD> GetCountryWisePackSizeStabilityData(long pidfId, int BUId, int countryid);
    }
}