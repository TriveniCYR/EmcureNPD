/*-------------------------------------------------------------------------------------

(C) Copyright 2018 Dahlia Technologies. 
Use or Copying of all or any part of this program, except as
permitted by License Agreement, is prohibited.

-------------------------------------------------------------------------------------*/
namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Contract.ServiceContracts;
    using EmcureCERI.Business.Core.ServiceImplementations;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyResolver
    {
        public static IServiceCollection ContainerResolver(this IServiceCollection services)
        {
            services.AddScoped<EmcureCERIDBContext>(_ => new EmcureCERIDBContext());
            services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPIDFService, PIDFService>();
            services.AddTransient<IDRFService, DRFService>();
            services.AddTransient<IMaster_ContinentService, Master_ContinentService>();
            services.AddTransient<IMaster_CountryService, Master_CountryService>();
            services.AddTransient<IPrescriberService, PrescriberService>();
            services.AddTransient<ISpecializationService, SpecializationService>();
            services.AddTransient<IQAService, QAService>();
            services.AddTransient<IBaselineDataMaster, BaselineDataMasterManager>();
            services.AddTransient<IQuestionnaireService, QuestionnaireService>();
            services.AddTransient<IFufquestionnaireService, FufquestionnaireService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IGlobalService, GlobalService>();
            services.AddTransient<IFollowUpFormMaster, FollowUpFormMasterManager>();
            services.AddTransient<IAdverseEventService, AdverseEventService>();
            services.AddTransient<IPidfServiceNew, PIDFServiceNew>();
            services.AddTransient<IMasterTaskService, MasterTaskService>();
            services.AddTransient<IMasterSubTaskService, MasterSubTaskService>();
            services.AddTransient<ITransactionsProjectTask, TransactionProjectTaskService>();
            services.AddTransient<IDRFInitialization, DRFInitialization>();
            services.AddTransient<IDRFManufacturing,DRFManufacturing>();
            services.AddTransient<IDRFSupplyChainManagement,DRFSupplyChainManagement >();
            services.AddTransient<IDRFRA, DRFRA>();
            services.AddTransient<IDRFIP, DRFIP>();
            services.AddTransient<IDRFFinance, DRFFinance>();
            services.AddTransient<IDRFFinal, DRFFinal>();
            services.AddTransient<IGanttSummaryService, GanttSummaryService>();
            services.AddTransient<IDRFMedical, DRFMedical>();
            services.AddTransient<IDashboardReportService, DashboardReportService>();
            services.AddTransient<IRealTimeNotificationService, RealTimeNotificationService>();
            services.AddTransient<IMasters, Masters>();
            services.AddTransient<ISMTPService, SMTPService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IReportsService, ReportsService>();
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IProductMasterDataService, ProductMasterDataService>();
            return services;
        }
    }
}

