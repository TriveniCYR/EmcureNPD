using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNotification
    {
        public long NotificationId { get; set; }
        public long? Pidfid { get; set; }
        public int? StatusId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? SentDatetime { get; set; }
        public bool IsEmailSent { get; set; }
    }
}
