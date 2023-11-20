﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility;

#nullable disable

namespace EmcureNPD.Data.DataAccess.DataContext
{
    public partial class EmcureNPDDevContext : DbContext
    {
        public EmcureNPDDevContext()
        {
        }

        public EmcureNPDDevContext(DbContextOptions<EmcureNPDDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DboMasterManageUser> DboMasterManageUsers { get; set; }
        public virtual DbSet<MasterActivityType> MasterActivityTypes { get; set; }
        public virtual DbSet<MasterAnalytical> MasterAnalyticals { get; set; }
        public virtual DbSet<MasterApiCharterAnalyticalDepartment> MasterApiCharterAnalyticalDepartments { get; set; }
        public virtual DbSet<MasterApiCharterCapitalOtherExpenditure> MasterApiCharterCapitalOtherExpenditures { get; set; }
        public virtual DbSet<MasterApiCharterHeadwiseBudget> MasterApiCharterHeadwiseBudgets { get; set; }
        public virtual DbSet<MasterApiCharterManhourEstimate> MasterApiCharterManhourEstimates { get; set; }
        public virtual DbSet<MasterApiCharterPrddepartment> MasterApiCharterPrddepartments { get; set; }
        public virtual DbSet<MasterApiCharterTimelineInMonth> MasterApiCharterTimelineInMonths { get; set; }
        public virtual DbSet<MasterApiInhouse> MasterApiInhouses { get; set; }
        public virtual DbSet<MasterApiOutsource> MasterApiOutsources { get; set; }
        public virtual DbSet<MasterApisourcing> MasterApisourcings { get; set; }
        public virtual DbSet<MasterAuditLog> MasterAuditLogs { get; set; }
        public virtual DbSet<MasterBatchSizeNumber> MasterBatchSizeNumbers { get; set; }
        public virtual DbSet<MasterBerequirement> MasterBerequirements { get; set; }
        public virtual DbSet<MasterBusinessUnit> MasterBusinessUnits { get; set; }
        public virtual DbSet<MasterBusinessUnitRegionMapping> MasterBusinessUnitRegionMappings { get; set; }
        public virtual DbSet<MasterCountry> MasterCountries { get; set; }
        public virtual DbSet<MasterCurrency> MasterCurrencies { get; set; }
        public virtual DbSet<MasterCurrencyCountryMapping> MasterCurrencyCountryMappings { get; set; }
        public virtual DbSet<MasterDepartment> MasterDepartments { get; set; }
        public virtual DbSet<MasterDepartmentBusinessUnitMapping> MasterDepartmentBusinessUnitMappings { get; set; }
        public virtual DbSet<MasterDium> MasterDia { get; set; }
        public virtual DbSet<MasterDosage> MasterDosages { get; set; }
        public virtual DbSet<MasterDosageForm> MasterDosageForms { get; set; }
        public virtual DbSet<MasterEmailLog> MasterEmailLogs { get; set; }
        public virtual DbSet<MasterException> MasterExceptions { get; set; }
        public virtual DbSet<MasterExcipientRequirement> MasterExcipientRequirements { get; set; }
        public virtual DbSet<MasterExipient> MasterExipients { get; set; }
        public virtual DbSet<MasterExpenseRegion> MasterExpenseRegions { get; set; }
        public virtual DbSet<MasterExtensionApplication> MasterExtensionApplications { get; set; }
        public virtual DbSet<MasterFilingType> MasterFilingTypes { get; set; }
        public virtual DbSet<MasterFinalSelection> MasterFinalSelections { get; set; }
        public virtual DbSet<MasterFormRnDdivision> MasterFormRnDdivisions { get; set; }
        public virtual DbSet<MasterFormulation> MasterFormulations { get; set; }
        public virtual DbSet<MasterHeadWiseBudgetActivity> MasterHeadWiseBudgetActivities { get; set; }
        public virtual DbSet<MasterManufacturing> MasterManufacturings { get; set; }
        public virtual DbSet<MasterMarketExtenstion> MasterMarketExtenstions { get; set; }
        public virtual DbSet<MasterModule> MasterModules { get; set; }
        public virtual DbSet<MasterNationApproval> MasterNationApprovals { get; set; }
        public virtual DbSet<MasterNationApprovalCountryMapping> MasterNationApprovalCountryMappings { get; set; }
        public virtual DbSet<MasterNotification> MasterNotifications { get; set; }
        public virtual DbSet<MasterNotificationUser> MasterNotificationUsers { get; set; }
        public virtual DbSet<MasterOral> MasterOrals { get; set; }
        public virtual DbSet<MasterPackSize> MasterPackSizes { get; set; }
        public virtual DbSet<MasterPackagingType> MasterPackagingTypes { get; set; }
        public virtual DbSet<MasterPackingType> MasterPackingTypes { get; set; }
        public virtual DbSet<MasterPatentStrategy> MasterPatentStrategies { get; set; }
        public virtual DbSet<MasterPbfworkFlow> MasterPbfworkFlows { get; set; }
        public virtual DbSet<MasterPbfworkflowTask> MasterPbfworkflowTasks { get; set; }
        public virtual DbSet<MasterPidfstatus> MasterPidfstatuses { get; set; }
        public virtual DbSet<MasterPlant> MasterPlants { get; set; }
        public virtual DbSet<MasterPlantLine> MasterPlantLines { get; set; }
        public virtual DbSet<MasterProductStrength> MasterProductStrengths { get; set; }
        public virtual DbSet<MasterProductType> MasterProductTypes { get; set; }
        public virtual DbSet<MasterProjectActivity> MasterProjectActivities { get; set; }
        public virtual DbSet<MasterProjectPriority> MasterProjectPriorities { get; set; }
        public virtual DbSet<MasterProjectStatus> MasterProjectStatuses { get; set; }
        public virtual DbSet<MasterRegion> MasterRegions { get; set; }
        public virtual DbSet<MasterRegionCountryMapping> MasterRegionCountryMappings { get; set; }
        public virtual DbSet<MasterRole> MasterRoles { get; set; }
        public virtual DbSet<MasterSubModule> MasterSubModules { get; set; }
        public virtual DbSet<MasterTestLicense> MasterTestLicenses { get; set; }
        public virtual DbSet<MasterTestType> MasterTestTypes { get; set; }
        public virtual DbSet<MasterTransform> MasterTransforms { get; set; }
        public virtual DbSet<MasterTypeOfSubmission> MasterTypeOfSubmissions { get; set; }
        public virtual DbSet<MasterUnitofMeasurement> MasterUnitofMeasurements { get; set; }
        public virtual DbSet<MasterUser> MasterUsers { get; set; }
        public virtual DbSet<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual DbSet<MasterUserCountryMapping> MasterUserCountryMappings { get; set; }
        public virtual DbSet<MasterUserDepartmentMapping> MasterUserDepartmentMappings { get; set; }
        public virtual DbSet<MasterUserRegionMapping> MasterUserRegionMappings { get; set; }
        public virtual DbSet<MasterWishListType> MasterWishListTypes { get; set; }
        public virtual DbSet<MasterWorkFlowTask> MasterWorkFlowTasks { get; set; }
        public virtual DbSet<MasterWorkFlowTask1> MasterWorkFlowTasks1 { get; set; }
        public virtual DbSet<MasterWorkflow> MasterWorkflows { get; set; }
        public virtual DbSet<PbfGeneralTdp> PbfGeneralTdps { get; set; }
        public virtual DbSet<Pidf> Pidfs { get; set; }
        public virtual DbSet<PidfApiCharter> PidfApiCharters { get; set; }
        public virtual DbSet<PidfApiCharterAnalyticalDepartment> PidfApiCharterAnalyticalDepartments { get; set; }
        public virtual DbSet<PidfApiCharterCapitalOtherExpenditure> PidfApiCharterCapitalOtherExpenditures { get; set; }
        public virtual DbSet<PidfApiCharterHeadwiseBudget> PidfApiCharterHeadwiseBudgets { get; set; }
        public virtual DbSet<PidfApiCharterManhourEstimate> PidfApiCharterManhourEstimates { get; set; }
        public virtual DbSet<PidfApiCharterPrddepartment> PidfApiCharterPrddepartments { get; set; }
        public virtual DbSet<PidfApiCharterTimelineInMonth> PidfApiCharterTimelineInMonths { get; set; }
        public virtual DbSet<PidfApiInhouse> PidfApiInhouses { get; set; }
        public virtual DbSet<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual DbSet<PidfApiIpdExcel> PidfApiIpdExcels { get; set; }
        public virtual DbSet<PidfApiMaster> PidfApiMasters { get; set; }
        public virtual DbSet<PidfApiOutsourceDatum> PidfApiOutsourceData { get; set; }
        public virtual DbSet<PidfApiRnD> PidfApiRnDs { get; set; }
        public virtual DbSet<PidfBusinessUnit> PidfBusinessUnits { get; set; }
        public virtual DbSet<PidfBusinessUnitCountry> PidfBusinessUnitCountries { get; set; }
        public virtual DbSet<PidfBusinessUnitInterested> PidfBusinessUnitInteresteds { get; set; }
        public virtual DbSet<PidfCommercial> PidfCommercials { get; set; }
        public virtual DbSet<PidfCommercialMaster> PidfCommercialMasters { get; set; }
        public virtual DbSet<PidfCommercialYear> PidfCommercialYears { get; set; }
        public virtual DbSet<PidfFinance> PidfFinances { get; set; }
        public virtual DbSet<PidfFinanceBatchSizeCoating> PidfFinanceBatchSizeCoatings { get; set; }
        public virtual DbSet<PidfFinanceProjection> PidfFinanceProjections { get; set; }
        public virtual DbSet<PidfIpd> PidfIpds { get; set; }
        public virtual DbSet<PidfIpdCountry> PidfIpdCountries { get; set; }
        public virtual DbSet<PidfIpdCountryExcel> PidfIpdCountryExcels { get; set; }
        public virtual DbSet<PidfIpdExcel> PidfIpdExcels { get; set; }
        public virtual DbSet<PidfIpdGeneral> PidfIpdGenerals { get; set; }
        public virtual DbSet<PidfIpdGeneralExcel> PidfIpdGeneralExcels { get; set; }
        public virtual DbSet<PidfIpdPatentDetail> PidfIpdPatentDetails { get; set; }
        public virtual DbSet<PidfIpdPatentDetailsExcel> PidfIpdPatentDetailsExcels { get; set; }
        public virtual DbSet<PidfIpdRegion> PidfIpdRegions { get; set; }
        public virtual DbSet<PidfIpdRegionExcel> PidfIpdRegionExcels { get; set; }
        public virtual DbSet<PidfManagementApprovalStatusHistory> PidfManagementApprovalStatusHistories { get; set; }
        public virtual DbSet<PidfMedical> PidfMedicals { get; set; }
        public virtual DbSet<PidfMedicalFile> PidfMedicalFiles { get; set; }
        public virtual DbSet<PidfPbf> PidfPbfs { get; set; }
        public virtual DbSet<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual DbSet<PidfPbfAnalyticalAmvcost> PidfPbfAnalyticalAmvcosts { get; set; }
        public virtual DbSet<PidfPbfAnalyticalAmvcostStrengthMapping> PidfPbfAnalyticalAmvcostStrengthMappings { get; set; }
        public virtual DbSet<PidfPbfAnalyticalCost> PidfPbfAnalyticalCosts { get; set; }
        public virtual DbSet<PidfPbfAnalyticalCostStrengthMapping> PidfPbfAnalyticalCostStrengthMappings { get; set; }
        public virtual DbSet<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual DbSet<PidfPbfGeneral> PidfPbfGenerals { get; set; }
        public virtual DbSet<PidfPbfGeneralRnd> PidfPbfGeneralRnds { get; set; }
        public virtual DbSet<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual DbSet<PidfPbfHeadWiseBudget> PidfPbfHeadWiseBudgets { get; set; }
        public virtual DbSet<PidfPbfMarketMapping> PidfPbfMarketMappings { get; set; }
        public virtual DbSet<PidfPbfOutsource> PidfPbfOutsources { get; set; }
        public virtual DbSet<PidfPbfOutsourceTask> PidfPbfOutsourceTasks { get; set; }
        public virtual DbSet<PidfPbfPhaseWiseBudget> PidfPbfPhaseWiseBudgets { get; set; }
        public virtual DbSet<PidfPbfRa> PidfPbfRas { get; set; }
        public virtual DbSet<PidfPbfReferenceProductDetail> PidfPbfReferenceProductDetails { get; set; }
        public virtual DbSet<PidfPbfRnDApirequirement> PidfPbfRnDApirequirements { get; set; }
        public virtual DbSet<PidfPbfRnDCapexMiscellaneousExpense> PidfPbfRnDCapexMiscellaneousExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientRequirement> PidfPbfRnDExicipientRequirements { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientScaleUp> PidfPbfRnDExicipientScaleUps { get; set; }
        public virtual DbSet<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDManPowerCost> PidfPbfRnDManPowerCosts { get; set; }
        public virtual DbSet<PidfPbfRnDMaster> PidfPbfRnDMasters { get; set; }
        public virtual DbSet<PidfPbfRnDPackSizeStability> PidfPbfRnDPackSizeStabilities { get; set; }
        public virtual DbSet<PidfPbfRnDPackagingMaterial> PidfPbfRnDPackagingMaterials { get; set; }
        public virtual DbSet<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual DbSet<PidfPbfRnDReferenceProductDetail> PidfPbfRnDReferenceProductDetails { get; set; }
        public virtual DbSet<PidfPbfRnDToolingChangepart> PidfPbfRnDToolingChangeparts { get; set; }
        public virtual DbSet<PidfPbfRndBatchSize> PidfPbfRndBatchSizes { get; set; }
        public virtual DbSet<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual DbSet<Pidfimsdatum> Pidfimsdata { get; set; }
        public virtual DbSet<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual DbSet<PidfproductStrengthCountryMapping> PidfproductStrengthCountryMappings { get; set; }
        public virtual DbSet<PidfstatusHistory> PidfstatusHistories { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<RoleModulePermission> RoleModulePermissions { get; set; }
        public virtual DbSet<TblSessionManager> TblSessionManagers { get; set; }
        public virtual DbSet<TblWishList> TblWishLists { get; set; }
        public virtual DbSet<UserSessionLogMaster> UserSessionLogMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				optionsBuilder.UseSqlServer(DatabaseConnection.NPDDatabaseConnection);
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("emcurenpddev_dbUser")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DboMasterManageUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("dbo.Master_ManageUser");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterActivityType>(entity =>
            {
                entity.HasKey(e => e.ActivityTypeId);

                entity.ToTable("Master_ActivityType", "dbo");

                entity.Property(e => e.ActivityTypeName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterAnalytical>(entity =>
            {
                entity.HasKey(e => e.AnalyticalId)
                    .HasName("PK_Master_AnalyticalGL");

                entity.ToTable("Master_Analytical", "dbo");

                entity.Property(e => e.AnalyticalName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterApiCharterAnalyticalDepartment>(entity =>
            {
                entity.ToTable("Master_API_Charter_AnalyticalDepartment", "dbo");

                entity.Property(e => e.MasterApiCharterAnalyticalDepartmentId).HasColumnName("Master_API_Charter_AnalyticalDepartmentId");

                entity.Property(e => e.Amt)
                    .HasMaxLength(100)
                    .HasColumnName("AMT");

                entity.Property(e => e.Amv)
                    .HasMaxLength(100)
                    .HasColumnName("AMV");

                entity.Property(e => e.Ard)
                    .HasMaxLength(100)
                    .HasColumnName("ARD");

                entity.Property(e => e.Impurity).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Stability).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiCharterCapitalOtherExpenditure>(entity =>
            {
                entity.ToTable("Master_API_Charter_CapitalOtherExpenditure", "dbo");

                entity.Property(e => e.MasterApiCharterCapitalOtherExpenditureId).HasColumnName("Master_API_Charter_CapitalOtherExpenditureId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiCharterHeadwiseBudget>(entity =>
            {
                entity.ToTable("Master_API_Charter_HeadwiseBudget", "dbo");

                entity.Property(e => e.MasterApiCharterHeadwiseBudgetId).HasColumnName("Master_API_Charter_HeadwiseBudgetId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiCharterManhourEstimate>(entity =>
            {
                entity.HasKey(e => e.MasterApiCharterManhourEstimatesId);

                entity.ToTable("Master_API_Charter_ManhourEstimates", "dbo");

                entity.Property(e => e.MasterApiCharterManhourEstimatesId).HasColumnName("Master_API_Charter_ManhourEstimatesId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiCharterPrddepartment>(entity =>
            {
                entity.ToTable("Master_API_Charter_PRDDepartment", "dbo");

                entity.Property(e => e.MasterApiCharterPrddepartmentId).HasColumnName("Master_API_Charter_PRDDepartmentId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiCharterTimelineInMonth>(entity =>
            {
                entity.HasKey(e => e.MasterApiCharterTimelineInMonthsId)
                    .HasName("PK_PIDF_API_Charter_TimelineInMonths");

                entity.ToTable("Master_API_Charter_TimelineInMonths", "dbo");

                entity.Property(e => e.MasterApiCharterTimelineInMonthsId).HasColumnName("Master_API_Charter_TimelineInMonthsId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameValue).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterApiInhouse>(entity =>
            {
                entity.HasKey(e => e.ApiinhouseId)
                    .HasName("PK__Master_A__35911A6BB6E89F8C");

                entity.ToTable("Master_API_Inhouse", "dbo");

                entity.Property(e => e.ApiinhouseId).HasColumnName("APIInhouseId");

                entity.Property(e => e.ApiinhouseName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("APIInhouseName");
            });

            modelBuilder.Entity<MasterApiOutsource>(entity =>
            {
                entity.HasKey(e => e.ApioutsourceId)
                    .HasName("PK__Master_A__891B61835D1383FE");

                entity.ToTable("Master_API_Outsource", "dbo");

                entity.Property(e => e.ApioutsourceId).HasColumnName("APIOutsourceId");

                entity.Property(e => e.ApioutsourceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("APIOutsourceName");
            });

            modelBuilder.Entity<MasterApisourcing>(entity =>
            {
                entity.HasKey(e => e.ApisourcingId);

                entity.ToTable("Master_APISourcing", "dbo");

                entity.Property(e => e.ApisourcingId).HasColumnName("APISourcingId");

                entity.Property(e => e.ApisourcingName)
                    .HasMaxLength(100)
                    .HasColumnName("APISourcingName");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterAuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditLogId);

                entity.ToTable("Master_AuditLog", "dbo");

                entity.Property(e => e.ActionType).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBatchSizeNumber>(entity =>
            {
                entity.HasKey(e => e.BatchSizeNumberId);

                entity.ToTable("Master_BatchSizeNumber", "dbo");

                entity.Property(e => e.BatchSizeNumberName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBerequirement>(entity =>
            {
                entity.HasKey(e => e.BerequirementId);

                entity.ToTable("Master_BERequirement", "dbo");

                entity.Property(e => e.BerequirementId).HasColumnName("BERequirementId");

                entity.Property(e => e.BerequirementName)
                    .HasMaxLength(100)
                    .HasColumnName("BERequirementName");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBusinessUnit>(entity =>
            {
                entity.HasKey(e => e.BusinessUnitId);

                entity.ToTable("Master_BusinessUnit", "dbo");

                entity.Property(e => e.BusinessUnitName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBusinessUnitRegionMapping>(entity =>
            {
                entity.HasKey(e => e.BusinessUnitCountryMappingId)
                    .HasName("PK_Master_BusinessCountryMapping");

                entity.ToTable("Master_BusinessUnitRegionMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.MasterBusinessUnitRegionMappings)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_BusinessUnitId");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.MasterBusinessUnitRegionMappings)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_CountryId");
            });

            modelBuilder.Entity<MasterCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("Master_Country", "dbo");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryCode).HasMaxLength(5);

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsdcountryCode)
                    .HasMaxLength(5)
                    .HasColumnName("ISDCountryCode");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.ToTable("Master_Currency", "dbo");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode).HasMaxLength(10);

                entity.Property(e => e.CurrencyName).HasMaxLength(100);

                entity.Property(e => e.CurrencySymbol).HasMaxLength(5);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterCurrencyCountryMapping>(entity =>
            {
                entity.HasKey(e => e.CurrencyCountryMappingId);

                entity.ToTable("Master_CurrencyCountryMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterCurrencyCountryMappings)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_CurrencyCountryMapping_Master_Country");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.MasterCurrencyCountryMappings)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_CurrencyCountryMapping_Master_Currency");
            });

            modelBuilder.Entity<MasterDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("Master_Department", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDepartmentBusinessUnitMapping>(entity =>
            {
                entity.HasKey(e => e.DepartmentBusinessUnitMappingId)
                    .HasName("PK_Master_BusinessBusinessUnitMapping");

                entity.ToTable("Master_DepartmentBusinessUnitMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.MasterDepartmentBusinessUnitMappings)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_DepartmentBusinessUnitMapping_Master_BusinessUnit");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MasterDepartmentBusinessUnitMappings)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_DepartmentId");
            });

            modelBuilder.Entity<MasterDium>(entity =>
            {
                entity.HasKey(e => e.Diaid)
                    .HasName("PK_Master_DIA_ANDA_ANDS");

                entity.ToTable("Master_DIA", "dbo");

                entity.Property(e => e.Diaid).HasColumnName("DIAId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dianame)
                    .HasMaxLength(100)
                    .HasColumnName("DIAName");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDosage>(entity =>
            {
                entity.HasKey(e => e.DosageId);

                entity.ToTable("Master_Dosage", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosageName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDosageForm>(entity =>
            {
                entity.HasKey(e => e.DosageFormId);

                entity.ToTable("Master_DosageForm", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosageFormName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterEmailLog>(entity =>
            {
                entity.HasKey(e => e.EmailLogId)
                    .HasName("PK__Master_E__E8CB41CCF4519698");

                entity.ToTable("Master_EmailLog", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Subject).HasMaxLength(1000);

                entity.Property(e => e.ToEmailAddress).HasMaxLength(1000);
            });

            modelBuilder.Entity<MasterException>(entity =>
            {
                entity.HasKey(e => e.ExceptionId);

                entity.ToTable("Master_Exception", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(500);

                entity.Property(e => e.Source).HasMaxLength(500);

                entity.Property(e => e.StrackTrace).HasMaxLength(4000);
            });

            modelBuilder.Entity<MasterExcipientRequirement>(entity =>
            {
                entity.HasKey(e => e.ExcipientRequirementId)
                    .HasName("PK__Master_E__F840444962441DA4");

                entity.ToTable("Master_ExcipientRequirement", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExcipientRequirementName).HasMaxLength(70);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExipient>(entity =>
            {
                entity.HasKey(e => e.ExipientId)
                    .HasName("PK_Master_Exipientr");

                entity.ToTable("Master_Exipient", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExipientName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExpenseRegion>(entity =>
            {
                entity.HasKey(e => e.ExpenseRegionId);

                entity.ToTable("Master_ExpenseRegion", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpenseRegionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExtensionApplication>(entity =>
            {
                entity.HasKey(e => e.ExtensionApplicationId);

                entity.ToTable("Master_ExtensionApplication", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExtensionApplicationName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFilingType>(entity =>
            {
                entity.HasKey(e => e.FilingTypeId);

                entity.ToTable("Master_FilingType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FilingTypeName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFinalSelection>(entity =>
            {
                entity.HasKey(e => e.FinalSelectionId);

                entity.ToTable("Master_FinalSelection", "dbo");

                entity.Property(e => e.FinalSelectionId).HasColumnName("FinalSelectionID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FinalSelectionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFormRnDdivision>(entity =>
            {
                entity.HasKey(e => e.FormRnDdivisionId)
                    .HasName("PK_Master_RNDDivision");

                entity.ToTable("Master_FormRnDDivision", "dbo");

                entity.Property(e => e.FormRnDdivisionId).HasColumnName("FormRnDDivisionId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormRnDdivisionName)
                    .HasMaxLength(100)
                    .HasColumnName("FormRnDDivisionName");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFormulation>(entity =>
            {
                entity.HasKey(e => e.FormulationId);

                entity.ToTable("Master_Formulation", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormulationName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterHeadWiseBudgetActivity>(entity =>
            {
                entity.HasKey(e => e.ProjectActivitiesId);

                entity.ToTable("Master_HeadWiseBudgetActivities", "dbo");

                entity.Property(e => e.ProjectActivitiesId).ValueGeneratedNever();

                entity.Property(e => e.ProjectActivitiesName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterManufacturing>(entity =>
            {
                entity.HasKey(e => e.ManufacturingId);

                entity.ToTable("Master_Manufacturing", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ManufacturingName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterMarketExtenstion>(entity =>
            {
                entity.HasKey(e => e.MarketExtenstionId);

                entity.ToTable("Master_MarketExtenstion", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MarketExtenstionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.ToTable("Master_Module", "dbo");

                entity.Property(e => e.ControlName).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleName).HasMaxLength(250);
            });

            modelBuilder.Entity<MasterNationApproval>(entity =>
            {
                entity.HasKey(e => e.NationApprovalId)
                    .HasName("PK__Master_N__F813848F1CA8F1BF");

                entity.ToTable("Master_NationApproval", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaxEop).HasColumnName("MaxEOP");

                entity.Property(e => e.MinEop).HasColumnName("MinEOP");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterNationApprovalCountryMapping>(entity =>
            {
                entity.HasKey(e => e.NationApprovalCountryId)
                    .HasName("PK__Master_N__8EAC6131075D825B");

                entity.ToTable("Master_NationApproval_CountryMapping", "dbo");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterNationApprovalCountryMappings)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Master_Na__Count__172F20A0");

                entity.HasOne(d => d.NationApproval)
                    .WithMany(p => p.MasterNationApprovalCountryMappings)
                    .HasForeignKey(d => d.NationApprovalId)
                    .HasConstraintName("FK__Master_Na__Natio__163AFC67");
            });

            modelBuilder.Entity<MasterNotification>(entity =>
			{
				entity.HasKey(e => e.NotificationId);

				entity.ToTable("Master_Notification", "dbo");

				entity.Property(e => e.CreatedDate).HasColumnType("datetime");

				entity.Property(e => e.NotificationDescription).HasMaxLength(500);

				entity.Property(e => e.NotificationTitle)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

				entity.Property(e => e.SentDatetime).HasColumnType("datetime");
			});

			modelBuilder.Entity<MasterNotificationUser>(entity =>
            {
                entity.HasKey(e => e.NotificationUserId);

                entity.ToTable("Master_Notification_User", "dbo");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MasterNotificationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_Notification_User_Master_User");
            });

            modelBuilder.Entity<MasterOral>(entity =>
            {
                entity.HasKey(e => e.OralId);

                entity.ToTable("Master_Oral", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.OralName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPackSize>(entity =>
            {
                entity.HasKey(e => e.PackSizeId)
                    .HasName("PK_Tbl_Master_PackSize");

                entity.ToTable("Master_PackSize", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PackSizeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPackagingType>(entity =>
            {
                entity.HasKey(e => e.PackagingTypeId);

                entity.ToTable("Master_PackagingType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PackagingTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPackingType>(entity =>
            {
                entity.HasKey(e => e.PackingTypeId);

                entity.ToTable("Master_PackingType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PackingTypeName).HasMaxLength(100);

                entity.Property(e => e.Unit).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterPatentStrategy>(entity =>
            {
                entity.HasKey(e => e.PatentStrategyId);

                entity.ToTable("Master_Patent_Strategy", "dbo");

                entity.Property(e => e.PatentStrategyId).HasColumnName("PatentStrategyID");

                entity.Property(e => e.PatentStrategyName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPbfworkFlow>(entity =>
            {
                entity.HasKey(e => e.PbfworkFlowId);

                entity.ToTable("Master_PBFWorkFlow", "dbo");

                entity.Property(e => e.PbfworkFlowId).HasColumnName("PBFWorkFlowId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PbfworkFlowName)
                    .HasMaxLength(100)
                    .HasColumnName("PBFWorkFlowName");
            });

            modelBuilder.Entity<MasterPbfworkflowTask>(entity =>
            {
                entity.HasKey(e => e.PbfWorkFlowTaskId);

                entity.ToTable("Master_PBFWorkflow_Task", "dbo");

                entity.Property(e => e.PbfWorkFlowTaskId).HasColumnName("PBfWorkFlowTaskId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PbfWorkFlowId).HasColumnName("PBfWorkFlowId");

                entity.Property(e => e.PbfworkFlowTaskName)
                    .HasMaxLength(100)
                    .HasColumnName("PBFWorkFlowTaskName");
            });

            modelBuilder.Entity<MasterPidfstatus>(entity =>
            {
                entity.HasKey(e => e.PidfstatusId);

                entity.ToTable("Master_PIDFStatus", "dbo");

                entity.Property(e => e.PidfstatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("PIDFStatusID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfstatus)
                    .HasMaxLength(200)
                    .HasColumnName("PIDFStatus");

                entity.Property(e => e.StatusColor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusTextColor)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterPlant>(entity =>
            {
                entity.HasKey(e => e.PlantId);

                entity.ToTable("Master_Plant", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PlantNameName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPlantLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.ToTable("Master_PlantLine", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LineName).HasMaxLength(70);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterProductStrength>(entity =>
            {
                entity.HasKey(e => e.ProductStrengthId);

                entity.ToTable("Master_ProductStrength", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductStrengthName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId);

                entity.ToTable("Master_ProductType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterProjectActivity>(entity =>
            {
                entity.HasKey(e => e.ProjectActivitiesId);

                entity.ToTable("Master_ProjectActivities", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectActivitiesName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterProjectPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("Master_Project_Priority", "dbo");

                entity.Property(e => e.PriorityId).ValueGeneratedNever();

                entity.Property(e => e.PriorityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MasterProjectStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("Master_Project_Status", "dbo");

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MasterRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("Master_Region", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.RegionName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterRegionCountryMapping>(entity =>
            {
                entity.HasKey(e => e.RegionCountryMappingId);

                entity.ToTable("Master_RegionCountryMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterRegionCountryMappings)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_RegionCountryMapping_Master_Country");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.MasterRegionCountryMappings)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_RegionId");
            });

            modelBuilder.Entity<MasterRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("Master_Role", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterSubModule>(entity =>
            {
                entity.HasKey(e => e.SubModuleId);

                entity.ToTable("Master_SubModule", "dbo");

                entity.Property(e => e.ControlName).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.SubModuleName).HasMaxLength(250);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.MasterSubModules)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_Master_SubModule_Master_Module");
            });

            modelBuilder.Entity<MasterTestLicense>(entity =>
            {
                entity.HasKey(e => e.TestLicenseId);

                entity.ToTable("Master_TestLicense", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.TestLicenseName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterTestType>(entity =>
            {
                entity.HasKey(e => e.TestTypeId);

                entity.ToTable("Master_TestType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.TestTypeCode).HasMaxLength(6);

                entity.Property(e => e.TestTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterTransform>(entity =>
            {
                entity.HasKey(e => e.TransformId)
                    .HasName("PK_Master_Transform_form");

                entity.ToTable("Master_Transform", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.TransformName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterTypeOfSubmission>(entity =>
            {
                entity.ToTable("Master_TypeOfSubmission", "dbo");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.MaxEop).HasColumnName("MaxEOP");

                entity.Property(e => e.MinEop).HasColumnName("MinEOP");

                entity.Property(e => e.TypeOfSubmission).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterUnitofMeasurement>(entity =>
            {
                entity.HasKey(e => e.UnitofMeasurementId);

                entity.ToTable("Master_UnitofMeasurement", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurementName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Master_User", "dbo");

                entity.HasIndex(e => e.EmailAddress, "EmailAddress_unique")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.AnalyticalGl).HasColumnName("AnalyticalGL");

                entity.Property(e => e.ApigroupLeader).HasColumnName("APIGroupLeader");

                entity.Property(e => e.Apiuser).HasColumnName("APIUser");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DesignationName).HasMaxLength(100);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ForgotPasswordDateTime).HasColumnType("datetime");

                entity.Property(e => e.ForgotPasswordToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FormulationGl).HasColumnName("FormulationGL");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterUserBusinessUnitMapping>(entity =>
            {
                entity.HasKey(e => e.UserBusinessUnitId);

                entity.ToTable("Master_UserBusinessUnitMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.MasterUserBusinessUnitMappings)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserBusinessUnitMapping_Master_BusinessUnit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MasterUserBusinessUnitMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserId");
            });

            modelBuilder.Entity<MasterUserCountryMapping>(entity =>
            {
                entity.HasKey(e => e.UserCountryId);

                entity.ToTable("Master_UserCountryMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterUserCountryMappings)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserCountryMapping_Master_Country");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MasterUserCountryMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserCountryMapping_Master_User");
            });

            modelBuilder.Entity<MasterUserDepartmentMapping>(entity =>
            {
                entity.HasKey(e => e.UserDepartmentId);

                entity.ToTable("Master_UserDepartmentMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MasterUserDepartmentMappings)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserDepartmentMapping_Master_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MasterUserDepartmentMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserDepartmentMapping_Master_User");
            });

            modelBuilder.Entity<MasterUserRegionMapping>(entity =>
            {
                entity.HasKey(e => e.UserRegionId);

                entity.ToTable("Master_UserRegionMapping", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.MasterUserRegionMappings)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserRegionMapping_Master_Region");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MasterUserRegionMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_UserRegionMapping_Master_User");
            });

            modelBuilder.Entity<MasterWishListType>(entity =>
            {
                entity.HasKey(e => e.WishListTypeId)
                    .HasName("PK__Master_W__15F0A6D83940699C");

                entity.ToTable("Master_WishListType", "dbo");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.WishListTyp).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterWorkFlowTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__Master_W__7C6949B16ABC543B");

                entity.ToTable("Master_WorkFlow_Tasks", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterWorkFlowTask1>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__Master_W__7C6949B1D5008284");

                entity.ToTable("Master_WorkFlowTasks", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.MasterWorkFlowTask1s)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("FK__Master_Wo__Workf__1CE7F9F6");
            });

            modelBuilder.Entity<MasterWorkflow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId);

                entity.ToTable("Master_Workflow", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.WorkflowName).HasMaxLength(100);
            });

            modelBuilder.Entity<PbfGeneralTdp>(entity =>
            {
                entity.HasKey(e => e.TradeDressProposalId)
                    .HasName("PK__pbf_gene__26222D7F3860C311");

                entity.ToTable("pbf_general_TDP", "dbo");

                entity.Property(e => e.Approch).IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Engraving)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FormulaterResponsiblePerson).HasMaxLength(100);

                entity.Property(e => e.IsEmcure).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPrimaryPackaging).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSecondryPackaging).HasDefaultValueSql("((0))");

                entity.Property(e => e.Packaging).IsUnicode(false);

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.PidfpbfGeneralId).HasColumnName("PIDFPbfGeneralId");

                entity.Property(e => e.PidfproductStrngthId).HasColumnName("PIDFProductStrngthId");

                entity.Property(e => e.PrimaryPackaging)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SecondryPackaging)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Shape)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ShelfLife)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Shelf_Life");

                entity.Property(e => e.StorageHandling)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Storage_Handling");
            });

            modelBuilder.Entity<Pidf>(entity =>
            {
                entity.ToTable("PIDF", "dbo");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.ApprovedGenerics).HasMaxLength(100);

                entity.Property(e => e.BrandName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Diaid).HasColumnName("DIAId");

                entity.Property(e => e.LaunchedGenerics).HasMaxLength(100);

                entity.Property(e => e.MarketExtenstionId).HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.MoleculeName).HasMaxLength(100);

                entity.Property(e => e.Pidfno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("PIDFNO");

                entity.Property(e => e.Rfdapplicant)
                    .HasMaxLength(100)
                    .HasColumnName("RFDApplicant");

                entity.Property(e => e.Rfdbrand)
                    .HasMaxLength(100)
                    .HasColumnName("RFDBrand");

                entity.Property(e => e.RfdcommercialBatchSize)
                    .HasMaxLength(100)
                    .HasColumnName("RFDCommercialBatchSize");

                entity.Property(e => e.RfdcountryId).HasColumnName("RFDCountryId");

                entity.Property(e => e.Rfdindication)
                    .HasMaxLength(100)
                    .HasColumnName("RFDIndication");

                entity.Property(e => e.RfdinitialRevenuePotential)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInitialRevenuePotential");

                entity.Property(e => e.Rfdinnovators)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInnovators");

                entity.Property(e => e.RfdpriceDiscounting)
                    .HasMaxLength(100)
                    .HasColumnName("RFDPriceDiscounting");

                entity.Property(e => e.StatusRemark).HasMaxLength(300);

                entity.Property(e => e.StatusUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.TradeNameDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDF_Master_BusinessUnit");

                entity.HasOne(d => d.Dia)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.Diaid)
                    .HasConstraintName("FK_PIDF_Master_DIA");

                entity.HasOne(d => d.DosageForm)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.DosageFormId)
                    .HasConstraintName("FK_PIDF_Master_DosageForm");

                entity.HasOne(d => d.LastStatus)
                    .WithMany(p => p.PidfLastStatuses)
                    .HasForeignKey(d => d.LastStatusId)
                    .HasConstraintName("FK_PIDF_Master_PIDFStatus1");

                entity.HasOne(d => d.MarketExtenstion)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.MarketExtenstionId)
                    .HasConstraintName("FK_PIDF_Master_MarketExtenstion");

                entity.HasOne(d => d.Oral)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.OralId)
                    .HasConstraintName("FK_PIDF_Master_Oral");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_PIDF_Master_PackagingType");

                entity.HasOne(d => d.Rfdcountry)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.RfdcountryId)
                    .HasConstraintName("FK_PIDF_Master_Country");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PidfStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_PIDFStatus");

                entity.HasOne(d => d.UnitofMeasurement)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.UnitofMeasurementId)
                    .HasConstraintName("FK_PIDF_Master_UnitofMeasurement");
            });

            modelBuilder.Entity<PidfApiCharter>(entity =>
            {
                entity.ToTable("PIDF_API_Charter", "dbo");

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.ApigroupLeader)
                    .HasMaxLength(100)
                    .HasColumnName("APIGroupLeader");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ManHourRates).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiCharters)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_PIDF");
            });

            modelBuilder.Entity<PidfApiCharterAnalyticalDepartment>(entity =>
            {
                entity.ToTable("PIDF_API_Charter_AnalyticalDepartment", "dbo");

                entity.Property(e => e.PidfApiCharterAnalyticalDepartmentId).HasColumnName("PIDF_API_Charter_AnalyticalDepartmentId");

                entity.Property(e => e.AnalyticalDepartmentAmtvalue)
                    .HasMaxLength(100)
                    .HasColumnName("AnalyticalDepartmentAMTValue");

                entity.Property(e => e.AnalyticalDepartmentAmvvalue)
                    .HasMaxLength(100)
                    .HasColumnName("AnalyticalDepartmentAMVValue");

                entity.Property(e => e.AnalyticalDepartmentArdvalue)
                    .HasMaxLength(100)
                    .HasColumnName("AnalyticalDepartmentARDValue");

                entity.Property(e => e.AnalyticalDepartmentImpurityValue).HasMaxLength(100);

                entity.Property(e => e.AnalyticalDepartmentStabilityValue).HasMaxLength(100);

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterAnalyticalDepartments)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_AnalyticalDepartment_PIDF_API_Charter");
            });

            modelBuilder.Entity<PidfApiCharterCapitalOtherExpenditure>(entity =>
            {
                entity.ToTable("PIDF_API_Charter_CapitalOtherExpenditure", "dbo");

                entity.Property(e => e.PidfApiCharterCapitalOtherExpenditureId).HasColumnName("PIDF_API_Charter_CapitalOtherExpenditureId");

                entity.Property(e => e.CapitalOtherExpenditureAmountValue).HasMaxLength(100);

                entity.Property(e => e.CapitalOtherExpenditureRemarkValue).HasMaxLength(100);

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterCapitalOtherExpenditures)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_CapitalOtherExpenditure_PIDF_API_Charter_CapitalOtherExpenditure");
            });

            modelBuilder.Entity<PidfApiCharterHeadwiseBudget>(entity =>
            {
                entity.ToTable("PIDF_API_Charter_HeadwiseBudget", "dbo");

                entity.Property(e => e.PidfApiCharterHeadwiseBudgetId).HasColumnName("PIDF_API_Charter_HeadwiseBudgetId");

                entity.Property(e => e.HeadwiseBudgetValue).HasMaxLength(100);

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterHeadwiseBudgets)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_HeadwiseBudget_PIDF_API_Charter");
            });

            modelBuilder.Entity<PidfApiCharterManhourEstimate>(entity =>
            {
                entity.HasKey(e => e.PidfApiCharterManhourEstimatesId);

                entity.ToTable("PIDF_API_Charter_ManhourEstimates", "dbo");

                entity.Property(e => e.PidfApiCharterManhourEstimatesId).HasColumnName("PIDF_API_Charter_ManhourEstimatesId");

                entity.Property(e => e.ManhourEstimatesCostValue).HasMaxLength(100);

                entity.Property(e => e.ManhourEstimatesHoursValue).HasMaxLength(100);

                entity.Property(e => e.ManhourEstimatesMonthsValue).HasMaxLength(100);

                entity.Property(e => e.ManhourEstimatesNoOfEmployeeValue).HasMaxLength(100);

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterManhourEstimates)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_ManhourEstimates_PIDF_API_Charter");
            });

            modelBuilder.Entity<PidfApiCharterPrddepartment>(entity =>
            {
                entity.ToTable("PIDF_API_Charter_PRDDepartment", "dbo");

                entity.Property(e => e.PidfApiCharterPrddepartmentId).HasColumnName("PIDF_API_Charter_PRDDepartmentId");

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PrddepartmentId).HasColumnName("PRDDepartmentId");

                entity.Property(e => e.PrddepartmentRawMaterialValue)
                    .HasMaxLength(100)
                    .HasColumnName("PRDDepartmentRawMaterialValue");

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterPrddepartments)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter");
            });

            modelBuilder.Entity<PidfApiCharterTimelineInMonth>(entity =>
            {
                entity.HasKey(e => e.PidfApiCharterTimelineInMonthsId)
                    .HasName("PK_PIDF_API_Charter_TimelineInMonths_1");

                entity.ToTable("PIDF_API_Charter_TimelineInMonths", "dbo");

                entity.Property(e => e.PidfApiCharterTimelineInMonthsId).HasColumnName("PIDF_API_Charter_TimelineInMonthsId");

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.TimelineInMonthsValue).HasMaxLength(100);

                entity.HasOne(d => d.PidfApiCharter)
                    .WithMany(p => p.PidfApiCharterTimelineInMonths)
                    .HasForeignKey(d => d.PidfApiCharterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Charter_TimelineInMonths_PIDF_API_Charter");
            });

            modelBuilder.Entity<PidfApiInhouse>(entity =>
            {
                entity.ToTable("PIDF_API_Inhouse", "dbo");

                entity.Property(e => e.PidfapiinhouseId).HasColumnName("PIDFAPIInhouseId");

                entity.Property(e => e.ApiinhouseId).HasColumnName("APIInhouseId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.Primary)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Apiinhouse)
                    .WithMany(p => p.PidfApiInhouses)
                    .HasForeignKey(d => d.ApiinhouseId)
                    .HasConstraintName("FK__PIDF_API___APIIn__668BE945");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiInhouses)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK__PIDF_API___PIDFI__67800D7E");
            });

            modelBuilder.Entity<PidfApiIpd>(entity =>
            {
                entity.ToTable("PIDF_API_IPD", "dbo");

                entity.Property(e => e.PidfApiIpdId).HasColumnName("PIDF_API_IPD_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DrugsCategory).HasMaxLength(200);

                entity.Property(e => e.MarketDetailsFileName).HasMaxLength(200);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.ProductStrength).HasMaxLength(200);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfApiIpds)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDF_API_IPD_BusinessUnitId");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiIpds)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK_PIDF_API_IPD_PIDFID");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.PidfApiIpds)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_PIDF_API_IPD_ProductTypeId");
            });

            modelBuilder.Entity<PidfApiIpdExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_API_IPD_Excel", "dbo");

                entity.Property(e => e.BusinessUnitId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"BusinessUnitId\"");

                entity.Property(e => e.Column11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 11");

                entity.Property(e => e.Column12)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 12");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedBy\"");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedDate\"");

                entity.Property(e => e.DrugsCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"DrugsCategory\"");

                entity.Property(e => e.MarketDetailsFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"MarketDetailsFileName\"");

                entity.Property(e => e.ModifyBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ModifyBy\"");

                entity.Property(e => e.ModifyDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ModifyDate\"");

                entity.Property(e => e.PidfApiIpdId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PIDF_API_IPD_ID\"");

                entity.Property(e => e.Pidfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PIDFID\"");

                entity.Property(e => e.ProductStrength)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ProductStrength\"");

                entity.Property(e => e.ProductTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ProductTypeId\"");
            });

            modelBuilder.Entity<PidfApiMaster>(entity =>
            {
                entity.ToTable("PIDF_API_Master", "dbo");

                entity.Property(e => e.PidfapimasterId).HasColumnName("PIDFAPIMasterId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiMasters)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_Master_PIDF");
            });

            modelBuilder.Entity<PidfApiOutsourceDatum>(entity =>
            {
                entity.HasKey(e => e.ApioutsourceDataId)
                    .HasName("PK__PIDF_API__994B5836112CF5A2");

                entity.ToTable("PIDF_API_Outsource_Data", "dbo");

                entity.Property(e => e.ApioutsourceDataId).HasColumnName("APIOutsourceDataId");

                entity.Property(e => e.ApioutsourceId).HasColumnName("APIOutsourceId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PotentialAlt1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Potential_Alt_1");

                entity.Property(e => e.PotentialAlt2)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Potential_Alt_2");

                entity.Property(e => e.Primary)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Apioutsource)
                    .WithMany(p => p.PidfApiOutsourceData)
                    .HasForeignKey(d => d.ApioutsourceId)
                    .HasConstraintName("FK__PIDF_API___APIOu__60D30FEF");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiOutsourceData)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK__PIDF_API___PIDFI__61C73428");
            });

            modelBuilder.Entity<PidfApiRnD>(entity =>
            {
                entity.ToTable("PIDF_API_RnD", "dbo");

                entity.Property(e => e.PidfApiRnDId).HasColumnName("PIDF_API_RnD_ID");

                entity.Property(e => e.ApimarketPrice)
                    .HasMaxLength(100)
                    .HasColumnName("APIMarketPrice");

                entity.Property(e => e.ApitargetRmcCcpc)
                    .HasMaxLength(100)
                    .HasColumnName("APITargetRMC_CCPC");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Development).HasMaxLength(100);

                entity.Property(e => e.Exhibit).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.PlantQc)
                    .HasMaxLength(100)
                    .HasColumnName("PlantQC");

                entity.Property(e => e.ScaleUp).HasMaxLength(100);

                entity.Property(e => e.SponsorBusinessPartner).HasMaxLength(100);

                entity.Property(e => e.Total).HasMaxLength(100);
            });

            modelBuilder.Entity<PidfBusinessUnit>(entity =>
            {
                entity.ToTable("PIDF_BusinessUnit", "dbo");

                entity.Property(e => e.PidfbusinessUnitId).HasColumnName("PIDFBusinessUnitID");

                entity.Property(e => e.ApprovedGenerics).HasMaxLength(100);

                entity.Property(e => e.BrandName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Diaid).HasColumnName("DIAId");

                entity.Property(e => e.LaunchedGenerics).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.Rfdapplicant)
                    .HasMaxLength(100)
                    .HasColumnName("RFDApplicant");

                entity.Property(e => e.Rfdbrand)
                    .HasMaxLength(100)
                    .HasColumnName("RFDBrand");

                entity.Property(e => e.RfdcommercialBatchSize)
                    .HasMaxLength(100)
                    .HasColumnName("RFDCommercialBatchSize");

                entity.Property(e => e.RfdcountryId).HasColumnName("RFDCountryId");

                entity.Property(e => e.Rfdindication)
                    .HasMaxLength(100)
                    .HasColumnName("RFDIndication");

                entity.Property(e => e.RfdinitialRevenuePotential)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInitialRevenuePotential");

                entity.Property(e => e.Rfdinnovators)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInnovators");

                entity.Property(e => e.RfdpriceDiscounting)
                    .HasMaxLength(100)
                    .HasColumnName("RFDPriceDiscounting");

                entity.Property(e => e.TradeNameDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PidfBusinessUnitCountry>(entity =>
            {
                entity.ToTable("PIDF_BusinessUnit_Country", "dbo");

                entity.Property(e => e.PidfbusinessUnitCountryId).HasColumnName("PIDFBusinessUnitCountryId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");
            });

            modelBuilder.Entity<PidfBusinessUnitInterested>(entity =>
            {
                entity.HasKey(e => e.PidfbusinessUnitId);

                entity.ToTable("PIDF_BusinessUnit_Interested", "dbo");

                entity.Property(e => e.PidfbusinessUnitId).HasColumnName("PIDFBusinessUnitId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfBusinessUnitInteresteds)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_BusinessUnit_Interested_Master_BusinessUnit");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfBusinessUnitInteresteds)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_BusinessUnit_Interested_PIDF");
            });

            modelBuilder.Entity<PidfCommercial>(entity =>
            {
                entity.ToTable("PIDF_Commercial", "dbo");

                entity.Property(e => e.PidfcommercialId).HasColumnName("PIDFCommercialId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.MarketSizeInUnit).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PidfproductStrengthId).HasColumnName("PIDFProductStrengthId");

                entity.Property(e => e.ShelfLife).HasMaxLength(50);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfCommercials)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Master_BusinessUnit");

                entity.HasOne(d => d.PackSize)
                    .WithMany(p => p.PidfCommercials)
                    .HasForeignKey(d => d.PackSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Master_PackSize");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfCommercials)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_PIDF");

                entity.HasOne(d => d.PidfproductStrength)
                    .WithMany(p => p.PidfCommercials)
                    .HasForeignKey(d => d.PidfproductStrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfCommercialMaster>(entity =>
            {
                entity.ToTable("PIDF_Commercial_Master", "dbo");

                entity.Property(e => e.PidfcommercialMasterId).HasColumnName("PIDFCommercialMasterId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfCommercialMasters)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Master_Master_BusinessUnit");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfCommercialMasters)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Master_PIDF");
            });

            modelBuilder.Entity<PidfCommercialYear>(entity =>
            {
                entity.ToTable("PIDF_Commercial_Years", "dbo");

                entity.Property(e => e.PidfcommercialYearId).HasColumnName("PIDFCommercialYearId");

                entity.Property(e => e.Apireq)
                    .HasMaxLength(20)
                    .HasColumnName("APIReq");

                entity.Property(e => e.BrandPrice).HasMaxLength(20);

                entity.Property(e => e.CommercialBatchSize).HasMaxLength(20);

                entity.Property(e => e.FreeOfCost).HasMaxLength(20);

                entity.Property(e => e.GenericPrice).HasMaxLength(20);

                entity.Property(e => e.MarketGrowth).HasMaxLength(20);

                entity.Property(e => e.MarketSharePercentageHigh).HasMaxLength(20);

                entity.Property(e => e.MarketSharePercentageLow).HasMaxLength(20);

                entity.Property(e => e.MarketSharePercentageMedium).HasMaxLength(20);

                entity.Property(e => e.MarketShareUnitHigh).HasMaxLength(20);

                entity.Property(e => e.MarketShareUnitLow).HasMaxLength(20);

                entity.Property(e => e.MarketShareUnitMedium).HasMaxLength(20);

                entity.Property(e => e.MarketSize).HasMaxLength(20);

                entity.Property(e => e.Nsphigh)
                    .HasMaxLength(20)
                    .HasColumnName("NSPHigh");

                entity.Property(e => e.Nsplow)
                    .HasMaxLength(20)
                    .HasColumnName("NSPLow");

                entity.Property(e => e.Nspmedium)
                    .HasMaxLength(20)
                    .HasColumnName("NSPMedium");

                entity.Property(e => e.NspunitsHigh)
                    .HasMaxLength(20)
                    .HasColumnName("NSPUnitsHigh");

                entity.Property(e => e.NspunitsLow)
                    .HasMaxLength(20)
                    .HasColumnName("NSPUnitsLow");

                entity.Property(e => e.NspunitsMedium)
                    .HasMaxLength(20)
                    .HasColumnName("NSPUnitsMedium");

                entity.Property(e => e.PidfcommercialId).HasColumnName("PIDFCommercialId");

                entity.Property(e => e.PriceDiscounting).HasMaxLength(20);

                entity.Property(e => e.PriceErosion).HasMaxLength(20);

                entity.Property(e => e.Suimsvolume)
                    .HasMaxLength(20)
                    .HasColumnName("SUIMSVolume");

                entity.Property(e => e.TargetCostOfGood).HasMaxLength(20);

                entity.Property(e => e.TotalApireq)
                    .HasMaxLength(20)
                    .HasColumnName("TotalAPIReq");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.PidfCommercialYears)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Year_Master_Currency");

                entity.HasOne(d => d.FinalSelection)
                    .WithMany(p => p.PidfCommercialYears)
                    .HasForeignKey(d => d.FinalSelectionId)
                    .HasConstraintName("FK_PIDF_Commercial_Year_PIDF_Commercial_Year");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PidfCommercialYears)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Year_Master_PackagingType");

                entity.HasOne(d => d.Pidfcommercial)
                    .WithMany(p => p.PidfCommercialYears)
                    .HasForeignKey(d => d.PidfcommercialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Commercial_Year_PIDF_Commercial");
            });

            modelBuilder.Entity<PidfFinance>(entity =>
            {
                entity.HasKey(e => e.PidffinaceId)
                    .HasName("PK_PIDF_Finance_1");

                entity.ToTable("PIDF_Finance", "dbo");

                entity.Property(e => e.PidffinaceId).HasColumnName("PIDFFinaceId");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalPeriodinDays).HasMaxLength(20);

                entity.Property(e => e.BatchManufacturing).HasColumnType("datetime");

                entity.Property(e => e.BatchmanufacturingcostOrApiactualsEst)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BatchmanufacturingcostOrAPIActualsEst");

                entity.Property(e => e.BatchmanufacturingcostOrApiactualsEstPhaseEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BatchmanufacturingcostOrAPIActualsEstPhaseEndDate");

                entity.Property(e => e.Bestudies)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BEstudies");

                entity.Property(e => e.BestudiesPhaseEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BEstudiesPhaseEndDate");

                entity.Property(e => e.BioStuddyCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BioStuddyCostPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.Capex).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CapexPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Currencyid).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Entity).HasMaxLength(70);

                entity.Property(e => e.EscalationinCogs)
                    .HasMaxLength(70)
                    .HasColumnName("EscalationinCOGS");

                entity.Property(e => e.ExpectedFilling).HasColumnType("datetime");

                entity.Property(e => e.Filingfees).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FilingfeesPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.ForecastDate).HasColumnType("datetime");

                entity.Property(e => e.GestationPeriodinYears).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.GrosstoNet).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Incometaxrate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ManufacturingSiteOrPartner).HasMaxLength(70);

                entity.Property(e => e.MarketShareErosionrate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarketingAllowance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Mspersentage).HasColumnName("MSPersentage");

                entity.Property(e => e.NoSkus).HasColumnName("NoSKUs");

                entity.Property(e => e.NoSkusPhaseEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NoSKUsPhaseEndDate");

                entity.Property(e => e.NoofbatchestobemanufacturedPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PriceErosion).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Product).HasMaxLength(70);

                entity.Property(e => e.ProductLaunchDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");

                entity.Property(e => e.RandDanalyticalcost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("RandDAnalyticalcost");

                entity.Property(e => e.RandDanalyticalcostPhaseEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RandDAnalyticalcostPhaseEndDate");

                entity.Property(e => e.RegulatoryMaintenanceCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rldsamplecost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("RLDsamplecost");

                entity.Property(e => e.RldsamplecostPhaseEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RLDsamplecostPhaseEndDate");

                entity.Property(e => e.Sixmonthsstabilitycost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SixmonthsstabilitycostPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.Skus)
                    .HasMaxLength(70)
                    .HasColumnName("SKUs");

                entity.Property(e => e.TechTransfer).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TechTransferPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.ToolingAndChangeParts).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ToolingAndChangePartsPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<PidfFinanceBatchSizeCoating>(entity =>
            {
                entity.HasKey(e => e.PidffinaceBatchSizeCoatingId)
                    .HasName("PK__PIDF_Fin__6C70D23F0E6D1BC3");

                entity.ToTable("PIDF_Finance_BatchSizeCoating", "dbo");

                entity.Property(e => e.PidffinaceBatchSizeCoatingId).HasColumnName("PIDFFinaceBatchSizeCoatingId");

                entity.Property(e => e.ApiCad).HasColumnName("API_CAD");

                entity.Property(e => e.BatchsizeinLtrTabs).HasColumnName("Batchsizein_ltr_tabs");

                entity.Property(e => e.Cagrover2016By12estMatunits).HasColumnName("CAGRover2016_By_12EstMATunits");

                entity.Property(e => e.CcpcCad).HasColumnName("CCPC_CAD");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmcureCogsPack).HasColumnName("EmcureCOGs_pack");

                entity.Property(e => e.EstMat2016By12units).HasColumnName("EstMAT2016_BY_12Units");

                entity.Property(e => e.EstMat2020By12units).HasColumnName("EstMAT2020_BY_12Units");

                entity.Property(e => e.ExcipientsCad).HasColumnName("Excipients_CAD");

                entity.Property(e => e.FreightCad).HasColumnName("Freight_CAD");

                entity.Property(e => e.PidffinaceId).HasColumnName("PIDFFinaceId");

                entity.Property(e => e.PmCad).HasColumnName("PM_CAD");

                entity.Property(e => e.Skus).HasColumnName("SKus");

                entity.HasOne(d => d.Pidffinace)
                    .WithMany(p => p.PidfFinanceBatchSizeCoatings)
                    .HasForeignKey(d => d.PidffinaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Finance_BatchSizeCoating_PIDF_Finance");
            });

            modelBuilder.Entity<PidfFinanceProjection>(entity =>
            {
                entity.HasKey(e => e.FinanceProjectionId)
                    .HasName("PK__PIDF_Fin__ADB1CEA6A527AFA7");

                entity.ToTable("PIDF_Finance_Projection", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PidffinaceId).HasColumnName("PIDFFinaceId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.Year).HasMaxLength(50);

                entity.HasOne(d => d.Pidffinace)
                    .WithMany(p => p.PidfFinanceProjections)
                    .HasForeignKey(d => d.PidffinaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Finance_Projection_PIDF_Finance");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfFinanceProjections)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK_PIDF_Finance_Projection_PIDF");
            });

            modelBuilder.Entity<PidfIpd>(entity =>
            {
                entity.HasKey(e => e.Ipdid)
                    .HasName("PK__PIDF_IPD__54D2918E72B886FB");

                entity.ToTable("PIDF_IPD", "dbo");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.ApprovedGenetics).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DataExclusivity).HasMaxLength(200);

                entity.Property(e => e.FillingType).HasMaxLength(200);

                entity.Property(e => e.Innovators).HasMaxLength(100);

                entity.Property(e => e.LaunchedGenetics).HasMaxLength(100);

                entity.Property(e => e.LegalStatus).HasMaxLength(100);

                entity.Property(e => e.MarketName).HasMaxLength(200);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PatentStatus).HasMaxLength(50);

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfIpds)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDF_IPD_BusinessUnitId");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfIpds)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK_PIDF_IPD_PIDFID");
            });

            modelBuilder.Entity<PidfIpdCountry>(entity =>
            {
                entity.HasKey(e => e.IpdcountryId);

                entity.ToTable("PIDF_IPD_Country", "dbo");

                entity.Property(e => e.IpdcountryId).HasColumnName("IPDCountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PidfIpdCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_PIDF_IPD_Country_Master_Country");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.PidfIpdCountries)
                    .HasForeignKey(d => d.Ipdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_Country_PIDF_IPD");
            });

            modelBuilder.Entity<PidfIpdCountryExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_IPD_Country_Excel", "dbo");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CountryId\"");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedDate\"");

                entity.Property(e => e.IpdcountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDCountryID\"");

                entity.Property(e => e.Ipdid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDID\"");
            });

            modelBuilder.Entity<PidfIpdExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_IPD_Excel", "dbo");

                entity.Property(e => e.ApprovedGenetics)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ApprovedGenetics\"");

                entity.Property(e => e.BusinessUnitId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"BusinessUnitId\"");

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Comments\"");

                entity.Property(e => e.CostOfLitication)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CostOfLitication\"");

                entity.Property(e => e.DataExclusivity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"DataExclusivity\"");

                entity.Property(e => e.FillingType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"FillingType\"");

                entity.Property(e => e.Innovators)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Innovators\"");

                entity.Property(e => e.Ipdid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDID\"");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IsActive\"");

                entity.Property(e => e.IsComment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IsComment\"");

                entity.Property(e => e.LaunchedGenetics)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"LaunchedGenetics\"");

                entity.Property(e => e.LegalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"LegalStatus\"");

                entity.Property(e => e.MarketName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"MarketName\"");

                entity.Property(e => e.PatentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentStatus\"");

                entity.Property(e => e.Pidfid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PIDFID\"");
            });

            modelBuilder.Entity<PidfIpdGeneral>(entity =>
            {
                entity.ToTable("PIDF_IPD_General", "dbo");

                entity.Property(e => e.PidfIpdGeneralId).HasColumnName("PIDF_IPD_General_Id");

                entity.Property(e => e.ApprovedGenetics).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DataExclusivity).HasMaxLength(200);

                entity.Property(e => e.ExpectedFilingDate).HasColumnType("datetime");

                entity.Property(e => e.ExpectedLaunchDate).HasColumnType("datetime");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.LaunchedGenetics).HasMaxLength(100);

                entity.Property(e => e.LegalStatus).HasMaxLength(100);

                entity.Property(e => e.MarketExclusivityDate).HasColumnType("datetime");

                entity.Property(e => e.MarketName).HasMaxLength(200);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.PidfIpdGenerals)
                    .HasForeignKey(d => d.Ipdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_General_IPDID");
            });

            modelBuilder.Entity<PidfIpdGeneralExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_IPD_General_Excel", "dbo");

                entity.Property(e => e.ApprovedGenetics)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ApprovedGenetics\"");

                entity.Property(e => e.BusinessUnitId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"BusinessUnitId\"");

                entity.Property(e => e.Column19)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 19");

                entity.Property(e => e.Column20)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 20");

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Comments\"");

                entity.Property(e => e.CostOfLitication)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CostOfLitication\"");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CountryId\"");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedBy\"");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedDate\"");

                entity.Property(e => e.DataExclusivity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"DataExclusivity\"");

                entity.Property(e => e.ExpectedFilingDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ExpectedFilingDate\"");

                entity.Property(e => e.ExpectedLaunchDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ExpectedLaunchDate\"");

                entity.Property(e => e.Ipdid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDID\"");

                entity.Property(e => e.IsComment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IsComment\"");

                entity.Property(e => e.LaunchedGenetics)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"LaunchedGenetics\"");

                entity.Property(e => e.LegalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"LegalStatus\"");

                entity.Property(e => e.MarketExclusivityDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"MarketExclusivityDate\"");

                entity.Property(e => e.MarketName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"MarketName\"");

                entity.Property(e => e.ModifyBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ModifyBy\"");

                entity.Property(e => e.ModifyDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ModifyDate\"");

                entity.Property(e => e.PidfIpdGeneralId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PIDF_IPD_General_Id\"");
            });

            modelBuilder.Entity<PidfIpdPatentDetail>(entity =>
            {
                entity.HasKey(e => e.PatentDetailsId);

                entity.ToTable("PIDF_IPD_PatentDetails", "dbo");

                entity.Property(e => e.PatentDetailsId).HasColumnName("PatentDetailsID");

                entity.Property(e => e.BasicPatentExpiry).HasColumnType("date");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.EarliestLaunchDate).HasColumnType("date");

                entity.Property(e => e.EarliestMarketEntry).HasColumnType("date");

                entity.Property(e => e.ExtensionExpiryDate).HasColumnType("date");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.Lawfirmbeingused).HasMaxLength(100);

                entity.Property(e => e.OriginalExpiryDate).HasColumnType("date");

                entity.Property(e => e.OtherLmitingPatentDate1).HasColumnType("date");

                entity.Property(e => e.OtherLmitingPatentDate2).HasColumnType("date");

                entity.Property(e => e.PatentNumber).HasMaxLength(50);

                entity.Property(e => e.PatentStrategyOther).HasMaxLength(100);

                entity.Property(e => e.PidfIpdGeneralId).HasColumnName("PIDF_IPD_General_Id");

                entity.Property(e => e.StimatedNumberofgenericsinthe)
                    .HasMaxLength(100)
                    .HasColumnName("stimatedNumberofgenericsinthe");

                entity.Property(e => e.Strategy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.PidfIpdPatentDetails)
                    .HasForeignKey(d => d.Ipdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_PatentDetails_IPDID");
            });

            modelBuilder.Entity<PidfIpdPatentDetailsExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_IPD_PatentDetails_Excel", "dbo");

                entity.Property(e => e.AnyPatentstobeFiled)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"AnyPatentstobeFiled\"");

                entity.Property(e => e.BasicPatentExpiry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"BasicPatentExpiry\"");

                entity.Property(e => e.Column21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 21");

                entity.Property(e => e.Column22)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 22");

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Comments\"");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CountryId\"");

                entity.Property(e => e.EarliestLaunchDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"EarliestLaunchDate\"");

                entity.Property(e => e.EarliestMarketEntry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"EarliestMarketEntry\"");

                entity.Property(e => e.ExtensionExpiryDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"ExtensionExpiryDate\"");

                entity.Property(e => e.Ipdid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDID\"");

                entity.Property(e => e.Lawfirmbeingused)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Lawfirmbeingused\"");

                entity.Property(e => e.OriginalExpiryDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"OriginalExpiryDate\"");

                entity.Property(e => e.OtherLmitingPatentDate1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"OtherLmitingPatentDate1\"");

                entity.Property(e => e.OtherLmitingPatentDate2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"OtherLmitingPatentDate2\"");

                entity.Property(e => e.PatentDetailsId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentDetailsID\"");

                entity.Property(e => e.PatentNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentNumber\"");

                entity.Property(e => e.PatentStrategy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentStrategy\"");

                entity.Property(e => e.PatentStrategyOther)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentStrategyOther\"");

                entity.Property(e => e.PatentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PatentType\"");

                entity.Property(e => e.PidfIpdGeneralId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"PIDF_IPD_General_Id\"");

                entity.Property(e => e.StimatedNumberofgenericsinthe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"stimatedNumberofgenericsinthe\"");

                entity.Property(e => e.Strategy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Strategy\"");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"Type\"");
            });

            modelBuilder.Entity<PidfIpdRegion>(entity =>
            {
                entity.HasKey(e => e.IpdregionId);

                entity.ToTable("PIDF_IPD_Region", "dbo");

                entity.Property(e => e.IpdregionId).HasColumnName("IPDRegionID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.PidfIpdRegions)
                    .HasForeignKey(d => d.Ipdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_Region_PIDF_IPD");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.PidfIpdRegions)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_PIDF_IPD_Region_Master_Region");
            });

            modelBuilder.Entity<PidfIpdRegionExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PIDF_IPD_Region_Excel", "dbo");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"CreatedDate\"");

                entity.Property(e => e.Ipdid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDID\"");

                entity.Property(e => e.IpdregionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"IPDRegionID\"");

                entity.Property(e => e.RegionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("\"RegionId\"");
            });

            modelBuilder.Entity<PidfManagementApprovalStatusHistory>(entity =>
            {
                entity.HasKey(e => e.ManagementApprovalStatusHistoryId);

                entity.ToTable("PIDF_ManagementApprovalStatusHistory", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfManagementApprovalStatusHistories)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK_PIDF_ManagementApprovalStatusHistory_PIDF");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PidfManagementApprovalStatusHistories)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_PIDF_ManagementApprovalStatusHistory_Master_PIDFStatus");
            });

            modelBuilder.Entity<PidfMedical>(entity =>
            {
                entity.ToTable("PIDF_Medical", "dbo");

                entity.Property(e => e.PidfmedicalId).HasColumnName("PIDFMedicalId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfMedicals)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Medical_PIDF");
            });

            modelBuilder.Entity<PidfMedicalFile>(entity =>
            {
                entity.ToTable("PIDF_Medical_File", "dbo");

                entity.Property(e => e.PidfmedicalFileId).HasColumnName("PIDFMedicalFileId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PidfmedicalId).HasColumnName("PIDFMedicalId");
            });

            modelBuilder.Entity<PidfPbf>(entity =>
            {
                entity.ToTable("PIDF_PBF", "dbo");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.Property(e => e.BatchManifacturingDate).HasColumnType("datetime");

                entity.Property(e => e.BerequirementId).HasColumnName("BERequirementId");

                entity.Property(e => e.BusinessRelationable).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FillingDateDate).HasColumnType("datetime");

                entity.Property(e => e.FormRnDdivisionId).HasColumnName("FormRnDDivisionId");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.NumberOfApprovedAnda)
                    .HasMaxLength(100)
                    .HasColumnName("NumberOfApprovedANDA");

                entity.Property(e => e.PatentStatus).HasMaxLength(100);

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.ProjectInitiationDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectManager).HasMaxLength(100);

                entity.Property(e => e.ProjectName).HasMaxLength(100);

                entity.Property(e => e.RnDhead)
                    .HasMaxLength(100)
                    .HasColumnName("RnDHead");

                entity.Property(e => e.ScopeObjectives).HasMaxLength(100);

                entity.Property(e => e.SponsorBusinessPartner).HasMaxLength(100);

                entity.HasOne(d => d.Berequirement)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.BerequirementId)
                    .HasConstraintName("FK_PIDF_PBF_Master_BERequirement");

                entity.HasOne(d => d.Dosage)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.DosageId)
                    .HasConstraintName("FK_PIDF_PBF_Master_Dosage1");

                entity.HasOne(d => d.FillingType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.FillingTypeId)
                    .HasConstraintName("FK_PIDF_PBF_Master_FilingType1");

                entity.HasOne(d => d.FormRnDdivision)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.FormRnDdivisionId)
                    .HasConstraintName("FK_PIDF_PBF_Master_FormRnDDivision");

                entity.HasOne(d => d.Manufacturing)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.ManufacturingId)
                    .HasConstraintName("FK_PIDF_PBF_Master_Manufacturing1");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_PIDF_PBF_Master_PackagingType1");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_PIDF1");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_PIDF_PBF_Master_Plant1");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_PIDF_PBF_Master_ProductType1");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("FK_PIDF_PBF_Master_Workflow1");
            });

            modelBuilder.Entity<PidfPbfAnalytical>(entity =>
            {
                entity.ToTable("PIDF_PBF_Analytical", "dbo");

                entity.Property(e => e.PidfpbfanalyticalId).HasColumnName("PIDFPBFAnalyticalId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.PrototypeDevelopment).HasMaxLength(100);

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfAnalyticals)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfAnalyticals)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_PIDFProductStrength");

                entity.HasOne(d => d.TestType)
                    .WithMany(p => p.PidfPbfAnalyticals)
                    .HasForeignKey(d => d.TestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_Master_TestType");
            });

            modelBuilder.Entity<PidfPbfAnalyticalAmvcost>(entity =>
            {
                entity.HasKey(e => e.TotalAmvcostId);

                entity.ToTable("PIDF_PBF_Analytical_AMVCost", "dbo");

                entity.Property(e => e.TotalAmvcostId).HasColumnName("TotalAMVCostId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.TotalAmvcost).HasColumnName("TotalAMVCost");

                entity.Property(e => e.TotalAmvtitle)
                    .HasMaxLength(100)
                    .HasColumnName("TotalAMVTitle");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfAnalyticalAmvcosts)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_AMVCost_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfAnalyticalAmvcostStrengthMapping>(entity =>
            {
                entity.HasKey(e => e.PbfanalyticalCostStrengthId);

                entity.ToTable("PIDF_PBF_Analytical_AMVCost_StrengthMapping", "dbo");

                entity.Property(e => e.PbfanalyticalCostStrengthId).HasColumnName("PBFAnalyticalCostStrengthId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmvcostId).HasColumnName("TotalAMVCostId");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfAnalyticalAmvcostStrengthMappings)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDFProductStrength");

                entity.HasOne(d => d.TotalAmvcost)
                    .WithMany(p => p.PidfPbfAnalyticalAmvcostStrengthMappings)
                    .HasForeignKey(d => d.TotalAmvcostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDF_PBF_Analytical_AMVCost");
            });

            modelBuilder.Entity<PidfPbfAnalyticalCost>(entity =>
            {
                entity.HasKey(e => e.PbfanalyticalCostId);

                entity.ToTable("PIDF_PBF_Analytical_Cost", "dbo");

                entity.Property(e => e.PbfanalyticalCostId).HasColumnName("PBFAnalyticalCostId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.TotalAmvcost).HasColumnName("TotalAMVCost");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfAnalyticalCosts)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_Cost_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfAnalyticalCostStrengthMapping>(entity =>
            {
                entity.HasKey(e => e.PbfanalyticalCostStrengthId);

                entity.ToTable("PIDF_PBF_Analytical_Cost_StrengthMapping", "dbo");

                entity.Property(e => e.PbfanalyticalCostStrengthId).HasColumnName("PBFAnalyticalCostStrengthId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfanalyticalCostId).HasColumnName("PBFAnalyticalCostId");

                entity.HasOne(d => d.PbfanalyticalCost)
                    .WithMany(p => p.PidfPbfAnalyticalCostStrengthMappings)
                    .HasForeignKey(d => d.PbfanalyticalCostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDF_PBF_Analytical_Cost");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfAnalyticalCostStrengthMappings)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfClinical>(entity =>
            {
                entity.HasKey(e => e.PbfclinicalId)
                    .HasName("PK_PIDF_PBF_PilotBioFasting");

                entity.ToTable("PIDF_PBF_Clinical", "dbo");

                entity.Property(e => e.PbfclinicalId).HasColumnName("PBFClinicalId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfClinicals)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Clinical_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfClinicals)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Clinical_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfGeneral>(entity =>
            {
                entity.HasKey(e => e.PbfgeneralId)
                    .HasName("PK_PIDF_PBF_g");

                entity.ToTable("PIDF_PBF_General", "dbo");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.AnalyticalGlid).HasColumnName("AnalyticalGLId");

                entity.Property(e => e.BestudyResults)
                    .HasMaxLength(50)
                    .HasColumnName("BEStudyResults");

                entity.Property(e => e.BudgetTimelineSubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.Capex).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormulationGlid).HasColumnName("FormulationGLId");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.Property(e => e.ProjectDevelopmentInitialDate).HasColumnType("datetime");

                entity.Property(e => e.TestLicenseAvailability)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AnalyticalGl)
                    .WithMany(p => p.PidfPbfGeneralAnalyticalGls)
                    .HasForeignKey(d => d.AnalyticalGlid)
                    .HasConstraintName("FK_PIDF_PBF_General_Master_User1");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfPbfGenerals)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_Master_BusinessUnit");

                entity.HasOne(d => d.FormulationGl)
                    .WithMany(p => p.PidfPbfGeneralFormulationGls)
                    .HasForeignKey(d => d.FormulationGlid)
                    .HasConstraintName("FK_PIDF_PBF_General_Master_User");

                entity.HasOne(d => d.Pidfpbf)
                    .WithMany(p => p.PidfPbfGenerals)
                    .HasForeignKey(d => d.Pidfpbfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_PIDF_PBF");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.PidfPbfGenerals)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_PIDF_PBF_General_Master_ProductType");
            });

            modelBuilder.Entity<PidfPbfGeneralRnd>(entity =>
            {
                entity.HasKey(e => e.PbfRndDetailsId)
                    .HasName("PK__PIDF_PBF__5350EF231EB5A49E");

                entity.ToTable("PIDF_PBF_General_RND", "dbo");

                entity.Property(e => e.ApiOrderedDate).HasColumnType("datetime");

                entity.Property(e => e.ApiReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.BatchSizes).HasMaxLength(100);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.FinalFormulationApproved).HasColumnType("datetime");

                entity.Property(e => e.NoMofBatchesPerStrength).HasColumnName("NoMOfBatchesPerStrength");

                entity.Property(e => e.PivotalBatchesManufacturedCompleted).HasColumnType("datetime");

                entity.Property(e => e.Pivotals).HasMaxLength(100);

                entity.Property(e => e.RndResponsiblePerson).HasMaxLength(100);

                entity.Property(e => e.SiteTransferDate).HasColumnType("datetime");

                entity.Property(e => e.StabilityResultsDayZero).HasColumnType("datetime");

                entity.Property(e => e.StabilityResultsSixMonth).HasColumnType("datetime");

                entity.Property(e => e.StabilityResultsThreeMonth).HasColumnType("datetime");

                entity.Property(e => e.TypeOfDevelopmentDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Pbf)
                    .WithMany(p => p.PidfPbfGeneralRnds)
                    .HasForeignKey(d => d.PbfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_RND_PIDF_PBF");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfGeneralRnds)
                    .HasForeignKey(d => d.PidfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_RND_PIDF");
            });

            modelBuilder.Entity<PidfPbfGeneralStrength>(entity =>
            {
                entity.ToTable("PIDF_PBF_General_Strength", "dbo");

                entity.Property(e => e.PidfpbfgeneralStrengthId).HasColumnName("PIDFPBFGeneralStrengthId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImprintingEmbossingCode).HasMaxLength(50);

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.ProjectCode).HasMaxLength(50);

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfGeneralStrengths)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_Strength_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfGeneralStrengths)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_Strength_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfHeadWiseBudget>(entity =>
            {
                entity.HasKey(e => e.HeadWiseBudgetId);

                entity.ToTable("PIDF_PBF_HeadWiseBudget", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfHeadWiseBudgets)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_HeadWiseBudget_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfMarketMapping>(entity =>
            {
                entity.HasKey(e => e.PidfpbfmarketId);

                entity.ToTable("PIDF_PBF_MarketMapping", "dbo");

                entity.Property(e => e.PidfpbfmarketId).HasColumnName("PIDFPBFMarketId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfPbfMarketMappings)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_MarketMapping_Master_BusinessUnit");

                entity.HasOne(d => d.Pidfpbf)
                    .WithMany(p => p.PidfPbfMarketMappings)
                    .HasForeignKey(d => d.Pidfpbfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_MarketMapping_PIDF_PBF");
            });

            modelBuilder.Entity<PidfPbfOutsource>(entity =>
            {
                entity.ToTable("PIDF_PBF_Outsource", "dbo");

                entity.Property(e => e.PidfpbfoutsourceId).HasColumnName("PIDFPBFOutsourceId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PbfworkflowId).HasColumnName("PBFWorkflowId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfOutsources)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Outsource_PIDFID");
            });

            modelBuilder.Entity<PidfPbfOutsourceTask>(entity =>
            {
                entity.ToTable("PIDF_PBF_Outsource_Task", "dbo");

                entity.Property(e => e.PidfpbfoutsourceTaskId).HasColumnName("PIDFPBFOutsourceTaskId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PbfWorkFlowId).HasColumnName("PBfWorkFlowId");

                entity.Property(e => e.PbfworkFlowTaskName)
                    .HasMaxLength(100)
                    .HasColumnName("PBFWorkFlowTaskName");

                entity.Property(e => e.PidfpbfoutsourceId).HasColumnName("PIDFPBFOutsourceId");

                entity.Property(e => e.Tentative).HasMaxLength(100);
            });

            modelBuilder.Entity<PidfPbfPhaseWiseBudget>(entity =>
            {
                entity.HasKey(e => e.PhaseWiseBudgetId);

                entity.ToTable("PIDF_PBF_PhaseWiseBudget", "dbo");

                entity.Property(e => e.AmvcumTotal).HasColumnName("AMVCumTotal");

                entity.Property(e => e.AmvcumTotalDate)
                    .HasColumnType("date")
                    .HasColumnName("AMVCumTotalDate");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExhibitCumTotalDate).HasColumnType("date");

                entity.Property(e => e.FeasabilityCumTotalDate).HasColumnType("date");

                entity.Property(e => e.FilingCumTotalDate).HasColumnType("date");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.PrototypeCumTotalDate).HasColumnType("date");

                entity.Property(e => e.ScaleUpCumTotalDate).HasColumnType("date");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfPhaseWiseBudgets)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_PhaseWiseBudget_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfRa>(entity =>
            {
                entity.ToTable("PIDF_PBF_RA", "dbo");

                entity.Property(e => e.Pidfpbfraid).HasColumnName("PIDFPBFRAId");

                entity.Property(e => e.BefinalReport)
                    .HasColumnType("datetime")
                    .HasColumnName("BEFinalReport");

                entity.Property(e => e.BudgetLaunchDate).HasColumnType("datetime");

                entity.Property(e => e.CountryApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DossierReadyDate).HasColumnType("datetime");

                entity.Property(e => e.EarliestLaunchDexcl)
                    .HasColumnType("datetime")
                    .HasColumnName("EarliestLaunchDExcl");

                entity.Property(e => e.EarliestSubmissionDexcl)
                    .HasColumnType("datetime")
                    .HasColumnName("EarliestSubmissionDExcl");

                entity.Property(e => e.EndOfProcedureDate).HasColumnType("datetime");

                entity.Property(e => e.LasDateToRegulatory).HasColumnType("datetime");

                entity.Property(e => e.LastDataFromRnD).HasColumnType("datetime");

                entity.Property(e => e.Pbfid).HasColumnName("PBFId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PivotalBatchManufactured).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Pbf)
                    .WithMany(p => p.PidfPbfRas)
                    .HasForeignKey(d => d.Pbfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RA_PIDF_PBF");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfRas)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RA_PIDF");
            });

            modelBuilder.Entity<PidfPbfReferenceProductDetail>(entity =>
            {
                entity.ToTable("PIDF_PBF_Reference_Product_detail", "dbo");

                entity.Property(e => e.PidfpbfreferenceProductdetailId).HasColumnName("PIDFPBFReferenceProductdetailId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.Rfdapplicant)
                    .HasMaxLength(100)
                    .HasColumnName("RFDApplicant");

                entity.Property(e => e.Rfdbrand)
                    .HasMaxLength(100)
                    .HasColumnName("RFDBrand");

                entity.Property(e => e.RfdcommercialBatchSize)
                    .HasMaxLength(100)
                    .HasColumnName("RFDCommercialBatchSize");

                entity.Property(e => e.RfdcountryId).HasColumnName("RFDCountryId");

                entity.Property(e => e.Rfdindication)
                    .HasMaxLength(100)
                    .HasColumnName("RFDIndication");

                entity.Property(e => e.RfdinitialRevenuePotential)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInitialRevenuePotential");

                entity.Property(e => e.Rfdinnovators)
                    .HasMaxLength(100)
                    .HasColumnName("RFDInnovators");

                entity.Property(e => e.RfdpriceDiscounting)
                    .HasMaxLength(100)
                    .HasColumnName("RFDPriceDiscounting");
            });

            modelBuilder.Entity<PidfPbfRnDApirequirement>(entity =>
            {
                entity.HasKey(e => e.ApirequirementId);

                entity.ToTable("PIDF_PBF_RnD_APIRequirement", "dbo");

                entity.Property(e => e.ApirequirementId).HasColumnName("APIRequirementId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDApirequirements)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_APIRequirement_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDApirequirements)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_APIRequirement_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDCapexMiscellaneousExpense>(entity =>
            {
                entity.HasKey(e => e.CapexMiscellaneousExpensesId);

                entity.ToTable("PIDF_PBF_RnD_CapexMiscellaneousExpenses", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MiscellaneousDevelopment).HasMaxLength(200);

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDCapexMiscellaneousExpenses)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDCapexMiscellaneousExpenses)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientPrototype>(entity =>
            {
                entity.HasKey(e => e.ExicipientProtoypeId);

                entity.ToTable("PIDF_PBF_RnD_ExicipientPrototype", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExicipientPrototype).HasMaxLength(80);

                entity.Property(e => e.MgPerUnitDosage).HasMaxLength(80);

                entity.HasOne(d => d.PidfPbfGeneral)
                    .WithMany(p => p.PidfPbfRnDExicipientPrototypes)
                    .HasForeignKey(d => d.PidfPbfGeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientPrototype_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDExicipientPrototypes)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientPrototype_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientRequirement>(entity =>
            {
                entity.HasKey(e => e.PidfpbfrndexicipientId)
                    .HasName("PK_PIDF_PBF_RnD_Exicipient");

                entity.ToTable("PIDF_PBF_RnD_ExicipientRequirement", "dbo");

                entity.Property(e => e.PidfpbfrndexicipientId).HasColumnName("PIDFPBFRNDExicipientId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExicipientPrototype).HasMaxLength(200);

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDExicipientRequirements)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Exicipient_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDExicipientRequirements)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Exicipient_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientScaleUp>(entity =>
            {
                entity.HasKey(e => e.ExicipientScaleUpId)
                    .HasName("PK__PIDF_PBF__25B6F2DB5321AFE9");

                entity.ToTable("PIDF_PBF_RnD_ExicipientScaleUp", "dbo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExicipientScaleUp).HasMaxLength(80);

                entity.Property(e => e.MgPerUnitDosage).HasMaxLength(80);

                entity.HasOne(d => d.PidfPbfGeneral)
                    .WithMany(p => p.PidfPbfRnDExicipientScaleUps)
                    .HasForeignKey(d => d.PidfPbfGeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientScaleUp_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfRnDFillingExpense>(entity =>
            {
                entity.HasKey(e => e.FillingExpensesId);

                entity.ToTable("PIDF_PBF_RnD_FillingExpenses", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfPbfRnDFillingExpenses)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_FillingExpenses_Master_BusinessUnit");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDFillingExpenses)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_FillingExpenses_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDFillingExpenses)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_FillingExpenses_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDManPowerCost>(entity =>
            {
                entity.HasKey(e => e.ManPowerCostId);

                entity.ToTable("PIDF_PBF_RnD_ManPowerCost", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDManPowerCosts)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ManPowerCost_PIDF_PBF_General");

                entity.HasOne(d => d.ProjectActivities)
                    .WithMany(p => p.PidfPbfRnDManPowerCosts)
                    .HasForeignKey(d => d.ProjectActivitiesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ManPowerCost_Master_ProjectActivities");
            });

            modelBuilder.Entity<PidfPbfRnDMaster>(entity =>
            {
                entity.HasKey(e => e.RnDmasterId);

                entity.ToTable("PIDF_PBF_RnD_Master", "dbo");

                entity.Property(e => e.RnDmasterId).HasColumnName("RnDMasterId");

                entity.Property(e => e.ApirequirementMarketPrice).HasColumnName("APIRequirementMarketPrice");

                entity.Property(e => e.ApirequirementVendorName)
                    .HasMaxLength(100)
                    .HasColumnName("APIRequirementVendorName");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDMasters)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Master_PIDF_PBF_General");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PidfPbfRnDMasters)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Master_Plant");
            });

            modelBuilder.Entity<PidfPbfRnDPackSizeStability>(entity =>
            {
                entity.HasKey(e => e.PackSizeStabilityId)
                    .HasName("PK__PIDF_PBF__5C057E65FAB7B4EF");

                entity.ToTable("PIDF_PBF_RnD_PackSizeStability", "dbo");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDPackSizeStabilities)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfRnDPackSizeStabilities)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackSizeStability_PIDF");
            });

            modelBuilder.Entity<PidfPbfRnDPackagingMaterial>(entity =>
            {
                entity.HasKey(e => e.PidfpbfrndpackagingId)
                    .HasName("PK_PIDF_PBF_RnD_Packaging");

                entity.ToTable("PIDF_PBF_RnD_PackagingMaterial", "dbo");

                entity.Property(e => e.PidfpbfrndpackagingId).HasColumnName("PIDFPBFRNDPackagingId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.UnitOfMeasurement).HasMaxLength(20);

                entity.HasOne(d => d.PackingType)
                    .WithMany(p => p.PidfPbfRnDPackagingMaterials)
                    .HasForeignKey(d => d.PackingTypeId)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingMaterial_Master_PackingType");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDPackagingMaterials)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Packaging_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPackagingMaterials)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Packaging_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDPlantSupportCost>(entity =>
            {
                entity.HasKey(e => e.PlantSupportCostId);

                entity.ToTable("PIDF_PBF_RnD_PlantSupportCost", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDPlantSupportCosts)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PlantSupportCost_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPlantSupportCosts)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PlantSupportCost_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDReferenceProductDetail>(entity =>
            {
                entity.HasKey(e => e.ReferenceProductDetailId);

                entity.ToTable("PIDF_PBF_RnD_ReferenceProductDetail", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.PilotBe).HasColumnName("PilotBE");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDReferenceProductDetails)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDReferenceProductDetails)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDToolingChangepart>(entity =>
            {
                entity.HasKey(e => e.ToolingChangepartId);

                entity.ToTable("PIDF_PBF_RnD_ToolingChangepart", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.Property(e => e.Prototype).HasMaxLength(100);

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDToolingChangeparts)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ToolingChangepart_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDToolingChangeparts)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ToolingChangepart_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRndBatchSize>(entity =>
            {
                entity.HasKey(e => e.BatchSizeId);

                entity.ToTable("PIDF_PBF_Rnd_BatchSize", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRndBatchSizes)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Rnd_BatchSize_PIDF_PBF_General");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRndBatchSizes)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Rnd_BatchSize_PIDFProductStrength");
            });

            modelBuilder.Entity<Pidfapidetail>(entity =>
            {
                entity.HasKey(e => e.Pidfapiid);

                entity.ToTable("PIDFAPIDetails", "dbo");

                entity.Property(e => e.Pidfapiid).HasColumnName("PIDFAPIId");

                entity.Property(e => e.Apiname)
                    .HasMaxLength(100)
                    .HasColumnName("APIName");

                entity.Property(e => e.ApisourcingId).HasColumnName("APISourcingId");

                entity.Property(e => e.Apivendor)
                    .HasMaxLength(100)
                    .HasColumnName("APIVendor");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.HasOne(d => d.Apisourcing)
                    .WithMany(p => p.Pidfapidetails)
                    .HasForeignKey(d => d.ApisourcingId)
                    .HasConstraintName("FK_PIDFAPIDetails_Master_APISourcing");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Pidfapidetails)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDFAPIDetails_Master_BussinessUnit");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.Pidfapidetails)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFAPIDetails_PIDF");
            });

            modelBuilder.Entity<Pidfimsdatum>(entity =>
            {
                entity.HasKey(e => e.PidfimsdataId);

                entity.ToTable("PIDFIMSData", "dbo");

                entity.Property(e => e.PidfimsdataId).HasColumnName("PIDFIMSDataId");

                entity.Property(e => e.Imsvalue).HasColumnName("IMSValue");

                entity.Property(e => e.Imsvolume).HasColumnName("IMSVolume");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Pidfimsdata)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDFIMSData_Master_BussinessUnit");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.Pidfimsdata)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFIMSData_PIDF");
            });

            modelBuilder.Entity<PidfproductStrength>(entity =>
            {
                entity.ToTable("PIDFProductStrength", "dbo");

                entity.Property(e => e.PidfproductStrengthId).HasColumnName("PIDFProductStrengthId");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.Strength).HasMaxLength(100);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfproductStrengths)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_PIDFProductStrength_Master_BussinessUnit");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfproductStrengths)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFProductStrength_PIDF");

                entity.HasOne(d => d.UnitofMeasurement)
                    .WithMany(p => p.PidfproductStrengths)
                    .HasForeignKey(d => d.UnitofMeasurementId)
                    .HasConstraintName("FK_PIDFProductStrength_Master_UnitofMeasurement");
            });

            modelBuilder.Entity<PidfproductStrengthCountryMapping>(entity =>
            {
                entity.HasKey(e => e.PidfproductStrengthCountryId);

                entity.ToTable("PIDFProductStrength_CountryMapping", "dbo");

                entity.Property(e => e.PidfproductStrengthCountryId).HasColumnName("PIDFProductStrengthCountryId");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PidfproductStrengthId).HasColumnName("PIDFProductStrengthId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PidfproductStrengthCountryMappings)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFProductStrength_CountryMapping_Master_Country");

                entity.HasOne(d => d.PidfproductStrength)
                    .WithMany(p => p.PidfproductStrengthCountryMappings)
                    .HasForeignKey(d => d.PidfproductStrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFProductStrength_CountryMapping_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfstatusHistory>(entity =>
            {
                entity.ToTable("PIDFStatusHistory", "dbo");

                entity.Property(e => e.PidfstatusHistoryId).HasColumnName("PIDFStatusHistoryId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.StatusRemark).HasMaxLength(300);

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfstatusHistories)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFStatusHistory_PIDF");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PidfstatusHistories)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFStatusHistory_Master_PIDFStatus");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.ToTable("ProjectTask", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PlannedEndDate).HasColumnType("datetime");

                entity.Property(e => e.PlannedStartDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_PIDF");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_Master_Project_Priority");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_Master_Project_Status");

                entity.HasOne(d => d.TaskOwner)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.TaskOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_Master_User");
            });

            modelBuilder.Entity<RoleModulePermission>(entity =>
            {
                entity.HasKey(e => e.RoleModuleId);

                entity.ToTable("RoleModulePermission", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSessionManager>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("Tbl_SessionManager", "dbo");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TokenIssuedAt).HasColumnType("datetime");

                entity.Property(e => e.VallidTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblWishList>(entity =>
            {
                entity.HasKey(e => e.WishListId)
                    .HasName("PK__Tbl_Wish__E41F87876701DC93");

                entity.ToTable("Tbl_WishList", "dbo");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfApproval).HasColumnType("datetime");

                entity.Property(e => e.DateOfFiling).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsInhouseOrInLicensed)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MoleculeName).HasMaxLength(100);

                entity.Property(e => e.NameofVendor).HasMaxLength(100);

                entity.Property(e => e.ReferenceDrugProduct).HasMaxLength(200);

                entity.Property(e => e.Strength).HasMaxLength(50);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.VendorEvaluationRemark).HasMaxLength(50);
            });

            modelBuilder.Entity<UserSessionLogMaster>(entity =>
            {
                entity.HasKey(e => e.UserLoginHistoryId);

                entity.ToTable("UserSessionLogMaster", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSessionLogMasters)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSessionLogMaster_Master_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
