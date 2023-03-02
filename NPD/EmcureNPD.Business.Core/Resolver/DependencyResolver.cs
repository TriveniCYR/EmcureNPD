using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmcureNPD.Business.Core.Resolver
{
    public static class DependencyResolver
    {
        public static IServiceCollection ContainerResolver(this IServiceCollection services)
        {
            services.AddScoped<DbContext, EmcureNPDDevContext>();
            services.AddScoped<IMapperFactory, MapperFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IMasterFormulationService, MasterFormulationService>();
            services.AddTransient<IMasterProductTypeService, MasterProductTypeService>();
            services.AddTransient<IMasterBusinessUnitService, MasterBusinessUnitService>();
            services.AddTransient<IMasterDepartmentService, MasterDepartmentService>();
            services.AddTransient<IMasterPackagingTypeService, MasterPackagingTypeService>();
            services.AddTransient<IMasterUnitofMeasurementService, MasterUnitofMeasurementService>();
            services.AddTransient<IMasterWorkflowService, MasterWorkflowService>();
            services.AddTransient<IMasterRoleService, MasterRoleService>();
            services.AddTransient<IMasterUserService, MasterUserService>();
            services.AddTransient<IMasterCountryService, MasterCountryService>();
            services.AddTransient<IMasterDosageFormService, MasterDosageFormService>();
            services.AddTransient<IMasterOralService, MasterOralService>();
            services.AddTransient<IMasterModuleService, MasterModuleService>();
            services.AddTransient<IMasterAPISourcingService, MasterAPISourcingService>();
            services.AddTransient<IMasterPlantService, MasterPlantService>();
            services.AddTransient<IMasterExipientService, MasterExipientService>();
            services.AddTransient<IMasterFormRNDDivisionService, MasterFormRNDDivisionService>();
            services.AddTransient<IMasterAnalyticalGLService, MasterAnalyticalGLService>();
            services.AddTransient<IMasterBatchSizeNumberService, MasterBatchSizeNumberService>();
            services.AddTransient<IRoleModulePermission, RoleModulePermissionService>();
            services.AddTransient<IPIDFService, PIDFService>();
            services.AddTransient<IMasterAuditLogService, MasterAuditLogService>();
            services.AddTransient<IMasterProductStrengthService, MasterProductStrengthService>();
            services.AddTransient<IMasterBERequirementService, MasterBERequirementService>();
            services.AddTransient<IMasterDIAService, MasterDIAService>();
            services.AddTransient<IMasterMarketExtensionService, MasterMarketExtensionService>();
            services.AddTransient<IMasterTransformFormService, MasterTransformFormService>();
            services.AddTransient<IMasterExtensionApplicationService, MasterExtensionApplicationService>();
            services.AddTransient<IMasterActivityTypeService, MasterActivityTypeService>();
            services.AddTransient<IMasterFillingExpenseService, MasterFillingExpenseService>();
            services.AddTransient<IPIDFormService, PIDFormService>();
            services.AddTransient<IAPIListService, APIListService>();
            services.AddTransient<IPidfProductStrengthService, PidfProductStrengthService>();
            services.AddTransient<IPidfApiDetailsService, PidfApiDetailsService>();
            services.AddTransient<IMasterRegionService, MasterRegionService>();
            services.AddTransient<IMasterCurrencyService, MasterCurrencyService>();
			services.AddTransient<IPBFService, PBFService>();
			services.AddTransient<ICommercialFormService, CommercialFormService>();
			
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<ISchedulerService, SchedulerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IHelper, Helper>();
            services.AddTransient<IMasterTestTypeService, MasterTestTypeService>();
            services.AddTransient<IMasterTestLicenseService, MasterTestLicenseService>();
            services.AddTransient<IPidfFinanceService, PidfFinanceService>();
            services.AddTransient<IManagementApproval, ManagementApproval>();

            return services;
        }
    }
}