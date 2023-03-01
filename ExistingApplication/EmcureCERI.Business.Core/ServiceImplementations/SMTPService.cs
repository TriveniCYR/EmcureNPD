using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class SMTPService : ISMTPService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        public SMTPService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public SMTPDetailsModel SMTPDetails()
        {
            IList<SMTPDetailsModel> result = new List<SMTPDetailsModel>();
            try
            {
                _db.LoadStoredProc("USP_GetSMTPDetails")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<SMTPDetailsModel>();
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
