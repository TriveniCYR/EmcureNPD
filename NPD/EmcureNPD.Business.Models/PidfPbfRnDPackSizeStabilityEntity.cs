﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfRnDPackSizeStabilityEntity
    {
        public long PackSizeStabilityId { get; set; }
        public long Pidfid { get; set; }
        public long PbfgeneralId { get; set; }
        public int? StrengthId { get; set; }
        public int? PackSizeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}