using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.NotificationScheduler.Helpers {
    public static class APIURLHelper {
        public static string LoginURL = "/api/Account/Login";

        #region Notifiactions
        public static string SendNotification = "api/Scheduler/SendNotification";
        #endregion
    }
}
