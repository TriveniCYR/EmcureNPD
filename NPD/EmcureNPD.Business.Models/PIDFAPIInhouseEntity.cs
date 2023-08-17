using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PIDFAPIInhouseEntity
    {
        public int PIDFAPIInhouseId { get; set; }
        public int APIInhouseId { get; set; }
        public long PIDFId { get; set; }
        public string Primary { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
