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
using System.Data;
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
        public async Task<string> SendRemiderForPIDFSubmitted(DataSet dbresult)
        {
            string _logMessage = string.Empty;
            var _objPIDFEntity = new List<PIDFEntity>();
            var _objPidfProductStregthEntity = new List<PidfProductStregthEntity>();
            var _objIMSDataEntity = new List<IMSDataEntity>();
            var _objMasterUserEntity = new List<MasterUserEntity>();
            if (dbresult != null)
            {
                // await SendRemiderForPIDFSubmitted();
                if (dbresult.Tables[1] != null && dbresult.Tables[1].Rows.Count > 0)
                {
                    _objPIDFEntity = dbresult.Tables[1].DataTableToList<PIDFEntity>();
                }
                if (dbresult.Tables[2] != null && dbresult.Tables[2].Rows.Count > 0)
                {
                    _objPidfProductStregthEntity = dbresult.Tables[2].DataTableToList<PidfProductStregthEntity>();
                }
                if (dbresult.Tables[3] != null && dbresult.Tables[3].Rows.Count > 0)
                {
                    _objIMSDataEntity = dbresult.Tables[3].DataTableToList<IMSDataEntity>();
                }
                if (dbresult.Tables[4] != null && dbresult.Tables[4].Rows.Count > 0)
                {
                    _objMasterUserEntity = dbresult.Tables[4].DataTableToList<MasterUserEntity>();
                }

                //foreach (var user in _BUObjects) {
                string commasepretedUserList = string.Empty;
                List<string> _UserLIst = _objMasterUserEntity.Select(x => x.FullName).ToList();
                commasepretedUserList = string.Join(',', _UserLIst);
                 _logMessage += "Fetch list of user to send reminder for PIDF Submitted { " + commasepretedUserList + "} \n";
                 //SendReminderMailForPIDFSubmitted(UserList, ref _logMessage);
                foreach(var User in _objMasterUserEntity)
                {
                    foreach(var pidf in _objPIDFEntity)
                    {
                        try
                        {
                            EmailHelper email = new EmailHelper();
                            string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\SendReminderMailTemplate_PIDFSubmitted.html");

                            strHtml = strHtml.Replace("{FullName}", User.FullName);
                            strHtml = strHtml.Replace("{PIDFNumber}", pidf.PIDFNO);
                            strHtml = strHtml.Replace("{MoleculeName}", pidf.MoleculeName);
                            strHtml = strHtml.Replace("{BrandName}", pidf.MoleculeName);
                            strHtml = strHtml.Replace("{BusinessUnit_Name}", pidf.MoleculeName);
                            strHtml = strHtml.Replace("{DosageFormName}", pidf.MoleculeName);



                            string strStrengthData = "";
                            foreach (var strengthdata in _objPidfProductStregthEntity.FindAll(x => x.Pidfid == pidf.PIDFID).ToList())
                            {
                                strStrengthData += "" + strengthdata.Strength;
                                strStrengthData += ",";
                            }

                            string strIMSData = "";
                           foreach(var imsdata in _objIMSDataEntity.FindAll(x=>x.Pidfid == pidf.PIDFID).ToList())
                            {
                                strIMSData += "IMS Value :" + imsdata.Imsvalue;
                                strIMSData += ", IMS Volume :" + imsdata.Imsvolume;
                                strIMSData += "<br>";
                            }
                            strHtml = strHtml.Replace("{StrengthList}", strStrengthData);
                            strHtml = strHtml.Replace("{IMSDatahList}", strIMSData);

                            Console.WriteLine(strHtml);
                            string str_subject = "PIDF : " + pidf.PIDFNO + " - Molecule Name : " + pidf.MoleculeName + " has been Submitted";
                            email.SendMail(User.EmailAddress, string.Empty, str_subject, strHtml, _MasterUserService.GetSMTPConfiguration());
                            _logMessage += " Email Sent to { " + User.EmailAddress + " } on " + DateTime.Now.ToString() + " for PIDFNO. :" + pidf.PIDFNO + "\n";
                        }
                        catch (Exception ex)
                        {
                            _logMessage += "Email failed to sent {" + User.EmailAddress + "} on " + DateTime.Now.ToString() + ex.InnerException.ToString() + "\n";
                        }
                    }
                }
                
            }
            return _logMessage;
        }
        public async Task<SendReminderModel> SendReminder()
        {
            string _logMessage = string.Empty;
            SendReminderModel model = new SendReminderModel();
            string _logMessage_PIDFSubmittted= "\n";
            var dbresult = await _masterUser.GetDataSetBySP("GetUserForReminder", System.Data.CommandType.StoredProcedure, null);
            dynamic UserList = new ExpandoObject();
            if (dbresult != null)
            {
                _logMessage_PIDFSubmittted = await SendRemiderForPIDFSubmitted(dbresult);
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
          //var AutoUpdatePIDFObj= await AutoUpdatePIDFStatus();
          //  if (AutoUpdatePIDFObj != null)
          //  {
          //      _logMessage += AutoUpdatePIDFObj.LogMessage;
          //  }
            model.LogMessage = _logMessage;
            model.LogMessage += _logMessage_PIDFSubmittted;
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
		public async Task<EmailNotificationEntity> SendNotification()
		{
			string _logMessage = string.Empty;
			EmailNotificationEntity model = new EmailNotificationEntity();
			string _logMessage_PIDFSubmittted = "\n";
			var dbresult = await _masterUser.GetDataSetBySP("GetEmailNotification", System.Data.CommandType.StoredProcedure, null);
			dynamic UserList = new ExpandoObject();
			if (dbresult != null)
			{
				
				if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
				{
					UserList = dbresult.Tables[0].DataTableToList<EmailNotificationEntity>();
					//foreach (var user in _BUObjects) {
					string commasepretedUserList = string.Empty;
					List<EmailNotificationEntity> _UserLIst = UserList;
					foreach (var u in _UserLIst)
					{
						commasepretedUserList += u.CreatedByName + ",";
					}
					_logMessage += "Fetch list of user to send notification { " + commasepretedUserList + "} \n";
					SendNotificationMail(UserList, ref _logMessage);
					//}
				}
			}
			model.LogMessage = _logMessage;
			model.LogMessage += _logMessage_PIDFSubmittted;
			return model;
		}
		public void SendNotificationMail(List<EmailNotificationEntity> sendNotificationModel_list, ref string _logMessage)
		{
			foreach (var sendNotificationModel in sendNotificationModel_list)
			{
				try
				{
					EmailHelper email = new EmailHelper();
					string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\EmailNotification.html");
                   // sendNotificationModel.EmailAddress = "pandey.kp.kamal@gmail.com";
					strHtml = strHtml.Replace("{PIDFNo}", sendNotificationModel.PidfNo);
					strHtml = strHtml.Replace("{DateTime}", sendNotificationModel.CreatedDate.ToString());
					strHtml = strHtml.Replace("{User}", sendNotificationModel.SendToName);
					strHtml = strHtml.Replace("{PIDFStatus}", sendNotificationModel.PIDFStatus);
					strHtml = strHtml.Replace("{UpdatedByUser}", sendNotificationModel.CreatedByName);
					strHtml = strHtml.Replace("{Url}", "https://www.emcure.com/");
					string str_subject = "PIDF : " + sendNotificationModel.PidfNo + " Updated";
					//email.SendMail(sendNotificationModel.EmailAddress, string.Empty, str_subject, strHtml, _MasterUserService.GetSMTPConfiguration());
					_logMessage += " Email Sent to { " + sendNotificationModel.EmailAddress + " } on " + DateTime.Now.ToString() + "" + "\n";
				}
				catch (Exception ex)
				{
					_logMessage += "Email failed to sent {" + sendNotificationModel.EmailAddress + "} on " + DateTime.Now.ToString() + ex.InnerException.ToString() + "\n";
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