﻿using System;
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
            PidfApiRnDs = new HashSet<PidfApiRnD>();
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfIpds = new HashSet<PidfIpd>();
            PidfMedicals = new HashSet<PidfMedical>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
            PidfPbfs = new HashSet<PidfPbf>();
            Pidfapidetails = new HashSet<Pidfapidetail>();
            PidfproductStrengths = new HashSet<PidfproductStrength>();
            PidfstatusHistories = new HashSet<PidfstatusHistory>();
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public long Pidfid { get; set; }
        public string Pidfno { get; set; }
        public int OralId { get; set; }
        public int UnitofMeasurementId { get; set; }
        public int DosageFormId { get; set; }
        public int? PackagingTypeId { get; set; }
        public int BusinessUnitId { get; set; }
        public string MoleculeName { get; set; }
        public string BrandName { get; set; }
        public string ApprovedGenerics { get; set; }
        public string LaunchedGenerics { get; set; }
        public string Rfdbrand { get; set; }
        public string Rfdapplicant { get; set; }
        public int RfdcountryId { get; set; }
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
        public int LastStatusId { get; set; }
        public bool? InHouses { get; set; }
        public int? MarketExtenstionId { get; set; }
        public int? Diaid { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedDate { get; set; }

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
        public virtual ICollection<PidfApiRnD> PidfApiRnDs { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfIpd> PidfIpds { get; set; }
        public virtual ICollection<PidfMedical> PidfMedicals { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
        public virtual ICollection<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual ICollection<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual ICollection<PidfstatusHistory> PidfstatusHistories { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
