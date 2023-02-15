using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DashboardDetails
    {
        //public IList<DashboardTabData> dashboardTabDatas { get; set; }
        public DashboardTabData dashboardTabDatas = new DashboardTabData();// { get; set; }
        public IList<DashboardWorldMapData> dashboardWorldMapDatas { get; set; }
    }
}
