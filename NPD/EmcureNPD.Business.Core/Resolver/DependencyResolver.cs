﻿using EmcureNPD.Business.Core.Implementation;
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
            services.AddTransient<IIPDService, IPDService>();
            services.AddTransient<IMedicalService, MedicalService>();
            services.AddTransient<IAPIListService, APIListService>();
            services.AddTransient<IPidfProductStrengthService, PidfProductStrengthService>();
            services.AddTransient<IPidfApiDetailsService, PidfApiDetailsService>();
            services.AddTransient<IMasterRegionService, MasterRegionService>();
            services.AddTransient<IMasterCurrencyService, MasterCurrencyService>();
			services.AddTransient<IPBFService, PBFService>();
			services.AddTransient<ICommercialFormService, CommercialFormService>();
            services.AddTransient<IAPIService, APIService>();

            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<ISchedulerService, SchedulerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IHelper, Helper>();
            services.AddTransient<IMasterTestTypeService, MasterTestTypeService>();
            services.AddTransient<IMasterTestLicenseService, MasterTestLicenseService>();
            services.AddTransient<IPidfFinanceService, PidfFinanceService>();
            services.AddTransient<IManagementApproval, ManagementApproval>();

            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IExceptionService, ExceptionService>();
            services.AddTransient<IMasterFilingTypeService, MasterFilingTypeService>();
            services.AddTransient<IMasterPackingTypeService, MasterPackingTypeService>();
            services.AddTransient<IMasterDosageService, MasterDosageService>();
            services.AddTransient<IMasterPackSizeService, MasterPackSizeService>();
            services.AddTransient<IMasterManufacturingService, MasterManufacturingService>();
            services.AddTransient<IDatabaseSubscription, DatabaseSubscription>();
            services.AddTransient<IMasterExcipientRequirement, MasterExcipientRequirementService>();
            services.AddTransient<IMasterPlantLine, MasterPlantLineService>();
            services.AddTransient<ISessionManager, SessionManagerService>();
            services.AddTransient<IWishList, WishListService>();
            services.AddTransient<ICurrencyConverter,EmcureNPD.Business.Core.Implementation.CurrencyConverter>();
            return services;
        }
    }
}