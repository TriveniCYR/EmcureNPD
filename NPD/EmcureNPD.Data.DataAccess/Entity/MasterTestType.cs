﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterTestType
    {
        public MasterTestType()
        {
            PidfPbfAnalyticalExhibits = new HashSet<PidfPbfAnalyticalExhibit>();
        }

        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfPbfAnalyticalExhibit> PidfPbfAnalyticalExhibits { get; set; }
    }
}