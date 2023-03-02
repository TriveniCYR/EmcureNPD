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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation {
    public class NotificationService : INotificationService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private IRepository<MasterNotification> _repository { get; set; }


        public NotificationService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IStringLocalizer<Errors> stringLocalizerError,
                                 Microsoft.Extensions.Configuration.IConfiguration _configuration) {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterNotification>();
            configuration = _configuration;
        }
        public async Task<DataTableResponseModel> GetAll() {
            string ColumnName = "NotificationTitle";
            string SortDir = "ASC";
            
            var model = new DataTableAjaxPostModel();
            model.start = 0;
            model.length = 25;

            SqlParameter[] osqlParameter = {
                new SqlParameter("@NotificationId", 0),
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
		public async Task<DataTableResponseModel> GetFilteredNotifications(string ColumnName, string SortDir, int start,int length)
		{
			
			var model = new DataTableAjaxPostModel();
			model.start = start;
			model.length = length;

			SqlParameter[] osqlParameter = {
				new SqlParameter("@NotificationId", 0),
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
		public void dbChangeNotification(object sender, SqlNotificationEventArgs e) {
            NotificationHub.Show();
        }

    }
}
