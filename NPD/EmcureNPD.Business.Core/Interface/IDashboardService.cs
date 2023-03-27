using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IDashboardService
    {
        Task<dynamic> FillDropdown();        
        Task<dynamic> GetPIDFByYearAndBusinessUnit(int id, string fromDate,string toDate);        
    }
}
