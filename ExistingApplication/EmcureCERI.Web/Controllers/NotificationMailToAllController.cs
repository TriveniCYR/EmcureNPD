using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class NotificationMailToAllController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        private readonly EmcureCERIDBContext _db;
        IHostingEnvironment _env;
        public NotificationMailToAllController(EmcureCERIDBContext context,IConfiguration config, IHostingEnvironment env, IEmailService emailService, ISMTPService sMTPService )
        {            
            _config = config;
            this._env = env;
            _emailService = emailService;
            _sMTPService = sMTPService;          
            _db = new EmcureCERIDBContext();
        }
        //[Authorize]
        [HttpPost]
        [Obsolete]
        [ActionName("FirstNotificationMailToAll")]
        public IActionResult FirstNotificationMailToAll()
        {
            try
            {
                MailDetails();
                return Json(new { data = "success", message = "Mail Sent to All User" }, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                return Json(new { data = "fail", message = "Mail Sent Faild" }, new JsonSerializerSettings());
            }
        }

        [Obsolete]
        private void MailDetails()
        {
            try
            {

                //send email notification code added by yogesh balapure on date 02/04/2021
                //get smtp details 
                SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                //EmailDetailsModel emailDetailsModel = new EmailDetailsModel();
                SMTPDetailsVM sMTPDetailsVM = new SMTPDetailsVM();
                EmailDetailsVM emailDetailsVM = new EmailDetailsVM();

                if (sMTPDetailsModel != null)
                {
                    sMTPDetailsVM.AliasName = sMTPDetailsModel.AliasName;
                    sMTPDetailsVM.HostName = sMTPDetailsModel.HostName;
                    sMTPDetailsVM.FromMail = sMTPDetailsModel.FromMail;
                    sMTPDetailsVM.FromPassword = sMTPDetailsModel.FromPassword;
                    sMTPDetailsVM.IsEnableSSL = sMTPDetailsModel.IsEnableSSL;
                    sMTPDetailsVM.PortNumber = sMTPDetailsModel.PortNumber;
                    sMTPDetailsVM.IsMailStatus = sMTPDetailsModel.IsMailStatus;
                    sMTPDetailsVM.IsDefaultCredentials = sMTPDetailsModel.IsDefaultCredentials;
                    sMTPDetailsVM.IsWithoutPassword = sMTPDetailsModel.IsWithoutPassword;
                }

                //Collect usermail list               
                var bccList = (from p in _db.PrescriberDetails
                               join q in _db.AspNetUsers on p.AspNetUserId equals q.UserId
                               where q.IsEnabled == true
                               select new { p.AspNetUserId, p.FirstName, p.LastName, q.Email }).ToList();

                if (bccList != null && bccList.Count > 0)
                {
                    foreach (var ddata in bccList)
                    {
                        if (!string.IsNullOrEmpty(ddata.Email))
                        {
                            //string strEmailMessage = String.Concat(ddata.FirstName, " ", ddata.LastName) + "</br>" +
                            //                        "Thank you for using the Emcure Projects portal." + "</br></br>" +
                            //                        "UserName: " + ddata.Email + "</br>" +
                            //                        "Default Password : Emcure@123" + "</br></br>" +
                            //                        "Should you wish to change the system generated password, once you login, you can change the same, by visiting your Profile section." + "</br>" +
                            //                        "or After Login Visit";
                            string strEmailMessage = "User" + "</br>" + "PFA" + "</br>" +
                                                    "Thank you for using the Emcure Projects portal." + "</br></br>" +
                                                    "Here we have attached the process documentation of the Emcure Project Portal for your reference." + "</br?" +
                                                    "In case of any query please let me know @ Rahula.Patil @emcure.co.in";
                            List<string> testBCC = new List<string>();
                            List<string> testCC = new List<string>();
                            testBCC.Add(ddata.Email.Trim());
                            testCC.Add("Rahula.patil@emcure.co.in");
                            emailDetailsVM.ToMail =  ddata.Email.Trim(); //"Rahula.patil@emcure.co.in";//
                            //emailDetailsVM.ToMail = "Rahula.patil@emcure.co.in";//

                            emailDetailsVM.CCMail = testCC;
                            //emailDetailsVM.BCCMail = testBCC;
                            //emailDetailsVM.Subject = "User Name and Default Password Notification";
                            emailDetailsVM.Subject = "User manual for Emcure Project Management";

                            clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                            string tempurl = _config.GetSection("ApplicationURL:ChangePasswordUrlLink").Value;
                            emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                            emailDetailsVM.DispalyName = "Emcure Project Management";

                            if (sMTPDetailsVM != null && emailDetailsVM != null)
                            {
                                EmailHelper emailHelper = new EmailHelper();
                                //clsPath.wwwRootPath = _env.ContentRootPath;
                                bool tempbln= emailHelper.SendMail1(sMTPDetailsVM, emailDetailsVM, _env.ContentRootPath,true);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}
