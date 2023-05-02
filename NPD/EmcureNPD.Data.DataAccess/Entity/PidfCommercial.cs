using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfCommercial
    {
        public PidfCommercial()
        {
            PidfCommercialYears = new HashSet<PidfCommercialYear>();
        }

        public long PidfcommercialId { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public long PidfproductStrengthId { get; set; }
        public string MarketSizeInUnit { get; set; }
        public string ShelfLife { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int PackSizeId { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterPackSize PackSize { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual PidfproductStrength PidfproductStrength { get; set; }
        public virtual ICollection<PidfCommercialYear> PidfCommercialYears { get; set; }
    }
}
