using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmcureNPD.Resource.Resources;

namespace EmcureNPD.Business.Models
{
    public class PidfApiDetailEntity
    {
        public long Pidfapiid { get; set; }
        public long Pidfid { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public string Apiname { get; set; }
        public int ApisourcingId { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public string Apivendor { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
    }
}
