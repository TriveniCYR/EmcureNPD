using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface {
    public interface INotificationService {
        Task<DataTableResponseModel> GetAll();
        void dbChangeNotification(object sender, System.Data.SqlClient.SqlNotificationEventArgs e);

        Task<DBOperation> CreateNotification(long pidfId, int statusid, string notificatioTitle, string notificationDescription, int loggedinUserId);
        Task<DBOperation> UpdateNotification(long notificationId,string notificationTitle, string notificationDescription, int loggedinUserId);
        Task<DataTableResponseModel> GetFilteredNotifications(string ColumnName, string SortDir, int start, int length, int RoleId);

    }
}
