using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalAmvcost
    {
        public PidfPbfAnalyticalAmvcost()
        {
            PidfPbfAnalyticalAmvcostStrengthMappings = new HashSet<PidfPbfAnalyticalAmvcostStrengthMapping>();
        }

        public long TotalAmvcostId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? TotalAmvcost { get; set; }
        public string TotalAmvtitle { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual ICollection<PidfPbfAnalyticalAmvcostStrengthMapping> PidfPbfAnalyticalAmvcostStrengthMappings { get; set; }
    }
}
