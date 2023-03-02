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
        public string NotificationTitleView { get; set; }
        public string StatusColor { get; set; }
        public string PidfNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterPidfstatus Status { get; set; }
    }
}
