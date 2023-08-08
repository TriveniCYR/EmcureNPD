using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterPbfworkFlowEntity
    {
        public int PbfworkFlowId { get; set; }
        public string PbfworkFlowName { get; set; }
    }
}