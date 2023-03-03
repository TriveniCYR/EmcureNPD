using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class UserSessionLogMaster
    {
        public long UserLoginHistoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterUser User { get; set; }
    }
}
