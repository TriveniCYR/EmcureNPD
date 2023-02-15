using EmcureCERI.Business.Contract;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.SignalR;
using EmcureCERI.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace EmcureCERI.Web.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;
        private readonly EmcureCERIDBContext _db;
        private readonly IDRFService _DRF;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;

        //private readonly Int64 _TempDrfID;
        [Obsolete]
        public TaskController(IConfiguration config, IHostingEnvironment env, EmcureCERIDBContext context, IDRFService dRFService, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            _config = config;
            _env = env;
            _db = context;
            _DRF = dRFService;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
            //_TempDrfID = DRFID;
        }
        // GET api/task
        [HttpGet]
        public IEnumerable<GanttTaskData> Get(Int64 DRFID,string strAction)        
        {
           
            int tempdrfid = Convert.ToInt32(DRFID);
            var result = _db.Tbl_Master_ProjectTask_Mapping
                        .AsNoTracking()
                        .Where(t=>t.Drfid== tempdrfid && t.IsActive==true && t.Action == strAction)
                        .Select(t => t).ToList();
            List<GanttTaskVM> objVM = new List<GanttTaskVM>();
            foreach (var ddata in result)
            {
                objVM.Add(new GanttTaskVM
                {
                    Id = (int)ddata.ProjectTaskMappingID,
                    Text = ddata.TaskName,
                    StartDate = Convert.ToDateTime(ddata.StartDate),
                    EndDate = Convert.ToDateTime(ddata.EndDate),
                    Duration = (int)ddata.TaskDuration,
                    SortOrder = (int)ddata.SortOrder.Value,// 0,//(int)ddata.SortOrder.Value,                    
                    Progress = Convert.ToDecimal(ddata.TotalPercentage.HasValue ? ddata.TotalPercentage.Value : 0),
                    ParentId = (int)ddata.ParentID,
                    Owner = (int)ddata.EmpID,
                    Type = ddata.Type,// " ",//ddata.Type,
                    Priority = Convert.ToString(ddata.PriorityID),
                    //deadLine = (ddata.DeadLine == null ? Convert.ToDateTime(ddata.EndDate)
                    //                    : Convert.ToDateTime(ddata.DeadLine)),
                    planned_start = (ddata.Planned_Start == null ? Convert.ToDateTime(ddata.StartDate)
                                        : Convert.ToDateTime(ddata.Planned_Start)),
                    planned_end = (ddata.Planned_End == null ? Convert.ToDateTime(ddata.EndDate)
                                        : Convert.ToDateTime(ddata.Planned_End))

                });
            }
            return objVM.ToList().Select(t => (GanttTaskData)t);            
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public GanttTaskData Get(int id)
        {
            var result = _db.Tbl_Master_ProjectTask_Mapping.ToList().Select(t => t);
            List<GanttTaskVM> objVM = new List<GanttTaskVM>();
            foreach (var ddata in result)
            {
                objVM.Add(new GanttTaskVM
                {
                    Id = (int)ddata.ProjectTaskMappingID,
                    Text = ddata.TaskName,
                    StartDate = Convert.ToDateTime(ddata.StartDate),
                    Duration = (int)ddata.TaskDuration,
                    SortOrder =0,// (int)ddata.SortOrder,
                    Progress = (decimal)ddata.TotalPercentage,
                    ParentId = (int)ddata.ParentID,
                    Owner = (int)ddata.EmpID,
                    Type = ddata.Type,
                    Priority = Convert.ToString(ddata.PriorityID)
                });
            }            
            return (GanttTaskData)objVM.Where(t => t.Id == id);            
        }

        // POST api/task
        //[Authorize(Roles = "Prescriber")]
        [HttpPost]
        [Obsolete]
        //[ActionName("Post")]
        public async Task<IActionResult> Post(GanttTaskData ganttTaskData)
        {
            int tempdrfid = Convert.ToInt32(HttpContext.Session.GetString("DrfID"));
            string tempAction = HttpContext.Session.GetString("Action");
            //Get priority details using priority id
            Tbl_Master_Priority tbl_Master_Priority = new Tbl_Master_Priority();
            var result = _db.Tbl_Master_Priority
                            .AsNoTracking()
                            .Where(p => p.PriorityID == Convert.ToInt32(ganttTaskData.priority))
                            .Select(p => new { p.PriorityID, p.PriorityName, p.IsActive, p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate });

            foreach (var pdata in result)
            {
                tbl_Master_Priority.PriorityID = pdata.PriorityID;
                tbl_Master_Priority.PriorityName = pdata.PriorityName;
                tbl_Master_Priority.IsActive = pdata.IsActive;
                tbl_Master_Priority.CreatedBy = pdata.CreatedBy;
                tbl_Master_Priority.CreatedDate = pdata.CreatedDate;
                tbl_Master_Priority.ModifiedBy = pdata.ModifiedBy;
                tbl_Master_Priority.ModifiedDate = pdata.ModifiedDate;

            }

            Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = new Tbl_Master_ProjectTask_Mapping();
            Tbl_Master_ProjectTask_Mapping objTemp = new Tbl_Master_ProjectTask_Mapping();
            var tempresult = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(t => t.Drfid == tempdrfid && t.Action == tempAction)
                                                        .Max(t => t.TaskOrder) + 1;

            tbl_Master_ProjectTask_Mapping.TaskOrder = Convert.ToInt32(tempresult);
            tbl_Master_ProjectTask_Mapping.TaskName = ganttTaskData.text;
            tbl_Master_ProjectTask_Mapping.ParentID = ganttTaskData.parent;
            tbl_Master_ProjectTask_Mapping.Action = tempAction;
            tbl_Master_ProjectTask_Mapping.Drfid = tempdrfid;
            tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(ganttTaskData.start_date);
            tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(ganttTaskData.start_date).AddDays(ganttTaskData.duration);
            tbl_Master_ProjectTask_Mapping.PriorityID = tbl_Master_Priority.PriorityID;
            tbl_Master_ProjectTask_Mapping.Priority = tbl_Master_Priority.PriorityName;

            tbl_Master_ProjectTask_Mapping.TaskStatusID = 8;
            tbl_Master_ProjectTask_Mapping.TaskStatus = "Initial";
            tbl_Master_ProjectTask_Mapping.TaskDuration = ganttTaskData.duration;
           
            decimal? tempprogress = ganttTaskData.progress;
            tbl_Master_ProjectTask_Mapping.TotalPercentage = tempprogress.HasValue ? tempprogress.Value : 0;

            tbl_Master_ProjectTask_Mapping.EmpID = Convert.ToInt32(ganttTaskData.owner_id);
            tbl_Master_ProjectTask_Mapping.Type =  String.IsNullOrEmpty(ganttTaskData.type) ? "task" : ganttTaskData.type;
            tbl_Master_ProjectTask_Mapping.SortOrder = Convert.ToInt32(tempresult);

            tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(ganttTaskData.start_date).AddDays(ganttTaskData.duration);// Convert.ToDateTime(ganttTaskData.planned_end);
            tbl_Master_ProjectTask_Mapping.Planned_Start = String.IsNullOrEmpty(ganttTaskData.planned_start) ? Convert.ToDateTime(ganttTaskData.start_date) : Convert.ToDateTime(ganttTaskData.planned_start);
            tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(ganttTaskData.start_date).AddDays(ganttTaskData.duration);//Convert.ToDateTime(ganttTaskData.planned_end);


            tbl_Master_ProjectTask_Mapping.IsActive = true;
            tbl_Master_ProjectTask_Mapping.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")); ;
            tbl_Master_ProjectTask_Mapping.CreatedDate = DateTime.Now;
            tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")); ;
            tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Now;

            _db.Tbl_Master_ProjectTask_Mapping.Add(tbl_Master_ProjectTask_Mapping);
            _db.SaveChanges();
            Int64 id = Convert.ToInt64(tbl_Master_ProjectTask_Mapping.ProjectTaskMappingID);

            int res = _DRF.InsertTransactionProjectTaskHistory(id);

            //Send alert to all 
            var drfresult = (from t in _db.Tbl_Master_ProjectTask_Mapping
                             where t.ProjectTaskMappingID == id
                             select new { t.Drfid }).FirstOrDefault();
            string strProjectName = null;
            if (tempAction == "DRF")
            {
                var result1 = (from TDI in _db.Tbl_DRF_Initialization
                               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.InitializationID == drfresult.Drfid
                               select new { TDR.ProjectName }).FirstOrDefault();
                strProjectName = result1.ProjectName;
            }
            else if (tempAction == "PIDF")
            {
                var result1 = (from TDI in _db.Tbl_PIDF_HeaderNew                                   
                               where TDI.PidfID == drfresult.Drfid
                               select new { TDI.ProjectorProductName }).FirstOrDefault();
                strProjectName = result1.ProjectorProductName;
            }
            else
            {
                var result1 = (from TDI in _db.Tbl_DRF_Initialization
                               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.InitializationID == drfresult.Drfid
                               select new { TDR.ProjectName }).FirstOrDefault();
                strProjectName = result1.ProjectName;
            }

            string userName = HttpContext.Session.GetString("CurrentUserName") + " has created the following task: ";
            //string userMessage = "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string userMessage = "Project Name : " + strProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string messageTime = Convert.ToString(DateTime.Now.Second) + "seconds ago.";

            //string strEmailMessage = userName + "</br>" + "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string strEmailMessage = userName + "</br>" + "Project Name : " + strProjectName + "</br>" + "Task Name : " + ganttTaskData.text;

            await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);

            //send email notification code added by yogesh balapure on date 08/02/2020
            //get smtp details 
            SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
            string tempNotification;
            if (tempAction == "PIDF")
            {
                tempNotification = "Pidf Task Create";
            }
            else
            {
                tempNotification = "Task Create";
            }
            //EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Task Create");
            EmailDetailsModel emailDetailsModel = _emailService.EmailDetails(tempNotification);
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
                string tempurl = null;
                if (tempAction == "DRF")
                {
                    tempurl = _config.GetSection("ApplicationURL:DrfUrlLink").Value + tempdrfid;
                }
                else
                {
                    //PIDF
                    tempurl = _config.GetSection("ApplicationURL:ApprovedPidfUrlLink").Value + tempdrfid;
                }

                emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                emailDetailsVM.DispalyName = "";
            }

            if (sMTPDetailsModel != null && emailDetailsModel != null)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (Convert.ToBoolean(_config.GetSection("MailSend:IsTaskCreate").Value) == true)
                {
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }                   
            }

            return Ok(new { tid = tbl_Master_ProjectTask_Mapping.ProjectTaskMappingID, action = "inserted" });
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        [Obsolete]
        public async Task<ObjectResult> Put(int id, GanttTaskData ganttTaskData)
        {
            int tempdrfid = Convert.ToInt32(HttpContext.Session.GetString("DrfID"));
            string tempAction = HttpContext.Session.GetString("Action");
            //Get priority details using priority id
            Tbl_Master_Priority tbl_Master_Priority = new Tbl_Master_Priority();
            var result = _db.Tbl_Master_Priority
                                .AsNoTracking()
                                .Where(p => p.PriorityID == Convert.ToInt32(ganttTaskData.priority))
                                .Select(p => new { p.PriorityID, p.PriorityName, p.IsActive, p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate });

            foreach (var pdata in result)
            {
                tbl_Master_Priority.PriorityID = pdata.PriorityID;
                tbl_Master_Priority.PriorityName = pdata.PriorityName;
                tbl_Master_Priority.IsActive = pdata.IsActive;
                tbl_Master_Priority.CreatedBy = pdata.CreatedBy;
                tbl_Master_Priority.CreatedDate = pdata.CreatedDate;
                tbl_Master_Priority.ModifiedBy = pdata.ModifiedBy;
                tbl_Master_Priority.ModifiedDate = pdata.ModifiedDate;
            }

            Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(d => d.Drfid == tempdrfid && d.ProjectTaskMappingID == id && d.Action == tempAction).FirstOrDefault();
            if (tbl_Master_ProjectTask_Mapping != null)
            {
                tbl_Master_ProjectTask_Mapping.ProjectTaskMappingID = Convert.ToInt64(id);                
                tbl_Master_ProjectTask_Mapping.TaskName = ganttTaskData.text;
                tbl_Master_ProjectTask_Mapping.ParentID = ganttTaskData.parent;
                tbl_Master_ProjectTask_Mapping.Action = tempAction;
                tbl_Master_ProjectTask_Mapping.Drfid = tempdrfid;
                tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(ganttTaskData.start_date);
                tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(ganttTaskData.start_date).AddDays(ganttTaskData.duration);
                          

                tbl_Master_ProjectTask_Mapping.PriorityID = tbl_Master_Priority.PriorityID;
                tbl_Master_ProjectTask_Mapping.Priority = tbl_Master_Priority.PriorityName;

                int tempTaskStatusID = ganttTaskData.progress == 0 ? 8
                                        : ganttTaskData.progress == 100 ? 1
                                        : 11;
                string TempTaskStatus = ganttTaskData.progress == 0 ? "Initial"
                                        : ganttTaskData.progress == 100 ? "Completed"
                                        : "InProgress";
                tbl_Master_ProjectTask_Mapping.TaskStatusID = tempTaskStatusID;
                tbl_Master_ProjectTask_Mapping.TaskStatus = TempTaskStatus;
                tbl_Master_ProjectTask_Mapping.TaskDuration = ganttTaskData.duration;
                decimal? tempprogress = ganttTaskData.progress;
                tbl_Master_ProjectTask_Mapping.TotalPercentage = tempprogress.HasValue ? tempprogress.Value : 0;
                                
                tbl_Master_ProjectTask_Mapping.EmpID = Convert.ToInt32(ganttTaskData.owner_id);
                tbl_Master_ProjectTask_Mapping.Type = ganttTaskData.type;
                //tbl_Master_ProjectTask_Mapping.SortOrder = 0;// ganttTaskData.sortorder;
                if (ganttTaskData.deadLine == "0001-01-01 00:00")
                {
                    ganttTaskData.deadLine = ganttTaskData.planned_end;
                }
                //final
                tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(ganttTaskData.deadLine);
                tbl_Master_ProjectTask_Mapping.Planned_Start = Convert.ToDateTime(ganttTaskData.planned_start);
                tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(ganttTaskData.planned_end);
                
                tbl_Master_ProjectTask_Mapping.IsActive = true;
                tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(ganttTaskData.target))
            {
                // reordering occurred
                this._UpdateOrders(tbl_Master_ProjectTask_Mapping, ganttTaskData.target);
            }
            decimal? _TotalPercentage = tbl_Master_ProjectTask_Mapping.TotalPercentage;
            _db.Entry(tbl_Master_ProjectTask_Mapping).State = EntityState.Modified;
            _db.SaveChanges();
            _db.Entry(tbl_Master_ProjectTask_Mapping).State = EntityState.Detached;

            //get dependent task id from tbl_gantt_link
            CheckDependentLink(id, tempdrfid, tempAction, ganttTaskData.end_date, ganttTaskData.deadLine);            

            if (ganttTaskData.parent == 0 && _TotalPercentage == 1)
            {
                var subtask = _db.Tbl_Master_ProjectTask_Mapping.Where(t => t.ParentID == id).ToList();
                foreach (var ddata in subtask)
                {
                    ddata.TotalPercentage = 1;
                    ddata.TaskStatusID = 1;
                    ddata.TaskStatus = "Completed";
                    ddata.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    ddata.ModifiedDate = DateTime.Now;
                    _db.Entry(ddata).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            else if (ganttTaskData.parent == 0 && _TotalPercentage == 0)
            {
                var subtask = _db.Tbl_Master_ProjectTask_Mapping.Where(t => t.ParentID == id).ToList();
                foreach (var ddata in subtask)
                {
                    ddata.TotalPercentage = 0;
                    ddata.TaskStatusID = 8;
                    ddata.TaskStatus = "Initial";
                    ddata.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    ddata.ModifiedDate = DateTime.Now;
                    _db.Entry(ddata).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            int res = _DRF.InsertTransactionProjectTaskHistory(id);

            //Send alert to all 
            var drfresult = (from t in _db.Tbl_Master_ProjectTask_Mapping
                             where t.ProjectTaskMappingID == id
                             select new { t.Drfid }).FirstOrDefault();
            string strProjectName = null;
            if (tempAction == "DRF")
            {
                var result1 = (from TDI in _db.Tbl_DRF_Initialization
                               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.InitializationID == drfresult.Drfid
                               select new { TDR.ProjectName }).FirstOrDefault();
                strProjectName = result1.ProjectName;
            }
            else if (tempAction == "PIDF")
            {
                var result1 = (from TDI in _db.Tbl_PIDF_HeaderNew
                               where TDI.PidfID == drfresult.Drfid
                               select new { TDI.ProjectorProductName }).FirstOrDefault();
                strProjectName = result1.ProjectorProductName;
            }
            else
            {
                var result1 = (from TDI in _db.Tbl_DRF_Initialization
                               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.InitializationID == drfresult.Drfid
                               select new { TDR.ProjectName }).FirstOrDefault();
                strProjectName = result1.ProjectName;
            }           


            string userName = HttpContext.Session.GetString("CurrentUserName") + " has updated the following task: ";
            //string userMessage = "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string userMessage = "Project Name : " + strProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string messageTime = Convert.ToString(DateTime.Now.Second) + "seconds ago.";

            //string strEmailMessage = userName + "</br>" + "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + ganttTaskData.text;
            string strEmailMessage = userName + "</br>" + "Project Name : " + strProjectName + "</br>" + "Task Name : " + ganttTaskData.text;

            await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);

            //send email notification code added by yogesh balapure on date 08/02/2020
            //get smtp details 
            SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
            string tempNotification;
            if(tempAction == "PIDF")
            {
                tempNotification = "Pidf Task Update";
            }
            else
            {
                tempNotification = "Task Updated";
            }
            EmailDetailsModel emailDetailsModel = _emailService.EmailDetails(tempNotification);
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
                string tempurl = null;
                if (tempAction == "DRF")
                {
                    tempurl = _config.GetSection("ApplicationURL:DrfUrlLink").Value + tempdrfid;
                }
                else
                {
                    //PIDF
                    tempurl = _config.GetSection("ApplicationURL:ApprovedPidfUrlLink").Value + tempdrfid;
                }
                emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                emailDetailsVM.DispalyName = "";
            }

            if (sMTPDetailsModel != null && emailDetailsModel != null)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (Convert.ToBoolean(_config.GetSection("MailSend:IsTaskUpdate").Value) == true)
                {
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }
                    
            }
            return Ok(new { action = "updated" });
        }

        private void CheckDependentLink(int TaskID,int DrfID, string ActionName,string End_Date,string DeadLine)
        {
            string strEnd_date;
            string strDeadLine;
            //get dependent task id from tbl_gantt_link
            var dependenttasklist = _db.Tbl_Gantt_Link.Where(x => x.SourceTaskId == TaskID && x.DRFID == DrfID && x.Action == ActionName)
                                                        .AsNoTracking().ToList();
            if (dependenttasklist != null && dependenttasklist.Count > 0)
            {
                foreach (var ddata in dependenttasklist)
                {
                    Tbl_Master_ProjectTask_Mapping _tbl_Master_ProjectTask_Mapping = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(d => d.Drfid == DrfID && d.ProjectTaskMappingID == ddata.TargetTaskId && d.Action == ActionName).FirstOrDefault();
                    if (_tbl_Master_ProjectTask_Mapping != null)
                    {
                        strEnd_date = _tbl_Master_ProjectTask_Mapping.EndDate.Value.ToString("yyyy-MM-dd HH:mm");
                        strDeadLine = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value).ToString("yyyy-MM-dd HH:mm");

                        _tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(End_Date);
                        _tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value);

                        _tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(DeadLine);
                        _tbl_Master_ProjectTask_Mapping.Planned_Start = Convert.ToDateTime(End_Date);
                        _tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value);

                        _db.Entry(_tbl_Master_ProjectTask_Mapping).State = EntityState.Modified;
                        _db.SaveChanges();
                        _db.Entry(_tbl_Master_ProjectTask_Mapping).State = EntityState.Detached;

                        //Check subchild
                        var subdependenttasklist = _db.Tbl_Gantt_Link.Where(x => x.SourceTaskId == ddata.TargetTaskId && x.DRFID == DrfID && x.Action == ActionName)
                                                        .AsNoTracking().ToList();
                        if (subdependenttasklist != null && subdependenttasklist.Count > 0)
                        {
                            CheckSubDependentLink(ddata.TargetTaskId, DrfID, ActionName, strEnd_date, strDeadLine);
                        }                            
                    }
                }
            }
        }

        private void CheckSubDependentLink(int TaskID, int DrfID, string ActionName, string End_Date, string DeadLine)
        {
            string strEnd_date;
            string strDeadLine;

            try
            {
                //get dependent task id from tbl_gantt_link
                var dependenttasklist = _db.Tbl_Gantt_Link.Where(x => x.SourceTaskId == TaskID && x.DRFID == DrfID && x.Action == ActionName)
                                                            .AsNoTracking().ToList();
                if (dependenttasklist != null && dependenttasklist.Count > 0)
                {
                    foreach (var ddata in dependenttasklist)
                    {
                        Tbl_Master_ProjectTask_Mapping _tbl_Master_ProjectTask_Mapping = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(d => d.Drfid == DrfID && d.ProjectTaskMappingID == ddata.TargetTaskId && d.Action == ActionName).FirstOrDefault();
                        if (_tbl_Master_ProjectTask_Mapping != null)
                        {
                            strEnd_date = _tbl_Master_ProjectTask_Mapping.EndDate.Value.ToString("yyyy-MM-dd HH:mm");
                            strDeadLine = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value).ToString("yyyy-MM-dd HH:mm");
                            _tbl_Master_ProjectTask_Mapping.StartDate = Convert.ToDateTime(End_Date);
                            _tbl_Master_ProjectTask_Mapping.EndDate = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value);

                            _tbl_Master_ProjectTask_Mapping.DeadLine = Convert.ToDateTime(DeadLine);
                            _tbl_Master_ProjectTask_Mapping.Planned_Start = Convert.ToDateTime(End_Date);
                            _tbl_Master_ProjectTask_Mapping.Planned_End = Convert.ToDateTime(End_Date).AddDays(_tbl_Master_ProjectTask_Mapping.TaskDuration.Value);

                            _db.Entry(_tbl_Master_ProjectTask_Mapping).State = EntityState.Modified;
                            _db.SaveChanges();
                            _db.Entry(_tbl_Master_ProjectTask_Mapping).State = EntityState.Detached;
                            //Check subchild
                            var subdependenttasklist = _db.Tbl_Gantt_Link.Where(x => x.SourceTaskId == ddata.TargetTaskId && x.DRFID == DrfID && x.Action == ActionName)
                                                            .AsNoTracking().ToList();
                            if (subdependenttasklist != null && subdependenttasklist.Count > 0)
                            {
                                CheckSubDependentLink(ddata.TargetTaskId, DrfID, ActionName, strEnd_date, strDeadLine);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        private void _UpdateOrders(Tbl_Master_ProjectTask_Mapping updatedTask, string orderTarget)/*!*/
        {
            //int adjacentTaskId;
            Int64 adjacentTaskId;
            var nextSibling = false;

            var targetId = orderTarget;

            // adjacent task id is sent either as '{id}' or as 'next:{id}' depending 
            // on whether it's the next or the previous sibling
            if (targetId.StartsWith("next:"))
            {
                targetId = targetId.Replace("next:", "");
                nextSibling = true;
            }

            if (!Int64.TryParse(targetId, out adjacentTaskId))
            {
                return;
            }

            var adjacentTask = _db.Tbl_Master_ProjectTask_Mapping.Find(adjacentTaskId);
            var startOrder = adjacentTask.SortOrder;

            if (nextSibling)
                startOrder++;

            updatedTask.SortOrder = startOrder;
            updatedTask.TaskOrder = startOrder;

            var updateOrders = _db.Tbl_Master_ProjectTask_Mapping
                .AsNoTracking()
                .Where(t => t.ProjectTaskMappingID != updatedTask.ProjectTaskMappingID)
                .Where(t => t.SortOrder >= startOrder)
                .OrderBy(t => t.SortOrder);

            var taskList = updateOrders.ToList();

            taskList.ForEach(t => t.SortOrder++);
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteTask(int id)
        {
            var task = _db.Tbl_Master_ProjectTask_Mapping.AsNoTracking().Where(t=>t.ProjectTaskMappingID == Convert.ToInt64(id)).FirstOrDefault();
            if (task != null)
            {
                //Check task is Parent
                if(task.ParentID==0)
                {
                    task.IsActive = false;
                    task.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    task.ModifiedDate = DateTime.Now;
                    _db.Entry(task).State = EntityState.Modified;
                    _db.SaveChanges();
                    //update all subtask related to parent id
                    var subtask = _db.Tbl_Master_ProjectTask_Mapping.Where(t => t.ParentID ==id).ToList();
                    foreach(var ddata in subtask)
                    {                        
                        ddata.IsActive = false;
                        ddata.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        ddata.ModifiedDate = DateTime.Now;
                        _db.Entry(ddata).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                else
                {                    
                    task.IsActive = false;
                    task.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    task.ModifiedDate = DateTime.Now;
                    _db.Entry(task).State = EntityState.Modified;
                    _db.SaveChanges();
                }               
            }
            return Ok(new { action = "deleted" });
        }
    }
}
