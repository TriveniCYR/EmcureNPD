using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Resource;
using EmcureNPD.Utility;
using EmcureNPD.Utility.Helpers;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using Dapper;

using static EmcureNPD.Utility.Enums.GeneralEnum;
using System.Data;
using Microsoft.AspNetCore.Http;
using EmcureNPD.Utility.Enums;

namespace EmcureNPD.Business.Core.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IExceptionService _ExceptionService;
        SqlTableDependency<MasterNotification> tableDependency;
        NotificationHub notificationHub;
        private readonly IDatabaseSubscription _databaseSubscription;
		private readonly IMasterUserService _MasterUserService;
		private IRepository<MasterUser> _masterUser { get; set; }
		private IRepository<MasterNotification> _repository { get; set; }
        private IRepository<MasterNotificationUser> _repositoryNotificationUser { get; set; }
        private readonly IHelper _helper;
		public NotificationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IStringLocalizer<Errors> stringLocalizerError,
                                 Microsoft.Extensions.Configuration.IConfiguration _configuration, IHelper helper, IExceptionService exceptionService, IMasterUserService MasterUserService)//, IDatabaseSubscription databaseSubscription
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterNotification>();
            _repositoryNotificationUser = _unitOfWork.GetRepository<MasterNotificationUser>();
            configuration = _configuration;
            _helper = helper;
            _ExceptionService = exceptionService;
            _MasterUserService= MasterUserService;
			_masterUser = unitOfWork.GetRepository<MasterUser>();
			// _schedulerService = schedulerService;
			//_databaseSubscription.Configure(DatabaseConnection.NPDDatabaseConnection);
			//tableDependency = new SqlTableDependency<MasterNotification>(DatabaseConnection.NPDDatabaseConnection, "Master_Notification", null, null, null, null, DmlTriggerType.Insert);
			//tableDependency.Start();
		}

        public async Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model)
        {
            DataTableResponseModel oDataTableResponseModel = null;
            if (model.columns == null)
            {
                return oDataTableResponseModel;
            }

            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            int userId = _helper.GetLoggedInUser().UserId;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var NotificationList = await _repository.GetBySP("stp_npd_GetNotificationList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (NotificationList != null && NotificationList.Rows.Count > 0 ? Convert.ToInt32(NotificationList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (NotificationList != null && NotificationList.Rows.Count > 0 ? Convert.ToInt32(NotificationList.Rows[0]["TotalCount"]) : 0);

            oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, NotificationList);

            //SqlDependency sqlDependency = new SqlDependency();
            //sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
            return oDataTableResponseModel;
        }

        public async Task<DataTableResponseModel> GetFilteredNotifications(string ColumnName, string SortDir, int start, int length, int RoleId)
        {
            var model = new DataTableAjaxPostModel();
            model.start = start;
            model.length = length;
            int userId = _helper.GetLoggedInUser().UserId;
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", "")
            };

            var NotificationList = await _repository.GetBySP("stp_npd_GetNotificationList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (NotificationList != null && NotificationList.Rows.Count > 0 ? Convert.ToInt32(NotificationList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (NotificationList != null && NotificationList.Rows.Count > 0 ? Convert.ToInt32(NotificationList.Rows[0]["TotalCount"]) : 0);

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, NotificationList.DataTableToList<MasterNotification>());

            //SqlDependency sqlDependency = new SqlDependency();
            //sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
            return oDataTableResponseModel;
        }

        public async void dbChangeNotification(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<MasterNotification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var pendingnotification = await this.NotificationCountForUser();
                //await _hubContext.Clients.All.SendAsync("ReceiveNotification", pendingnotification.Count);
                await notificationHub.GetNotification(pendingnotification.Count);//
            }
        }
        //to be continue..
        public async Task<DBOperation> CreateNotification(long pidfId, int statusid, string notificationTitle, string notificationDescription, int loggedinUserId)
        {
            try
            {
                MasterNotification objNotification;
                var notification = new MasterNotificationEntity
                {
                    PIDFId = pidfId,
                    StatusId = statusid,
                    NotificationTitle = notificationTitle,
                    NotificationDescription = notificationDescription,
                    CreatedDate = DateTime.Now,
                    CreatedBy = loggedinUserId,
                };
                objNotification = _mapperFactory.Get<MasterNotificationEntity, MasterNotification>(notification);
                _repository.AddAsync(objNotification);

                await _unitOfWork.SaveChangesAsync();
                if (objNotification.NotificationId == 0)
                    return DBOperation.Error;
                //SqlDependency sqlDependency = new SqlDependency();
                //sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);


                //tableDependency = new SqlTableDependency<MasterNotification>(DatabaseConnection.NPDDatabaseConnection, "Master_Notification", null, null, null, null, DmlTriggerType.Insert);
                //tableDependency.OnChanged += dbChangeNotification;
                //tableDependency.Start();
                //_databaseSubscription.Changed += dbChangeNotification;
				var task = Task.Run(() => SendNotification(objNotification.NotificationId));
				//bool result = task.Result;
				return DBOperation.Success;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return DBOperation.Error;
            }
        }
		public async Task<EmailNotificationEntity> SendNotification(long NotificationId)
		{
			string _logMessage = string.Empty;
			EmailNotificationEntity model = new EmailNotificationEntity();
			string _logMessage_PIDFSubmittted = "\n";
			SqlParameter[] osqlParameter = {
				new SqlParameter("@NotificationId",NotificationId),
			};
			var dbresult =  _masterUser.GetDataSetBySP("GetEmailNotification", System.Data.CommandType.StoredProcedure, osqlParameter).Result;
			dynamic UserList = new ExpandoObject();
			if (dbresult != null)
			{

				if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
				{
					UserList = dbresult.Tables[0].DataTableToList<EmailNotificationEntity>();
					
					string commasepretedUserList = string.Empty;
					List<EmailNotificationEntity> _UserLIst = UserList;
					foreach (var u in _UserLIst)
					{
						commasepretedUserList += u.SendToName + ",";
					}
					_logMessage += "Fetch list of user to send notification { " + commasepretedUserList + "} \n";
				SendNotificationMail(UserList, ref _logMessage);
				}
			}
			model.LogMessage = _logMessage;
			model.LogMessage += _logMessage_PIDFSubmittted;
			return model;
		}
		public void SendNotificationMail(List<EmailNotificationEntity> sendNotificationModel_list, ref string _logMessage)
		{
            var landingUrl = configuration.GetSection("AllowedOrigins").Value.ToString(); //+ "/PIDF/PIDFList?ScreenId=" + (int)PIDFScreen.PBF;
			foreach (var sendNotificationModel in sendNotificationModel_list)
			{
				try
				{
					EmailHelper email = new EmailHelper();
					string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\EmailNotification.html");
					strHtml = strHtml.Replace("{PIDFNo}", sendNotificationModel.PidfNo);
					strHtml = strHtml.Replace("{DateTime}", sendNotificationModel.CreatedDate.ToString());
					strHtml = strHtml.Replace("{User}", sendNotificationModel.SendToName);
					strHtml = strHtml.Replace("{PIDFStatus}", sendNotificationModel.PIDFStatus);
					strHtml = strHtml.Replace("{UpdatedByUser}", sendNotificationModel.CreatedByName);
					strHtml = strHtml.Replace("{Url}", landingUrl);
					string str_subject = "PIDF : " + sendNotificationModel.PidfNo + " Updated";
					email.SendMail(sendNotificationModel.EmailAddress, string.Empty, str_subject, strHtml, _MasterUserService.GetSMTPConfiguration());
					_logMessage += " Email Sent to { " + sendNotificationModel.EmailAddress + " } on " + DateTime.Now.ToString() + "" + "\n";
                    UpdateSentNotification(sendNotificationModel.NotificationId);
                   
				}
				catch (Exception ex)
				{
					_logMessage += "Email failed to sent {" + sendNotificationModel.EmailAddress + "} on " + DateTime.Now.ToString() + ex.InnerException.ToString() + "\n";
					 
				}
				
			}
			
		}
		public void  UpdateSentNotification(long NotificationId)
		{
			try
			{
				SqlConnection con = new SqlConnection(configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
				
				var data = new DynamicParameters();
				data.Add("@NotificationId", NotificationId);
				data.Add("@Success", "", direction: ParameterDirection.Output);
				int count =  con.Execute("ProcUpdateEmailNotificationMaster", data, commandType: CommandType.StoredProcedure);
			//	if (count > 0 && data.Get<string>("Success").Trim() == "success")
			//  {
			//  	//return true;
			//  }
            //    else
            //    {
            //        //return false;
            //    }
			}
			catch (Exception ex)
			{

				//return false;
			}
		}
		public async Task<DBOperation> UpdateNotification(long notificationId, string notificationTitle, string notificationDescription, int loggedinUserId)
        {
            var _objExistingNotf = _repository.Get(x => x.NotificationId == notificationId);
            if (_objExistingNotf != null)
            {
                _objExistingNotf.NotificationTitle = notificationTitle;
                _objExistingNotf.NotificationDescription = notificationDescription;
                _objExistingNotf.CreatedDate = DateTime.Now;
                _objExistingNotf.CreatedBy = loggedinUserId;
                _repository.UpdateAsync(_objExistingNotf);
            }
            await _unitOfWork.SaveChangesAsync();
            return DBOperation.Success;
        }

        public async Task<PendingNotification> ClickedNotification()
        {
            int userId = _helper.GetLoggedInUser().UserId;
            var _objClickedgNotUser = _repositoryNotificationUser.GetAllQuery().Where(x => x.UserId == userId).FirstOrDefault();
            if (_objClickedgNotUser != null)
            {
                _objClickedgNotUser.UpdateDate = DateTime.Now;
                _repositoryNotificationUser.UpdateAsync(_objClickedgNotUser);
            }
            else
            {
                MasterNotificationUser _objClickedgNotUserAdd = new();
                _objClickedgNotUserAdd.UserId = userId;
                _objClickedgNotUserAdd.UpdateDate = DateTime.Now;
                _repositoryNotificationUser.AddAsync(_objClickedgNotUserAdd);
            }
            await _unitOfWork.SaveChangesAsync();

            return await NotificationCountForUser();
        }

        public async Task<PendingNotification> NotificationCountForUser()
        {
            var model = new DataTableAjaxPostModel();
            int userId = _helper.GetLoggedInUser().UserId;
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId",userId),
            };

            var PendingNotification = await _repository.GetBySP("stp_npd_GetRealTimeNotificationForUser", System.Data.CommandType.StoredProcedure, osqlParameter);

            var count = (PendingNotification != null && PendingNotification.Rows.Count > 0 ? Convert.ToInt32(PendingNotification.Rows[0]["PendingNotification"]) : 0);

            PendingNotification oResponseModel = new PendingNotification { Count = count };

            //SqlDependency sqlDependency = new SqlDependency();
            //sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
            return oResponseModel;
        }
    }
}