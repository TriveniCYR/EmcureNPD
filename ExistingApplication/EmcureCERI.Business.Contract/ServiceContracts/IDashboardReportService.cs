using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDashboardReportService
    {
        DashboardDetails dashboardDetails(string FromDate, string Todate, string CountryName, int CompanyID);
    }
}
