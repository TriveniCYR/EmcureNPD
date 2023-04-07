using System;
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

        public virtual DbSet<Abc1> Abc1s { get; set; }
        public virtual DbSet<MasterActivityType> MasterActivityTypes { get; set; }
        public virtual DbSet<MasterAnalytical> MasterAnalyticals { get; set; }
        public virtual DbSet<MasterApiCharterAnalyticalDepartment> MasterApiCharterAnalyticalDepartments { get; set; }
        public virtual DbSet<MasterApiCharterCapitalOtherExpenditure> MasterApiCharterCapitalOtherExpenditures { get; set; }
        public virtual DbSet<MasterApiCharterHeadwiseBudget> MasterApiCharterHeadwiseBudgets { get; set; }
        public virtual DbSet<MasterApiCharterManhourEstimate> MasterApiCharterManhourEstimates { get; set; }
        public virtual DbSet<MasterApiCharterPrddepartment> MasterApiCharterPrddepartments { get; set; }
        public virtual DbSet<MasterApiCharterTimelineInMonth> MasterApiCharterTimelineInMonths { get; set; }
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
        public virtual DbSet<MasterExipient> MasterExipients { get; set; }
        public virtual DbSet<MasterExpenseRegion> MasterExpenseRegions { get; set; }
        public virtual DbSet<MasterExtensionApplication> MasterExtensionApplications { get; set; }
        public virtual DbSet<MasterFilingType> MasterFilingTypes { get; set; }
        public virtual DbSet<MasterFinalSelection> MasterFinalSelections { get; set; }
        public virtual DbSet<MasterFormRnDdivision> MasterFormRnDdivisions { get; set; }
        public virtual DbSet<MasterFormulation> MasterFormulations { get; set; }
        public virtual DbSet<MasterManufacturing> MasterManufacturings { get; set; }
        public virtual DbSet<MasterMarketExtenstion> MasterMarketExtenstions { get; set; }
        public virtual DbSet<MasterModule> MasterModules { get; set; }
        public virtual DbSet<MasterNotification> MasterNotifications { get; set; }
        public virtual DbSet<MasterOral> MasterOrals { get; set; }
        public virtual DbSet<MasterPackagingType> MasterPackagingTypes { get; set; }
        public virtual DbSet<MasterPidfstatus> MasterPidfstatuses { get; set; }
        public virtual DbSet<MasterPlant> MasterPlants { get; set; }
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
        public virtual DbSet<MasterUnitofMeasurement> MasterUnitofMeasurements { get; set; }
        public virtual DbSet<MasterUser> MasterUsers { get; set; }
        public virtual DbSet<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual DbSet<MasterUserCountryMapping> MasterUserCountryMappings { get; set; }
        public virtual DbSet<MasterUserDepartmentMapping> MasterUserDepartmentMappings { get; set; }
        public virtual DbSet<MasterUserRegionMapping> MasterUserRegionMappings { get; set; }
        public virtual DbSet<MasterWorkflow> MasterWorkflows { get; set; }
        public virtual DbSet<Pidf> Pidfs { get; set; }
        public virtual DbSet<PidfApiCharter> PidfApiCharters { get; set; }
        public virtual DbSet<PidfApiCharterAnalyticalDepartment> PidfApiCharterAnalyticalDepartments { get; set; }
        public virtual DbSet<PidfApiCharterCapitalOtherExpenditure> PidfApiCharterCapitalOtherExpenditures { get; set; }
        public virtual DbSet<PidfApiCharterHeadwiseBudget> PidfApiCharterHeadwiseBudgets { get; set; }
        public virtual DbSet<PidfApiCharterManhourEstimate> PidfApiCharterManhourEstimates { get; set; }
        public virtual DbSet<PidfApiCharterPrddepartment> PidfApiCharterPrddepartments { get; set; }
        public virtual DbSet<PidfApiCharterTimelineInMonth> PidfApiCharterTimelineInMonths { get; set; }
        public virtual DbSet<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual DbSet<PidfApiRnD> PidfApiRnDs { get; set; }
        public virtual DbSet<PidfCommercial> PidfCommercials { get; set; }
        public virtual DbSet<PidfCommercialYear> PidfCommercialYears { get; set; }
        public virtual DbSet<PidfFinance> PidfFinances { get; set; }
        public virtual DbSet<PidfFinanceBatchSizeCoating> PidfFinanceBatchSizeCoatings { get; set; }
        public virtual DbSet<PidfIpd> PidfIpds { get; set; }
        public virtual DbSet<PidfIpdCountry> PidfIpdCountries { get; set; }
        public virtual DbSet<PidfIpdPatentDetail> PidfIpdPatentDetails { get; set; }
        public virtual DbSet<PidfIpdRegion> PidfIpdRegions { get; set; }
        public virtual DbSet<PidfManagementApprovalStatusHistory> PidfManagementApprovalStatusHistories { get; set; }
        public virtual DbSet<PidfMedical> PidfMedicals { get; set; }
        public virtual DbSet<PidfMedicalFile> PidfMedicalFiles { get; set; }
        public virtual DbSet<PidfPbf> PidfPbfs { get; set; }
        public virtual DbSet<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual DbSet<PidfPbfAnalyticalCost> PidfPbfAnalyticalCosts { get; set; }
        public virtual DbSet<PidfPbfAnalyticalCostStrengthMapping> PidfPbfAnalyticalCostStrengthMappings { get; set; }
        public virtual DbSet<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual DbSet<PidfPbfGeneral> PidfPbfGenerals { get; set; }
        public virtual DbSet<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual DbSet<PidfPbfMarketMapping> PidfPbfMarketMappings { get; set; }
        public virtual DbSet<PidfPbfRnDApirequirement> PidfPbfRnDApirequirements { get; set; }
        public virtual DbSet<PidfPbfRnDCapexMiscellaneousExpense> PidfPbfRnDCapexMiscellaneousExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientRequirement> PidfPbfRnDExicipientRequirements { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientScaleUp> PidfPbfRnDExicipientScaleUps { get; set; }
        public virtual DbSet<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDManPowerCost> PidfPbfRnDManPowerCosts { get; set; }
        public virtual DbSet<PidfPbfRnDMaster> PidfPbfRnDMasters { get; set; }
        public virtual DbSet<PidfPbfRnDPackagingMaterial> PidfPbfRnDPackagingMaterials { get; set; }
        public virtual DbSet<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual DbSet<PidfPbfRnDReferenceProductDetail> PidfPbfRnDReferenceProductDetails { get; set; }
        public virtual DbSet<PidfPbfRnDToolingChangepart> PidfPbfRnDToolingChangeparts { get; set; }
        public virtual DbSet<PidfPbfRndBatchSize> PidfPbfRndBatchSizes { get; set; }
        public virtual DbSet<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual DbSet<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual DbSet<PidfstatusHistory> PidfstatusHistories { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<RoleModulePermission> RoleModulePermissions { get; set; }
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

            modelBuilder.Entity<Abc1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ABC_1");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.AnalyticalGl).HasColumnName("AnalyticalGL");

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

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
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

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

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

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.MasterNotifications)
                    .HasForeignKey(d => d.Pidfid)
                    .HasConstraintName("FK_Master_Notification_PIDF");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.MasterNotifications)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Master_Notification_Master_PIDFStatus");
            });

            modelBuilder.Entity<MasterOral>(entity =>
            {
                entity.HasKey(e => e.OralId);

                entity.ToTable("Master_Oral", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.OralName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPackagingType>(entity =>
            {
                entity.HasKey(e => e.PackagingTypeId);

                entity.ToTable("Master_PackagingType", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PackagingTypeName).HasMaxLength(100);
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
            });

            modelBuilder.Entity<MasterPlant>(entity =>
            {
                entity.HasKey(e => e.PlantId);

                entity.ToTable("Master_Plant", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PlantNameName).HasMaxLength(100);
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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MasterUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Master_User_Master_Role");
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

            modelBuilder.Entity<MasterWorkflow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId);

                entity.ToTable("Master_Workflow", "dbo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.WorkflowName).HasMaxLength(100);
            });

            modelBuilder.Entity<Pidf>(entity =>
            {
                entity.ToTable("PIDF", "dbo");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.ApprovedGenerics).HasMaxLength(100);

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Diaid).HasColumnName("DIAId");

                entity.Property(e => e.LaunchedGenerics).HasMaxLength(100);

                entity.Property(e => e.MarketExtenstionId).HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.MoleculeName)
                    .IsRequired()
                    .HasMaxLength(100);

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

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_BusinessUnit");

                entity.HasOne(d => d.Dia)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.Diaid)
                    .HasConstraintName("FK_PIDF_Master_DIA");

                entity.HasOne(d => d.DosageForm)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.DosageFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_DosageForm");

                entity.HasOne(d => d.LastStatus)
                    .WithMany(p => p.PidfLastStatuses)
                    .HasForeignKey(d => d.LastStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_PIDFStatus1");

                entity.HasOne(d => d.MarketExtenstion)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.MarketExtenstionId)
                    .HasConstraintName("FK_PIDF_Master_MarketExtenstion");

                entity.HasOne(d => d.Oral)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.OralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_Oral");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_PIDF_Master_PackagingType");

                entity.HasOne(d => d.Rfdcountry)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.RfdcountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_Country");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PidfStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_PIDFStatus");

                entity.HasOne(d => d.UnitofMeasurement)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.UnitofMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_UnitofMeasurement");
            });

            modelBuilder.Entity<PidfApiCharter>(entity =>
            {
                entity.ToTable("PIDF_API_Charter", "dbo");

                entity.Property(e => e.PidfApiCharterId).HasColumnName("PIDF_API_CharterId");

                entity.Property(e => e.ApigroupLeader)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("APIGroupLeader");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

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

            modelBuilder.Entity<PidfCommercialYear>(entity =>
            {
                entity.ToTable("PIDF_Commercial_Years", "dbo");

                entity.Property(e => e.PidfcommercialYearId).HasColumnName("PIDFCommercialYearId");

                entity.Property(e => e.Apireq)
                    .HasMaxLength(20)
                    .HasColumnName("APIReq");

                entity.Property(e => e.CommercialBatchSize).HasMaxLength(20);

                entity.Property(e => e.FreeOfCost).HasMaxLength(20);

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

                entity.Property(e => e.BatchManufacturing).HasMaxLength(70);

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

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Entity).HasMaxLength(70);

                entity.Property(e => e.EscalationinCogs)
                    .HasMaxLength(70)
                    .HasColumnName("EscalationinCOGS");

                entity.Property(e => e.ExpectedFilling).HasMaxLength(70);

                entity.Property(e => e.Filingfees).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FilingfeesPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.ForecastDate).HasColumnType("datetime");

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

                entity.Property(e => e.CcpcCad).HasColumnName("CCPC_CAD");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmcureCogsPack).HasColumnName("EmcureCOGs_pack");

                entity.Property(e => e.ExcipientsCad).HasColumnName("Excipients_CAD");

                entity.Property(e => e.FreightCad).HasColumnName("Freight_CAD");

                entity.Property(e => e.PidffinaceId).HasColumnName("PIDFFinaceId");

                entity.Property(e => e.PmCad).HasColumnName("PM_CAD");

                entity.HasOne(d => d.Pidffinace)
                    .WithMany(p => p.PidfFinanceBatchSizeCoatings)
                    .HasForeignKey(d => d.PidffinaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Finance_BatchSizeCoating_PIDF_Finance");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_Country_Master_Country");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.PidfIpdCountries)
                    .HasForeignKey(d => d.Ipdid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_Country_PIDF_IPD");
            });

            modelBuilder.Entity<PidfIpdPatentDetail>(entity =>
            {
                entity.HasKey(e => e.PatentDetailsId);

                entity.ToTable("PIDF_IPD_PatentDetails", "dbo");

                entity.Property(e => e.PatentDetailsId).HasColumnName("PatentDetailsID");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.ExtensionExpiryDate).HasColumnType("date");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.OriginalExpiryDate).HasColumnType("date");

                entity.Property(e => e.PatentNumber).HasMaxLength(50);

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_IPD_Region_Master_Region");
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

                entity.Property(e => e.BerequirementId).HasColumnName("BERequirementId");

                entity.Property(e => e.BusinessRelationable).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormRnDdivisionId).HasColumnName("FormRnDDivisionId");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.NumberOfApprovedAnda)
                    .HasMaxLength(100)
                    .HasColumnName("NumberOfApprovedANDA");

                entity.Property(e => e.PatentStatus).HasMaxLength(100);

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.ProjectInitiationDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectManager).HasMaxLength(100);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RnDhead)
                    .HasMaxLength(100)
                    .HasColumnName("RnDHead");

                entity.Property(e => e.ScopeObjectives).HasMaxLength(100);

                entity.Property(e => e.SponsorBusinessPartner).HasMaxLength(100);

                entity.HasOne(d => d.Berequirement)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.BerequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_FormRnDDivision");

                entity.HasOne(d => d.Manufacturing)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.ManufacturingId)
                    .HasConstraintName("FK_PIDF_PBF_Master_Manufacturing1");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_PackagingType1");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_PIDF1");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_Plant1");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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

                entity.Property(e => e.BudgetTimelineSubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.Capex).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormulationGlid).HasColumnName("FormulationGLId");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.Property(e => e.ProjectComplexity).HasMaxLength(50);

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_General_Master_ProductType");
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
                    .HasName("PK__PIDF_PBF__25B6F2DB7DDA7A4A");

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

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDManPowerCosts)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ManPowerCost_PIDFProductStrength");
            });

            modelBuilder.Entity<PidfPbfRnDMaster>(entity =>
            {
                entity.HasKey(e => e.RnDmasterId);

                entity.ToTable("PIDF_PBF_RnD_Master", "dbo");

                entity.Property(e => e.RnDmasterId).HasColumnName("RnDMasterId");

                entity.Property(e => e.ApirequirementMarketPrice).HasColumnName("APIRequirementMarketPrice");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.Pbfgeneral)
                    .WithMany(p => p.PidfPbfRnDMasters)
                    .HasForeignKey(d => d.PbfgeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Master_PIDF_PBF_General");
            });

            modelBuilder.Entity<PidfPbfRnDPackagingMaterial>(entity =>
            {
                entity.HasKey(e => e.PidfpbfrndpackagingId)
                    .HasName("PK_PIDF_PBF_RnD_Packaging");

                entity.ToTable("PIDF_PBF_RnD_PackagingMaterial", "dbo");

                entity.Property(e => e.PidfpbfrndpackagingId).HasColumnName("PIDFPBFRNDPackagingId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PbfgeneralId).HasColumnName("PBFGeneralId");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PidfPbfRnDPackagingMaterials)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Packaging_Master_PackagingType");

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
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("APIName");

                entity.Property(e => e.ApisourcingId).HasColumnName("APISourcingId");

                entity.Property(e => e.Apivendor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("APIVendor");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.HasOne(d => d.Apisourcing)
                    .WithMany(p => p.Pidfapidetails)
                    .HasForeignKey(d => d.ApisourcingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFAPIDetails_Master_APISourcing");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.Pidfapidetails)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFAPIDetails_PIDF");
            });

            modelBuilder.Entity<PidfproductStrength>(entity =>
            {
                entity.ToTable("PIDFProductStrength", "dbo");

                entity.Property(e => e.PidfproductStrengthId).HasColumnName("PIDFProductStrengthId");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

                entity.Property(e => e.Strength)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfproductStrengths)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFProductStrength_PIDF");

                entity.HasOne(d => d.UnitofMeasurement)
                    .WithMany(p => p.PidfproductStrengths)
                    .HasForeignKey(d => d.UnitofMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDFProductStrength_Master_UnitofMeasurement");
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
