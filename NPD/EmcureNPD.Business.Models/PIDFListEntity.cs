using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public partial class PIDFListEntity
    {
        public int PIDFID { get; set; }
        public string PIDFNO { get; set; }
        public string DosageFormName { get; set; }
        public string ProductPackagingName { get; set; }
        public string MoleculeName { get; set; }
        public string BrandName { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

    }
}
