using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUnitofMeasurement
    {
        public MasterUnitofMeasurement()
        {
            PidfproductStrengths = new HashSet<PidfproductStrength>();
            Pidfs = new HashSet<Pidf>();
        }

        public int UnitofMeasurementId { get; set; }
        public string UnitofMeasurementName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
