using EmcureCERI.Business.Models;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmcureCERI.Business.Contract
{
   public interface IDRFService
    {
        ServiceResponseList<DRFDetails> GetAllDRF();
        DRFDetails GetPIDF(int Id);

        void AddDRFDetails(DRFDetails entity);

        int UpdateDRFTaskSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity);
        int DeleteDRFTaskSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity);

        int insertDRFTaskDetails(Tbl_Master_ProjectTask_Mapping entity);
        int insertDRFSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity);

        void UpdateDRFDetails(DRFDetails entity);

        void DRFContinentCountryMapping(Tbl_DSR_NewProductContinentMapping entity);

        void DRFPIDFMapping(Tbl_DRF_PIDF_Mapping entity);

        void DRFRegistrationFeesMapping(Tbl_DRF_Percentage_Mapping entity);

        int InsertTaskSubTaskDetails(TaskSubTaskInputs entity);

        IList<PIDFDetailsNew> GetAttachedPIDFList(int DRFID, int CountryID);

        Object GetDRFTAskSubTaskList(string DRFID);

        IList<DRFTaskSubTaskOutput> GetMixedTaskSubTaskListForDRFInsertion();

        int InsertTransactionProjectTaskHistory(Int64 projectMappingTaskID);
    }
}
