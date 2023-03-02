using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Scheduler.Helpers {
    public static class APIURLHelper {
        public static string LoginURL = "/api/Account/Login";

        #region Reminder
        public static string SendReminder = "api/Scheduler/SendReminder";
        #endregion
    }
}
