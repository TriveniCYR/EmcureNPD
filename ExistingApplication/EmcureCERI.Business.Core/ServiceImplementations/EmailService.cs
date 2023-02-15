using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class EmailService : IEmailService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;
        public EmailService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }

        [Obsolete]
        public EmailDetailsModel EmailDetails(string NotificationName)
        {
            IList<EmailDetailsModel> result = new List<EmailDetailsModel>();
            try
            {
                _db.LoadStoredProc("USP_GetEMailDetails")
                    .WithSqlParam("@NotificationName", NotificationName)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<EmailDetailsModel>();
                 });
                if (result.Count != 0)
                    return result[0];
                else
                    return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
