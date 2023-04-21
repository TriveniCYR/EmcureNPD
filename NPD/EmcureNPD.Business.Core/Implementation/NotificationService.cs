using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Helpers;
using EmcureNPD.Utility.Utility;
using Microsoft.Extensions.Localization;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IExceptionService _ExceptionService;
        private IRepository<MasterNotification> _repository { get; set; }
        private IRepository<MasterNotificationUser> _repositoryNotificationUser { get; set; }
        private readonly IHelper _helper;

        public NotificationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IStringLocalizer<Errors> stringLocalizerError,
                                 Microsoft.Extensions.Configuration.IConfiguration _configuration, IHelper helper, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterNotification>();
            _repositoryNotificationUser = _unitOfWork.GetRepository<MasterNotificationUser>();
            configuration = _configuration;
            _helper = helper;
            _ExceptionService = exceptionService;
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

            SqlDependency sqlDependency = new SqlDependency();
            sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
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

            SqlDependency sqlDependency = new SqlDependency();
            sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
            return oDataTableResponseModel;
        }

        public void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            //NotificationHub.Show();
        }

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
                return DBOperation.Success;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return DBOperation.Error;
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

            SqlDependency sqlDependency = new SqlDependency();
            sqlDependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
            return oResponseModel;
        }
    }
}