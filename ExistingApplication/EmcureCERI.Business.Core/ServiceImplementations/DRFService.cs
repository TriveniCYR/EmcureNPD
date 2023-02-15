

using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Models;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmcureCERI.Business.Core
{
    public class DRFService : IDRFService
    {
        private readonly IEntityBaseRepository<DRFDetails> _DRF;
        private readonly IEntityBaseRepository<Tbl_DSR_NewProductContinentMapping> _DRFContinent;
        private readonly IEntityBaseRepository<Tbl_DRF_PIDF_Mapping> _DRFPRDF;
        private readonly IEntityBaseRepository<Tbl_DRF_Percentage_Mapping> _DRFPER;
        private readonly EmcureCERIDBContext _db;
        public DRFService(IEntityBaseRepository<DRFDetails> drf, IEntityBaseRepository<Tbl_DSR_NewProductContinentMapping> drfContinent, IEntityBaseRepository<Tbl_DRF_PIDF_Mapping> drfPIDF,IEntityBaseRepository<Tbl_DRF_Percentage_Mapping> drfPer)
        {
            _DRF = drf;
            _DRFContinent = drfContinent;
            _DRFPRDF=drfPIDF;
            _DRFPER = drfPer;
            _db = new EmcureCERIDBContext();
        }
        public void AddDRFDetails(DRFDetails entity)
        {
            _DRF.Add(entity);
            _DRF.Commit();
        }

        public int DeleteDRFTaskSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity)
        {
            try
            {
                _db.LoadStoredProc("USP_DeleteDRFTaskSubTaskDetails")
               .WithSqlParam("@projectTaskMappingID", entity.ProjectTaskMappingID)
               .WithSqlParam("@modifiedBy", entity.ModifiedBy)
                .WithSqlParam("@modifiedDate", entity.ModifiedDate)
                 .ExecuteStoredNonQuery(); 
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void DRFContinentCountryMapping(Tbl_DSR_NewProductContinentMapping entity)
        {
            _DRFContinent.Add(entity);
            _DRFContinent.Commit();
        }

        public void DRFPIDFMapping(Tbl_DRF_PIDF_Mapping entity)
        {
            _DRFPRDF.Add(entity);
            _DRFPRDF.Commit();
        }

        public void DRFRegistrationFeesMapping(Tbl_DRF_Percentage_Mapping entity)
        {
            _DRFPER.Add(entity);
            _DRFPER.Commit();
        }

        public ServiceResponseList<DRFDetails> GetAllDRF()
        {
            ServiceResponseList<DRFDetails> response = new ServiceResponseList<DRFDetails>() { Success = true };
            response.Results = _DRF.AllIncluding().ToList();
            if (null == response.Results)
            {
                response.Success = false;
                response.Messages.Add(new Message() { Detail = "No DRF found", Status = MessageType.Error });
            }
            return response;
        }

        public IList<PIDFDetailsNew> GetAttachedPIDFList(int DRFID, int CountryID)
        {
            IList<PIDFDetailsNew> result = new List<PIDFDetailsNew>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFAttachedPIDFDetails")
                    .WithSqlParam("@DRFId", DRFID)
                    .WithSqlParam("@CountryId", CountryID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<PIDFDetailsNew>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public object GetDRFTAskSubTaskList(string DRFID)
        {
            IList<DRFTaskSubTaskListOutput> result = new List<DRFTaskSubTaskListOutput>();
            //try
            //{
                _db.LoadStoredProc("USP_GetDRFTaskSubTaskList")
                .WithSqlParam("@DRFID", DRFID)
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<DRFTaskSubTaskListOutput>();
                });

                List<Object> result1 = (from x in result select (Object)new { projectTaskMappingID = x.ProjectTaskMappingID, parentID=x.ParentID,taskName = x.TaskName, empName = x.EmpName, duration = x.Duration, totalPercentage = "<div class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='"+ Convert.ToInt32(x.TotalPercentage) + "' aria-valuemin='0' aria-valuemax='100' style='width:"+ Convert.ToInt32(x.TotalPercentage) + "% '>"+ Convert.ToInt32(x.TotalPercentage) + "%</div></div>", taskStatus = x.TaskStatus,priority=x.Priority, startDate = x.StartDate.ToString("dd/MM/yyyy"), endDate = x.EndDate.ToString("dd/MM/yyy"), modifiedDate = x.ModifiedDate.ToString("dd/MM/yyyy") ,action = "<div class='btn-group'><a href='javascript:void(0)' class='btn btn-primary btn-sm ttip'  data-toggle='tooltip' title='EDIT' onclick=Edit('" + x.ProjectTaskMappingID+ "')><i class='fa fa-edit'></i></a>&nbsp;<a href='javascript:void(0)' class='btn btn-danger btn-sm ttip'  data-toggle='tooltip' title='DELETE' onclick=Delete('" + x.ProjectTaskMappingID + "')><i class='fa fa-trash-alt'></i></a></div> " }).ToList();

                return result1;
            //}
            //catch (Exception ex)
            //{
            //    return result1;
            //}
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



        //public DRFTaskSubTaskJSON GetDRFTAskSubTaskList(int DRFID)
        //{

        //    DataSet dsRequest = new DataSet();
        //    using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_db.Database.GetDbConnection().ConnectionString))
        //    {
        //        Microsoft.Data.SqlClient.SqlCommand sqlComm = new Microsoft.Data.SqlClient.SqlCommand("USP_GetDRFTaskSubTaskList", conn);
        //        sqlComm.Parameters.AddWithValue("@DRFID", DRFID);

        //        sqlComm.CommandType = CommandType.StoredProcedure;
        //        Microsoft.Data.SqlClient.SqlDataAdapter da = new Microsoft.Data.SqlClient.SqlDataAdapter();
        //        da.SelectCommand = sqlComm;
        //        da.Fill(dsRequest);
        //    }

        //   // DRFTaskSubTaskJSON result = new DRFTaskSubTaskJSON();
        //    var taskList = JsonConvert.SerializeObject( clsUtility.ConvertDataTable<DRFTaskList>(dsRequest.Tables[0]));
        //    var subTaskList = JsonConvert.SerializeObject(clsUtility.ConvertDataTable<DRFSubTaskList>(dsRequest.Tables[1]));

        //    var result = taskList.Concat(subTaskList);

        //    //foreach (var data in taskList)
        //    //{
        //    //    result.TaskID = data.TaskID;
        //    //    result.TaskName = data.TaskName;
        //    //    result.EMPID = data.EMPID;
        //    //    result.EmpName = data.EmpName;
        //    //    result.Duration = data.Duration;
        //    //    result.TotalPercentage = data.TotalPercentage;
        //    //}


        //    //result.drfSubTaskList = subTaskList;
        //    return result;
        //}

        public DRFDetails GetPIDF(int Id)
        {
            throw new NotImplementedException();
        }

        public int insertDRFSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity)
        {
            _db.LoadStoredProc("USP_insertDRFSubTaskDetails")
                 .WithSqlParam("@action", entity.Action)
                 .WithSqlParam("@drfID", entity.Drfid)
                 .WithSqlParam("@taskName", entity.TaskName)
                  .WithSqlParam("@parentID", entity.ParentID)
                 .WithSqlParam("@empID", entity.EmpID)
                .WithSqlParam("@startDate", entity.StartDate)
                .WithSqlParam("@endDate", entity.EndDate)
                 .WithSqlParam("@taskDuration", entity.TaskDuration)
                .WithSqlParam("@totalPercentage", entity.TotalPercentage)
                .WithSqlParam("@priorityID", entity.PriorityID)
                .WithSqlParam("@taskStatusID", entity.TaskStatusID)
                .WithSqlParam("@isActive", entity.IsActive)
                .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@modifiedBy", entity.ModifiedBy)
                .WithSqlParam("@modifiedDate", entity.ModifiedDate)
                .ExecuteStoredNonQuery();
            return 1;
        }

        public int insertDRFTaskDetails(Tbl_Master_ProjectTask_Mapping entity)
        {
                 _db.LoadStoredProc("USP_insertDRFTaskDetails")
                 .WithSqlParam("@action", entity.Action)
                 .WithSqlParam("@drfID", entity.Drfid)
                 .WithSqlParam("@taskName", entity.TaskName)
                  .WithSqlParam("@parentID", entity.ParentID)
                 .WithSqlParam("@empID", entity.EmpID)
                .WithSqlParam("@startDate", entity.StartDate)
                .WithSqlParam("@endDate", entity.EndDate)
                 .WithSqlParam("@taskDuration", entity.TaskDuration)
                .WithSqlParam("@totalPercentage", entity.TotalPercentage)
                .WithSqlParam("@priorityID", entity.PriorityID)
                .WithSqlParam("@taskStatusID", entity.TaskStatusID)
                .WithSqlParam("@isActive", entity.IsActive)
                .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@modifiedBy", entity.ModifiedBy)
                .WithSqlParam("@modifiedDate", entity.ModifiedDate)
                .ExecuteStoredNonQuery();
            return 1;
        }

        public int InsertTaskSubTaskDetails(TaskSubTaskInputs entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFTaskSubTask")
                .WithSqlParam("@taskOrder", entity.TaskOrder)
                 .WithSqlParam("@taskName", entity.TaskName)
                 .WithSqlParam("@parentID", entity.ParentID)
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
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertTransactionProjectTaskHistory(long projectMappingTaskID)
        {
            _db.LoadStoredProc("USP_Tbl_Transaction_ProjectTask_Gantt_Insert")
                 .WithSqlParam("@projectTaskMappingID", projectMappingTaskID)                 
                .ExecuteStoredNonQuery();
            return 1;
        }

        public void UpdateDRFDetails(DRFDetails entity)
        {
            _DRF.Edit(entity);
            _DRF.Commit();
        }

        public int UpdateDRFTaskSubTaskDetails(Tbl_Master_ProjectTask_Mapping entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFTaskSubTaskDetails")
              .WithSqlParam("@projectTaskMappingID", entity.ProjectTaskMappingID)
                 .WithSqlParam("@taskName", entity.TaskName)
                 .WithSqlParam("@empID", entity.EmpID)
                .WithSqlParam("@startDate", entity.StartDate)
                .WithSqlParam("@endDate", entity.EndDate)
                 .WithSqlParam("@taskDuration", entity.TaskDuration)
                .WithSqlParam("@totalPercentage", entity.TotalPercentage)
                .WithSqlParam("@priorityID", entity.PriorityID)
                .WithSqlParam("@taskStatusID", entity.TaskStatusID)
                .WithSqlParam("@modifiedBy", entity.ModifiedBy)
                .WithSqlParam("@modifiedDate", entity.ModifiedDate)
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
