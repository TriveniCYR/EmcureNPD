using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNotificationUser
    {
        public long NotificationUserId { get; set; }
        public int UserId { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual MasterUser User { get; set; }
    }
}
