using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Hubs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using EmcureCERI.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EmcureCERI.Web.Controllers
{
    [Authorize]
    public class PIDFApprovalController : Controller
    {
        private readonly IConfiguration _config;
        IHostingEnvironment _env;
        private readonly IPidfServiceNew _pidfServiceNew;
        private readonly EmcureCERIDBContext _db;
        private readonly IDRFFinal _dRFFinal;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        public PIDFApprovalController(IPidfServiceNew pidfServiceNew, IDRFFinal dRFFinal, IConfiguration config, IHostingEnvironment env, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            _config = config;
            this._pidfServiceNew = pidfServiceNew;
            _dRFFinal = dRFFinal;
            _db = new EmcureCERIDBContext();
            this._env = env;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }


		[Authorize(Roles = "Prescriber, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Prescriber, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development")]
        public IActionResult PIDFApproval(int id = 0)
        {
            return View(id);
        }

        [Authorize(Roles = "Prescriber, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development")]
        [HttpPost]
        public JsonResult GetPIDFApprovalList(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels)
        {
            getPIDFApprovalListRequestModels.userID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            IList<Tbl_PIDF_Header> PIDFApprovalList = new List<Tbl_PIDF_Header>();
            PIDFApprovalList = _pidfServiceNew.GetAllApprovalPIDF(getPIDFApprovalListRequestModels);
            return Json(new { data = PIDFApprovalList }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult GetPIDFDetailedApprovalList(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels)
        {
            getPIDFApprovalListRequestModels.userID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            //IList<Tbl_PIDF_CountryDetailsNew> tbl_PIDF_CountryDetails = new List<Tbl_PIDF_CountryDetailsNew>();
            //tbl_PIDF_CountryDetails = _pidfServiceNew.GetAllDetailedApprovalPIDF(getPIDFApprovalListRequestModels);
            IList<PidfCountryDetailsNewModel> tbl_PIDF_CountryDetails = new List<PidfCountryDetailsNewModel>();
            tbl_PIDF_CountryDetails = _pidfServiceNew.GetAllDetailedApprovalPIDF(getPIDFApprovalListRequestModels);

            IList<Tbl_PIDF_Header> tbl_PIDF_Header = new List<Tbl_PIDF_Header>();
            tbl_PIDF_Header = _pidfServiceNew.GetAllApprovalPIDF(getPIDFApprovalListRequestModels);

            List<FileModel> uploadFileList = new List<FileModel>();
            var fileresult = (from x in _db.Tbl_PIDF_UploadFileDetails where x.PIDFID == getPIDFApprovalListRequestModels.PidfID select new { x.FilePath, x.FileName }).ToList();
            if (fileresult.Count > 0 && fileresult != null)
            {
                foreach (var ddata in fileresult)
                {
                    uploadFileList.Add(new FileModel { SaveFilePath = ddata.FilePath, SaveFileName = ddata.FileName });
                }
            }
            else
            {
                uploadFileList = null;
            }

            var Data = new
            {
                PIDFHeader = tbl_PIDF_Header,
                PIDFDetail = tbl_PIDF_CountryDetails,
                uploadfilelist = uploadFileList
            };

            return Json(new { data = Data }, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> UpdateApprovalPIDFStatusAsync(UpdateApprovalPIDFStatusRequestModels updateApprovalPIDFStatusRequestModels)
        {
            string strTempMessage = HttpContext.Session.GetString("CurrentUserRole") == "Prescriber" ? "Administrator PIDF Approved Successfully"
                                        : HttpContext.Session.GetString("CurrentUserRole") == "HOD Of Dossier" ? "HOD Of Dossier Approved Scuccessfully"
                                        : HttpContext.Session.GetString("CurrentUserRole") == "Finance Manager" ? "Finance Manager Approved Successfully"
                                        : HttpContext.Session.GetString("CurrentUserRole") == "President – Commercial" ? "Commercial Approved Successfully"
                                        : HttpContext.Session.GetString("CurrentUserRole") == "President – Research & Development" ? "Final Approved Successfully"
                                        : "PIDF Approved Successfully";// "";// HttpContext.Session.GetString("CurrentUserRole");
            int intPIDFID = Convert.ToInt16(updateApprovalPIDFStatusRequestModels.pidfID);
            updateApprovalPIDFStatusRequestModels.userID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            int data = _pidfServiceNew.UpdateApprovalPIDFStatus(updateApprovalPIDFStatusRequestModels);


            if (data == 1)
            {
                //CHECK PIDF STATUS IS FINAL APPROVED
                var pidfresult = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == Convert.ToInt64(intPIDFID) select new { x.PidfStatusID, x.PidfStatus,x.ProjectorProductName,x.WorkflowID, x.WorkflowName }).FirstOrDefault();
                if(pidfresult.PidfStatusID == 16 && pidfresult.PidfStatus == "Final Approved")
                {
                    //Add PIDF PROJECT TASK
                    string strTaskType = "";
                    if( pidfresult.WorkflowID == 1)//"Normal"
                    {
                        strTaskType = "PIDF";
                    }
                    else if (pidfresult.WorkflowID == 2)// "Brazil"
                    {
                        strTaskType = "BRAZIL";
                    }
                    else if (pidfresult.WorkflowID == 3)// "New Development"
                    {
                        strTaskType = "New Development";
                    }
                    else if (pidfresult.WorkflowID == 4)// "Site Transfer"
                    {
                        strTaskType = "Site Transfer";
                    }
                    else if (pidfresult.WorkflowID == 5)// "Supplimentary"
                    {
                        strTaskType = "Supplimentary";
                    }

                    IList<DRFTaskSubTaskOutputNew> drFTaskSubTaskOutputs = new List<DRFTaskSubTaskOutputNew>();
                    drFTaskSubTaskOutputs = _pidfServiceNew.GetMixedTaskSubTaskListForPIDFInsertion(strTaskType);

                    for (int i = 0; i < drFTaskSubTaskOutputs.Count; i++)
                    {
                        TaskSubTaskInputs taskSubTaskInputs = new TaskSubTaskInputs();

                        taskSubTaskInputs.TaskOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                        taskSubTaskInputs.TaskName = drFTaskSubTaskOutputs[i].TaskName;
                        //taskSubTaskInputs.SortOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                        var tempList = (from TMST in _db.Tbl_Master_PIDF_SubTask
                                        join TMT in _db.Tbl_Master_PIDF_Task on TMST.TaskID equals TMT.TaskID
                                        where TMST.SubTaskName == drFTaskSubTaskOutputs[i].TaskName && TMST.SubTaskID == drFTaskSubTaskOutputs[i].SubTaskID && TMST.Action == strTaskType
                                        select new { TMST.SubTaskName,TMT.TaskName}).ToList();

                        if (tempList.Count > 0)
                        {
                            var tempID = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                                          where TMPM.TaskName == tempList[0].TaskName && TMPM.Drfid == Convert.ToInt32(intPIDFID) && TMPM.Action == "PIDF"
                                          select new { TMPM.ProjectTaskMappingID }).ToList();

                            taskSubTaskInputs.ParentID = Convert.ToInt32(tempID[0].ProjectTaskMappingID);
                            taskSubTaskInputs.TaskDuration = drFTaskSubTaskOutputs[i].TaskDuration.Value;
                            taskSubTaskInputs.EndDate = DateTime.Today.AddDays(drFTaskSubTaskOutputs[i].TaskDuration.Value);
                            taskSubTaskInputs.Type = "task";
                        }
                        else
                        {
                            taskSubTaskInputs.ParentID = 0;
                            taskSubTaskInputs.TaskDuration = 1;
                            taskSubTaskInputs.EndDate = DateTime.Today.AddDays(1);
                            //taskSubTaskInputs.Type = "project";
                            //FOR PROJECT OR TASK
                            //CHECK ANY CHILD SUBTASK IS FOUND OR NOT
                            var checkchild = (from TMST in _db.Tbl_Master_PIDF_SubTask
                                              join TMT in _db.Tbl_Master_PIDF_Task on TMST.TaskID equals TMT.TaskID
                                              where TMT.TaskName == drFTaskSubTaskOutputs[i].TaskName && TMST.Action == strTaskType
                                              select new { TMST.SubTaskName, TMT.TaskName }).ToList();
                            if(checkchild.Count>0)
                            {
                                taskSubTaskInputs.Type = "project";
                            }
                            else
                            {
                                taskSubTaskInputs.Type = "task";
                            }
                        }
                        
                        taskSubTaskInputs.Action = "PIDF";
                        taskSubTaskInputs.DRFID = Convert.ToInt32(intPIDFID);
                        taskSubTaskInputs.StartDate = DateTime.Today;                        
                        taskSubTaskInputs.PriorityID = 1;// Id of Normal Priority.
                        taskSubTaskInputs.Priority = "Normal";
                        taskSubTaskInputs.TaskStatusID = 8;//Initial Status
                        taskSubTaskInputs.TaskStatus = "Initial";                       
                        taskSubTaskInputs.TotalPercentage = 0;
                        taskSubTaskInputs.EmpID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));                        
                        taskSubTaskInputs.IsActive = true;
                        taskSubTaskInputs.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        taskSubTaskInputs.CreatedDate = DateTime.Today;
                        taskSubTaskInputs.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        taskSubTaskInputs.ModifiedDate = DateTime.Today;

                        int dataInserted = _dRFFinal.InsertTaskSubTaskDetails(taskSubTaskInputs);                        
                    }
                    //ADD DATA INTO LINK FOR DEPENDECY
                    //Add link data
                    var tempsubtasklist = (from TMST in _db.Tbl_Master_PIDF_SubTask
                                           join tmpts in _db.Tbl_Master_PIDF_Task on TMST.TaskID equals tmpts.TaskID
                                           join tmst1 in _db.Tbl_Master_PIDF_SubTask on TMST.TaskDependency equals tmst1.SubTaskID
                                           where TMST.Action == strTaskType
                                           select new { TMST.SubTaskID, TMST.TaskID, tmpts.TaskName, TMST.SubTaskName, TaskDependencyName = tmst1.SubTaskName }).ToList();
                    if (tempsubtasklist.Count > 0)
                    {
                        foreach (var ddata in tempsubtasklist)
                        {
                            //Get data from table project task mapping
                            //Get ProjectTaskMappingID of source task and target task
                            //Get target ProjectTaskMappingID 
                            var targetResult = (from tmpm in _db.Tbl_Master_ProjectTask_Mapping
                                                where tmpm.Drfid == Convert.ToInt32(intPIDFID) && tmpm.Action == "PIDF" & tmpm.TaskName == ddata.SubTaskName
                                                select new { tmpm.ProjectTaskMappingID }).FirstOrDefault();

                            //Get source ProjectTaskMappingID 
                            var sourceResult = (from tmpm in _db.Tbl_Master_ProjectTask_Mapping
                                                where tmpm.Drfid == Convert.ToInt32(intPIDFID) && tmpm.Action == "PIDF" & tmpm.TaskName == ddata.TaskDependencyName
                                                select new { tmpm.ProjectTaskMappingID }).FirstOrDefault();

                            if ((targetResult != null) && (sourceResult != null))
                            {
                                //Insert into Gantt Link Table
                                Tbl_Gantt_Link tbl_Gantt_Link = new Tbl_Gantt_Link();
                                tbl_Gantt_Link.Id = 0;
                                tbl_Gantt_Link.Action = "PIDF";
                                tbl_Gantt_Link.DRFID = Convert.ToInt64(intPIDFID);
                                tbl_Gantt_Link.Type = "0";
                                tbl_Gantt_Link.SourceTaskId = Convert.ToInt32(sourceResult.ProjectTaskMappingID);
                                tbl_Gantt_Link.TargetTaskId = Convert.ToInt32(targetResult.ProjectTaskMappingID);
                                _db.Tbl_Gantt_Link.Add(tbl_Gantt_Link);
                                _db.SaveChanges();

                                //Update planned_start and planned_end and deadline of target ProjectTaskMappingID 
                                //as per source ProjectTaskMappingID 
                                var tempsource = _db.Tbl_Master_ProjectTask_Mapping.Where(d => d.Drfid == Convert.ToInt64(intPIDFID) && d.ProjectTaskMappingID == sourceResult.ProjectTaskMappingID && d.Action == "PIDF").FirstOrDefault();
                                if (tempsource != null)
                                {
                                    Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = _db.Tbl_Master_ProjectTask_Mapping.Where(d => d.Drfid == Convert.ToInt64(intPIDFID) && d.ProjectTaskMappingID == targetResult.ProjectTaskMappingID && d.Action == "PIDF").FirstOrDefault();
                                    if (tbl_Master_ProjectTask_Mapping != null)
                                    {
                                        try
                                        {
                                            tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(tempsource.EndDate);
                                            tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(tempsource.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                                            tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(tempsource.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                                            tbl_Master_ProjectTask_Mapping.Planned_Start = Convert.ToDateTime(tempsource.EndDate);
                                            tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(tempsource.EndDate).AddDays(tbl_Master_ProjectTask_Mapping.TaskDuration.Value);
                                            _db.Entry(tbl_Master_ProjectTask_Mapping).State = EntityState.Modified;
                                            _db.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                }
                            }
                        }
                    }
                

                    //add mail code
                    string userMessage = "Project Name : " + pidfresult.ProjectorProductName;
                    string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
                    string userName = HttpContext.Session.GetString("CurrentUserName") + " has approved following project : ";
                    string strEmailMessage = userName + "</br>" + "Project Name : " + pidfresult.ProjectorProductName;

                
                    await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                    ModelState.Clear();

                    //send email notification code added by yogesh balapure on date 08/02/2020
                    //get smtp details 
                    SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                    EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Pidf Approved");
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

                    if (emailDetailsModel != null)
                    {
                        //get details
                        emailDetailsVM.ToMail = emailDetailsModel.ToList;
                        List<string> testCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.CCList))
                        {
                            if (emailDetailsModel.CCList.Contains(";"))
                            {
                                string[] splitcc = emailDetailsModel.CCList.Split(";");
                                foreach (var ccdata in splitcc)
                                {
                                    testCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testCC.Add(emailDetailsModel.CCList);
                            }
                        }


                        List<string> testBCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.BCCList))
                        {
                            if (emailDetailsModel.BCCList.Contains(";"))
                            {
                                string[] splitbcc = emailDetailsModel.BCCList.Split(";");
                                foreach (var ccdata in splitbcc)
                                {
                                    testBCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testBCC.Add(emailDetailsModel.BCCList);
                            }
                        }

                        emailDetailsVM.CCMail = testCC;
                        emailDetailsVM.BCCMail = testBCC;
                        emailDetailsVM.Subject = emailDetailsModel.MailSubject;
                        //emailDetailsVM.Body = emailDetailsModel.MailBody;
                        clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                        string tempurl = _config.GetSection("ApplicationURL:ApprovedPidfUrlLink").Value + intPIDFID;
                        emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                        emailDetailsVM.DispalyName = "";
                    }

                    if (sMTPDetailsModel != null && emailDetailsModel != null)
                    {
                        EmailHelper emailHelper = new EmailHelper();
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsPidfFAApproval").Value) == true)
                        {
                            var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                        }
                            
                    }
                }

                return Json(new { data = "success", message=strTempMessage}, new JsonSerializerSettings());
            }            
            else
                return Json(new { data = "fail" }, new JsonSerializerSettings());
        }
    }
}