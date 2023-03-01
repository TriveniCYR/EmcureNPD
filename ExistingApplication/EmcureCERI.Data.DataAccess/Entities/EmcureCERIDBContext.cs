using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class EmcureCERIDBContext : DbContext
    {
        public EmcureCERIDBContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public EmcureCERIDBContext(DbContextOptions<EmcureCERIDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswerDetails> AnswerDetails { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<BaselineDataMaster> BaselineDataMaster { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<FollowUpFormMaster> FollowUpFormMaster { get; set; }
        public virtual DbSet<Fufquestionnaire1> Fufquestionnaire1 { get; set; }
        public virtual DbSet<Fufquestionnaire2> Fufquestionnaire2 { get; set; }
        public virtual DbSet<Fufquestionnaire3> Fufquestionnaire3 { get; set; }
        public virtual DbSet<Fufquestionnaire4> Fufquestionnaire4 { get; set; }
        public virtual DbSet<Fufquestionnaire5> Fufquestionnaire5 { get; set; }
        public virtual DbSet<Fufquestionnaire6> Fufquestionnaire6 { get; set; }
        public virtual DbSet<Fufquestionnaire7> Fufquestionnaire7 { get; set; }
        public virtual DbSet<Outcome> Outcome { get; set; }
        public virtual DbSet<PatientDetails> PatientDetails { get; set; }
        public virtual DbSet<PIDFDetails> PIDFDetails { get; set; }
        public virtual DbSet<PatientFollowUpForm> PatientFollowUpForm { get; set; }
        public virtual DbSet<PrescriberDetails> PrescriberDetails { get; set; }
        public virtual DbSet<QuestionDetails> QuestionDetails { get; set; }
        public virtual DbSet<Questionnaire1> Questionnaire1 { get; set; }
        public virtual DbSet<Questionnaire2> Questionnaire2 { get; set; }
        public virtual DbSet<Questionnaire3> Questionnaire3 { get; set; }
        public virtual DbSet<Questionnaire4> Questionnaire4 { get; set; }
        public virtual DbSet<RelaStudyDrug> RelaStudyDrug { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<StudyDrug> StudyDrug { get; set; }

        public virtual DbSet<Master_Country> Master_Country { get; set; }

        public virtual DbSet<Master_Continent> Master_Continent { get; set; }

        public virtual DbSet<DRFDetails> DRFDetails { get; set; }
        public virtual DbSet<Tbl_Master_TherapeuticCategory> Tbl_Master_TherapeuticCategory { get; set; }
        public virtual DbSet<Tbl_Master_ProductManufacture> Tbl_Master_ProductManufacture { get; set; }

        public virtual DbSet<Tbl_Master_Formulation> Tbl_Master_Formulation { get; set; }

        public virtual DbSet<Tbl_Master_DossierTemplate> Tbl_Master_DossierTemplate { get; set; }

        public virtual DbSet<Tbl_Master_DrugCategory> Tbl_Master_DrugCategory { get; set; }

        public virtual DbSet<Tbl_DRF_PIDF_Mapping> Tbl_DRF_PIDF_Mapping { get; set; }

        public virtual DbSet<Tbl_DSR_NewProductContinentMapping> Tbl_DSR_NewProductContinentMapping { get; set; }
        public virtual DbSet<Tbl_Master_RegistrationFees> Tbl_Master_RegistrationFees { get; set; }
        public virtual DbSet<Tbl_DRF_Percentage_Mapping> Tbl_DRF_Percentage_Mapping { get; set; }

        public virtual DbSet<Tbl_Master_PIDFStatus> Tbl_Master_PIDFStatus { get; set; }

        public virtual DbSet<Tbl_PIDF_Header> Tbl_PIDF_Header { get; set; }

        public virtual DbSet<Tbl_PIDF_CountryDetails> Tbl_PIDF_CountryDetails { get; set; }
        public virtual DbSet<Tbl_Master_Packing> Tbl_Master_Packing { get; set; }

        public virtual DbSet<Tbl_Master_ProjectTask_Mapping> Tbl_Master_ProjectTask_Mapping { get; set; }

        public virtual DbSet<Tbl_Master_SubTask> Tbl_Master_SubTask { get; set; }
        public virtual DbSet<Tbl_Master_Task> Tbl_Master_Task { get; set; }
        public virtual DbSet<Tbl_Master_Priority> Tbl_Master_Priority { get; set; }

        public virtual DbSet<Tbl_DRF_Initialization> Tbl_DRF_Initialization { get; set; }
        public virtual DbSet<Tbl_Gantt_Link> Tbl_Gantt_Link { get; set; }
        public virtual DbSet<Tbl_Gantt_Resources> Tbl_Gantt_Resources { get; set; }

        public virtual DbSet<Tbl_Master_ArtworkType> Tbl_Master_ArtworkType { get; set; }
        public virtual DbSet<Tbl_Master_APISite> Tbl_Master_APISite { get; set; }
        public virtual DbSet<Tbl_Master_ManufacturingSite> Tbl_Master_ManufacturingSite { get; set; }
        public virtual DbSet<Tbl_DRF_Manufacturing_APISite> Tbl_DRF_Manufacturing_APISite { get; set; }
        public virtual DbSet<Tbl_DRF_SupplyChainMgmt> Tbl_DRF_SupplyChainMgmt { get; set; }

        public virtual DbSet<Tbl_Master_GMPAvailability> Tbl_Master_GMPAvailability { get; set; }
        public virtual DbSet<Tbl_DRF_Manufacturing> Tbl_DRF_Manufacturing { get; set; }
        public virtual DbSet<Tbl_DRF_Requisite_RAInfo> Tbl_DRF_Requisite_RAInfo { get; set; }
        public virtual DbSet<Tbl_DRF_IP_Details> Tbl_DRF_IP_Details { get; set; }
        public virtual DbSet<Tbl_DRF_Patent_Details> Tbl_DRF_Patent_Details { get; set; }

        public virtual DbSet<Tbl_DRF_FinanceDetails> Tbl_DRF_FinanceDetails { get; set; }
        public virtual DbSet<Tbl_DRF_FinalApprovelDetails> Tbl_DRF_FinalApprovelDetails { get; set; }
        public virtual DbSet<Tbl_DRFDataMaster> Tbl_DRFDataMaster { get; set; }
        public virtual DbSet<Tbl_Master_PackSize> Tbl_Master_PackSize { get; set; }
        public virtual DbSet<Tbl_Master_PackStyle> Tbl_Master_PackStyle { get; set; }
        public virtual DbSet<Tbl_Master_Strength> Tbl_Master_Strength { get; set; }
        public virtual DbSet<Tbl_Master_ModeofFeesPayment> Tbl_Master_ModeofFeesPayment { get; set; }
        public virtual DbSet<Tbl_Master_Incoterms> Tbl_Master_Incoterms { get; set; }
        public virtual DbSet<Tbl_Master_Modeofshipment> Tbl_Master_Modeofshipment { get; set; }
        public virtual DbSet<Tbl_DRF_Medical> Tbl_DRF_Medical { get; set; }

        public virtual DbSet<Tbl_Master_Currency> Tbl_Master_Currency { get; set; }

        public virtual DbSet<Tbl_Master_Product> Tbl_Master_Product { get; set; }
        public virtual DbSet<Tbl_Master_Unit> Tbl_Master_Unit { get; set; }
        public virtual DbSet<Tbl_PIDF_HeaderNew> Tbl_PIDF_HeaderNew { get; set; }
        public virtual DbSet<Tbl_PIDF_CountryDetailsNew> Tbl_PIDF_CountryDetailsNew { get; set; }

        public virtual DbSet<Tbl_Master_FolderStructure> Tbl_Master_FolderStructure { get; set; }
        public virtual DbSet<Tbl_PIDF_UploadFileDetails> Tbl_PIDF_UploadFileDetails { get; set; }
        public virtual DbSet<Tbl_Master_PIDF_Task> Tbl_Master_PIDF_Task { get; set; }
        public virtual DbSet<Tbl_Master_PIDF_SubTask> Tbl_Master_PIDF_SubTask { get; set; }
        public virtual DbSet<Tbl_Pidf_Strength> Tbl_Pidf_Strength { get; set; }
        public virtual DbSet<Tbl_Master_Pidf_Workflow> Tbl_Master_Pidf_Workflow { get; set; }
        public virtual DbSet<Tbl_Transaction_CheckList> Tbl_Transaction_CheckList { get; set; }
        public virtual DbSet<Tbl_Master_MAHolder> Tbl_Master_MAHolder { get; set; }
        public virtual DbSet<CountryDailingCode> CountryDailingCode { get; set; }
        public virtual DbSet<Tbl_DRF_FormApprovals> Tbl_DRF_FormApprovals { get; set; }
        public virtual DbSet<Tbl_Master_Orderfrequency> Tbl_Master_Orderfrequency { get; set; } 
        public virtual DbSet<Tbl_Master_Department> Tbl_Master_Department { get; set; }
        public virtual DbSet<Tbl_Master_User_Continent_Country_Mapping> Tbl_Master_User_Continent_Country_Mapping { get; set; } 
        public virtual DbSet<Tbl_Master_User_Country_Mapping> Tbl_Master_User_Country_Mapping { get; set; } 
        public virtual DbSet<Tbl_Master_MarketingStatus> Tbl_Master_MarketingStatus { get; set; } 
        public virtual DbSet<Tbl_Master_ProductType> Tbl_Master_ProductType { get; set; }
        
        public virtual DbSet<Tbl_Master_Company> Tbl_Master_Company { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(clsEFDBConnection.MyEFDBConnection); 

            }
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<AnswerDetails>(entity =>
    {
        entity.Property(e => e.Answer).HasMaxLength(250);
    });

    modelBuilder.Entity<AspNetRoleClaims>(entity =>
    {
        entity.Property(e => e.RoleId)
            .IsRequired()
            .HasMaxLength(450);

        entity.HasOne(d => d.Role)
            .WithMany(p => p.AspNetRoleClaims)
            .HasForeignKey(d => d.RoleId);
    });

    modelBuilder.Entity<AspNetRoles>(entity =>
    {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Name).HasMaxLength(256);

        entity.Property(e => e.NormalizedName).HasMaxLength(256);
    });

    modelBuilder.Entity<AspNetUserClaims>(entity =>
    {
        entity.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(450);

        entity.HasOne(d => d.User)
            .WithMany(p => p.AspNetUserClaims)
            .HasForeignKey(d => d.UserId);
    });

    modelBuilder.Entity<AspNetUserLogins>(entity =>
    {
        entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        entity.Property(e => e.LoginProvider).HasMaxLength(128);

        entity.Property(e => e.ProviderKey).HasMaxLength(128);

        entity.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(450);

        entity.HasOne(d => d.User)
            .WithMany(p => p.AspNetUserLogins)
            .HasForeignKey(d => d.UserId);
    });

    modelBuilder.Entity<AspNetUserRoles>(entity =>
    {
        entity.HasKey(e => new { e.UserId, e.RoleId });

        entity.HasOne(d => d.Role)
            .WithMany(p => p.AspNetUserRoles)
            .HasForeignKey(d => d.RoleId);

        entity.HasOne(d => d.User)
            .WithMany(p => p.AspNetUserRoles)
            .HasForeignKey(d => d.UserId);
    });

    modelBuilder.Entity<AspNetUsers>(entity =>
    {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.DeactivationReason).HasMaxLength(250);

        entity.Property(e => e.Email).HasMaxLength(256);

        entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

        entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

        entity.Property(e => e.RejectionReason).HasMaxLength(250);

        entity.Property(e => e.UserId).ValueGeneratedOnAdd();

        entity.Property(e => e.UserName).HasMaxLength(256);
    });

    modelBuilder.Entity<AspNetUserTokens>(entity =>
    {
        entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

        entity.Property(e => e.LoginProvider).HasMaxLength(128);

        entity.Property(e => e.Name).HasMaxLength(128);

        entity.HasOne(d => d.User)
            .WithMany(p => p.AspNetUserTokens)
            .HasForeignKey(d => d.UserId);
    });

    modelBuilder.Entity<BaselineDataMaster>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.IsConfirmedByHcp).HasColumnName("IsConfirmedByHCP");

        entity.Property(e => e.RejectionReason).HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

        entity.HasOne(d => d.Patient)
            .WithMany(p => p.BaselineDataMaster)
            .HasForeignKey(d => d.PatientId)
            .HasConstraintName("FK_BaselineDataMaster_PatientDetails");

        entity.HasOne(d => d.Quest1Navigation)
            .WithMany(p => p.BaselineDataMaster)
            .HasForeignKey(d => d.Quest1)
            .HasConstraintName("FK_BaselineDataMaster_Questionnaire1");

        entity.HasOne(d => d.Quest2Navigation)
            .WithMany(p => p.BaselineDataMaster)
            .HasForeignKey(d => d.Quest2)
            .HasConstraintName("FK_BaselineDataMaster_Questionnaire2");

        entity.HasOne(d => d.Quest3Navigation)
            .WithMany(p => p.BaselineDataMaster)
            .HasForeignKey(d => d.Quest3)
            .HasConstraintName("FK_BaselineDataMaster_Questionnaire3");

        entity.HasOne(d => d.Quest4Navigation)
            .WithMany(p => p.BaselineDataMaster)
            .HasForeignKey(d => d.Quest4)
            .HasConstraintName("FK_BaselineDataMaster_Questionnaire4");
    });

    modelBuilder.Entity<Country>(entity =>
    {
        entity.Property(e => e.CountryName).HasMaxLength(250);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<FollowUpFormMaster>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Fufquest1).HasColumnName("FUFQuest1");

        entity.Property(e => e.Fufquest2).HasColumnName("FUFQuest2");

        entity.Property(e => e.Fufquest3).HasColumnName("FUFQuest3");

        entity.Property(e => e.Fufquest4).HasColumnName("FUFQuest4");

        entity.Property(e => e.Fufquest5).HasColumnName("FUFQuest5");

        entity.Property(e => e.Fufquest6).HasColumnName("FUFQuest6");

        entity.Property(e => e.Fufquest7).HasColumnName("FUFQuest7");

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

        entity.HasOne(d => d.Fufquest1Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest1)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire1");

        entity.HasOne(d => d.Fufquest2Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest2)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire2");

        entity.HasOne(d => d.Fufquest3Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest3)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire3");

        entity.HasOne(d => d.Fufquest4Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest4)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire4");

        entity.HasOne(d => d.Fufquest5Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest5)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire5");

        entity.HasOne(d => d.Fufquest6Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest6)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire6");

        entity.HasOne(d => d.Fufquest7Navigation)
            .WithMany(p => p.FollowUpFormMaster)
            .HasForeignKey(d => d.Fufquest7)
            .HasConstraintName("FK_FollowUpForm_FUFQuestionnaire7");
    });

    modelBuilder.Entity<Fufquestionnaire1>(entity =>
    {
        entity.ToTable("FUFQuestionnaire1");

        entity.Property(e => e.Aedetails)
            .HasColumnName("AEDetails")
            .HasMaxLength(1000);

        entity.Property(e => e.ComMedId1).HasMaxLength(250);

        entity.Property(e => e.ComMedId2).HasMaxLength(250);

        entity.Property(e => e.ComMedId3).HasMaxLength(250);

        entity.Property(e => e.ComMedId4).HasMaxLength(250);

        entity.Property(e => e.ComMedId5).HasMaxLength(250);

        entity.Property(e => e.ComMedId6).HasMaxLength(250);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.EventTerm1).HasMaxLength(250);

        entity.Property(e => e.EventTerm2).HasMaxLength(250);

        entity.Property(e => e.EventTerm3).HasMaxLength(250);

        entity.Property(e => e.EventTerm4).HasMaxLength(250);

        entity.Property(e => e.EventTerm5).HasMaxLength(250);

        entity.Property(e => e.EventTerm6).HasMaxLength(250);

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.Pexperienced).HasColumnName("PExperienced");

        entity.Property(e => e.PrescriberName).HasMaxLength(250);

        entity.Property(e => e.SaeId1).HasMaxLength(250);

        entity.Property(e => e.SaeId2).HasMaxLength(250);

        entity.Property(e => e.SaeId3).HasMaxLength(250);

        entity.Property(e => e.SaeId4).HasMaxLength(250);

        entity.Property(e => e.SaeId5).HasMaxLength(250);

        entity.Property(e => e.SaeId6).HasMaxLength(250);

        entity.Property(e => e.Specify).HasMaxLength(500);

        entity.Property(e => e.StartDate1).HasColumnType("datetime");

        entity.Property(e => e.StartDate2).HasColumnType("datetime");

        entity.Property(e => e.StartDate3).HasColumnType("datetime");

        entity.Property(e => e.StartDate4).HasColumnType("datetime");

        entity.Property(e => e.StartDate5).HasColumnType("datetime");

        entity.Property(e => e.StartDate6).HasColumnType("datetime");

        entity.Property(e => e.StopDate1).HasColumnType("datetime");

        entity.Property(e => e.StopDate2).HasColumnType("datetime");

        entity.Property(e => e.StopDate3).HasColumnType("datetime");

        entity.Property(e => e.StopDate4).HasColumnType("datetime");

        entity.Property(e => e.StopDate5).HasColumnType("datetime");

        entity.Property(e => e.StopDate6).HasColumnType("datetime");

        entity.Property(e => e.StudyDaid1).HasColumnName("StudyDAId1");

        entity.Property(e => e.StudyDaid2).HasColumnName("StudyDAId2");

        entity.Property(e => e.StudyDaid3).HasColumnName("StudyDAId3");

        entity.Property(e => e.StudyDaid4).HasColumnName("StudyDAId4");

        entity.Property(e => e.StudyDaid5).HasColumnName("StudyDAId5");

        entity.Property(e => e.StudyDaid6).HasColumnName("StudyDAId6");

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Fufquestionnaire2>(entity =>
    {
        entity.ToTable("FUFQuestionnaire2");

        entity.Property(e => e.BbicarbonateA)
            .HasColumnName("BBicarbonateA")
            .HasMaxLength(250);

        entity.Property(e => e.BbicarbonateC)
            .HasColumnName("BBicarbonateC")
            .HasMaxLength(250);

        entity.Property(e => e.BbiochemistryA)
            .HasColumnName("BBiochemistryA")
            .HasMaxLength(250);

        entity.Property(e => e.BbiochemistryC)
            .HasColumnName("BBiochemistryC")
            .HasMaxLength(250);

        entity.Property(e => e.Bcomments)
            .HasColumnName("BComments")
            .HasMaxLength(250);

        entity.Property(e => e.BcreatinineA)
            .HasColumnName("BCreatinineA")
            .HasMaxLength(250);

        entity.Property(e => e.BcreatinineC)
            .HasColumnName("BCreatinineC")
            .HasMaxLength(250);

        entity.Property(e => e.Bdoa)
            .HasColumnName("BDOA")
            .HasColumnType("datetime");

        entity.Property(e => e.BhaematologyA)
            .HasColumnName("BHaematologyA")
            .HasMaxLength(250);

        entity.Property(e => e.BhaematologyC)
            .HasColumnName("BHaematologyC")
            .HasMaxLength(250);

        entity.Property(e => e.BothersSpecify)
            .HasColumnName("BOthersSpecify")
            .HasMaxLength(250);

        entity.Property(e => e.BothersSpecifyA)
            .HasColumnName("BOthersSpecifyA")
            .HasMaxLength(250);

        entity.Property(e => e.BothersSpecifyC)
            .HasColumnName("BOthersSpecifyC")
            .HasMaxLength(250);

        entity.Property(e => e.BphosphateA)
            .HasColumnName("BPhosphateA")
            .HasMaxLength(250);

        entity.Property(e => e.BphosphateC)
            .HasColumnName("BPhosphateC")
            .HasMaxLength(250);

        entity.Property(e => e.BserologyA)
            .HasColumnName("BSerologyA")
            .HasMaxLength(250);

        entity.Property(e => e.BserologyC)
            .HasColumnName("BSerologyC")
            .HasMaxLength(250);

        entity.Property(e => e.BureaA)
            .HasColumnName("BUreaA")
            .HasMaxLength(250);

        entity.Property(e => e.BureaC)
            .HasColumnName("BUreaC")
            .HasMaxLength(250);

        entity.Property(e => e.BuricAcidA)
            .HasColumnName("BUricAcidA")
            .HasMaxLength(250);

        entity.Property(e => e.BuricAcidC)
            .HasColumnName("BUricAcidC")
            .HasMaxLength(250);

        entity.Property(e => e.BurineAnalysisA)
            .HasColumnName("BUrineAnalysisA")
            .HasMaxLength(250);

        entity.Property(e => e.BurineAnalysisC)
            .HasColumnName("BUrineAnalysisC")
            .HasMaxLength(250);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.FbicarbonateA)
            .HasColumnName("FBicarbonateA")
            .HasMaxLength(250);

        entity.Property(e => e.FbicarbonateC)
            .HasColumnName("FBicarbonateC")
            .HasMaxLength(250);

        entity.Property(e => e.FbiochemistryA)
            .HasColumnName("FBiochemistryA")
            .HasMaxLength(250);

        entity.Property(e => e.FbiochemistryC)
            .HasColumnName("FBiochemistryC")
            .HasMaxLength(250);

        entity.Property(e => e.Fcomments)
            .HasColumnName("FComments")
            .HasMaxLength(250);

        entity.Property(e => e.FcreatinineA)
            .HasColumnName("FCreatinineA")
            .HasMaxLength(250);

        entity.Property(e => e.FcreatinineC)
            .HasColumnName("FCreatinineC")
            .HasMaxLength(250);

        entity.Property(e => e.Fdoa)
            .HasColumnName("FDOA")
            .HasColumnType("datetime");

        entity.Property(e => e.FhaematologyA)
            .HasColumnName("FHaematologyA")
            .HasMaxLength(250);

        entity.Property(e => e.FhaematologyC)
            .HasColumnName("FHaematologyC")
            .HasMaxLength(250);

        entity.Property(e => e.FothersSpecify)
            .HasColumnName("FOthersSpecify")
            .HasMaxLength(250);

        entity.Property(e => e.FothersSpecifyA)
            .HasColumnName("FOthersSpecifyA")
            .HasMaxLength(250);

        entity.Property(e => e.FothersSpecifyC)
            .HasColumnName("FOthersSpecifyC")
            .HasMaxLength(250);

        entity.Property(e => e.FphosphateA)
            .HasColumnName("FPhosphateA")
            .HasMaxLength(250);

        entity.Property(e => e.FphosphateC)
            .HasColumnName("FPhosphateC")
            .HasMaxLength(250);

        entity.Property(e => e.FserologyA)
            .HasColumnName("FSerologyA")
            .HasMaxLength(250);

        entity.Property(e => e.FserologyC)
            .HasColumnName("FSerologyC")
            .HasMaxLength(250);

        entity.Property(e => e.FureaA)
            .HasColumnName("FUreaA")
            .HasMaxLength(250);

        entity.Property(e => e.FureaC)
            .HasColumnName("FUreaC")
            .HasMaxLength(250);

        entity.Property(e => e.FuricAcidA)
            .HasColumnName("FUricAcidA")
            .HasMaxLength(250);

        entity.Property(e => e.FuricAcidC)
            .HasColumnName("FUricAcidC")
            .HasMaxLength(250);

        entity.Property(e => e.FurineAnalysisA)
            .HasColumnName("FUrineAnalysisA")
            .HasMaxLength(250);

        entity.Property(e => e.FurineAnalysisC)
            .HasColumnName("FUrineAnalysisC")
            .HasMaxLength(250);

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Fufquestionnaire3>(entity =>
    {
        entity.ToTable("FUFQuestionnaire3");

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Doa)
            .HasColumnName("DOA")
            .HasColumnType("datetime");

        entity.Property(e => e.Dop).HasColumnName("DOP");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.OtherOutcomesSpecify).HasMaxLength(500);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.WithdrawalReason).HasMaxLength(500);
    });

    modelBuilder.Entity<Fufquestionnaire4>(entity =>
    {
        entity.ToTable("FUFQuestionnaire4");

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Dose).HasMaxLength(500);

        entity.Property(e => e.DotstartDate)
            .HasColumnName("DOTStartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.DotstopDate)
            .HasColumnName("DOTStopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Frequency).HasMaxLength(500);

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.Indication).HasMaxLength(500);

        entity.Property(e => e.Remarks).HasMaxLength(500);

        entity.Property(e => e.Route).HasMaxLength(500);

        entity.Property(e => e.TotalDose).HasMaxLength(500);

        entity.Property(e => e.TotalTreatment).HasMaxLength(500);

        entity.Property(e => e.Tremarks)
            .HasColumnName("TRemarks")
            .HasMaxLength(500);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Fufquestionnaire5>(entity =>
    {
        entity.ToTable("FUFQuestionnaire5");

        entity.Property(e => e.C10condition)
            .HasColumnName("C10Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C10ongoing).HasColumnName("C10Ongoing");

        entity.Property(e => e.C10startDate)
            .HasColumnName("C10StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C10stopDate)
            .HasColumnName("C10StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C1condition)
            .HasColumnName("C1Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C1ongoing).HasColumnName("C1Ongoing");

        entity.Property(e => e.C1startDate)
            .HasColumnName("C1StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C1stopDate)
            .HasColumnName("C1StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C2condition)
            .HasColumnName("C2Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C2ongoing).HasColumnName("C2Ongoing");

        entity.Property(e => e.C2startDate)
            .HasColumnName("C2StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C2stopDate)
            .HasColumnName("C2StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C3condition)
            .HasColumnName("C3Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C3ongoing).HasColumnName("C3Ongoing");

        entity.Property(e => e.C3startDate)
            .HasColumnName("C3StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C3stopDate)
            .HasColumnName("C3StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C4condition)
            .HasColumnName("C4Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C4ongoing).HasColumnName("C4Ongoing");

        entity.Property(e => e.C4startDate)
            .HasColumnName("C4StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C4stopDate)
            .HasColumnName("C4StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C5condition)
            .HasColumnName("C5Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C5ongoing).HasColumnName("C5Ongoing");

        entity.Property(e => e.C5startDate)
            .HasColumnName("C5StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C5stopDate)
            .HasColumnName("C5StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C6condition)
            .HasColumnName("C6Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C6ongoing).HasColumnName("C6Ongoing");

        entity.Property(e => e.C6startDate)
            .HasColumnName("C6StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C6stopDate)
            .HasColumnName("C6StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C7condition)
            .HasColumnName("C7Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C7ongoing).HasColumnName("C7Ongoing");

        entity.Property(e => e.C7startDate)
            .HasColumnName("C7StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C7stopDate)
            .HasColumnName("C7StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C8condition)
            .HasColumnName("C8Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C8ongoing).HasColumnName("C8Ongoing");

        entity.Property(e => e.C8startDate)
            .HasColumnName("C8StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C8stopDate)
            .HasColumnName("C8StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C9condition)
            .HasColumnName("C9Condition")
            .HasMaxLength(500);

        entity.Property(e => e.C9ongoing).HasColumnName("C9Ongoing");

        entity.Property(e => e.C9startDate)
            .HasColumnName("C9StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.C9stopDate)
            .HasColumnName("C9StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Comments).HasMaxLength(500);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Doa)
            .HasColumnName("DOA")
            .HasColumnType("datetime");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Fufquestionnaire6>(entity =>
    {
        entity.ToTable("FUFQuestionnaire6");

        entity.Property(e => e.Comments).HasMaxLength(250);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Doa)
            .HasColumnName("DOA")
            .HasColumnType("datetime");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.M10dosageUnits)
            .HasColumnName("M10DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M10frequency)
            .HasColumnName("M10Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M10indication)
            .HasColumnName("M10Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M10medication)
            .HasColumnName("M10Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M10ongoing).HasColumnName("M10Ongoing");

        entity.Property(e => e.M10reason)
            .HasColumnName("M10Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M10route)
            .HasColumnName("M10Route")
            .HasMaxLength(250);

        entity.Property(e => e.M10startDate)
            .HasColumnName("M10StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M10stopDate)
            .HasColumnName("M10StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M1dosageUnits)
            .HasColumnName("M1DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M1frequency)
            .HasColumnName("M1Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M1indication)
            .HasColumnName("M1Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M1medication)
            .HasColumnName("M1Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M1ongoing).HasColumnName("M1Ongoing");

        entity.Property(e => e.M1reason)
            .HasColumnName("M1Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M1route)
            .HasColumnName("M1Route")
            .HasMaxLength(250);

        entity.Property(e => e.M1startDate)
            .HasColumnName("M1StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M1stopDate)
            .HasColumnName("M1StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M2dosageUnits)
            .HasColumnName("M2DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M2frequency)
            .HasColumnName("M2Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M2indication)
            .HasColumnName("M2Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M2medication)
            .HasColumnName("M2Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M2ongoing).HasColumnName("M2Ongoing");

        entity.Property(e => e.M2reason)
            .HasColumnName("M2Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M2route)
            .HasColumnName("M2Route")
            .HasMaxLength(250);

        entity.Property(e => e.M2startDate)
            .HasColumnName("M2StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M2stopDate)
            .HasColumnName("M2StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M3dosageUnits)
            .HasColumnName("M3DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M3frequency)
            .HasColumnName("M3Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M3indication)
            .HasColumnName("M3Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M3medication)
            .HasColumnName("M3Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M3ongoing).HasColumnName("M3Ongoing");

        entity.Property(e => e.M3reason)
            .HasColumnName("M3Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M3route)
            .HasColumnName("M3Route")
            .HasMaxLength(250);

        entity.Property(e => e.M3startDate)
            .HasColumnName("M3StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M3stopDate)
            .HasColumnName("M3StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M4dosageUnits)
            .HasColumnName("M4DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M4frequency)
            .HasColumnName("M4Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M4indication)
            .HasColumnName("M4Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M4medication)
            .HasColumnName("M4Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M4ongoing).HasColumnName("M4Ongoing");

        entity.Property(e => e.M4reason)
            .HasColumnName("M4Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M4route)
            .HasColumnName("M4Route")
            .HasMaxLength(250);

        entity.Property(e => e.M4startDate)
            .HasColumnName("M4StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M4stopDate)
            .HasColumnName("M4StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M5dosageUnits)
            .HasColumnName("M5DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M5frequency)
            .HasColumnName("M5Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M5indication)
            .HasColumnName("M5Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M5medication)
            .HasColumnName("M5Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M5ongoing).HasColumnName("M5Ongoing");

        entity.Property(e => e.M5reason)
            .HasColumnName("M5Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M5route)
            .HasColumnName("M5Route")
            .HasMaxLength(250);

        entity.Property(e => e.M5startDate)
            .HasColumnName("M5StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M5stopDate)
            .HasColumnName("M5StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M6dosageUnits)
            .HasColumnName("M6DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M6frequency)
            .HasColumnName("M6Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M6indication)
            .HasColumnName("M6Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M6medication)
            .HasColumnName("M6Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M6ongoing).HasColumnName("M6Ongoing");

        entity.Property(e => e.M6reason)
            .HasColumnName("M6Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M6route)
            .HasColumnName("M6Route")
            .HasMaxLength(250);

        entity.Property(e => e.M6startDate)
            .HasColumnName("M6StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M6stopDate)
            .HasColumnName("M6StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M7dosageUnits)
            .HasColumnName("M7DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M7frequency)
            .HasColumnName("M7Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M7indication)
            .HasColumnName("M7Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M7medication)
            .HasColumnName("M7Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M7ongoing).HasColumnName("M7Ongoing");

        entity.Property(e => e.M7reason)
            .HasColumnName("M7Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M7route)
            .HasColumnName("M7Route")
            .HasMaxLength(250);

        entity.Property(e => e.M7startDate)
            .HasColumnName("M7StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M7stopDate)
            .HasColumnName("M7StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M8dosageUnits)
            .HasColumnName("M8DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M8frequency)
            .HasColumnName("M8Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M8indication)
            .HasColumnName("M8Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M8medication)
            .HasColumnName("M8Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M8ongoing).HasColumnName("M8Ongoing");

        entity.Property(e => e.M8reason)
            .HasColumnName("M8Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M8route)
            .HasColumnName("M8Route")
            .HasMaxLength(250);

        entity.Property(e => e.M8startDate)
            .HasColumnName("M8StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M8stopDate)
            .HasColumnName("M8StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M9dosageUnits)
            .HasColumnName("M9DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M9frequency)
            .HasColumnName("M9Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M9indication)
            .HasColumnName("M9Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M9medication)
            .HasColumnName("M9Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M9ongoing).HasColumnName("M9Ongoing");

        entity.Property(e => e.M9reason)
            .HasColumnName("M9Reason")
            .HasMaxLength(250);

        entity.Property(e => e.M9route)
            .HasColumnName("M9Route")
            .HasMaxLength(250);

        entity.Property(e => e.M9startDate)
            .HasColumnName("M9StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M9stopDate)
            .HasColumnName("M9StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Fufquestionnaire7>(entity =>
    {
        entity.ToTable("FUFQuestionnaire7");

        entity.Property(e => e.ConfirmFuf).HasColumnName("ConfirmFUF");

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Doa)
            .HasColumnName("DOA")
            .HasColumnType("datetime");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.PrescriberName).HasMaxLength(250);

        entity.Property(e => e.Statement).HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Outcome>(entity =>
    {
        entity.Property(e => e.NameBe)
            .HasColumnName("NameBE")
            .HasMaxLength(250);

        entity.Property(e => e.NameDe)
            .HasColumnName("NameDE")
            .HasMaxLength(250);

        entity.Property(e => e.NameEs)
            .HasColumnName("NameES")
            .HasMaxLength(250);

        entity.Property(e => e.NameFr)
            .HasColumnName("NameFR")
            .HasMaxLength(250);

        entity.Property(e => e.NameGb)
            .HasColumnName("NameGB")
            .HasMaxLength(250);
    });

    modelBuilder.Entity<PatientDetails>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.FirstName).HasMaxLength(250);

        entity.Property(e => e.IsConsentFcheckByAdmin).HasColumnName("IsConsentFCheckByAdmin");

        entity.Property(e => e.IsConsentFcheckByHcp).HasColumnName("IsConsentFCheckByHCP");

        entity.Property(e => e.LastName).HasMaxLength(250);

        entity.Property(e => e.PdfUploadDate).HasColumnType("datetime");

        entity.Property(e => e.Point1Date).HasColumnType("datetime");

        entity.Property(e => e.RejectionReason).HasMaxLength(250);

        entity.Property(e => e.RfirstName)
            .HasColumnName("RFirstName")
            .HasMaxLength(250);

        entity.Property(e => e.RlastName)
            .HasColumnName("RLastName")
            .HasMaxLength(250);

        entity.Property(e => e.UniqueId)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<PatientFollowUpForm>(entity =>
    {
        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.FollowUpFormName).HasMaxLength(50);
    });

    modelBuilder.Entity<PrescriberDetails>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.FirstName).HasMaxLength(250);

        entity.Property(e => e.GmcgpHcnumber)
            .HasColumnName("GMCGpHCNumber")
            .HasMaxLength(250);

        entity.Property(e => e.LastName).HasMaxLength(250);

        entity.Property(e => e.OtherSpecialization).HasMaxLength(250);

        entity.Property(e => e.TelephoneNumber).HasMaxLength(250);

        entity.Property(e => e.UniqueId)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<QuestionDetails>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.QuestionBe)
            .HasColumnName("QuestionBE")
            .HasMaxLength(250);

        entity.Property(e => e.QuestionDe)
            .HasColumnName("QuestionDE")
            .HasMaxLength(250);

        entity.Property(e => e.QuestionEs)
            .HasColumnName("QuestionES")
            .HasMaxLength(250);

        entity.Property(e => e.QuestionFr)
            .HasColumnName("QuestionFR")
            .HasMaxLength(250);

        entity.Property(e => e.QuestionGb)
            .HasColumnName("QuestionGB")
            .HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Questionnaire1>(entity =>
    {
        entity.Property(e => e.Cmv).HasColumnName("CMV");

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.DateAssessment).HasColumnType("datetime");

        entity.Property(e => e.DateParticipant).HasColumnType("datetime");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.Icg).HasColumnName("ICG");

        entity.Property(e => e.OtherIndication).HasMaxLength(500);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Questionnaire2>(entity =>
    {
        entity.Property(e => e.BicarbonateA).HasMaxLength(250);

        entity.Property(e => e.BicarbonateC).HasMaxLength(250);

        entity.Property(e => e.BiochemistryA).HasMaxLength(250);

        entity.Property(e => e.BiochemistryC).HasMaxLength(250);

        entity.Property(e => e.Comments).HasMaxLength(250);

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.CreatinineA).HasMaxLength(250);

        entity.Property(e => e.CreatinineC).HasMaxLength(250);

        entity.Property(e => e.Dob)
            .HasColumnName("DOB")
            .HasColumnType("datetime");

        entity.Property(e => e.HaematologyA).HasMaxLength(250);

        entity.Property(e => e.HaematologyC).HasMaxLength(250);

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.M10dosageUnits)
            .HasColumnName("M10DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M10frequency)
            .HasColumnName("M10Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M10indication)
            .HasColumnName("M10Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M10medication)
            .HasColumnName("M10Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M10ongoing).HasColumnName("M10Ongoing");

        entity.Property(e => e.M10route)
            .HasColumnName("M10Route")
            .HasMaxLength(250);

        entity.Property(e => e.M10startDate)
            .HasColumnName("M10StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M10stopDate)
            .HasColumnName("M10StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M11dosageUnits)
            .HasColumnName("M11DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M11frequency)
            .HasColumnName("M11Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M11indication)
            .HasColumnName("M11Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M11medication)
            .HasColumnName("M11Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M11ongoing).HasColumnName("M11Ongoing");

        entity.Property(e => e.M11route)
            .HasColumnName("M11Route")
            .HasMaxLength(250);

        entity.Property(e => e.M11startDate)
            .HasColumnName("M11StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M11stopDate)
            .HasColumnName("M11StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M12dosageUnits)
            .HasColumnName("M12DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M12frequency)
            .HasColumnName("M12Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M12indication)
            .HasColumnName("M12Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M12medication)
            .HasColumnName("M12Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M12ongoing).HasColumnName("M12Ongoing");

        entity.Property(e => e.M12route)
            .HasColumnName("M12Route")
            .HasMaxLength(250);

        entity.Property(e => e.M12startDate)
            .HasColumnName("M12StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M12stopDate)
            .HasColumnName("M12StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M1dosageUnits)
            .HasColumnName("M1DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M1frequency)
            .HasColumnName("M1Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M1indication)
            .HasColumnName("M1Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M1medication)
            .HasColumnName("M1Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M1ongoing).HasColumnName("M1Ongoing");

        entity.Property(e => e.M1route)
            .HasColumnName("M1Route")
            .HasMaxLength(250);

        entity.Property(e => e.M1startDate)
            .HasColumnName("M1StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M1stopDate)
            .HasColumnName("M1StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M2dosageUnits)
            .HasColumnName("M2DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M2frequency)
            .HasColumnName("M2Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M2indication)
            .HasColumnName("M2Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M2medication)
            .HasColumnName("M2Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M2ongoing).HasColumnName("M2Ongoing");

        entity.Property(e => e.M2route)
            .HasColumnName("M2Route")
            .HasMaxLength(250);

        entity.Property(e => e.M2startDate)
            .HasColumnName("M2StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M2stopDate)
            .HasColumnName("M2StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M3dosageUnits)
            .HasColumnName("M3DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M3frequency)
            .HasColumnName("M3Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M3indication)
            .HasColumnName("M3Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M3medication)
            .HasColumnName("M3Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M3ongoing).HasColumnName("M3Ongoing");

        entity.Property(e => e.M3route)
            .HasColumnName("M3Route")
            .HasMaxLength(250);

        entity.Property(e => e.M3startDate)
            .HasColumnName("M3StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M3stopDate)
            .HasColumnName("M3StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M4dosageUnits)
            .HasColumnName("M4DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M4frequency)
            .HasColumnName("M4Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M4indication)
            .HasColumnName("M4Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M4medication)
            .HasColumnName("M4Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M4ongoing).HasColumnName("M4Ongoing");

        entity.Property(e => e.M4route)
            .HasColumnName("M4Route")
            .HasMaxLength(250);

        entity.Property(e => e.M4startDate)
            .HasColumnName("M4StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M4stopDate)
            .HasColumnName("M4StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M5dosageUnits)
            .HasColumnName("M5DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M5frequency)
            .HasColumnName("M5Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M5indication)
            .HasColumnName("M5Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M5medication)
            .HasColumnName("M5Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M5ongoing).HasColumnName("M5Ongoing");

        entity.Property(e => e.M5route)
            .HasColumnName("M5Route")
            .HasMaxLength(250);

        entity.Property(e => e.M5startDate)
            .HasColumnName("M5StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M5stopDate)
            .HasColumnName("M5StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M6dosageUnits)
            .HasColumnName("M6DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M6frequency)
            .HasColumnName("M6Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M6indication)
            .HasColumnName("M6Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M6medication)
            .HasColumnName("M6Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M6ongoing).HasColumnName("M6Ongoing");

        entity.Property(e => e.M6route)
            .HasColumnName("M6Route")
            .HasMaxLength(250);

        entity.Property(e => e.M6startDate)
            .HasColumnName("M6StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M6stopDate)
            .HasColumnName("M6StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M7dosageUnits)
            .HasColumnName("M7DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M7frequency)
            .HasColumnName("M7Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M7indication)
            .HasColumnName("M7Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M7medication)
            .HasColumnName("M7Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M7ongoing).HasColumnName("M7Ongoing");

        entity.Property(e => e.M7route)
            .HasColumnName("M7Route")
            .HasMaxLength(250);

        entity.Property(e => e.M7startDate)
            .HasColumnName("M7StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M7stopDate)
            .HasColumnName("M7StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M8dosageUnits)
            .HasColumnName("M8DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M8frequency)
            .HasColumnName("M8Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M8indication)
            .HasColumnName("M8Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M8medication)
            .HasColumnName("M8Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M8ongoing).HasColumnName("M8Ongoing");

        entity.Property(e => e.M8route)
            .HasColumnName("M8Route")
            .HasMaxLength(250);

        entity.Property(e => e.M8startDate)
            .HasColumnName("M8StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M8stopDate)
            .HasColumnName("M8StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M9dosageUnits)
            .HasColumnName("M9DosageUnits")
            .HasMaxLength(250);

        entity.Property(e => e.M9frequency)
            .HasColumnName("M9Frequency")
            .HasMaxLength(250);

        entity.Property(e => e.M9indication)
            .HasColumnName("M9Indication")
            .HasMaxLength(250);

        entity.Property(e => e.M9medication)
            .HasColumnName("M9Medication")
            .HasMaxLength(250);

        entity.Property(e => e.M9ongoing).HasColumnName("M9Ongoing");

        entity.Property(e => e.M9route)
            .HasColumnName("M9Route")
            .HasMaxLength(250);

        entity.Property(e => e.M9startDate)
            .HasColumnName("M9StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.M9stopDate)
            .HasColumnName("M9StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh1anySignificant)
            .HasColumnName("MH1AnySignificant")
            .HasMaxLength(500);

        entity.Property(e => e.Mh1ongoing).HasColumnName("MH1Ongoing");

        entity.Property(e => e.Mh1startDate)
            .HasColumnName("MH1StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh1stopDate)
            .HasColumnName("MH1StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh2anySignificant)
            .HasColumnName("MH2AnySignificant")
            .HasMaxLength(500);

        entity.Property(e => e.Mh2ongoing).HasColumnName("MH2Ongoing");

        entity.Property(e => e.Mh2startDate)
            .HasColumnName("MH2StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh2stopDate)
            .HasColumnName("MH2StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh3anySignificant)
            .HasColumnName("MH3AnySignificant")
            .HasMaxLength(500);

        entity.Property(e => e.Mh3ongoing).HasColumnName("MH3Ongoing");

        entity.Property(e => e.Mh3startDate)
            .HasColumnName("MH3StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh3stopDate)
            .HasColumnName("MH3StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh4anySignificant)
            .HasColumnName("MH4AnySignificant")
            .HasMaxLength(500);

        entity.Property(e => e.Mh4ongoing).HasColumnName("MH4Ongoing");

        entity.Property(e => e.Mh4startDate)
            .HasColumnName("MH4StartDate")
            .HasColumnType("datetime");

        entity.Property(e => e.Mh4stopDate)
            .HasColumnName("MH4StopDate")
            .HasColumnType("datetime");

        entity.Property(e => e.OthersSpecify).HasMaxLength(250);

        entity.Property(e => e.OthersSpecifyA).HasMaxLength(250);

        entity.Property(e => e.OthersSpecifyC).HasMaxLength(250);

        entity.Property(e => e.PhosphateA).HasMaxLength(250);

        entity.Property(e => e.PhosphateC).HasMaxLength(250);

        entity.Property(e => e.SerologyA).HasMaxLength(250);

        entity.Property(e => e.SerologyC).HasMaxLength(250);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.UreaA).HasMaxLength(250);

        entity.Property(e => e.UreaC).HasMaxLength(250);

        entity.Property(e => e.UricAcidA).HasMaxLength(250);

        entity.Property(e => e.UricAcidC).HasMaxLength(250);

        entity.Property(e => e.UrineAnalysisA).HasMaxLength(250);

        entity.Property(e => e.UrineAnalysisC).HasMaxLength(250);
    });

    modelBuilder.Entity<Questionnaire3>(entity =>
    {
        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Dose).HasMaxLength(500);

        entity.Property(e => e.Frequency).HasMaxLength(500);

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.Indication).HasMaxLength(500);

        entity.Property(e => e.Remarks).HasMaxLength(500);

        entity.Property(e => e.Route).HasMaxLength(500);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<Questionnaire4>(entity =>
    {
        entity.Property(e => e.ConfirmBdf).HasColumnName("ConfirmBDF");

        entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");

        entity.Property(e => e.Doa)
            .HasColumnName("DOA")
            .HasColumnType("datetime");

        entity.Property(e => e.Heading).HasMaxLength(250);

        entity.Property(e => e.Statement).HasMaxLength(500);

        entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
    });

    modelBuilder.Entity<RelaStudyDrug>(entity =>
    {
        entity.Property(e => e.NameBe)
            .HasColumnName("NameBE")
            .HasMaxLength(250);

        entity.Property(e => e.NameDe)
            .HasColumnName("NameDE")
            .HasMaxLength(250);

        entity.Property(e => e.NameEs)
            .HasColumnName("NameES")
            .HasMaxLength(250);

        entity.Property(e => e.NameFr)
            .HasColumnName("NameFR")
            .HasMaxLength(250);

        entity.Property(e => e.NameGb)
            .HasColumnName("NameGB")
            .HasMaxLength(250);
    });

    modelBuilder.Entity<Specialization>(entity =>
    {
        entity.Property(e => e.NameBe)
            .HasColumnName("NameBE")
            .HasMaxLength(500);

        entity.Property(e => e.NameDe)
            .HasColumnName("NameDE")
            .HasMaxLength(500);

        entity.Property(e => e.NameEs)
            .HasColumnName("NameES")
            .HasMaxLength(500);

        entity.Property(e => e.NameFr)
            .HasColumnName("NameFR")
            .HasMaxLength(500);

        entity.Property(e => e.NameGb)
            .HasColumnName("NameGB")
            .HasMaxLength(500);
    });

    modelBuilder.Entity<StudyDrug>(entity =>
    {
        entity.Property(e => e.NameBe)
            .HasColumnName("NameBE")
            .HasMaxLength(250);

        entity.Property(e => e.NameDe)
            .HasColumnName("NameDE")
            .HasMaxLength(250);

        entity.Property(e => e.NameEs)
            .HasColumnName("NameES")
            .HasMaxLength(250);

        entity.Property(e => e.NameFr)
            .HasColumnName("NameFR")
            .HasMaxLength(250);

        entity.Property(e => e.NameGb)
            .HasColumnName("NameGB")
            .HasMaxLength(250);
    });
}
    }
}
