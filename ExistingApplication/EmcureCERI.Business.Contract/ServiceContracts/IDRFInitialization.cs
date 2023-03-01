using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
     public interface IDRFInitialization
    {
        int insertDRFInitialization(Tbl_DRF_Initialization entity);
        int updateDRFInitialization(Tbl_DRF_Initialization entity);
        int deleteDRFInitialization(Tbl_DRF_Initialization entity);
        IList<Tbl_DRF_Initialization> GetDRFInitializationLists(int Userid, int CompanyID);
        IList<Tbl_DRF_Initialization> GetDRFInitializationSingleRecord(int InitializationID);
        int updateDRFInitialApproval(Tbl_DRF_Initialization entity);
        void DRFPIDFMapping(Tbl_DRF_PIDF_Mapping entity);
        IList<PIDFDetailsNew> GetAttachedPIDFList(int DRFID, int CountryID);
        IList<Tbl_DRF_Initialization> GetFinalApprovedProjectList();

        Object GetFinalApprovedDRFList(int Userid,int CompanyID);

        int UpdateDRFApprovals(Tbl_DRF_FormApprovals entity,string UserRole,Int32 CurrentStatusID);
        IList<ProductMasterDropdownData> GetAllProductMasterNameList();
        IList<DropdownDetails> GetAllDetailsByProductMasterName(string DropdownType, string GenericName);

        IList<DRFInitializationDraftModel> GetAllDraftInitializationDataByUserID(int UserID);
        DRFInitializationDraftModel GetDraftInitializationDataByUserID_DraftID(int UserID, int DraftID);
        int DeleteDraftInitializationDataByUserID_DraftID(int UserID, int DraftID);
        int InsertUpdateDraft_InitializationData(DRFInitializationDraftModel entity);
        DRFInitializationDraftModel Check_Exists_DraftInitializationData(int? CountryID,string GenericName,int? FormulationID,int? StrengthID,int? PackSizeID, int? PackStyleID, int? PlantID, int? ProductTypeID);
    }
}
