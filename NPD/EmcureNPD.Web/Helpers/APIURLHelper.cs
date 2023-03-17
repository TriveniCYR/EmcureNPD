using Microsoft.Extensions.Configuration;

namespace EmcureNPD.Web.Helpers
{
    public static class APIURLHelper
    {
        public static string LoginURL = "/api/Account/Login";
        public static string GetBusinessUnit = "/api/Account/GetAllBusinessUnit";
        public static string GetRegion = "/api/Account/GetAllRegion";
        public static string GetCurrency = "/api/Account/GetAllCurrency";
        public static string GetByPermisionRoleUsingRoleId = "api/Role/GetByPermisionRoleUsingRoleId";
        public static string ForgotPassword = "api/Account/ForgotPassword";
        public static string ResetPassword = "api/Account/ResetPassword";
        
        public static IConfiguration Configuration;

        #region Formulation 
        public static string GetAllFormulation = "api/Formulation/GetAllFormulation";
        public static string SaveFormulation = "api/Formulation/InsertUpdateFormulation";
        public static string GetFormulationById = "api/Formulation/GetFormulationById";
        public static string DeleteFormulationById = "api/Formulation/DeleteFormulation";
        #endregion

        #region PackagingType 
        public static string GetAllPackagingType = "api/PackagingType/GetAllPackagingType";
        public static string SavePackagingType = "api/PackagingType/InsertUpdatePackagingType";
        public static string GetPackagingTypeById = "api/PackagingType/GetPackagingTypeById";
        public static string DeletePackagingTypeById = "api/PackagingType/DeletePackagingType";
        #endregion

        #region ProductType 
        public static string GetAllProductType = "api/ProductType/GetAllProductType";
        public static string SaveProductType = "api/ProductType/InsertUpdateProductType";
        public static string GetProductTypeById = "api/ProductType/GetProductTypeById";
        public static string DeleteProductTypeById = "api/ProductType/DeleteProductType";
        #endregion

        #region UnitofMeasurement 
        public static string GetAllUnitofMeasurement = "api/UnitofMeasurement/GetAllUnitofMeasurement";
        public static string SaveUnitofMeasurement = "api/UnitofMeasurement/InsertUpdateUnitofMeasurement";
        public static string GetUnitofMeasurementById = "api/UnitofMeasurement/GetUnitofMeasurementById";
        public static string DeleteUnitofMeasurementById = "api/UnitofMeasurement/DeleteUnitofMeasurement";
        #endregion

        #region Workflow 
        public static string GetAllWorkflow = "api/Workflow/GetAllWorkflow";
        public static string SaveWorkflow = "api/Workflow/InsertUpdateWorkflow";
        public static string GetWorkflowById = "api/Workflow/GetWorkflowById";
        public static string DeleteWorkflowById = "api/Workflow/DeleteWorkflow";
        #endregion

        #region BusinessUnit 
        public static string GetAllBusinessUnit = "api/BusinessUnit/GetAllBusinessUnit";
        public static string GetAllActiveBusinessUnit = "api/BusinessUnit/GetAllActiveBusinessUnit";
        public static string SaveBusinessUnit = "api/BusinessUnit/InsertUpdateBusinessUnit";
        public static string GetBusinessUnitById = "api/BusinessUnit/GetBusinessUnitById";
        public static string DeleteBusinessUnitById = "api/BusinessUnit/DeleteBusinessUnit";
        public static string GetCountryByBusinessUnitId = "api/BusinessUnit/GetCountryByBusinessUnitId";
        public static string GetActiveBusinessUnit = "api/BusinessUnit/GetActiveBusinessUnit";
        #endregion

        #region Region 
        public static string GetAllRegion = "api/Region/GetAllRegion";
        public static string GetAllActiveRegion = "api/Region/GetAllActiveRegion";
        public static string SaveRegion = "api/Region/InsertUpdateRegion";
        public static string GetRegionById = "api/Region/GetRegionById";
        public static string DeleteRegionById = "api/Region/DeleteRegion";
        public static string GetCountryByRegionId = "api/Region/GetCountryByRegionId";
        #endregion

        #region Region 
        public static string GetAllCurrency = "api/Currency/GetAllCurrency";
        public static string GetAllActiveCurrency = "api/Currency/GetAllActiveCurrency";
        public static string SaveCurrency = "api/Currency/InsertUpdateCurrency";
        public static string GetCurrencyById = "api/Currency/GetCurrencyById";
        public static string DeleteCurrencyById = "api/Currency/DeleteCurrency";
        public static string GetCountryByCurrencyId = "api/Currency/GetCountryByCurrencyId";
        #endregion

        #region Department 
        public static string GetAllDepartment = "api/Department/GetAllDepartment";
        public static string GetAllActiveDepartment = "api/Department/GetAllActiveDepartment";
        public static string SaveDepartment = "api/Department/InsertUpdateDepartment";
        public static string GetDepartmentById = "api/Department/GetDepartmentById";
        public static string DeleteDepartmentById = "api/Department/DeleteDepartment";
        public static string GetBusinessUnitByDepartmentId = "api/Department/GetBusinessUnitByDepartmentId";
        #endregion

        #region BusinessUnitMapping 
        public static string GetAllBusinessUnitRegionMapping = "api/BusinessUnitRegionMapping/GetAllBusinessUnitRegionMapping";
        public static string SaveBusinessUnitRegionMapping = "api/BusinessUnitRegionMapping/InsertUpdateBusinessUnitRegionMapping";
        public static string GetBusinessUnitRegionMappingById = "api/BusinessUnitRegionMapping/GetBusinessUnitRegionMappingById";
        public static string DeleteBusinessUnitRegionMappingById = "api/BusinessUnitRegionMapping/DeleteBusinessUnitRegionMapping";
        #endregion

        #region RegionMapping 
        public static string GetAllRegionCountryMapping = "api/RegionCountryMapping/GetAllRegionCountryMapping";
        public static string SaveRegionCountryMapping = "api/RegionCountryMapping/InsertUpdateRegionCountryMapping";
        public static string GetRegionCountryMappingById = "api/RegionCountryMapping/GetRegionCountryMappingById";
        public static string DeleteRegionCountryMappingById = "api/RegionCountryMapping/DeleteRegionCountryMapping";
        #endregion

        #region CurrencyMapping 
        public static string GetAllCurrencyCountryMapping = "api/CurrencyCountryMapping/GetAllCurrencyCountryMapping";
        public static string SaveCurrencyCountryMapping = "api/CurrencyCountryMapping/InsertUpdateCurrencyCountryMapping";
        public static string GetCurrencyCountryMappingById = "api/CurrencyCountryMapping/GetCurrencyCountryMappingById";
        public static string DeleteCurrencyCountryMappingById = "api/CurrencyCountryMapping/DeleteCurrencyCountryMapping";
        #endregion

        #region Role Management
        public static string GetAllRole = "api/Role/GetAllRole";
        public static string GetAllActiveRole = "api/Role/GetAllActiveRole";
        public static string GetRoleById = "api/Role/GetRoleById";
        public static string SaveRole = "api/Role/InsertUpdateRole";
        public static string DeleteRoleById = "api/Role/DeleteRole";
        #endregion

        #region Dosage Form
        public static string GetAllDosageForm = "api/DosageForm/GetAllDosageForm";
        public static string SaveDosageForm = "api/DosageForm/InsertUpdateDosageForm";
        public static string GetDosageFormById = "api/DosageForm/GetDosageFormById";
        public static string DeleteDosageFormById = "api/DosageForm/DeleteDosageForm";
        #endregion

        #region Oral
        public static string GetAllOral = "api/Oral/GetAllOral";
        public static string SaveOral = "api/Oral/InsertUpdateOral";
        public static string GetOralById = "api/Oral/GetOralById";
        public static string DeleteOralById = "api/Oral/DeleteOral";
        #endregion

        #region API Sourcing
        public static string GetAllAPISourcing = "api/APISourcing/GetAllAPISourcing";
        public static string SaveAPISourcing = "api/APISourcing/InsertUpdateAPISourcing";
        public static string GetAPISourcingById = "api/APISourcing/GetAPISourcingById";
        public static string DeleteAPISourcingById = "api/APISourcing/DeleteAPISourcing";
        #endregion

        #region Plant
        public static string GetAllPlant = "api/Plant/GetAllPlant";
        public static string SavePlant = "api/Plant/InsertUpdatePlant";
        public static string GetPlantById = "api/Plant/GetPlantById";
        public static string DeletePlantById = "api/Plant/DeletePlant";
        #endregion

        #region Exipient
        public static string GetAllExipient = "api/Exipient/GetAllExipient";
        public static string SaveExipient = "api/Exipient/InsertUpdateExipient";
        public static string GetExipientById = "api/Exipient/GetExipientById";
        public static string DeleteExipientById = "api/Exipient/DeleteExipient";
        #endregion

        #region Country
        public static string GetAllCountry = "api/Country/GetAllCountry";
        public static string SaveCountry = "api/Country/InsertUpdateCountry";
        public static string GetCountryById = "api/Country/GetCountryById";
        public static string DeleteCountryById = "api/Country/DeleteCountry";
        #endregion

        #region FormRNDDivision
        public static string GetAllFormRNDDivision = "api/FormRNDDivision/GetAllFormRNDDivision";
        public static string SaveFormRNDDivision = "api/FormRNDDivision/InsertUpdateFormRNDDivision";
        public static string GetFormRNDDivisionById = "api/FormRNDDivision/GetFormRNDDivisionById";
        public static string DeleteFormRNDDivisionById = "api/FormRNDDivision/DeleteFormRNDDivision";
        #endregion

        #region AnalyticalGL
        public static string GetAllAnalyticalGL = "api/AnalyticalGL/GetAllAnalyticalGL";
        public static string SaveAnalyticalGL = "api/AnalyticalGL/InsertUpdateAnalyticalGL";
        public static string GetAnalyticalGLById = "api/AnalyticalGL/GetAnalyticalGLById";
        public static string DeleteAnalyticalGLById = "api/AnalyticalGL/DeleteAnalyticalGL";
        #endregion

        #region BatchSizeNumber
        public static string GetAllBatchSizeNumber = "api/BatchSizeNumber/GetAllBatchSizeNumber";
        public static string SaveBatchSizeNumber = "api/BatchSizeNumber/InsertUpdateBatchSizeNumber";
        public static string GetBatchSizeNumberById = "api/BatchSizeNumber/GetBatchSizeNumberById";
        public static string DeleteBatchSizeNumberById = "api/BatchSizeNumber/DeleteBatchSizeNumber";
        #endregion

        #region ProductStrength
        public static string GetAllProductStrength = "api/ProductStrength/GetAllProductStrength";
        public static string SaveProductStrength = "api/ProductStrength/InsertUpdateProductStrength";
        public static string GetProductStrengthById = "api/ProductStrength/GetProductStrengthById";
        public static string DeleteProductStrengthById = "api/ProductStrength/DeleteProductStrength";

        #endregion

        #region BERequirement
        public static string GetAllBERequirement = "api/BERequirement/GetAllBERequirement";
        public static string SaveBERequirement = "api/BERequirement/InsertUpdateBERequirement";
        public static string GetBERequirementById = "api/BERequirement/GetBERequirementById";
        public static string DeleteBERequirementById = "api/BERequirement/DeleteBERequirement";
        #endregion

        #region DIA
        public static string GetAllDIA = "api/Dashboard/GetAllDIA";
        public static string SaveDIA = "api/Dashboard/InsertUpdateDIA";
        public static string GetDIAById = "api/Dashboard/GetDIAById";
        public static string DeleteDIAById = "api/Dashboard/DeleteDIA";
        #endregion

        #region User
        public static string GetAllUser = "api/User/GetAllUser";
        public static string SaveUser = "api/User/InsertUpdateUser";
        public static string GetUserById = "api/User/GetUserById";
        public static string GetUserDropdown = "api/User/GetUserDropdown";
        public static string GetDepartmentCountryByBusinessUnit = "api/User/GetDepartmentCountryByBusinessUnit";
        public static string GetRegionByBusinessUnit = "api/User/GetRegionByBusinessUnit";
        public static string DeleteUserById = "api/User/DeleteUser";
        public static string ChangeUserPassword = "api/User/ChangePassword";
        public static string CheckEmailAddressExists = "api/User/CheckEmailAddressExists";
        public static string GetCountryByRegion = "api/User/GetCountryByRegion"; 
        public static string GetDepartmentList = "api/User/GetDepartmentList";
        public static string GetBusinessUnitByUserId = "api/User/GetBusinessUnitByUserId";

        // Anonymous_Access API for Forgot Passsword
        public static string Anonymous_CheckEmailAddressExists = "api/Account/CheckEmailAddressExists"; 
        public static string Anonymous_IsTokenValid = "api/Account/IsTokenValid"; 
        #endregion

        #region Module
        public static string GetAllModule = "api/Module/GetAllModule";
        #endregion

        #region AuditLog
        public static string GetAuditLogByModuleId = "api/AuditLog/GetAuditLogById";
        public static string GetAllAuditlog = "api/AuditLog/GetAllAuditLog";
        #endregion

        #region PIDF
        public static string GetPbfFormDetailsAnalytical = "api/PBF/GetPbfFormDetailsAnalytical";

        public static string GetPIDFDropdown = "api/PIDF/FillDropdown";
        public static string GetAllPIDF = "api/PIDF/GetAllPIDFList";
        public static string SavePIDF = "api/PIDF/InsertUpdatePIDF";
        public static string GetPIDFById = "api/PIDF/GetPIDFById";
        public static string ApproveRejectPidf = "api/PIDF/ApproveRejectPidf";
        public static string ApproveRejectDeletePidf = "api/PIDF/ApproveRejectDeletePidf";
        public static string GetCommonPIDFList = "api/PIDF/GetCommonPIDFList";
        public static string GetPFBAPIPIDFList = "api/PIDF/GetPFBAPIPIDFList";
        public static string CommonApproveRejectDeletePidf = "api/PIDF/CommonApproveRejectDeletePidf";        
		public static string GetPBFDropdown = "api/PBF/FillDropdown";
		public static string SavePBFRnD = "api/PBF/InsertUpdatePBF";
        public static string SaveCommercialPIDF = "api/CommercialPIDFForm/SaveCommercialPIDF";
        public static string GetPbfFormData = "api/PBF/GetPbfFormData";
        public static string SavePBF = "api/PBF/InsertUpdatePBFDetails";
        public static string SavePBFAnatical = "api/PBF/PBFAnaLytical";
        public static string GetPBFReadonlyDataByPIDFId = "api/PBF/GetPBFAnalyticalReadonlyData";
        public static string GetPbfFormDetails = "api/PBF/GetPbfFormDetails";
        public static string SavePBFAnalytical = "api/PBF/InsertUpdatePBFDetailsAnalytical";

        
        public static string SavePBFClinical = "api/PBF/InsertUpdatePBFClinicalDetails";



        #endregion


        #region MarketExtension
        public static string GetAllMarketExtension = "api/MarketExtension/GetAllMarketExtension";
        public static string SaveMarketExtension = "api/MarketExtension/InsertUpdateMarketExtension";
        public static string GetMarketExtensionById = "api/MarketExtension/GetMarketExtensionById";
        public static string DeleteMarketExtensionById = "api/MarketExtension/DeleteMarketExtension";
        #endregion

        #region TransformForm
        public static string GetAllTransformForm = "api/TransformForm/GetAllTransformForm";
        public static string SaveTransformForm = "api/TransformForm/InsertUpdateTransformForm";
        public static string GetTransformFormById = "api/TransformForm/GetTransformFormById";
        public static string DeleteTransformFormById = "api/TransformForm/DeleteTransformForm";
        #endregion

        #region ExtensionApplication
        public static string GetAllExtensionApplication = "api/ExtensionApplication/GetAllExtensionApplication";
        public static string SaveExtensionApplication = "api/ExtensionApplication/InsertUpdateExtensionApplication";
        public static string GetExtensionApplicationById = "api/ExtensionApplication/GetExtensionApplicationById";
        public static string DeleteExtensionApplicationById = "api/ExtensionApplication/DeleteExtensionApplication";
        #endregion

        #region ActivityType
        public static string GetAllActivityType = "api/ActivityType/GetAllActivityType";
        public static string SaveActivityType = "api/ActivityType/InsertUpdateActivityType";
        public static string GetActivityTypeById = "api/ActivityType/GetActivityTypeById";
        public static string DeleteActivityTypeById = "api/ActivityType/DeleteActivityType";
        #endregion

        #region FillingExpense
        public static string GetAllFillingExpense = "api/FillingExpense/GetAllFillingExpense";
        public static string SaveFillingExpense = "api/FillingExpense/InsertUpdateFillingExpense";
        public static string GetFillingExpenseById = "api/FillingExpense/GetFillingExpenseById";
        public static string DeleteFillingExpenseById = "api/FillingExpense/DeleteFillingExpense";
        #endregion

        #region IPD
        public static string GetIPD = "api/IPD/GetIPD";
        public static string GetAllIPD = "api/IPD/GetAllIPD";
        public static string SaveIPDForm = "api/IPD/SaveIPDForm";
        public static string GetIPDFormData = "api/IPD/GetIPDFormData";
        public static string GetAllIPDPIDFList = "api/IPD/GetAllIPDPIDFList";
        public static string GetAllRegionIPD = "api/IPD/GetAllRegion";
        public static string GetCountryRefByRegionIds = "api/IPD/GetCountryRefByRegionIds";
        public static string ApproveRejectIpdPidf = "api/IPD/ApproveRejectIpdPidf";
		public static string GetCommercialFormData = "api/CommercialPIDFForm/GetCommercialFormData";
        public static string GetAllFinalSelection = "api/CommercialPIDFForm/GetAllFinalSelection";
        public static string PIDMedicalForm = "api/Medical/PIDMedicalForm";
        public static string GetMedicalFormdata = "api/Medical/GetPIDFMedicalFormData";
        #endregion

        #region API
        public static string GetAPIIPDFormData = "api/API/GetAPIIPDFormData";
        public static string InsertUpdateAPIIPD = "api/API/InsertUpdateAPIIPD";
        public static string GetAPIRnDFormData = "api/API/GetAPIRnDFormData";
        public static string InsertUpdateAPIRnD = "api/API/InsertUpdateAPIRnD";
        public static string GetAPICharterFormData = "api/API/GetAPICharterFormData";
        public static string InsertUpdateAPICharter = "api/API/InsertUpdateAPICharter";  
        #endregion

        #region API List
        public static string GetAllAPIList = "api/APIList/GetAllAPIList";
        #endregion

        #region Dashboard
        public static string GetAllDashboard = "api/Dashboard/FillDropdown";        
        public static string GetPIDFList = "api/Dashboard/GetPIDFByYearAndBusinessUnit";
        #endregion

        #region Notification
        public static string GetAllNotification = "api/Notification/GetAllNotification";
		public static string GetFilteredNotifications = "api/Notification/GetFilteredNotifications";
        public static string GetWebFilteredNotifications = "Notifications/GetFilteredNotifications";
        #endregion

        #region PIDFFinance
        public static string AddUpdatePidfFinance = "api/PidfFinance/AddUpdatePidfFinance";
        public static string AddUpdatePidfFinanceBatchSizeCoating = "api/PidfFinance/AddUpdatePidfFinanceBatchSizeCoating";
        public static string GetPidfFinance = "api/PidfFinance/GetPidfFinance";
        public static string GetFinanceBatchSizeCoating = "api/PidfFinance/GetFinanceBatchSizeCoating";
        public static string GetProjectNameAndStrength = "api/ManagementApproval/GetProjectNameAndStrength";
        #endregion

        #region Project
        public static string FillTaskDropdown = "api/Project/GetDropdownsForAddTask";
        public static string AddUpdateTask = "api/Project/AddUpdateTaskDetails";
        public static string GetAllTaskSubTaskList = "api/Project/GetTaskSubTask";
        public static string DeleteTaskSubTAsk = "api/Project/DeleteTaskSubTask";
        public static string GetTaskSubTaskById = "api/Project/GetTaskSubTaskById";
        public static string GetAllData = "api/Project/GetTaskSubTaskAndProjDetails";
        public static string GetBusinessUnitDeatil = "api/Project/GetBusinessUnitDetails";
        #endregion

    }
}