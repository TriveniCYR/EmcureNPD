using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IDashboardService
    {
        Task<dynamic> FillDropdown();

        Task<dynamic> GetPIDFByYearAndBusinessUnit(int id, string fromDate, string toDate);
    }
}