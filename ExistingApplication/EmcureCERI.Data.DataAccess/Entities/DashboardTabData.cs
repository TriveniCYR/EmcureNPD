using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DashboardTabData
    {
        public int totalInitial { get; set; }
        public int totalRejected { get; set; }
        public int totalInitialApproved { get; set; }
        public int totalPartialApproved { get; set; }
        public int totalPendingFinanceApproval { get; set; }
        public int totalFinanceApproved { get; set; }
        public int totalFinalApproved { get; set; }
        public int totalFinalRejected { get; set; }
    }
}
