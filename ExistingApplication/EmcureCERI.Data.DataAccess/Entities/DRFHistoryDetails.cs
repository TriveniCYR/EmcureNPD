using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DRFHistoryDetails
    {
        public int SrNo { get; set; }
        public Int64 DRFHID { get; set; }
        public int InitializationID { get; set; }
        public string DRFNo { get; set; }
        public string GenericName { get; set; }
        public string HistroyDescription { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime DRFDate { get; set; }

    }
}
