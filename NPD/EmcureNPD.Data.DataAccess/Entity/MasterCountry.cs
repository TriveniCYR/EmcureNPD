using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterCountry
    {
        public MasterCountry()
        {
            MasterCurrencyCountryMappings = new HashSet<MasterCurrencyCountryMapping>();
            MasterNationApprovalCountryMappings = new HashSet<MasterNationApprovalCountryMapping>();
            MasterRegionCountryMappings = new HashSet<MasterRegionCountryMapping>();
            MasterUserCountryMappings = new HashSet<MasterUserCountryMapping>();
            PidfIpdCountries = new HashSet<PidfIpdCountry>();
            PidfproductStrengthCountryMappings = new HashSet<PidfproductStrengthCountryMapping>();
            Pidfs = new HashSet<Pidf>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CountryCode { get; set; }
        public string IsdcountryCode { get; set; }

        public virtual ICollection<MasterCurrencyCountryMapping> MasterCurrencyCountryMappings { get; set; }
        public virtual ICollection<MasterNationApprovalCountryMapping> MasterNationApprovalCountryMappings { get; set; }
        public virtual ICollection<MasterRegionCountryMapping> MasterRegionCountryMappings { get; set; }
        public virtual ICollection<MasterUserCountryMapping> MasterUserCountryMappings { get; set; }
        public virtual ICollection<PidfIpdCountry> PidfIpdCountries { get; set; }
        public virtual ICollection<PidfproductStrengthCountryMapping> PidfproductStrengthCountryMappings { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
