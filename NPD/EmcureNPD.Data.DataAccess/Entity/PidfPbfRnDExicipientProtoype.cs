﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDExicipientProtoype
    {
        public PidfPbfRnDExicipientProtoype()
        {
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
        }

        public int ExicipientProtoypeId { get; set; }
        public long StrengthId { get; set; }
        public string ExicipientPrototype { get; set; }
        public decimal? Cost { get; set; }
        public string DosagePerUnit { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual PidfproductStrength Strength { get; set; }
        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
    }
}
