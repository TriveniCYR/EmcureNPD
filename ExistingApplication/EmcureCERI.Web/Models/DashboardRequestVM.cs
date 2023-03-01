using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class DashboardRequestVM
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string countryName { get; set; }
    }
}
