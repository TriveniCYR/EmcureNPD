using System.Data.SqlClient;
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
using SqlParameter = System.Data.SqlClient.SqlParameter;
using EmcureCERI.Business.Contract.ServiceContracts;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class GanttSummaryService : IGanttSummaryService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public GanttSummaryService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public IList<SummaryGanttDataModel> GetAllProjectSummary(string strAction)
        {
            IList<SummaryGanttDataModel> result = new List<SummaryGanttDataModel>();
            try
            {
                _db.LoadStoredProc("USP_GanttSummary")
                    .WithSqlParam("@action", strAction)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<SummaryGanttDataModel>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
