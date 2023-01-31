using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IDashboardReportService
    {
        DashboardDetails dashboardDetails(string FromDate, string Todate, string CountryName, int CompanyID);
    }
}
