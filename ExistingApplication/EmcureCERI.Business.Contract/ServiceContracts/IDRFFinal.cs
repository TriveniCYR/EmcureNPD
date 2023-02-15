using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDRFFinal
    {
        int insertDRFFinalApprovel(Tbl_DRF_FinalApprovelDetails entity);
        IList<DRFTaskSubTaskOutput> GetMixedTaskSubTaskListForDRFInsertion();
        int InsertTaskSubTaskDetails(TaskSubTaskInputs entity);
        IList<Tbl_Master_FolderStructure> GetFolderStructureCountryWise(int DossierTemplateID,int CountryID);
        int InsertGeneralCheckList(int DRFID,int CreatedBy,DateTime CreatedDate);
        int InsertNationalCheckList(int DRFID, string Name, int CreatedBy, DateTime CreatedDate,int ParentID);
        IList<Tbl_Transaction_CheckList> GetCheckList(int DRFID);
        int UncheckedAllDRFwise(int DRFID, int ModifyBy, DateTime ModifiedDate);
        int checkedIDsDRFwise(int ID,int ModifyBy, DateTime ModifiedDate);
        IList<ParentNationalCheckList> ParentNationalCheckList(int DRFID);
        IList<ChildrenNationalCheckList> ChildrenNationalCheckList(int DRFID,int TransactionID);
        FinalApprovalDetails GetFinalApprovalDetails(int InitializationID);
    }
}
