using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class RealtimeNotificationModel
    {
        public int projectID { get; set; }
        public string projectType { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public string userMessage { get; set; }
        public string messageTime { get; set; }
    }
}
