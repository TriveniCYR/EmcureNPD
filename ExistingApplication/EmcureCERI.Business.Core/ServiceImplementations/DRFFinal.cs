using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFFinal : IDRFFinal
    {
        private readonly EmcureCERIDBContext _db;

        public DRFFinal()
        {
            _db = new EmcureCERIDBContext();
        }

        public int checkedIDsDRFwise(int ID,int ModifyBy, DateTime ModifiedDate)
        {
            try
            {
                _db.LoadStoredProc("USP_checkedIDsDRFwise")
               .WithSqlParam("@ID", ID)
                .WithSqlParam("@ModifyBy", ModifyBy)
                .WithSqlParam("@ModifiedDate", ModifiedDate)
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<ChildrenNationalCheckList> ChildrenNationalCheckList(int DRFID, int TransactionID)
        {
            IList<ChildrenNationalCheckList> result = new List<ChildrenNationalCheckList>();

            _db.LoadStoredProc("USP_GetChild_NationalCheckList")
            .WithSqlParam("@DRFID", DRFID)
            .WithSqlParam("@TransactionID", TransactionID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<ChildrenNationalCheckList>();
            });

            return result;
        }

        public IList<Tbl_Transaction_CheckList> GetCheckList(int DRFID)
        {
            IList<Tbl_Transaction_CheckList> result = new List<Tbl_Transaction_CheckList>();
            try
            {
                _db.LoadStoredProc("USP_GetCheckList")
                 .WithSqlParam("@DRFID", DRFID)
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_Transaction_CheckList>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public FinalApprovalDetails GetFinalApprovalDetails(int InitializationID)
        {
            IList<FinalApprovalDetails> result = new List<FinalApprovalDetails>();
            _db.LoadStoredProc("USP_GetFinalApprovalDetails")
            .WithSqlParam("@InitializationID", InitializationID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<FinalApprovalDetails>();
            });

            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        public IList<Tbl_Master_FolderStructure> GetFolderStructureCountryWise(int DossierTemplateID, int CountryID)
        {
            IList<Tbl_Master_FolderStructure> result = new List<Tbl_Master_FolderStructure>();
            try
            {
                _db.LoadStoredProc("USP_GetFolderStructureDossierTemplateCountryWise")
                 .WithSqlParam("@DossierTemplateID", DossierTemplateID)
                 .WithSqlParam("@CountryID", CountryID)
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_Master_FolderStructure>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<DRFTaskSubTaskOutput> GetMixedTaskSubTaskListForDRFInsertion()
        {
            IList<DRFTaskSubTaskOutput> result = new List<DRFTaskSubTaskOutput>();
            try
            {
                _db.LoadStoredProc("USP_GetMixedTaskSubTaskListForDRFInsertion")
                  .ExecuteStoredProc((handler) =>
                  {
                      result = handler.ReadToList<DRFTaskSubTaskOutput>();
                  });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFFinalApprovel(Tbl_DRF_FinalApprovelDetails entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFFinalApprovel")
               .WithSqlParam("@InitializationID", entity.InitializationID)
               .WithSqlParam("@ApprovedReject", entity.ApprovedReject)
               .WithSqlParam("@Comment", entity.Comment)
               .WithSqlParam("@CreatedBy", entity.Createdby)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertGeneralCheckList(int DRFID,int CreatedBy,DateTime CreatedDate)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertGeneralCheckList")
                      .WithSqlParam("@DRFID", DRFID)
                      .WithSqlParam("@CreatedBy", CreatedBy)
                      .WithSqlParam("@CreatedDate", CreatedDate)
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertNationalCheckList(int DRFID, string Name, int CreatedBy, DateTime CreatedDate,int ParentID)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertNationalCheckList")
                      .WithSqlParam("@DRFID", DRFID)
                      .WithSqlParam("@Name", Name)
                      .WithSqlParam("@ParentID", ParentID)
                      .WithSqlParam("@CreatedBy", CreatedBy)
                      .WithSqlParam("@CreatedDate", CreatedDate)
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertTaskSubTaskDetails(TaskSubTaskInputs entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFTaskSubTask")
                .WithSqlParam("@taskOrder", entity.TaskOrder)
                 .WithSqlParam("@taskName", entity.TaskName)
                 .WithSqlParam("@parentID", entity.ParentID)
                 .WithSqlParam("@action", entity.Action)
                .WithSqlParam("@drfid", entity.DRFID)
                .WithSqlParam("@startDate", entity.StartDate)
                .WithSqlParam("@endDate", entity.EndDate)
                .WithSqlParam("@priorityID", entity.PriorityID)
                .WithSqlParam("@priority", entity.Priority)
                .WithSqlParam("@taskStatusID", entity.TaskStatusID)
                .WithSqlParam("@taskStatus", entity.TaskStatus)
                .WithSqlParam("@taskDuration", entity.TaskDuration)
                .WithSqlParam("@totalPercentage", entity.TotalPercentage)
                .WithSqlParam("@empID", entity.EmpID)
                .WithSqlParam("@isActive", entity.IsActive)
                .WithSqlParam("@createdBy", entity.CreatedBy)
                .WithSqlParam("@createdDate", entity.CreatedDate)
                .WithSqlParam("@modifiedBy", entity.ModifiedBy)
                .WithSqlParam("@modifiedDate", entity.ModifiedDate)
                .WithSqlParam("@type", entity.Type)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<ParentNationalCheckList> ParentNationalCheckList(int DRFID)
        {
            IList<ParentNationalCheckList> result = new List<ParentNationalCheckList>();

            _db.LoadStoredProc("USP_GetParent_NationalCheckList")
            .WithSqlParam("@DRFID",DRFID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<ParentNationalCheckList>();
            });

            return result;
        }

        public int UncheckedAllDRFwise(int DRFID, int ModifyBy, DateTime ModifiedDate)
        {
            try
            {
                _db.LoadStoredProc("USP_UncheckedAllDRFwise")
               .WithSqlParam("@DRFID", DRFID)
                .WithSqlParam("@ModifyBy", ModifyBy)
                .WithSqlParam("@ModifiedDate", ModifiedDate)
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
