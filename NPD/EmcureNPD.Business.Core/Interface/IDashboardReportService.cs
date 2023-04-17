using EmcureNPD.Data.DataAccess.Entity;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IDashboardReportService
    {
        DashboardDetails dashboardDetails(string FromDate, string Todate, string CountryName, int CompanyID);
    }
}