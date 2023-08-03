﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterPbfworkflowTask
    {
        public int PbfWorkFlowTaskId { get; set; }
        public string PbfworkFlowTaskName { get; set; }
        public int PbfWorkFlowId { get; set; }
        public int? TaskLevel { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}