using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPackagingScaleUp
    {
        public PidfPbfRnDPackagingScaleUp()
        {
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
        }

        public int PackagingScaleUpId { get; set; }
        public long StrengthId { get; set; }
        public int PackagingTypeId { get; set; }
        public string UnitofMeasurement { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual PidfproductStrength Strength { get; set; }
        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
    }
}
