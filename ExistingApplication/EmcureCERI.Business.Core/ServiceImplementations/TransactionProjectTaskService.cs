//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class TransactionProjectTaskService : ITransactionsProjectTask
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public TransactionProjectTaskService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public IList<Tbl_Transaction_ProjectTask> GetAllProjectTaskTransaction(int ProjectID)
        {
            IList<Tbl_Transaction_ProjectTask> result = new List<Tbl_Transaction_ProjectTask>();
            try
            {
                _db.LoadStoredProc("USP_TblTransactionProjecttask_SelectAll")
                    //.WithSqlParam("@taskID", TaskID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Transaction_ProjectTask>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int InsertTransactionProjectTask(Tbl_Transaction_ProjectTask tbl_Transaction_ProjectTask)
        {
            try
            {
                object testoutput = _db.LoadStoredProc("USP_TblTransactionProjecttask_Insert")
                .WithSqlParam("@projectTaskMappingID", tbl_Transaction_ProjectTask.ProjectTaskMappingID)
                .WithSqlParam("@drfid", tbl_Transaction_ProjectTask.Drfid)
                .WithSqlParam("@pidfid", tbl_Transaction_ProjectTask.Pidfid)
                .WithSqlParam("@countryID", tbl_Transaction_ProjectTask.CountryID)
                .WithSqlParam("@taskID", tbl_Transaction_ProjectTask.TaskID)
                .WithSqlParam("@subTaskID", tbl_Transaction_ProjectTask.SubTaskID)
                .WithSqlParam("@startDate", tbl_Transaction_ProjectTask.StartDate)

                .WithSqlParam("@endDate", tbl_Transaction_ProjectTask.EndDate)
                .WithSqlParam("@taskStatusID", tbl_Transaction_ProjectTask.TaskStatusID)
                .WithSqlParam("@taskStatus", tbl_Transaction_ProjectTask.TaskStatus)
                .WithSqlParam("@taskDuration", tbl_Transaction_ProjectTask.TaskDuration)
                .WithSqlParam("@totalPercentage", tbl_Transaction_ProjectTask.TotalPercentage)
                .WithSqlParam("@projectOwnerID", tbl_Transaction_ProjectTask.ProjectOwnerID)
                .WithSqlParam("@empID", tbl_Transaction_ProjectTask.EmpID)

                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", tbl_Transaction_ProjectTask.CreatedBy)
                .WithSqlParam("@createdDate", System.DateTime.Now)
                .WithSqlParam("@modifiedby", tbl_Transaction_ProjectTask.ModifiedBy)
                .WithSqlParam("@modifiedDate", System.DateTime.Now)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int UpdateTransactionProjectTask(Tbl_Transaction_ProjectTask tbl_Transaction_ProjectTask)
        {
            try
            {
                object testoutput = _db.LoadStoredProc("USP_TblTransactionProjecttask_Update")
                .WithSqlParam("@projectTransactionID", tbl_Transaction_ProjectTask.ProjectTransactionID)
                .WithSqlParam("@projectTaskMappingID", tbl_Transaction_ProjectTask.ProjectTaskMappingID)
                .WithSqlParam("@drfid", tbl_Transaction_ProjectTask.Drfid)
                .WithSqlParam("@pidfid", tbl_Transaction_ProjectTask.Pidfid)
                .WithSqlParam("@countryID", tbl_Transaction_ProjectTask.CountryID)
                .WithSqlParam("@taskID", tbl_Transaction_ProjectTask.TaskID)
                .WithSqlParam("@subTaskID", tbl_Transaction_ProjectTask.SubTaskID)
                .WithSqlParam("@startDate", tbl_Transaction_ProjectTask.StartDate)

                .WithSqlParam("@endDate", tbl_Transaction_ProjectTask.EndDate)
                .WithSqlParam("@taskStatusID", tbl_Transaction_ProjectTask.TaskStatusID)
                .WithSqlParam("@taskStatus", tbl_Transaction_ProjectTask.TaskStatus)
                .WithSqlParam("@taskDuration", tbl_Transaction_ProjectTask.TaskDuration)
                .WithSqlParam("@totalPercentage", tbl_Transaction_ProjectTask.TotalPercentage)
                .WithSqlParam("@projectOwnerID", tbl_Transaction_ProjectTask.ProjectOwnerID)
                .WithSqlParam("@empID", tbl_Transaction_ProjectTask.EmpID)

                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", tbl_Transaction_ProjectTask.CreatedBy)
                .WithSqlParam("@createdDate", System.DateTime.Now)
                .WithSqlParam("@modifiedby", tbl_Transaction_ProjectTask.ModifiedBy)
                .WithSqlParam("@modifiedDate", System.DateTime.Now)
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
