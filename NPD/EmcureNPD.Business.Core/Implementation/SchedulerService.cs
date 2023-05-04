using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Helpers;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IMasterUserService _MasterUserService;
        private IRepository<MasterUser> _masterUser { get; set; }

        public SchedulerService(IUnitOfWork unitOfWork, Microsoft.Extensions.Configuration.IConfiguration _configuration
                , IMasterUserService MasterUserService
            )
        {
            _unitOfWork = unitOfWork;
            _masterUser = unitOfWork.GetRepository<MasterUser>();
            configuration = _configuration;
            _MasterUserService = MasterUserService;
        }

        public async Task<SendReminderModel> SendReminder()
        {
            string _logMessage = string.Empty;
            SendReminderModel model = new SendReminderModel();
            var dbresult = await _masterUser.GetDataSetBySP("GetUserForReminder", System.Data.CommandType.StoredProcedure, null);
            dynamic UserList = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    UserList = dbresult.Tables[0].DataTableToList<SendReminderModel>();
                    //foreach (var user in _BUObjects) {
                    string commasepretedUserList = string.Empty;
                    List<SendReminderModel> _UserLIst = UserList;
                    foreach(var u in _UserLIst)
                    {
                        commasepretedUserList += u.FullName + ",";
                    }
                    _logMessage += "Fetch list of user to send reminder { " + commasepretedUserList + "} \n";
                    SendReminderMail(UserList, ref _logMessage);
                    //}
                }
            }
          var AutoUpdatePIDFObj= await AutoUpdatePIDFStatus();
            if (AutoUpdatePIDFObj != null)
            {
                _logMessage += AutoUpdatePIDFObj.LogMessage;
            }
            model.LogMessage = _logMessage;
            return model;
        }
        public void SendReminderMail(List<SendReminderModel> sendReminderModel_list,ref string _logMessage)
        {
            foreach (var sendReminderModel in sendReminderModel_list)
            {
                try
                {
                    EmailHelper email = new EmailHelper();
                    string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\SendReminderMailTemplate.html");

                    strHtml = strHtml.Replace("{PIDFNumber}", sendReminderModel.PIDFNO);
                    strHtml = strHtml.Replace("{Email}", sendReminderModel.EmailAddress);
                    strHtml = strHtml.Replace("{DateTime}", ((DateTime)sendReminderModel.IPDApprovedDate).AddDays(15).ToString());
                    strHtml = strHtml.Replace("{FullName}", sendReminderModel.FullName);
                    strHtml = strHtml.Replace("{MoleculeName}", sendReminderModel.MoleculeName);
                    string str_subject = "PIDF : " + sendReminderModel.PIDFNO + " - Molecule Name : " + sendReminderModel.MoleculeName + " Commercial Detail pending";
                    email.SendMail(sendReminderModel.EmailAddress, string.Empty, str_subject, strHtml, _MasterUserService.GetSMTPConfiguration());
                    _logMessage += " Email Sent to { " + sendReminderModel.EmailAddress + " } on "+ DateTime.Now.ToString() + "" + "\n";
                }
                catch (Exception ex) 
                {
                    _logMessage += "Email failed to sent {" + sendReminderModel.EmailAddress +"} on "+ DateTime.Now.ToString()+ ex.InnerException.ToString() + "\n";
                }
            }
        }

        public async Task<SendReminderModel> AutoUpdatePIDFStatus()
        {
            var sentreminderModel = new SendReminderModel();
            string _logMessagePIDFreject = string.Empty;
            var dbresult = await _masterUser.GetDataSetBySP("AutoUpdatePIDFStatus", System.Data.CommandType.StoredProcedure, null);
            dynamic _BUObjects = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _BUObjects = dbresult.Tables[0].DataTableToList<AutoUpdatePIDFStatusModel>();

                    string commasepretedPIDFList = string.Empty;
                    List<AutoUpdatePIDFStatusModel> _pidfList = _BUObjects;
                    foreach (var p in _pidfList)
                    {
                        commasepretedPIDFList += p.PIDFNO + ",";
                    }
                    _logMessagePIDFreject += "Auto updated PIDF status for  { " + commasepretedPIDFList + "} \n";
                    foreach (var user in _BUObjects)
                    {
                        AutoUpdatePIDFStatusMail(user,ref _logMessagePIDFreject);
                    }
                }
            }
            sentreminderModel.LogMessage = _logMessagePIDFreject;
            return sentreminderModel;
        }               
        public void AutoUpdatePIDFStatusMail(AutoUpdatePIDFStatusModel autoUpdatePIDFStatusModel, ref string _logMessagePIDFreject)
        {
            try
            {
                EmailHelper email = new EmailHelper();
                string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\AutoUpdatePIDFStatusMailTemplate.html");

                strHtml = strHtml.Replace("{PIDFNumber}", autoUpdatePIDFStatusModel.PIDFNO);
                strHtml = strHtml.Replace("{PIDFStatus}", autoUpdatePIDFStatusModel.PIDFStatus);
                strHtml = strHtml.Replace("{MoleculeName}", autoUpdatePIDFStatusModel.MoleculeName);
                strHtml = strHtml.Replace("{FullName}", autoUpdatePIDFStatusModel.FullName);
                string str_subject = " PIDF : " + autoUpdatePIDFStatusModel.PIDFNO + " - Molecule Name : " + autoUpdatePIDFStatusModel.MoleculeName + " - Auto Updated status : " + autoUpdatePIDFStatusModel.PIDFStatus;
                email.SendMail(autoUpdatePIDFStatusModel.EmailAddress, string.Empty, str_subject, strHtml, _MasterUserService.GetSMTPConfiguration());
                _logMessagePIDFreject += " Sent email to user { " + autoUpdatePIDFStatusModel.EmailAddress + " } for reject PIDF number { " + autoUpdatePIDFStatusModel.PIDFNO + "} on " + DateTime.Now.ToString() + "" + "\n";
            }
            catch (Exception ex)
            {
                _logMessagePIDFreject += "PIDF No.{" + autoUpdatePIDFStatusModel.PIDFNO + "} Reject Email failed to sent {" + autoUpdatePIDFStatusModel.EmailAddress + "} on " + DateTime.Now.ToString() + ex.InnerException.ToString() + "\n";
            }
        }
    }
}