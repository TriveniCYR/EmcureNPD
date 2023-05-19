using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using EmcureNPD.Resource.Resources;
using System.Xml.Linq;

namespace EmcureNPD.Business.Models
{
    public class PidfProductStregthEntity
    {
        public long PidfproductStrengthId { get; set; }
        public long Pidfid { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public string Strength { get; set; }
        public int? UnitofMeasurementId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
    }
}
