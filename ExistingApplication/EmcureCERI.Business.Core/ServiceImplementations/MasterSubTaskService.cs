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
    public class MasterSubTaskService : IMasterSubTaskService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public MasterSubTaskService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public IList<Tbl_Master_SubTask> GetAllSubTask(int TaskID)
        {
            IList<Tbl_Master_SubTask> result = new List<Tbl_Master_SubTask>();
            try
            {
                _db.LoadStoredProc("USP_TblMasterSubtask_SelectAll")
                    .WithSqlParam("@taskID", TaskID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_SubTask>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Task> GetAllTask()
        {
            IList<Tbl_Master_Task> result = new List<Tbl_Master_Task>();
            try
            {
                _db.LoadStoredProc("USP_TblMasterTask_SelectAll")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Task>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int InsertTaskDetails(Tbl_Master_SubTask tbl_Master_SubTask)
        {
            try
            {
                object testoutput = _db.LoadStoredProc("USP_TblMasterSubtask_Insert")
                .WithSqlParam("@taskID", tbl_Master_SubTask.TaskID)
                .WithSqlParam("@subTaskName", tbl_Master_SubTask.SubTaskName)
                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", tbl_Master_SubTask.CreatedBy)
                .WithSqlParam("@createdDate", System.DateTime.Now)
                .WithSqlParam("@modifiedby", tbl_Master_SubTask.ModifiedBy)
                .WithSqlParam("@modifiedDate", System.DateTime.Now)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int UpdateTaskDetails(Tbl_Master_SubTask tbl_Master_SubTask)
        {
            try
            {
                object testoutput = _db.LoadStoredProc("USP_TblMasterSubtask_Update")
                .WithSqlParam("@subTaskID", tbl_Master_SubTask.SubTaskID)
                .WithSqlParam("@taskID", tbl_Master_SubTask.TaskID)
                .WithSqlParam("@subTaskName", tbl_Master_SubTask.SubTaskName)
                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", tbl_Master_SubTask.CreatedBy)
                .WithSqlParam("@createdDate", System.DateTime.Now)
                .WithSqlParam("@modifiedby", tbl_Master_SubTask.ModifiedBy)
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
