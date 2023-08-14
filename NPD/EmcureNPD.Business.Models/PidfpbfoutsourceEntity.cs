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
    public class PidfpbfoutsourceEntity
    {
        public int PidfpbfoutsourceId { get; set; }
        public long Pidfid { get; set; }
        public int ProjectWorkflowId { get; set; }
        public int PbfworkflowId { get; set; }
        public string SaveType { get; set; }
        
        public List<PidfpbfoutsourceTaskEntity> pidfpbfoutsourceTaskEntityList { get; set; }
    }
}
