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

        public virtual DbSet<MasterActivityType> MasterActivityTypes { get; set; }
        public virtual DbSet<MasterAnalytical> MasterAnalyticals { get; set; }
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
        public virtual DbSet<MasterFinalSelection> MasterFinalSelections { get; set; }
        public virtual DbSet<MasterFormRnDdivision> MasterFormRnDdivisions { get; set; }
        public virtual DbSet<MasterFormulation> MasterFormulations { get; set; }
        public virtual DbSet<MasterMarketExtenstion> MasterMarketExtenstions { get; set; }
        public virtual DbSet<MasterModule> MasterModules { get; set; }
        public virtual DbSet<MasterNotification> MasterNotifications { get; set; }
        public virtual DbSet<MasterOral> MasterOrals { get; set; }
        public virtual DbSet<MasterPackagingType> MasterPackagingTypes { get; set; }
        public virtual DbSet<MasterPidfstatus> MasterPidfstatuses { get; set; }
        public virtual DbSet<MasterPlant> MasterPlants { get; set; }
        public virtual DbSet<MasterProductStrength> MasterProductStrengths { get; set; }
        public virtual DbSet<MasterProductType> MasterProductTypes { get; set; }
        public virtual DbSet<MasterRegion> MasterRegions { get; set; }
        public virtual DbSet<MasterRegionCountryMapping> MasterRegionCountryMappings { get; set; }
        public virtual DbSet<MasterRole> MasterRoles { get; set; }
        public virtual DbSet<MasterSubModule> MasterSubModules { get; set; }
        public virtual DbSet<MasterTransform> MasterTransforms { get; set; }
        public virtual DbSet<MasterUnitofMeasurement> MasterUnitofMeasurements { get; set; }
        public virtual DbSet<MasterUser> MasterUsers { get; set; }
        public virtual DbSet<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual DbSet<MasterUserCountryMapping> MasterUserCountryMappings { get; set; }
        public virtual DbSet<MasterUserDepartmentMapping> MasterUserDepartmentMappings { get; set; }
        public virtual DbSet<MasterUserRegionMapping> MasterUserRegionMappings { get; set; }
        public virtual DbSet<MasterWorkflow> MasterWorkflows { get; set; }
        public virtual DbSet<Pidf> Pidfs { get; set; }
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
        public virtual DbSet<PidfMedical> PidfMedicals { get; set; }
        public virtual DbSet<PidfMedicalFile> PidfMedicalFiles { get; set; }
        public virtual DbSet<PidfPbf> PidfPbfs { get; set; }
        public virtual DbSet<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual DbSet<PidfPbfAnalyticalExhibit> PidfPbfAnalyticalExhibits { get; set; }
        public virtual DbSet<PidfPbfAnalyticalPrototype> PidfPbfAnalyticalPrototypes { get; set; }
        public virtual DbSet<PidfPbfAnalyticalScaleUp> PidfPbfAnalyticalScaleUps { get; set; }
        public virtual DbSet<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual DbSet<PidfPbfClinicalPilotBioFasting> PidfPbfClinicalPilotBioFastings { get; set; }
        public virtual DbSet<PidfPbfClinicalPilotBioFed> PidfPbfClinicalPilotBioFeds { get; set; }
        public virtual DbSet<PidfPbfClinicalPivotalBioFasting> PidfPbfClinicalPivotalBioFastings { get; set; }
        public virtual DbSet<PidfPbfRnD> PidfPbfRnDs { get; set; }
        public virtual DbSet<PidfPbfRnDCapexandMiscellaneousExpense> PidfPbfRnDCapexandMiscellaneousExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientExhibit> PidfPbfRnDExicipientExhibits { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientProtoype> PidfPbfRnDExicipientProtoypes { get; set; }
        public virtual DbSet<PidfPbfRnDExicipientScaleUp> PidfPbfRnDExicipientScaleUps { get; set; }
        public virtual DbSet<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual DbSet<PidfPbfRnDManPowerCostAndProjectDuration> PidfPbfRnDManPowerCostAndProjectDurations { get; set; }
        public virtual DbSet<PidfPbfRnDPackagingExhibit> PidfPbfRnDPackagingExhibits { get; set; }
        public virtual DbSet<PidfPbfRnDPackagingPrototype> PidfPbfRnDPackagingPrototypes { get; set; }
        public virtual DbSet<PidfPbfRnDPackagingScaleUp> PidfPbfRnDPackagingScaleUps { get; set; }
        public virtual DbSet<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual DbSet<PidfPbfRnDToolingandChangePartCost> PidfPbfRnDToolingandChangePartCosts { get; set; }
        public virtual DbSet<PidfPbfRndProjectActivity> PidfPbfRndProjectActivities { get; set; }
        public virtual DbSet<PidfPbfRndProjectEstimation> PidfPbfRndProjectEstimations { get; set; }
        public virtual DbSet<PidfPbfRndReferenceProductDetail> PidfPbfRndReferenceProductDetails { get; set; }
        public virtual DbSet<Pidfapidetail> Pidfapidetails { get; set; }
        public virtual DbSet<PidfproductStrength> PidfproductStrengths { get; set; }
        public virtual DbSet<PidfstatusHistory> PidfstatusHistories { get; set; }
        public virtual DbSet<RoleModulePermission> RoleModulePermissions { get; set; }

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
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MasterActivityType>(entity =>
            {
                entity.HasKey(e => e.ActivityTypeId);

                entity.ToTable("Master_ActivityType");

                entity.Property(e => e.ActivityTypeName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterAnalytical>(entity =>
            {
                entity.HasKey(e => e.AnalyticalId)
                    .HasName("PK_Master_AnalyticalGL");

                entity.ToTable("Master_Analytical");

                entity.Property(e => e.AnalyticalName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterApisourcing>(entity =>
            {
                entity.HasKey(e => e.ApisourcingId);

                entity.ToTable("Master_APISourcing");

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

                entity.ToTable("Master_AuditLog");

                entity.Property(e => e.ActionType).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBatchSizeNumber>(entity =>
            {
                entity.HasKey(e => e.BatchSizeNumberId);

                entity.ToTable("Master_BatchSizeNumber");

                entity.Property(e => e.BatchSizeNumberName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBerequirement>(entity =>
            {
                entity.HasKey(e => e.BerequirementId);

                entity.ToTable("Master_BERequirement");

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

                entity.ToTable("Master_BusinessUnit");

                entity.Property(e => e.BusinessUnitName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterBusinessUnitRegionMapping>(entity =>
            {
                entity.HasKey(e => e.BusinessUnitCountryMappingId)
                    .HasName("PK_Master_BusinessCountryMapping");

                entity.ToTable("Master_BusinessUnitRegionMapping");

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

                entity.ToTable("Master_Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.ToTable("Master_Currency");

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

                entity.ToTable("Master_CurrencyCountryMapping");

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

                entity.ToTable("Master_Department");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDepartmentBusinessUnitMapping>(entity =>
            {
                entity.HasKey(e => e.DepartmentBusinessUnitMappingId)
                    .HasName("PK_Master_BusinessBusinessUnitMapping");

                entity.ToTable("Master_DepartmentBusinessUnitMapping");

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

                entity.ToTable("Master_DIA");

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

                entity.ToTable("Master_Dosage");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosageName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterDosageForm>(entity =>
            {
                entity.HasKey(e => e.DosageFormId);

                entity.ToTable("Master_DosageForm");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosageFormName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExipient>(entity =>
            {
                entity.HasKey(e => e.ExipientId)
                    .HasName("PK_Master_Exipientr");

                entity.ToTable("Master_Exipient");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExipientName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExpenseRegion>(entity =>
            {
                entity.HasKey(e => e.ExpenseRegionId);

                entity.ToTable("Master_ExpenseRegion");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpenseRegionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterExtensionApplication>(entity =>
            {
                entity.HasKey(e => e.ExtensionApplicationId);

                entity.ToTable("Master_ExtensionApplication");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExtensionApplicationName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFinalSelection>(entity =>
            {
                entity.HasKey(e => e.FinalSelectionId);

                entity.ToTable("Master_FinalSelection");

                entity.Property(e => e.FinalSelectionId).HasColumnName("FinalSelectionID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FinalSelectionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFormRnDdivision>(entity =>
            {
                entity.HasKey(e => e.FormRnDdivisionId)
                    .HasName("PK_Master_RNDDivision");

                entity.ToTable("Master_FormRnDDivision");

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

                entity.ToTable("Master_Formulation");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormulationName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterMarketExtenstion>(entity =>
            {
                entity.HasKey(e => e.MarketExtenstionId);

                entity.ToTable("Master_MarketExtenstion");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MarketExtenstionName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.ToTable("Master_Module");

                entity.Property(e => e.ControlName).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleName).HasMaxLength(250);
            });

            modelBuilder.Entity<MasterNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("Master_Notification");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationDescription).HasMaxLength(500);

                entity.Property(e => e.NotificationTitle)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MasterOral>(entity =>
            {
                entity.HasKey(e => e.OralId);

                entity.ToTable("Master_Oral");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.OralName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPackagingType>(entity =>
            {
                entity.HasKey(e => e.PackagingTypeId);

                entity.ToTable("Master_PackagingType");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PackagingTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterPidfstatus>(entity =>
            {
                entity.HasKey(e => e.PidfstatusId);

                entity.ToTable("Master_PIDFStatus");

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

                entity.ToTable("Master_Plant");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PlantNameName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterProductStrength>(entity =>
            {
                entity.HasKey(e => e.ProductStrengthId);

                entity.ToTable("Master_ProductStrength");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductStrengthName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId);

                entity.ToTable("Master_ProductType");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("Master_Region");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.RegionName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterRegionCountryMapping>(entity =>
            {
                entity.HasKey(e => e.RegionCountryMappingId);

                entity.ToTable("Master_RegionCountryMapping");

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

                entity.ToTable("Master_Role");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterSubModule>(entity =>
            {
                entity.HasKey(e => e.SubModuleId);

                entity.ToTable("Master_SubModule");

                entity.Property(e => e.ControlName).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.SubModuleName).HasMaxLength(250);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.MasterSubModules)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_Master_SubModule_Master_Module");
            });

            modelBuilder.Entity<MasterTransform>(entity =>
            {
                entity.HasKey(e => e.TransformId)
                    .HasName("PK_Master_Transform_form");

                entity.ToTable("Master_Transform");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.TransformName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterUnitofMeasurement>(entity =>
            {
                entity.HasKey(e => e.UnitofMeasurementId);

                entity.ToTable("Master_UnitofMeasurement");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurementName).HasMaxLength(100);
            });

            modelBuilder.Entity<MasterUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Master_User");

                entity.HasIndex(e => e.EmailAddress, "EmailAddress_unique")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ForgotPasswordDateTime).HasColumnType("datetime");

                entity.Property(e => e.ForgotPasswordToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.ToTable("Master_UserBusinessUnitMapping");

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

                entity.ToTable("Master_UserCountryMapping");

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

                entity.ToTable("Master_UserDepartmentMapping");

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

                entity.ToTable("Master_UserRegionMapping");

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

                entity.ToTable("Master_Workflow");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.WorkflowName).HasMaxLength(100);
            });

            modelBuilder.Entity<Pidf>(entity =>
            {
                entity.ToTable("PIDF");

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

                entity.Property(e => e.StatusUpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Master_BusinessUnit");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Pidfs)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PIDF__CreatedBy__5A6F5FCC");

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

            modelBuilder.Entity<PidfApiIpd>(entity =>
            {
                entity.ToTable("PIDF_API_IPD");

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
                entity.ToTable("PIDF_API_RnD");

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

                entity.HasOne(d => d.MarketExtenstion)
                    .WithMany(p => p.PidfApiRnDs)
                    .HasForeignKey(d => d.MarketExtenstionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_RnD_MarketExtenstionId");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfApiRnDs)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_API_RnD_PIDFID");
            });

            modelBuilder.Entity<PidfCommercial>(entity =>
            {
                entity.ToTable("PIDF_Commercial");

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
                entity.ToTable("PIDF_Commercial_Years");

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
                    .HasName("PK__PIDF_Fin__985A8F5626D521A6");

                entity.ToTable("PIDF_Finance");

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

                entity.Property(e => e.Currency).HasMaxLength(20);

                entity.Property(e => e.DiscountRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Entity).HasMaxLength(70);

                entity.Property(e => e.ExpectedFilling).HasMaxLength(70);

                entity.Property(e => e.Filingfees).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FilingfeesPhaseEndDate).HasColumnType("datetime");

                entity.Property(e => e.ForecastDate).HasColumnType("datetime");

                entity.Property(e => e.GrosstoNet).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Incometaxrate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ManufacturingSiteOrPartner).HasMaxLength(70);

                entity.Property(e => e.MarketShareErosionrate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MarketingAllowance).HasColumnType("numeric(18, 2)");

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

                entity.ToTable("PIDF_Finance_BatchSizeCoating");

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
            });

            modelBuilder.Entity<PidfIpd>(entity =>
            {
                entity.HasKey(e => e.Ipdid)
                    .HasName("PK__PIDF_IPD__54D2918E72B886FB");

                entity.ToTable("PIDF_IPD");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.ApprovedGenetics).HasMaxLength(100);

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DataExclusivity).HasMaxLength(200);

                entity.Property(e => e.FillingType).HasMaxLength(200);

                entity.Property(e => e.Innovators).HasMaxLength(100);

                entity.Property(e => e.LaunchedGenetics).HasMaxLength(100);

                entity.Property(e => e.LegalStatus).HasMaxLength(100);

                entity.Property(e => e.MarketName).HasMaxLength(200);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

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

                entity.ToTable("PIDF_IPD_Country");

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

                entity.ToTable("PIDF_IPD_PatentDetails");

                entity.Property(e => e.PatentDetailsId).HasColumnName("PatentDetailsID");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.ExtensionExpiryDate).HasColumnType("date");

                entity.Property(e => e.Ipdid).HasColumnName("IPDID");

                entity.Property(e => e.OriginalExpiryDate).HasColumnType("date");

                entity.Property(e => e.PatentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

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

                entity.ToTable("PIDF_IPD_Region");

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

            modelBuilder.Entity<PidfMedical>(entity =>
            {
                entity.ToTable("PIDF_Medical");

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
                entity.ToTable("PIDF_Medical_File");

                entity.Property(e => e.PidfmedicalFileId).HasColumnName("PIDFMedicalFileId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PidfmedicalId).HasColumnName("PIDFMedicalId");
            });

            modelBuilder.Entity<PidfPbf>(entity =>
            {
                entity.ToTable("PIDF_PBF");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.Property(e => e.BerequirementId).HasColumnName("BERequirementId");

                entity.Property(e => e.BusinessRelationable).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FormRnDdivisionId).HasColumnName("FormRnDDivisionId");

                entity.Property(e => e.Market).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.NumberOfApprovedAnda)
                    .HasMaxLength(100)
                    .HasColumnName("NumberOfApprovedANDA");

                entity.Property(e => e.PatentStatus).HasMaxLength(100);

                entity.Property(e => e.Pidfid).HasColumnName("PIDFId");

                entity.Property(e => e.PidfproductStrengthId).HasColumnName("PIDFProductStrengthId");

                entity.Property(e => e.ProjectInitiationDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectManager).HasMaxLength(100);

                entity.Property(e => e.ProjectName).HasMaxLength(100);

                entity.Property(e => e.RnDhead)
                    .HasMaxLength(100)
                    .HasColumnName("RnDHead");

                entity.Property(e => e.SponsorBusinessPartner).HasMaxLength(100);

                entity.HasOne(d => d.Berequirement)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.BerequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_BERequirementId");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Pbf_Master_BusinessUnit");

                entity.HasOne(d => d.Dosage)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.DosageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_Dosage");

                entity.HasOne(d => d.FormRnDdivision)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.FormRnDdivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_FormRnDDivision");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_PackagingType");

                entity.HasOne(d => d.Pidf)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.Pidfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_PIDF");

                entity.HasOne(d => d.PidfproductStrength)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PidfproductStrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_Pbf_PIDFProductStrength");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_Plant");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_ProductType");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.PidfPbfs)
                    .HasForeignKey(d => d.WorkflowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Master_Workflow");
            });

            modelBuilder.Entity<PidfPbfAnalytical>(entity =>
            {
                entity.HasKey(e => e.AnalyticalId)
                    .HasName("PK_PIDF_PBF_A");

                entity.ToTable("PIDF_PBF_Analytical");

                entity.Property(e => e.AnalyticalId).ValueGeneratedNever();

                entity.Property(e => e.AmvcostId).HasColumnName("AMVCostId");

                entity.Property(e => e.TotalAmvcostId).HasColumnName("TotalAMVCostId");
            });

            modelBuilder.Entity<PidfPbfAnalyticalExhibit>(entity =>
            {
                entity.HasKey(e => e.PrototypeId)
                    .HasName("PK_PIDF_PBF_Exhibit");

                entity.ToTable("PIDF_PBF_Analytical_Exhibit");

                entity.Property(e => e.PrototypeId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrototypeCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrototypeDevelopment).HasMaxLength(50);
            });

            modelBuilder.Entity<PidfPbfAnalyticalPrototype>(entity =>
            {
                entity.HasKey(e => e.PrototypeId)
                    .HasName("PK_PIDF_PBF_Prototype");

                entity.ToTable("PIDF_PBF_Analytical_Prototype");

                entity.Property(e => e.PrototypeId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrototypeCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrototypeDevelopment).HasMaxLength(50);
            });

            modelBuilder.Entity<PidfPbfAnalyticalScaleUp>(entity =>
            {
                entity.HasKey(e => e.PrototypeId)
                    .HasName("PK_PIDF_PBF_ScaleUp");

                entity.ToTable("PIDF_PBF_Analytical_ScaleUp");

                entity.Property(e => e.PrototypeId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrototypeCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrototypeDevelopment).HasMaxLength(50);
            });

            modelBuilder.Entity<PidfPbfClinical>(entity =>
            {
                entity.HasKey(e => e.ClinicalId)
                    .HasName("PK_PIDF_PBF_C");

                entity.ToTable("PIDF_PBF_Clinical");

                entity.Property(e => e.ClinicalId).ValueGeneratedNever();

                entity.Property(e => e.PilotBioFedid).HasColumnName("PilotBioFEDId");

                entity.Property(e => e.PivotalBioFedid).HasColumnName("PivotalBioFEDId");
            });

            modelBuilder.Entity<PidfPbfClinicalPilotBioFasting>(entity =>
            {
                entity.HasKey(e => e.PilotBioFastingId)
                    .HasName("PK_PIDF_PBF_PilotBioFasting");

                entity.ToTable("PIDF_PBF_Clinical_PilotBioFasting");

                entity.Property(e => e.PilotBioFastingId).ValueGeneratedNever();

                entity.Property(e => e.ClinicalCostandVol)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ClinicalCostandVOl");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocCostandStudy).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fasting).HasMaxLength(50);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfClinicalPilotBioFed>(entity =>
            {
                entity.HasKey(e => e.PilotBioFedid)
                    .HasName("PK_PIDF_PBF_PilotBioFED");

                entity.ToTable("PIDF_PBF_Clinical_PilotBioFED");

                entity.Property(e => e.PilotBioFedid)
                    .ValueGeneratedNever()
                    .HasColumnName("PilotBioFEDId");

                entity.Property(e => e.ClinicalCostandVol)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ClinicalCostandVOl");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocCostandStudy).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fed)
                    .HasMaxLength(50)
                    .HasColumnName("FED");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfClinicalPivotalBioFasting>(entity =>
            {
                entity.HasKey(e => e.PilotBioFastingId)
                    .HasName("PK_PIDF_PBF_PivotalBioFasting");

                entity.ToTable("PIDF_PBF_Clinical_PivotalBioFasting");

                entity.Property(e => e.PilotBioFastingId).ValueGeneratedNever();

                entity.Property(e => e.ClinicalCostandVol)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ClinicalCostandVOl");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocCostandStudy).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fasting).HasMaxLength(50);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfRnD>(entity =>
            {
                entity.ToTable("PIDF_PBF_RnD");

                entity.Property(e => e.PidfpbfrnDid)
                    .ValueGeneratedNever()
                    .HasColumnName("PIDFPBFRnDId");

                entity.Property(e => e.Pidfpbfid).HasColumnName("PIDFPBFId");

                entity.Property(e => e.TotalExicipientCosts).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalPackagingCosts).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CapexAndMiscellaneousExpenses)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.CapexAndMiscellaneousExpensesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_CapexAndMiscellaneousExpenses");

                entity.HasOne(d => d.ExicipientExhibit)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.ExicipientExhibitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientExhibit");

                entity.HasOne(d => d.ExicipientProtoype)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.ExicipientProtoypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientProtoype");

                entity.HasOne(d => d.ExicipientScaleUp)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.ExicipientScaleUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientScaleUp");

                entity.HasOne(d => d.FillingExpenses)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.FillingExpensesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_FillingExpenses_Id");

                entity.HasOne(d => d.PackagingExhibit)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.PackagingExhibitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingExhibit");

                entity.HasOne(d => d.PackagingPrototype)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.PackagingPrototypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingPrototype");

                entity.HasOne(d => d.PackagingScaleUp)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.PackagingScaleUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingScaleUp");

                entity.HasOne(d => d.Pidfpbf)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.Pidfpbfid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PIDF");

                entity.HasOne(d => d.PlantSupportCost)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.PlantSupportCostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PlantSupportCost");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PIDF_Strength_Id");

                entity.HasOne(d => d.ToolingAndChangePartCost)
                    .WithMany(p => p.PidfPbfRnDs)
                    .HasForeignKey(d => d.ToolingAndChangePartCostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ToolingAndChangePartCost");
            });

            modelBuilder.Entity<PidfPbfRnDCapexandMiscellaneousExpense>(entity =>
            {
                entity.HasKey(e => e.CapexandMiscellaneousExpensesId)
                    .HasName("PK_Master_CapexandMiscellaneousExpenses");

                entity.ToTable("PIDF_PBF_RnD_CapexandMiscellaneousExpenses");

                entity.Property(e => e.CapexandMiscellaneousExpensesId).ValueGeneratedNever();

                entity.Property(e => e.Capex1).HasMaxLength(50);

                entity.Property(e => e.Capex2).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Licensing).HasMaxLength(50);

                entity.Property(e => e.Miscellaneous).HasMaxLength(50);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientExhibit>(entity =>
            {
                entity.HasKey(e => e.ExicipientExhibitId)
                    .HasName("PK_Master_ExicipientExhibit");

                entity.ToTable("PIDF_PBF_RnD_ExicipientExhibit");

                entity.Property(e => e.ExicipientExhibitId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosagePerUnit).HasMaxLength(50);

                entity.Property(e => e.ExicipientExhibit).HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDExicipientExhibits)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientExhibit_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientProtoype>(entity =>
            {
                entity.HasKey(e => e.ExicipientProtoypeId)
                    .HasName("PK_Master_ExicipientProtoype");

                entity.ToTable("PIDF_PBF_RnD_ExicipientProtoype");

                entity.Property(e => e.ExicipientProtoypeId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosagePerUnit).HasMaxLength(50);

                entity.Property(e => e.ExicipientPrototype)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDExicipientProtoypes)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_ExicipientProtoype_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDExicipientScaleUp>(entity =>
            {
                entity.HasKey(e => e.ExicipientScaleUpId)
                    .HasName("PK_Master_ExicipientScaleUp");

                entity.ToTable("PIDF_PBF_RnD_ExicipientScaleUp");

                entity.Property(e => e.ExicipientScaleUpId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DosagePerUnit)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.ExicipientPrototype).HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDExicipientScaleUps)
                    .HasForeignKey(d => d.StrengthId)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Scaleup_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDFillingExpense>(entity =>
            {
                entity.HasKey(e => e.FillingExpensesId)
                    .HasName("PK_Master_FillingExpenses");

                entity.ToTable("PIDF_PBF_RnD_FillingExpenses");

                entity.Property(e => e.FillingExpensesId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfRnDManPowerCostAndProjectDuration>(entity =>
            {
                entity.HasKey(e => e.ManPowerCostAndProjectDurationId)
                    .HasName("PK_ManPowerCostAndProjectDuration");

                entity.ToTable("PIDF_PBF_RnD_ManPowerCostAndProjectDuration");

                entity.Property(e => e.ManPowerCostAndProjectDurationId).ValueGeneratedNever();

                entity.Property(e => e.AmvandTt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("AMVandTT");

                entity.Property(e => e.AnalyticalDevelopment).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExhibitBatch).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExhibitCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Filling).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FormulationDevelopment).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LiteratureReviewAndSourcing).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ManHourRate).HasMaxLength(50);

                entity.Property(e => e.PilotBioStudy).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PivotalBioStudy).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProjectInitiation).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrototypeCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ScaleUp).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ScaleUpCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Stability).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PidfPbfRnDPackagingExhibit>(entity =>
            {
                entity.HasKey(e => e.PackagingExhibitId)
                    .HasName("PK_Master_PackagingExhibit");

                entity.ToTable("PIDF_PBF_RnD_PackagingExhibit");

                entity.Property(e => e.PackagingExhibitId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurement).HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPackagingExhibits)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingExhibit_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDPackagingPrototype>(entity =>
            {
                entity.HasKey(e => e.PackagingPrototypeId)
                    .HasName("PK_Master_PackagingPrototype");

                entity.ToTable("PIDF_PBF_RnD_PackagingPrototype");

                entity.Property(e => e.PackagingPrototypeId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurement).HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPackagingPrototypes)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDPackagingScaleUp>(entity =>
            {
                entity.HasKey(e => e.PackagingScaleUpId)
                    .HasName("PK_Master_PackagingScaleUp");

                entity.ToTable("PIDF_PBF_RnD_PackagingScaleUp");

                entity.Property(e => e.PackagingScaleUpId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurement).HasMaxLength(50);

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPackagingScaleUps)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PackagingScaleUp_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDPlantSupportCost>(entity =>
            {
                entity.HasKey(e => e.PlantSupportCostId)
                    .HasName("PK_Master_PlantSupportCost");

                entity.ToTable("PIDF_PBF_RnD_PlantSupportCost");

                entity.Property(e => e.PlantSupportCostId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExhibitBatch).HasMaxLength(50);

                entity.Property(e => e.ScaleUp).HasMaxLength(50);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRnDPlantSupportCosts)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_RnD_PlantSupportCost_Strength");
            });

            modelBuilder.Entity<PidfPbfRnDToolingandChangePartCost>(entity =>
            {
                entity.HasKey(e => e.ToolingandChangePartCostId)
                    .HasName("PK_Master_ToolingandChangePartCost");

                entity.ToTable("PIDF_PBF_RnD_ToolingandChangePartCost");

                entity.Property(e => e.ToolingandChangePartCostId).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Prototype).HasMaxLength(50);

                entity.Property(e => e.PrototypeDevelopment).HasMaxLength(50);

                entity.Property(e => e.ScaleUpandExhibitBatch).HasMaxLength(50);

                entity.Property(e => e.TotalCost1)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Total_Cost");

                entity.Property(e => e.TotalScaleupandExhibitBatch).HasMaxLength(50);
            });

            modelBuilder.Entity<PidfPbfRndProjectActivity>(entity =>
            {
                entity.HasKey(e => e.ProjectActivitiesId);

                entity.ToTable("PIDF_PBF_Rnd_ProjectActivities");

                entity.Property(e => e.ProjectActivitiesId).ValueGeneratedNever();

                entity.Property(e => e.ProjectActivityName).HasMaxLength(50);
            });

            modelBuilder.Entity<PidfPbfRndProjectEstimation>(entity =>
            {
                entity.HasKey(e => e.ProjectActivitiesId);

                entity.ToTable("PIDF_PBF_Rnd_ProjectEstimation");

                entity.Property(e => e.ProjectActivitiesId).ValueGeneratedNever();

                entity.Property(e => e.NonRld).HasColumnName("NonRLD");

                entity.Property(e => e.Rld).HasColumnName("RLD");

                entity.HasOne(d => d.ProjectActivity)
                    .WithMany(p => p.PidfPbfRndProjectEstimations)
                    .HasForeignKey(d => d.ProjectActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Rnd_ProjectEstimation_ProjectActivity");

                entity.HasOne(d => d.Strength)
                    .WithMany(p => p.PidfPbfRndProjectEstimations)
                    .HasForeignKey(d => d.StrengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PIDF_PBF_Rnd_ProjectEstimation_Strength");
            });

            modelBuilder.Entity<PidfPbfRndReferenceProductDetail>(entity =>
            {
                entity.HasKey(e => e.ReferenceProductDetailId)
                    .HasName("PK_PIDF_PBF_ReferenceProductDetail");

                entity.ToTable("PIDF_PBF_Rnd_ReferenceProductDetail");

                entity.Property(e => e.ReferenceProductDetailId).ValueGeneratedNever();

                entity.Property(e => e.FormulationDevelopment).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PharmasuiticalEquivalence).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PilotBe)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PilotBE");

                entity.Property(e => e.PivotalBio).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnitCostOfReferenceProduct).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Pidfapidetail>(entity =>
            {
                entity.HasKey(e => e.Pidfapiid);

                entity.ToTable("PIDFAPIDetails");

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
                entity.ToTable("PIDFProductStrength");

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
                entity.ToTable("PIDFStatusHistory");

                entity.Property(e => e.PidfstatusHistoryId).HasColumnName("PIDFStatusHistoryId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Pidfid).HasColumnName("PIDFID");

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

            modelBuilder.Entity<RoleModulePermission>(entity =>
            {
                entity.HasKey(e => e.RoleModuleId);

                entity.ToTable("RoleModulePermission");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
