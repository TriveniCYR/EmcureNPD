using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmcureNPD.Business.Models
{
    public class PidfpbfoutsourceTaskEntity
    {
        public int PidfpbfoutsourceTaskId { get; set; }
        public int PidfpbfoutsourceId { get; set; }
        public string PbfworkFlowTaskName { get; set; }
        public int PbfWorkFlowId { get; set; }
        public int? TaskLevel { get; set; }
        public int? ParentId { get; set; }
        public double? Cost { get; set; }
        public string Tentative { get; set; }
    }
}
