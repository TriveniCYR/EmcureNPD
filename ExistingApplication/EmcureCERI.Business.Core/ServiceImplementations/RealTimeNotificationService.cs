using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SqlParameter = System.Data.SqlClient.SqlParameter;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class RealTimeNotificationService : IRealTimeNotificationService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;
        [Obsolete]
        public RealTimeNotificationService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public IList<RealtimeNotificationModel> GetRealTimeNotificationData()
        {
            IList<RealtimeNotificationModel> result = new List<RealtimeNotificationModel>();
            try
            {
                _db.LoadStoredProc("USP_GetRealTimeNotificationData")
                      .WithSqlParam("@Action", "ALL")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<RealtimeNotificationModel>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<RealtimeNotificationModel> GetTop5RealTimeNotificationData()
        {
            IList<RealtimeNotificationModel> result = new List<RealtimeNotificationModel>();
            try
            {
                _db.LoadStoredProc("USP_GetRealTimeNotificationData")
                    .WithSqlParam("@Action", "TOP_5")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<RealtimeNotificationModel>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public IList<RealtimeNotificationModel> GetTop5upRealTimeNotificationData()
        {
            IList<RealtimeNotificationModel> result = new List<RealtimeNotificationModel>();
            try
            {
                _db.LoadStoredProc("USP_GetRealTimeNotificationData")
                      .WithSqlParam("@Action", "UP")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<RealtimeNotificationModel>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int UpdateViewNotification(int InitializationID)
        {
            try
            {
                _db.LoadStoredProc("ITUSP_Notificatiion_Update")
                .WithSqlParam("@InitializationID", InitializationID)

              .ExecuteStoredNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateAllNotification()
        {
            try
            {
                _db.LoadStoredProc("ITUSP_AllNotificatiion_Flag_Update")

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
