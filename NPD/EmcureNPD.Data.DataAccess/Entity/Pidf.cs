using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class Pidf
    {
        public Pidf()
        {
            MasterNotifications = new HashSet<MasterNotification>();
            PidfApiCharters = new HashSet<PidfApiCharter>();
            PidfApiIpds = new HashSet<PidfApiIpd>();
            PidfApiMasters = new HashSet<PidfApiMaster>();
            PidfCommercialMasters = new HashSet<PidfCommercialMaster>();
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfFinanceProjections = new HashSet<PidfFinanceProjection>();
            PidfIpds = new HashSet<PidfIpd>();
            PidfManagementApprovalStatusHistories = new HashSet<PidfManagementApprovalStatusHistory>();
            PidfMedicals = new HashSet<PidfMedical>();
            PidfPbfs = new HashSet<PidfPbf>();
            Pidfapidetails = new HashSet<Pidfapidetail>();
            Pidfimsdata = new HashSet<Pidfimsdatum>();
            PidfproductStrengths = new HashSet<PidfproductStrength>();
            PidfstatusHistories = new HashSet<PidfstatusHistory>();
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public long Pidfid { get; set; }
        public string Pidfno { get; set; }
        public int? OralId { get; set; }
        public int? UnitofMeasurementId { get; set; }
        public int? DosageFormId { get; set; }
        public int? PackagingTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string MoleculeName { get; set; }
        public string BrandName { get; set; }
        public string ApprovedGenerics { get; set; }
        public string LaunchedGenerics { get; set; }
        public string Rfdbrand { get; set; }
        public string Rfdapplicant { get; set; }
        public int? RfdcountryId { get; set; }
        public string Rfdindication { get; set; }
        public string Rfdinnovators { get; set; }
        public string RfdinitialRevenuePotential { get; set; }
        public string RfdpriceDiscounting { get; set; }
        public string RfdcommercialBatchSize { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public int StatusId { get; set; }
        public int? LastStatusId { get; set; }
        public bool? InHouses { get; set; }
        public int? MarketExtenstionId { get; set; }
        public int? Diaid { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedDate { get; set; }
        public string StatusRemark { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterDium Dia { get; set; }
        public virtual MasterDosageForm DosageForm { get; set; }
        public virtual MasterPidfstatus LastStatus { get; set; }
        public virtual MasterMarketExtenstion MarketExtenstion { get; set; }
        public virtual MasterOral Oral { get; set; }
        public virtual MasterPackagingType PackagingType { get; set; }
        public virtual MasterCountry Rfdcountry { get; set; }
        public virtual MasterPidfstatus Status { get; set; }
        public virtual MasterUnitofMeasurement UnitofMeasurement { get; set; }
        public virtual ICollection<MasterNotification> MasterNotifications { get; set; }
        public virtual ICollection<PidfApiCharter> PidfApiCharters { get; set; }
        public virtual ICollection<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual ICollection<PidfApiMaster> PidfApiMasters { get; set; }
        public virtual ICollection<PidfCommercialMaster> PidfCommercialMasters { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfFinanceProjection> PidfFinanceProjections { get; set; }
        public virtual ICollection<PidfIpd> PidfIpds { get; set; }
        public virtual ICollection<PidfManagementApprovalStatusHistory> PidfManagementApprovalStatusHistories { get; set; }
        public virtual ICollection<PidfMedical> PidfMedicals { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
        public virtual ICollection<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual ICollection<Pidfimsdatum> Pidfimsdata { get; set; }
        public virtual ICollection<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual ICollection<PidfstatusHistory> PidfstatusHistories { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
