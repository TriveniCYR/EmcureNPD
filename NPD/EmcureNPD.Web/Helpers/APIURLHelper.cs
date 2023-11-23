using Microsoft.Extensions.Configuration;

namespace EmcureNPD.Web.Helpers
{
    public static class APIURLHelper
    {
        public static string LoginURL = "/api/Account/Login";
		public static string ValidateToken = "/api/Account/ValidateToken";
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

        #endregion Formulation

        #region PackagingType

        public static string GetAllPackagingType = "api/PackagingType/GetAllPackagingType";
        public static string SavePackagingType = "api/PackagingType/InsertUpdatePackagingType";
        public static string GetPackagingTypeById = "api/PackagingType/GetPackagingTypeById";
        public static string DeletePackagingTypeById = "api/PackagingType/DeletePackagingType";

        #endregion PackagingType

        #region ProductType

        public static string GetAllProductType = "api/ProductType/GetAllProductType";
        public static string SaveProductType = "api/ProductType/InsertUpdateProductType";
        public static string GetProductTypeById = "api/ProductType/GetProductTypeById";
        public static string DeleteProductTypeById = "api/ProductType/DeleteProductType";

        #endregion ProductType

        #region UnitofMeasurement

        public static string GetAllUnitofMeasurement = "api/UnitofMeasurement/GetAllUnitofMeasurement";
        public static string SaveUnitofMeasurement = "api/UnitofMeasurement/InsertUpdateUnitofMeasurement";
        public static string GetUnitofMeasurementById = "api/UnitofMeasurement/GetUnitofMeasurementById";
        public static string DeleteUnitofMeasurementById = "api/UnitofMeasurement/DeleteUnitofMeasurement";

        #endregion UnitofMeasurement

        #region Workflow

        public static string GetAllWorkflow = "api/Workflow/GetAllWorkflow";
        public static string SaveWorkflow = "api/Workflow/InsertUpdateWorkflow";
        public static string GetWorkflowById = "api/Workflow/GetWorkflowById";
        public static string DeleteWorkflowById = "api/Workflow/DeleteWorkflow";

        #endregion Workflow

        #region BusinessUnit

        public static string GetAllBusinessUnit = "api/BusinessUnit/GetAllBusinessUnit";
        public static string GetAllActiveBusinessUnit = "api/BusinessUnit/GetAllActiveBusinessUnit";
        public static string SaveBusinessUnit = "api/BusinessUnit/InsertUpdateBusinessUnit";
        public static string GetBusinessUnitById = "api/BusinessUnit/GetBusinessUnitById";
        public static string DeleteBusinessUnitById = "api/BusinessUnit/DeleteBusinessUnit";
        public static string GetCountryByBusinessUnitId = "api/BusinessUnit/GetCountryByBusinessUnitId";
        public static string GetActiveBusinessUnit = "api/BusinessUnit/GetActiveBusinessUnit";
		public static string GetActiveEncryptedBusinessUnit = "api/BusinessUnit/GetActiveEncryptedBusinessUnit";
		

		#endregion BusinessUnit

		#region Region

		public static string GetAllRegion = "api/Region/GetAllRegion";
        public static string GetAllActiveRegion = "api/Region/GetAllActiveRegion";
        public static string SaveRegion = "api/Region/InsertUpdateRegion";
        public static string GetRegionById = "api/Region/GetRegionById";
        public static string DeleteRegionById = "api/Region/DeleteRegion";
        public static string GetCountryByRegionId = "api/Region/GetCountryByRegionId";

        #endregion Region

        #region Region

        public static string GetAllCurrency = "api/Currency/GetAllCurrency";
        public static string GetAllCurrencyByUser = "api/Currency/GetCurrencyByLoggedInUser";
        public static string GetAllActiveCurrency = "api/Currency/GetAllActiveCurrency";
        public static string SaveCurrency = "api/Currency/InsertUpdateCurrency";
        public static string GetCurrencyById = "api/Currency/GetCurrencyById";
        public static string DeleteCurrencyById = "api/Currency/DeleteCurrency";
        public static string GetCountryByCurrencyId = "api/Currency/GetCountryByCurrencyId";

        #endregion Region

        #region Department

        public static string GetAllDepartment = "api/Department/GetAllDepartment";
        public static string GetAllActiveDepartment = "api/Department/GetAllActiveDepartment";
        public static string SaveDepartment = "api/Department/InsertUpdateDepartment";
        public static string GetDepartmentById = "api/Department/GetDepartmentById";
        public static string DeleteDepartmentById = "api/Department/DeleteDepartment";
        public static string GetBusinessUnitByDepartmentId = "api/Department/GetBusinessUnitByDepartmentId";

        #endregion Department

        #region BusinessUnitMapping

        public static string GetAllBusinessUnitRegionMapping = "api/BusinessUnitRegionMapping/GetAllBusinessUnitRegionMapping";
        public static string SaveBusinessUnitRegionMapping = "api/BusinessUnitRegionMapping/InsertUpdateBusinessUnitRegionMapping";
        public static string GetBusinessUnitRegionMappingById = "api/BusinessUnitRegionMapping/GetBusinessUnitRegionMappingById";
        public static string DeleteBusinessUnitRegionMappingById = "api/BusinessUnitRegionMapping/DeleteBusinessUnitRegionMapping";

        #endregion BusinessUnitMapping

        #region RegionMapping

        public static string GetAllRegionCountryMapping = "api/RegionCountryMapping/GetAllRegionCountryMapping";
        public static string SaveRegionCountryMapping = "api/RegionCountryMapping/InsertUpdateRegionCountryMapping";
        public static string GetRegionCountryMappingById = "api/RegionCountryMapping/GetRegionCountryMappingById";
        public static string DeleteRegionCountryMappingById = "api/RegionCountryMapping/DeleteRegionCountryMapping";

        #endregion RegionMapping

        #region CurrencyMapping

        public static string GetAllCurrencyCountryMapping = "api/CurrencyCountryMapping/GetAllCurrencyCountryMapping";
        public static string SaveCurrencyCountryMapping = "api/CurrencyCountryMapping/InsertUpdateCurrencyCountryMapping";
        public static string GetCurrencyCountryMappingById = "api/CurrencyCountryMapping/GetCurrencyCountryMappingById";
        public static string DeleteCurrencyCountryMappingById = "api/CurrencyCountryMapping/DeleteCurrencyCountryMapping";

        #endregion CurrencyMapping

        #region Role Management

        public static string GetAllRole = "api/Role/GetAllRole";
        public static string GetAllActiveRole = "api/Role/GetAllActiveRole";
        public static string GetRoleById = "api/Role/GetRoleById";
        public static string SaveRole = "api/Role/InsertUpdateRole";
        public static string DeleteRoleById = "api/Role/DeleteRole";

        #endregion Role Management

        #region Dosage

        public static string GetAllDosage = "api/Dosage/GetAllDosage";
        public static string SaveDosage = "api/Dosage/InsertUpdateDosage";
        public static string GetDosageById = "api/Dosage/GetDosageById";
        public static string DeleteDosageById = "api/Dosage/DeleteDosage";
        public static string GetAllActiveDosageFinance = "api/Dosage/GetAllActiveDosageFinance";

        #endregion Dosage

        #region Dosage Form

        public static string GetAllDosageForm = "api/DosageForm/GetAllDosageForm";
        public static string SaveDosageForm = "api/DosageForm/InsertUpdateDosageForm";
        public static string GetDosageFormById = "api/DosageForm/GetDosageFormById";
        public static string DeleteDosageFormById = "api/DosageForm/DeleteDosageForm";
        public static string GetAllActiveDosageFormFinance = "api/DosageForm/GetAllActiveDosageFormFinance";

        #endregion Dosage Form


        #region FilingType

        public static string GetAllFilingType = "api/FilingType/GetAllFilingType";
        public static string SaveFilingType = "api/FilingType/InsertUpdateFilingType";
        public static string GetFilingTypeById = "api/FilingType/GetFilingTypeById";
        public static string DeleteFilingTypeById = "api/FilingType/DeleteFilingType";

        #endregion FilingType
        #region Manufacturing

        public static string GetAllManufacturing = "api/Manufacturing/GetAllManufacturing";
        public static string SaveManufacturing = "api/Manufacturing/InsertUpdateManufacturing";
        public static string GetManufacturingById = "api/Manufacturing/GetManufacturingById";
        public static string DeleteManufacturingById = "api/Manufacturing/DeleteManufacturing";

        #endregion Manufacturing
        #region TestType

        public static string GetAllTestType = "api/TestType/GetAllTestType";
        public static string SaveTestType = "api/TestType/InsertUpdateTestType";
        public static string GetTestTypeById = "api/TestType/GetTestTypeById";
        public static string DeleteTestTypeById = "api/TestType/DeleteTestType";

        #endregion TestType
        #region PackingType

        public static string GetAllPackingType = "api/PackingType/GetAllPackingType";
        public static string SavePackingType = "api/PackingType/InsertUpdatePackingType";
        public static string GetPackingTypeById = "api/PackingType/GetPackingTypeById";
        public static string DeletePackingTypeById = "api/PackingType/DeletePackingType";

        #endregion PackingType
        #region PackSize

        public static string GetAllPackSize = "api/PackSize/GetAllPackSize";
        public static string SavePackSize = "api/PackSize/InsertUpdatePackSize";
        public static string GetPackSizeById = "api/PackSize/GetPackSizeById";
        public static string DeletePackSizeById = "api/PackSize/DeletePackSize";

        #endregion PackSize

        #region Oral

        public static string GetAllOral = "api/Oral/GetAllOral";
        public static string SaveOral = "api/Oral/InsertUpdateOral";
        public static string GetOralById = "api/Oral/GetOralById";
        public static string DeleteOralById = "api/Oral/DeleteOral";

        #endregion Oral

        #region API Sourcing

        public static string GetAllAPISourcing = "api/APISourcing/GetAllAPISourcing";
        public static string SaveAPISourcing = "api/APISourcing/InsertUpdateAPISourcing";
        public static string GetAPISourcingById = "api/APISourcing/GetAPISourcingById";
        public static string DeleteAPISourcingById = "api/APISourcing/DeleteAPISourcing";

        #endregion API Sourcing

        #region Plant

        public static string GetAllPlant = "api/Plant/GetAllPlant";
        public static string SavePlant = "api/Plant/InsertUpdatePlant";
        public static string GetPlantById = "api/Plant/GetPlantById";
        public static string DeletePlantById = "api/Plant/DeletePlant";

        #endregion Plant

        #region Exipient

        public static string GetAllExipient = "api/Exipient/GetAllExipient";
        public static string SaveExipient = "api/Exipient/InsertUpdateExipient";
        public static string GetExipientById = "api/Exipient/GetExipientById";
        public static string DeleteExipientById = "api/Exipient/DeleteExipient";

        #endregion Exipient

        #region Country

        public static string GetAllCountry = "api/Country/GetAllCountry"; 
		public static string GetAllActiveCountry = "api/Country/GetAllActiveCountry";
		public static string SaveCountry = "api/Country/InsertUpdateCountry";
        public static string GetCountryById = "api/Country/GetCountryById";
        public static string DeleteCountryById = "api/Country/DeleteCountry";

        #endregion Country

        #region FormRNDDivision

        public static string GetAllFormRNDDivision = "api/FormRNDDivision/GetAllFormRNDDivision";
        public static string SaveFormRNDDivision = "api/FormRNDDivision/InsertUpdateFormRNDDivision";
        public static string GetFormRNDDivisionById = "api/FormRNDDivision/GetFormRNDDivisionById";
        public static string DeleteFormRNDDivisionById = "api/FormRNDDivision/DeleteFormRNDDivision";

        #endregion FormRNDDivision

        #region AnalyticalGL

        public static string GetAllAnalyticalGL = "api/AnalyticalGL/GetAllAnalyticalGL";
        public static string SaveAnalyticalGL = "api/AnalyticalGL/InsertUpdateAnalyticalGL";
        public static string GetAnalyticalGLById = "api/AnalyticalGL/GetAnalyticalGLById";
        public static string DeleteAnalyticalGLById = "api/AnalyticalGL/DeleteAnalyticalGL";

        #endregion AnalyticalGL

        #region BatchSizeNumber

        public static string GetAllBatchSizeNumber = "api/BatchSizeNumber/GetAllBatchSizeNumber";
        public static string SaveBatchSizeNumber = "api/BatchSizeNumber/InsertUpdateBatchSizeNumber";
        public static string GetBatchSizeNumberById = "api/BatchSizeNumber/GetBatchSizeNumberById";
        public static string DeleteBatchSizeNumberById = "api/BatchSizeNumber/DeleteBatchSizeNumber";

        #endregion BatchSizeNumber

        #region ProductStrength

        public static string GetAllProductStrength = "api/ProductStrength/GetAllProductStrength";
        public static string SaveProductStrength = "api/ProductStrength/InsertUpdateProductStrength";
        public static string GetProductStrengthById = "api/ProductStrength/GetProductStrengthById";
        public static string DeleteProductStrengthById = "api/ProductStrength/DeleteProductStrength";

        #endregion ProductStrength

        #region BERequirement

        public static string GetAllBERequirement = "api/BERequirement/GetAllBERequirement";
        public static string SaveBERequirement = "api/BERequirement/InsertUpdateBERequirement";
        public static string GetBERequirementById = "api/BERequirement/GetBERequirementById";
        public static string DeleteBERequirementById = "api/BERequirement/DeleteBERequirement";

        #endregion BERequirement

        #region DIA

        public static string GetAllDIA = "api/Dashboard/GetAllDIA";
        public static string SaveDIA = "api/Dashboard/InsertUpdateDIA";
        public static string GetDIAById = "api/Dashboard/GetDIAById";
        public static string DeleteDIAById = "api/Dashboard/DeleteDIA";

        #endregion DIA

        #region User

        public static string GetAllUser = "api/User/GetAllUser";
        public static string SaveUser = "api/User/InsertUpdateUser";
        public static string GetUserById = "api/User/GetUserById";
        public static string GetUserDropdown = "api/User/GetUserDropdown";
        public static string GetDepartmentCountryByBusinessUnit = "api/User/GetDepartmentCountryByBusinessUnit";
        public static string GetRegionByBusinessUnit = "api/User/GetRegionByBusinessUnit";
        public static string DeleteUserById = "api/User/DeleteUser";
        public static string ChangeUserPassword = "api/User/ChangePassword";
        public static string ChangeUserProfile = "api/User/ChangeProfile";
        public static string CheckEmailAddressExists = "api/User/CheckEmailAddressExists";
        public static string GetCountryByRegion = "api/User/GetCountryByRegion";
        public static string GetDepartmentList = "api/User/GetDepartmentList";
        public static string GetBusinessUnitByUserId = "api/User/GetBusinessUnitByUserId";
        public static string GetUserForAPIInterested = "api/User/GetUserForAPIInterested";
        

        // Anonymous_Access API for Forgot Passsword
        public static string Anonymous_CheckEmailAddressExists = "api/Account/CheckEmailAddressExists";

        public static string Anonymous_IsTokenValid = "api/Account/IsTokenValid";

        #endregion User

        #region Module

        public static string GetAllModule = "api/Module/GetAllModule";

        #endregion Module

        #region AuditLog

        public static string GetAuditLogByModuleId = "api/AuditLog/GetAuditLogById";
        public static string GetAllAuditlog = "api/AuditLog/GetAllAuditLog";

        #endregion AuditLog

        #region PIDF

        public static string GetPbfFormDetailsAnalytical = "api/PBF/GetPbfFormDetailsAnalytical";

        public static string GetPIDFListFilterDropdown = "api/PIDF/GetPIDFFilterFormData";
        public static string GetPIDFDropdown = "api/PIDF/FillDropdown";
        public static string GetAllPIDF = "api/PIDF/GetAllPIDFList";
        public static string SavePIDF = "api/PIDF/InsertUpdatePIDF";
        public static string GetPIDFById = "api/PIDF/GetPIDFById";
        public static string GetPIDFById_BUID = "api/PIDF/GetPIDFById_BUID";
        public static string ApproveRejectPidf = "api/PIDF/ApproveRejectPidf";
        public static string ApproveRejectDeletePidf = "api/PIDF/ApproveRejectDeletePidf";
        public static string GetCommonPIDFList = "api/PIDF/GetCommonPIDFList";
        public static string GetPFBAPIPIDFList = "api/PIDF/GetPFBAPIPIDFList";
        public static string CommonApproveRejectDeletePidf = "api/PIDF/CommonApproveRejectDeletePidf";
        public static string GetPBFDropdown = "api/PBF/FillDropdown";
        public static string GetLineByPlantId = "api/PBF/GetLineByPlantId";        
        public static string SavePBFRnD = "api/PBF/InsertUpdatePBF";
        public static string SaveCommercialPIDF = "api/CommercialPIDFForm/SaveCommercialPIDF";
        public static string GetAllPackSizeCommercial = "api/CommercialPIDFForm/GetAllPackSize";
        public static string GetPbfFormData = "api/PBF/GetPbfFormData";
        public static string SavePBF = "api/PBF/InsertUpdatePBFDetails";
        public static string SavePBFAnatical = "api/PBF/PBFAnaLytical";
        public static string GetPBFReadonlyDataByPIDFId = "api/PBF/GetPBFAnalyticalReadonlyData";
        public static string GetPbfFormDetails = "api/PBF/GetPbfFormDetails";
        public static string SavePBFAnalytical = "api/PBF/InsertUpdatePBFDetailsAnalytical";
        public static string GetPBFOutsourcingTabDropDownData = "api/CommercialPIDFForm/GetPBFOutsourcingTabDropDownData";
        public static string GetPBFWorkFlowTaskNames = "api/CommercialPIDFForm/GetPBFWorkFlowTaskNames";
        public static string AddUpdatePBFoutsourceData = "api/CommercialPIDFForm/AddUpdatePBFoutsourceData";
        
        public static string SavePBFClinical = "api/PBF/InsertUpdatePBFClinicalDetails";
        public static string GetPBFTabDetails = "api/PBF/PBFTabDetails";
        public static string GetTypeOfSubmission = "api/PBF/GetTypeOfSubmission";
        public static string GetRa = "api/PBF/GetRa";
        public static string GetNationApprovals = "api/PBF/GetNationApprovals";
        public static string GetPBFRADates = "api/PBF/GetPBFRADates";
        public static string GetCountryWisePackSizeStabilityData = "api/PBF/GetAllPackSizeByCountry";
        public static string GetCommercialSummary = "api/CommercialPIDFForm/GetCommercialSummary";
        #endregion PIDF

        #region MarketExtension

        public static string GetAllMarketExtension = "api/MarketExtension/GetAllMarketExtension";
        public static string SaveMarketExtension = "api/MarketExtension/InsertUpdateMarketExtension";
        public static string GetMarketExtensionById = "api/MarketExtension/GetMarketExtensionById";
        public static string DeleteMarketExtensionById = "api/MarketExtension/DeleteMarketExtension";

        #endregion MarketExtension

        #region TransformForm

        public static string GetAllTransformForm = "api/TransformForm/GetAllTransformForm";
        public static string SaveTransformForm = "api/TransformForm/InsertUpdateTransformForm";
        public static string GetTransformFormById = "api/TransformForm/GetTransformFormById";
        public static string DeleteTransformFormById = "api/TransformForm/DeleteTransformForm";

        #endregion TransformForm

        #region ExtensionApplication

        public static string GetAllExtensionApplication = "api/ExtensionApplication/GetAllExtensionApplication";
        public static string SaveExtensionApplication = "api/ExtensionApplication/InsertUpdateExtensionApplication";
        public static string GetExtensionApplicationById = "api/ExtensionApplication/GetExtensionApplicationById";
        public static string DeleteExtensionApplicationById = "api/ExtensionApplication/DeleteExtensionApplication";

        #endregion ExtensionApplication

        #region ActivityType

        public static string GetAllActivityType = "api/ActivityType/GetAllActivityType";
        public static string SaveActivityType = "api/ActivityType/InsertUpdateActivityType";
        public static string GetActivityTypeById = "api/ActivityType/GetActivityTypeById";
        public static string DeleteActivityTypeById = "api/ActivityType/DeleteActivityType";

        #endregion ActivityType

        #region FillingExpense

        public static string GetAllFillingExpense = "api/FillingExpense/GetAllFillingExpense";
        public static string SaveFillingExpense = "api/FillingExpense/InsertUpdateFillingExpense";
        public static string GetFillingExpenseById = "api/FillingExpense/GetFillingExpenseById";
        public static string DeleteFillingExpenseById = "api/FillingExpense/DeleteFillingExpense";

        #endregion FillingExpense

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
        public static string GetCountryByIsInterestedCountry = "api/IPD/GetCountryByIsInterestedCountry"; 
        public static string GetPatentStrategyList = "api/IPD/GetPatentStrategyList";
        public static string GetIsInterestedByPIDFandBU = "api/PIDF/GetIsInterestedByPIDFandBU";

        #endregion IPD

        #region API

        public static string GetAPIIPDFormData = "api/API/GetAPIIPDFormData";
        public static string InsertUpdateAPIIPD = "api/API/InsertUpdateAPIIPD";
        public static string GetAPIRnDFormData = "api/API/GetAPIRnDFormData";
        public static string InsertUpdateAPIRnD = "api/API/InsertUpdateAPIRnD";
        public static string GetAPICharterFormData = "api/API/GetAPICharterFormData";
        public static string InsertUpdateAPICharter = "api/API/InsertUpdateAPICharter";
        public static string GetIPDByPIDF = "api/API/GetIPDByPIDF";
        public static string SaveAPIInterestedUser = "api/API/SaveAPIInterestedUser";
        public static string SaveAPIInterestedUserData = "api/API/SaveAPIInterestedUserData";
        public static string GetMasterAPIOutsourcelabels = "api/API/GetMasterAPIOutsourcelabels";
        public static string GetMasterAPIInhouselabels = "api/API/GetMasterAPIInhouselabels";
        public static string GetAPICharterDataByPIDF = "api/API/GetAPICharterDataByPIDF";
        #endregion API

        #region API List

        public static string GetAllAPIList = "api/APIList/GetAllAPIList";

        #endregion API List

        #region Dashboard

        public static string GetAllDashboard = "api/Dashboard/FillDropdown";
        public static string GetPIDFDashBoardData = "api/Dashboard/GetPIDFByYearAndBusinessUnit";

        #endregion Dashboard

        #region Notification

        public static string GetAllNotification = "api/Notification/GetAllNotification";
        public static string GetFilteredNotifications = "api/Notification/GetFilteredNotifications";
        public static string GetWebFilteredNotifications = "Notifications/GetFilteredNotifications";
        public static string NotificationsClickedByUser = "api/Notification/NotificationsClickedByUser";
        public static string NotificationsCountUser = "api/Notification/NotificationsCountUser";

        #endregion Notification

        #region PIDFFinance

        public static string AddUpdatePidfFinance = "api/PidfFinance/AddUpdatePidfFinance";
        public static string AddUpdatePidfFinanceBatchSizeCoating = "api/PidfFinance/AddUpdatePidfFinanceBatchSizeCoating";
        public static string GetPidfFinance = "api/PidfFinance/GetPidfFinance";
        public static string GetPidfFinancePhasewisebudget = "api/PidfFinance/GetPidfFinancePhasewisebudget";
        public static string GetFinanceBatchSizeCoating = "api/PidfFinance/GetFinanceBatchSizeCoating";
        public static string GetManagmentApprovalBatchSizeCoating = "api/PidfFinance/GetManagmentApprovalBatchSizeCoating";        
        public static string GetProjectNameAndStrength = "api/ManagementApproval/GetProjectNameAndStrength";
        public static string GetFinaceProjectionYear = "api/PidfFinance/GetFinaceProjectionYear";

        #endregion PIDFFinance

        #region Project

        public static string FillTaskDropdown = "api/Project/GetDropdownsForAddTask";
        public static string AddUpdateTask = "api/Project/AddUpdateTaskDetails";
        public static string GetAllTaskSubTaskList = "api/Project/GetTaskSubTask";
        public static string DeleteTaskSubTAsk = "api/Project/DeleteTaskSubTask";
        public static string GetTaskSubTaskById = "api/Project/GetTaskSubTaskById";
        public static string GetAllData = "api/Project/GetTaskSubTaskAndProjDetails";
        public static string GetBusinessUnitDeatil = "api/Project/GetBusinessUnitDetails";

        #endregion Project
        #region LogExceptionMethod
        public static string LogException = "api/LogException/LogException";
		#endregion

		#region ExcipientRequirement
		public static string GetAllExcipientRequirement = "api/ExcipientRequirement/GetAllExcipientRequirement";
		public static string GetExcipientRequirementById = "api/ExcipientRequirement/ExcipientRequirementById";
		public static string SaveExcipientRequirement = "api/ExcipientRequirement/AddUpdateExcipientRequirement";
		public static string DeleteSaveExcipientRequirementId = "api/ExcipientRequirement/DeleteExcipientRequirement";
		#endregion
		#region MasterPlantLine
		public static string GetAllPlantLine = "api/PlantLine/GetAllPlantLine";
		public static string GetPlantLineById = "api/PlantLine/PlantLineById";
		public static string SavePlantLine = "api/PlantLine/AddUpdatePlantLine";
		public static string DeletePlantLineId = "api/PlantLine/DeletePlantLine";
		public static string GetAllActivePlants = "api/PlantLine/GetAllActivePlants";
        #endregion
        #region WishList
        public static string GetAllWishList = "api/WishList/GetAllWishList";
        public static string GetAllWishListType = "api/WishList/GetWishListType";
        public static string AddUpdateWishList = "api/WishList/AddUpdateWishList";
        public static string GetWishListById = "api/WishList/GetWishListById";
        #endregion
    }
}
