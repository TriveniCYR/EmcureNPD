using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnD
    {
        public long PidfpbfrnDid { get; set; }
        public long Pidfpbfid { get; set; }
        public int? NumberOf { get; set; }
        public long StrengthId { get; set; }
        public int ExicipientProtoypeId { get; set; }
        public int ExicipientScaleUpId { get; set; }
        public int ExicipientExhibitId { get; set; }
        public decimal? TotalExicipientCosts { get; set; }
        public int PlantSupportCostId { get; set; }
        public int FillingExpensesId { get; set; }
        public int PackagingPrototypeId { get; set; }
        public int PackagingScaleUpId { get; set; }
        public int PackagingExhibitId { get; set; }
        public int? ReferenceProductDetailId { get; set; }
        public decimal? TotalPackagingCosts { get; set; }
        public int ToolingAndChangePartCostId { get; set; }
        public int CapexAndMiscellaneousExpensesId { get; set; }

        public virtual PidfPbfRnDCapexandMiscellaneousExpense CapexAndMiscellaneousExpenses { get; set; }
        public virtual PidfPbfRnDExicipientExhibit ExicipientExhibit { get; set; }
        public virtual PidfPbfRnDExicipientProtoype ExicipientProtoype { get; set; }
        public virtual PidfPbfRnDExicipientScaleUp ExicipientScaleUp { get; set; }
        public virtual PidfPbfRnDFillingExpense FillingExpenses { get; set; }
        public virtual PidfPbfRnDPackagingExhibit PackagingExhibit { get; set; }
        public virtual PidfPbfRnDPackagingPrototype PackagingPrototype { get; set; }
        public virtual PidfPbfRnDPackagingScaleUp PackagingScaleUp { get; set; }
        public virtual Pidf Pidfpbf { get; set; }
        public virtual PidfPbfRnDPlantSupportCost PlantSupportCost { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
        public virtual PidfPbfRnDToolingandChangePartCost ToolingAndChangePartCost { get; set; }
    }
}
