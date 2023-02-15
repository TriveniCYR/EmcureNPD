using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

//namespace EmcureCERI.Business.Contract.ServiceContracts
namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IPidfServiceNew
    {
        IList<ApprovedPIDFParentList> GetAllApprovedPidfList();
        IList<AprovedPidfCountryList> GetAllApprovedPidfCountryList(int PidID);
        IList<PIDFDetailsNew> GetAllPIDF(string PidfNumber);
        IList<Tbl_PIDF_Header> GetAllApprovalPIDF(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels);
        IList<PidfCountryDetailsNewModel> GetAllDetailedApprovalPIDF(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels);
        IList<clsPidfStrength> GetPidfStrengthDetails(Int64 PidfID);
        int UpdateApprovalPIDFStatus(UpdateApprovalPIDFStatusRequestModels updateApprovalPIDFStatusRequestModels);

        //Tbl_PIDF_Header GetPIDF(int Id);

        int AddPIDFDetails(PIDFHeaderAndDetails entity);

        int UpdatePIDFDetails(PIDFHeaderAndDetails entity);

        int InsertUploadFileDetails(UploadedFileModel entity);
        int DeleteAllUploadFileDetails(int PidfID);

        //FOLLOWING CODE ADDED BY YOGESH B ON DATE 16/02/2020
        int AddInitialPidfDetails(Tbl_PIDF_HeaderNew tbl_PIDF_HeaderNew, DataTable tblDetails);
        int UpdateInitialPidfDetails(Tbl_PIDF_HeaderNew tbl_PIDF_HeaderNew);
		int AddPidfCountryDetails(DataTable tblDetails);
        int UpdatePidfCountryDetails(PidfCountryDetailsNew entity);
        Object GetPIDFStrengthList();
        Object GetPIDFCountrywiseList(int ID);

        IList<DRFTaskSubTaskOutputNew> GetMixedTaskSubTaskListForPIDFInsertion(string strAction);

        clsAllContinentData GetAllPidfNewDetails(Int64 PidfID);

        PidfCountryDetailsNew GetCountryDetails(Int64 PidfID, int CountryID, int StrengthID);

        IList<PIDFParentStrengthList> PIDFParentStrengthList();

        IList<children> PIDFChildrenStrengthList(int PIDFID);

        IList<PIDFParentCountrywiseList> PIDFParentCountrywiseList(int PIDFID);
        IList<countryChildren> PIDFChildrenCountrywiseList(int PIDFID);

        Object GetPIDFTAskSubTaskList(string PidfID);
        IList<Tbl_DRF_Initialization> GetDRFList(int PidfID);

        IList<PidfCountryDetailsNew> GetAllCountryDetails(int PidfID);
    }
}
