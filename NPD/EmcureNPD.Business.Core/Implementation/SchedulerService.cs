using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Helpers;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Models;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation {

    public class SchedulerService : ISchedulerService {

        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IMasterUserService _MasterUserService;
        private IRepository<MasterUser> _masterUser { get; set; }
        public SchedulerService(IUnitOfWork unitOfWork, Microsoft.Extensions.Configuration.IConfiguration _configuration
                , IMasterUserService MasterUserService
            ) {
            _unitOfWork = unitOfWork;
            _masterUser = unitOfWork.GetRepository<MasterUser>();
            configuration = _configuration;
            _MasterUserService = MasterUserService;
        }

        public async Task<SendReminderModel> SendReminder() {
            
            var dbresult = await _masterUser.GetDataSetBySP("GetUserForReminder", System.Data.CommandType.StoredProcedure, null);
            dynamic _BUObjects = new ExpandoObject();
            if (dbresult != null) {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0) {
                    _BUObjects = dbresult.Tables[0].DataTableToList<SendReminderModel>();
                    //foreach (var user in _BUObjects) {
                        SendReminderMail(_BUObjects[1]);
                    //}
                }
            }
            return _BUObjects;
        }

        public async Task<SendReminderModel> AutoUpdatePIDFStatus() {
            var dbresult = await _masterUser.GetDataSetBySP("AutoUpdatePIDFStatus", System.Data.CommandType.StoredProcedure, null);
            dynamic _BUObjects = new ExpandoObject();
            if (dbresult != null) {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0) {
                    _BUObjects = dbresult.Tables[0].DataTableToList<SendReminderModel>();
                    foreach (var user in _BUObjects) {
                        AutoUpdatePIDFStatusMail(user);
                    }
                }
            }
            return _BUObjects;
        }

        public void SendReminderMail(SendReminderModel sendReminderModel) {
            EmailHelper email = new EmailHelper();
            string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\SendReminderMailTemplate.html");
           
            strHtml = strHtml.Replace("{PIDFNumber}", sendReminderModel.PIDFNO);
            strHtml = strHtml.Replace("{Email}", sendReminderModel.EmailAddress);
            strHtml = strHtml.Replace("{DateTime}", sendReminderModel.RejectedDateTime.ToString());
            strHtml = strHtml.Replace("{FullName}", sendReminderModel.FullName);
            strHtml = strHtml.Replace("{MoleculeName}", sendReminderModel.MoleculeName);
            email.SendMail(sendReminderModel.EmailAddress, string.Empty, "PIDF : {"+ sendReminderModel.PIDFNO+ "} - Molecule Name : {"+ sendReminderModel.MoleculeName+"} Commercial Detail pending", strHtml,_MasterUserService.GetSMTPConfiguration());
        }

        public void AutoUpdatePIDFStatusMail(AutoUpdatePIDFStatusModel autoUpdatePIDFStatusModel) {
            EmailHelper email = new EmailHelper();
            string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\AutoUpdatePIDFStatusMailTemplate.html");

            strHtml = strHtml.Replace("{PIDFNumber}", autoUpdatePIDFStatusModel.PIDFNO);
            strHtml = strHtml.Replace("{PIDFStatus}", autoUpdatePIDFStatusModel.PIDFStatus);
            strHtml = strHtml.Replace("{MoleculeName}", autoUpdatePIDFStatusModel.MoleculeName);
            strHtml = strHtml.Replace("{FullName}", autoUpdatePIDFStatusModel.FullName);
            email.SendMail(autoUpdatePIDFStatusModel.EmailAddress, string.Empty, " PIDF : {"+ autoUpdatePIDFStatusModel.PIDFNO+"} - Molecule Name : {"+ autoUpdatePIDFStatusModel.MoleculeName+"} - Auto Updated status : {" + autoUpdatePIDFStatusModel.PIDFStatus+"}", strHtml, _MasterUserService.GetSMTPConfiguration());
        }
    }
}
