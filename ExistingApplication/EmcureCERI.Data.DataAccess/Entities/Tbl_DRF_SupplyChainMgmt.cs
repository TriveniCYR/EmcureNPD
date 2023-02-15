using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_SupplyChainMgmt
    {
        public int Id { get; set; }
        public decimal FreightCost { get; set; }
        public DateTime TentativeShipmente { get; set; }
        public DateTime TentativeDestination { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InitializationId { get; set; }
    }
}
