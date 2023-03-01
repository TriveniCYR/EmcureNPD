using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IRealTimeNotificationService
    {
        IList<RealtimeNotificationModel> GetRealTimeNotificationData();
        IList<RealtimeNotificationModel> GetTop5RealTimeNotificationData();
        IList<RealtimeNotificationModel> GetTop5upRealTimeNotificationData();
        int UpdateViewNotification(int ProjectID);
       
        int UpdateAllNotification();
    }
}
