using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class DashboardRequestVM
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string countryName { get; set; }
    }
}
