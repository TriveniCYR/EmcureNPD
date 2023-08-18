using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PIDFAPIOutsourceData
    {
        public int APIOutsourceDataId { get; set; }
        public int APIOutsourceId { get; set; }
        public long PIDFId { get; set; }
        public string Primary { get; set; }
        public string Potential_Alt_1 { get; set; }
        public string Potential_Alt_2 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
