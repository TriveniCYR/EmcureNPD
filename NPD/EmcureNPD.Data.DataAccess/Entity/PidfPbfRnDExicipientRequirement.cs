﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDExicipientRequirement
    {
        public int ExicipientRequirementId { get; set; }
        public long PidfPbfGeneralId { get; set; }
        public long? BusinessUnitId { get; set; }
        public long? StrengthId { get; set; }
        public string ExicipientPrototype { get; set; }
        public double? RsPerKg { get; set; }
        public string MgPerUnitDosage { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfPbfGeneral PidfPbfGeneral { get; set; }
    }
}
