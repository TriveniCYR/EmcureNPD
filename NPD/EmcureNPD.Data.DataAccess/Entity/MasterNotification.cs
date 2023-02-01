using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNotification
    {
        public long NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
