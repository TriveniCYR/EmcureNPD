using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiIpd
    {
        public long PidfApiIpdId { get; set; }
        public long? Pidfid { get; set; }
        public string FormulationQuantity { get; set; }
        public string Development { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string PlantQc { get; set; }
        public string Total { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public int? BusinessUnitId { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
