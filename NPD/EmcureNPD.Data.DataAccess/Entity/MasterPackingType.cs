using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterPackingType
    {
        public MasterPackingType()
        {
            PidfPbfRnDPackagingMaterials = new HashSet<PidfPbfRnDPackagingMaterial>();
        }

        public int PackingTypeId { get; set; }
        public string PackingTypeName { get; set; }
        public double? PackingCost { get; set; }
        public int? Ref { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfPbfRnDPackagingMaterial> PidfPbfRnDPackagingMaterials { get; set; }
    }
}
