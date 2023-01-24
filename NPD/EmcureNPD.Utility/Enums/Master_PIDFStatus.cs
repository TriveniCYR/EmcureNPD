using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Utility.Enums
{
    public enum Master_PIDFStatus
    {
        PIDFCreated = 1,
        PIDFPendingApproval = 2,
        PIDFApproved = 3,
        PIDFRejected = 4,
        IPDCreated = 5,
        IPDBDPendingApproval = 6,
        IPDBDApproved = 7,
        IPDBDRejected = 8,
        FinancePendingApproval = 9,
        FinanceApproved = 10,
        FinanceRejected = 11,
        FinalRejected = 12,
        FinalApproved = 13,
        Completed = 14
    }
}
