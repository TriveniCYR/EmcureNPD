using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNationApprovalCountryMapping
    {
        public int NationApprovalCountryId { get; set; }
        public int NationApprovalId { get; set; }
        public int CountryId { get; set; }
        
    }
}
