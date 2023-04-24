using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface INotificationService
    {
        Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model);

        void dbChangeNotification(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<MasterNotification> e);

        Task<DBOperation> CreateNotification(long pidfId, int statusid, string notificatioTitle, string notificationDescription, int loggedinUserId);

        Task<DBOperation> UpdateNotification(long notificationId, string notificationTitle, string notificationDescription, int loggedinUserId);

        Task<DataTableResponseModel> GetFilteredNotifications(string ColumnName, string SortDir, int start, int length, int RoleId);

        Task<PendingNotification> ClickedNotification();

        Task<PendingNotification> NotificationCountForUser();
    }
}