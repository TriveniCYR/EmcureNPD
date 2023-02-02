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
        public string MarketExtension { get; set; }
        public string LaunchedGenerics { get; set; }
        public string Applicant { get; set; }
        public string Inidication { get; set; }
        public string DiaName { get; set; }        
        public string MoleculeName { get; set; }
        public string BrandName { get; set; }
        public string TransformFormRandDDivision { get; set; }
        public string PreviousProjectCode { get; set; }
        public string SinkCost { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

    }
}
