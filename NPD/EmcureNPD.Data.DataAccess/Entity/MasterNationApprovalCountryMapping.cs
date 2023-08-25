using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterNationApprovalCountryMapping
    {
        public int NationApprovalCountryId { get; set; }
        public int? NationApprovalId { get; set; }
        public int? CountryId { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual MasterNationApproval NationApproval { get; set; }
    }
}
