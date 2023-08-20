using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterBusinessUnit
    {
        public MasterBusinessUnit()
        {
            MasterBusinessUnitRegionMappings = new HashSet<MasterBusinessUnitRegionMapping>();
            MasterDepartmentBusinessUnitMappings = new HashSet<MasterDepartmentBusinessUnitMapping>();
            MasterUserBusinessUnitMappings = new HashSet<MasterUserBusinessUnitMapping>();
            PidfApiIpds = new HashSet<PidfApiIpd>();
            PidfBusinessUnitInteresteds = new HashSet<PidfBusinessUnitInterested>();
            PidfCommercialMasters = new HashSet<PidfCommercialMaster>();
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfIpds = new HashSet<PidfIpd>();
            PidfPbfGenerals = new HashSet<PidfPbfGeneral>();
            PidfPbfMarketMappings = new HashSet<PidfPbfMarketMapping>();
            PidfPbfRnDFillingExpenses = new HashSet<PidfPbfRnDFillingExpense>();
            Pidfapidetails = new HashSet<Pidfapidetail>();
            Pidfimsdata = new HashSet<Pidfimsdatum>();
            PidfproductStrengths = new HashSet<PidfproductStrength>();
            Pidfs = new HashSet<Pidf>();
        }

        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public bool? IsDomestic { get; set; }

        public virtual ICollection<MasterBusinessUnitRegionMapping> MasterBusinessUnitRegionMappings { get; set; }
        public virtual ICollection<MasterDepartmentBusinessUnitMapping> MasterDepartmentBusinessUnitMappings { get; set; }
        public virtual ICollection<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual ICollection<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual ICollection<PidfBusinessUnitInterested> PidfBusinessUnitInteresteds { get; set; }
        public virtual ICollection<PidfCommercialMaster> PidfCommercialMasters { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfIpd> PidfIpds { get; set; }
        public virtual ICollection<PidfPbfGeneral> PidfPbfGenerals { get; set; }
        public virtual ICollection<PidfPbfMarketMapping> PidfPbfMarketMappings { get; set; }
        public virtual ICollection<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual ICollection<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual ICollection<Pidfimsdatum> Pidfimsdata { get; set; }
        public virtual ICollection<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
