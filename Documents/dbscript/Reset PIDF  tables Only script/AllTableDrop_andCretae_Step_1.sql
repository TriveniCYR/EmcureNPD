
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSessionLogMaster]') AND type in (N'U'))
ALTER TABLE [dbo].[UserSessionLogMaster] DROP CONSTRAINT IF EXISTS [FK_UserSessionLogMaster_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_SessionManager]') AND type in (N'U'))
ALTER TABLE [dbo].[Tbl_SessionManager] DROP CONSTRAINT IF EXISTS [FK_Tbl_SessionManager_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTask]') AND type in (N'U'))
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT IF EXISTS [FK_ProjectTask_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTask]') AND type in (N'U'))
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT IF EXISTS [FK_ProjectTask_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTask]') AND type in (N'U'))
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT IF EXISTS [FK_ProjectTask_Master_Project_Status]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTask]') AND type in (N'U'))
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT IF EXISTS [FK_ProjectTask_Master_Project_Priority]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFStatusHistory]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFStatusHistory] DROP CONSTRAINT IF EXISTS [FK_PIDFStatusHistory_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFStatusHistory]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFStatusHistory] DROP CONSTRAINT IF EXISTS [FK_PIDFStatusHistory_Master_PIDFStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_CountryMapping_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_Master_UnitofMeasurement]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_Master_BussinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFIMSData]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFIMSData] DROP CONSTRAINT IF EXISTS [FK_PIDFIMSData_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFIMSData]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFIMSData] DROP CONSTRAINT IF EXISTS [FK_PIDFIMSData_Master_BussinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFAPIDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFAPIDetails] DROP CONSTRAINT IF EXISTS [FK_PIDFAPIDetails_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFAPIDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFAPIDetails] DROP CONSTRAINT IF EXISTS [FK_PIDFAPIDetails_Master_BussinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFAPIDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFAPIDetails] DROP CONSTRAINT IF EXISTS [FK_PIDFAPIDetails_Master_APISourcing]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ToolingChangepart]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ToolingChangepart_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ToolingChangepart]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ToolingChangepart_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ReferenceProductDetail]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ReferenceProductDetail]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PlantSupportCost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_PlantSupportCost_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PlantSupportCost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_PlantSupportCost_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackSizeStability]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackSizeStability]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_PackSizeStability_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackSizeStability]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT IF EXISTS [FK__PIDF_PBF___Count__781FBE44]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackagingMaterial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_PackagingMaterial_Master_PackingType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackagingMaterial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_Packaging_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackagingMaterial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_Packaging_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_Master]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_Master] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_Master_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ManPowerCost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ManPowerCost_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ManPowerCost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ManPowerCost_Master_ProjectActivities]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_FillingExpenses]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_FillingExpenses_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_FillingExpenses]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_FillingExpenses_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_FillingExpenses]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_FillingExpenses_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientScaleUp]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ExicipientScaleUp_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientRequirement]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_Exicipient_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientRequirement]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_Exicipient_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientPrototype]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientPrototype]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Rnd_BatchSize]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Rnd_BatchSize_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Rnd_BatchSize]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Rnd_BatchSize_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_APIRequirement]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_APIRequirement_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_APIRequirement]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RnD_APIRequirement_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RA] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RA_PIDF_PBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RA] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_RA_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_PhaseWiseBudget]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_PhaseWiseBudget] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_PhaseWiseBudget_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Outsource]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Outsource] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Outsource_PIDFID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_MarketMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_MarketMapping_PIDF_PBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_MarketMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_MarketMapping_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_HeadWiseBudget]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_HeadWiseBudget] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_HeadWiseBudget_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_Strength]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_Strength] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Strength_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_Strength]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_Strength] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Strength_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_RND_PIDF_PBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_RND_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_PIDF_PBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Master_User1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Master_ProductType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Clinical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Clinical] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Clinical_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Clinical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Clinical] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Clinical_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDF_PBF_Analytical_Cost]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_Cost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_Cost_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDF_PBF_Analytical_AMVCost]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical_AMVCost]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_AMVCost_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_PIDF_PBF_General]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Analytical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Analytical] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Analytical_Master_TestType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_PIDF1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_Workflow1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_ProductType1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_Plant1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_PackagingType1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_Manufacturing1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_FormRnDDivision]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_FilingType1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_Dosage1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Master_BERequirement]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Medical]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Medical] DROP CONSTRAINT IF EXISTS [FK_PIDF_Medical_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_ManagementApprovalStatusHistory]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] DROP CONSTRAINT IF EXISTS [FK_PIDF_ManagementApprovalStatusHistory_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_ManagementApprovalStatusHistory]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] DROP CONSTRAINT IF EXISTS [FK_PIDF_ManagementApprovalStatusHistory_Master_PIDFStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_Region]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_Region] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_Region_PIDF_IPD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_Region]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_Region] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_Region_Master_Region]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_PatentDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_PatentDetails] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_PatentDetails_IPDID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_General]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_General] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_General_IPDID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_Country]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_Country] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_Country_PIDF_IPD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD_Country]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD_Country] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_Country_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_PIDFID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_IPD]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_IPD] DROP CONSTRAINT IF EXISTS [FK_PIDF_IPD_BusinessUnitId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance_Projection]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance_Projection] DROP CONSTRAINT IF EXISTS [FK_PIDF_Finance_Projection_PIDF_Finance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance_Projection]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance_Projection] DROP CONSTRAINT IF EXISTS [FK_PIDF_Finance_Projection_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance_BatchSizeCoating]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance_BatchSizeCoating] DROP CONSTRAINT IF EXISTS [FK_PIDF_Finance_BatchSizeCoating_PIDF_Finance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Years]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Years] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Year_PIDF_Commercial_Year]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Years]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Years] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Year_PIDF_Commercial]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Years]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Years] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Year_Master_PackagingType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Years]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Years] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Year_Master_Currency]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Master]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Master] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Master_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial_Master]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial_Master] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Master_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Master_PackSize]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Commercial]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Commercial] DROP CONSTRAINT IF EXISTS [FK_PIDF_Commercial_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_BusinessUnit_Interested]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested] DROP CONSTRAINT IF EXISTS [FK_PIDF_BusinessUnit_Interested_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_BusinessUnit_Interested]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested] DROP CONSTRAINT IF EXISTS [FK_PIDF_BusinessUnit_Interested_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Outsource_Data]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Outsource_Data] DROP CONSTRAINT IF EXISTS [FK__PIDF_API___PIDFI__038683F8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Outsource_Data]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Outsource_Data] DROP CONSTRAINT IF EXISTS [FK__PIDF_API___APIOu__02925FBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Master]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Master] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Master_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_IPD]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_IPD] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_IPD_ProductTypeId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_IPD]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_IPD] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_IPD_PIDFID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_IPD]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_IPD] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_IPD_BusinessUnitId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Inhouse]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Inhouse] DROP CONSTRAINT IF EXISTS [FK__PIDF_API___PIDFI__7DCDAAA2]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Inhouse]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Inhouse] DROP CONSTRAINT IF EXISTS [FK__PIDF_API___APIIn__7CD98669]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_TimelineInMonths]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_TimelineInMonths] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_TimelineInMonths_PIDF_API_Charter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_PRDDepartment]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter_PRDDepartment]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_PRDDepartment]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_ManhourEstimates]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_ManhourEstimates] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_ManhourEstimates_PIDF_API_Charter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_HeadwiseBudget]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_HeadwiseBudget] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_HeadwiseBudget_PIDF_API_Charter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_CapitalOtherExpenditure]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_CapitalOtherExpenditure] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_CapitalOtherExpenditure_PIDF_API_Charter_CapitalOtherExpenditure]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter_AnalyticalDepartment]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter_AnalyticalDepartment] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_AnalyticalDepartment_PIDF_API_Charter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_API_Charter]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_API_Charter] DROP CONSTRAINT IF EXISTS [FK_PIDF_API_Charter_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_UnitofMeasurement]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_PIDFStatus1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_PIDFStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_PackagingType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_Oral]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_MarketExtenstion]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_DosageForm]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_DIA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [FK_PIDF_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_WorkFlowTasks]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_WorkFlowTasks] DROP CONSTRAINT IF EXISTS [FK__Master_Wo__Workf__531856C7]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserRegionMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserRegionMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserRegionMapping_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserRegionMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserRegionMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserRegionMapping_Master_Region]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserDepartmentMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserDepartmentMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserDepartmentMapping_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserDepartmentMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserDepartmentMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserDepartmentMapping_Master_Department]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserCountryMapping_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserCountryMapping_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserBusinessUnitMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_UserBusinessUnitMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping] DROP CONSTRAINT IF EXISTS [FK_Master_UserBusinessUnitMapping_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_User]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_User] DROP CONSTRAINT IF EXISTS [FK_Master_User_Master_Role]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_SubModule]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_SubModule] DROP CONSTRAINT IF EXISTS [FK_Master_SubModule_Master_Module]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_RegionCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_RegionCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_RegionId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_RegionCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_RegionCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_RegionCountryMapping_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_PlantLine]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_PlantLine] DROP CONSTRAINT IF EXISTS [FK_Master_PlantLine_Master_PlantLine]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_Notification_User]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_Notification_User] DROP CONSTRAINT IF EXISTS [FK_Master_Notification_User_Master_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_Notification]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_Notification] DROP CONSTRAINT IF EXISTS [FK_Master_Notification_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_Notification]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_Notification] DROP CONSTRAINT IF EXISTS [FK_Master_Notification_Master_PIDFStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_NationApproval_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_NationApproval_CountryMapping] DROP CONSTRAINT IF EXISTS [FK__Master_Na__Natio__42E1EEFE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_NationApproval_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_NationApproval_CountryMapping] DROP CONSTRAINT IF EXISTS [FK__Master_Na__Count__41EDCAC5]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_DepartmentBusinessUnitMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping] DROP CONSTRAINT IF EXISTS [FK_Master_DepartmentId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_DepartmentBusinessUnitMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping] DROP CONSTRAINT IF EXISTS [FK_Master_DepartmentBusinessUnitMapping_Master_BusinessUnit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_CurrencyCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_CurrencyCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_CurrencyCountryMapping_Master_Currency]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_CurrencyCountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_CurrencyCountryMapping] DROP CONSTRAINT IF EXISTS [FK_Master_CurrencyCountryMapping_Master_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_BusinessUnitRegionMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping] DROP CONSTRAINT IF EXISTS [FK_Master_CountryId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_BusinessUnitRegionMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping] DROP CONSTRAINT IF EXISTS [FK_Master_BusinessUnitId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_WishList]') AND type in (N'U'))
ALTER TABLE [dbo].[Tbl_WishList] DROP CONSTRAINT IF EXISTS [DF_Tbl_WishList_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_WishList]') AND type in (N'U'))
ALTER TABLE [dbo].[Tbl_WishList] DROP CONSTRAINT IF EXISTS [DF_Tbl_WishList_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackSizeStability]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_RnD_PackSizeStability_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientScaleUp]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_RnD_ExicipientScaleUp_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_ExicipientPrototype]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_RnD_ExicipientPrototype_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_RA] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_RA_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_General_RND_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_ManagementApprovalStatusHistory]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] DROP CONSTRAINT IF EXISTS [DF_PIDF_ManagementApprovalStatusHistory_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance_Projection]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance_Projection] DROP CONSTRAINT IF EXISTS [DF_PIDF_Finance_Projection_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance_BatchSizeCoating]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance_BatchSizeCoating] DROP CONSTRAINT IF EXISTS [DF_PIDF_Finance_BatchSizeCoating_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance] DROP CONSTRAINT IF EXISTS [DF_PIDF_Finance_IsDeleted]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_Finance]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_Finance] DROP CONSTRAINT IF EXISTS [DF_PIDF_Finance_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF] DROP CONSTRAINT IF EXISTS [DF__PIDF__MarketExte__6A50C1DA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pbf_general_TDP]') AND type in (N'U'))
ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT IF EXISTS [DF__pbf_gener__Creat__3D54C988]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pbf_general_TDP]') AND type in (N'U'))
ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT IF EXISTS [DF__pbf_gener__IsEmc__3C60A54F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pbf_general_TDP]') AND type in (N'U'))
ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT IF EXISTS [DF__pbf_gener__IsSec__3B6C8116]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pbf_general_TDP]') AND type in (N'U'))
ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT IF EXISTS [DF__pbf_gener__IsPri__3A785CDD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_WishListType]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_WishListType] DROP CONSTRAINT IF EXISTS [DF_Master_WishListType_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_WishListType]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_WishListType] DROP CONSTRAINT IF EXISTS [DF_Master_WishListType_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_TypeOfSubmission]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_TypeOfSubmission] DROP CONSTRAINT IF EXISTS [DF_Master_TypeOfSubmission_CreatedOn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_PlantLine]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_PlantLine] DROP CONSTRAINT IF EXISTS [DF_Master_PlantLine_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_PlantLine]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_PlantLine] DROP CONSTRAINT IF EXISTS [DF_Master_PlantLine_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_Notification]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_Notification] DROP CONSTRAINT IF EXISTS [DF__Master_No__IsEma__367C1819]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_NationApproval]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_NationApproval] DROP CONSTRAINT IF EXISTS [DF__Master_Na__Creat__3587F3E0]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_ExcipientRequirement]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_ExcipientRequirement] DROP CONSTRAINT IF EXISTS [DF_Master_ExcipientRequirement_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_EmailLog]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_EmailLog] DROP CONSTRAINT IF EXISTS [DF__Master_Em__Creat__339FAB6E]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_EmailLog]') AND type in (N'U'))
ALTER TABLE [dbo].[Master_EmailLog] DROP CONSTRAINT IF EXISTS [DF__Master_Em__SentS__32AB8735]
GO
/****** Object:  Table [dbo].[UserSessionLogMaster]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[UserSessionLogMaster]
GO
/****** Object:  Table [dbo].[Tbl_WishList]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Tbl_WishList]
GO
/****** Object:  Table [dbo].[Tbl_SessionManager]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Tbl_SessionManager]
GO
/****** Object:  Table [dbo].[RoleModulePermission]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[RoleModulePermission]
GO
/****** Object:  Table [dbo].[ProjectTask]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[ProjectTask]
GO
/****** Object:  Table [dbo].[PIDFStatusHistory]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDFStatusHistory]
GO
/****** Object:  Table [dbo].[PIDFProductStrength_CountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDFProductStrength_CountryMapping]
GO
/****** Object:  Table [dbo].[PIDFProductStrength]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDFProductStrength]
GO
/****** Object:  Table [dbo].[PIDFIMSData]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDFIMSData]
GO
/****** Object:  Table [dbo].[PIDFAPIDetails]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDFAPIDetails]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ToolingChangepart]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ToolingChangepart]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ReferenceProductDetail]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ReferenceProductDetail]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PlantSupportCost]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_PlantSupportCost]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackSizeStability]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_PackSizeStability]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackagingMaterial]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_PackagingMaterial]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_Master]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ManPowerCost]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ManPowerCost]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_FillingExpenses]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_FillingExpenses]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientScaleUp]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ExicipientScaleUp]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ExicipientRequirement]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientPrototype]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_ExicipientPrototype]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Rnd_BatchSize]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Rnd_BatchSize]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_APIRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RnD_APIRequirement]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Reference_Product_detail]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Reference_Product_detail]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RA]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_RA]
GO
/****** Object:  Table [dbo].[PIDF_PBF_PhaseWiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_PhaseWiseBudget]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource_Task]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Outsource_Task]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Outsource]
GO
/****** Object:  Table [dbo].[PIDF_PBF_MarketMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_MarketMapping]
GO
/****** Object:  Table [dbo].[PIDF_PBF_HeadWiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_HeadWiseBudget]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_Strength]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_General_Strength]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_RND]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_General_RND]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_General]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Clinical]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Clinical]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_Cost]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Analytical_Cost]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_AMVCost]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Analytical_AMVCost]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Analytical]
GO
/****** Object:  Table [dbo].[PIDF_PBF]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF]
GO
/****** Object:  Table [dbo].[PIDF_Medical_File]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Medical_File]
GO
/****** Object:  Table [dbo].[PIDF_Medical]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Medical]
GO
/****** Object:  Table [dbo].[PIDF_ManagementApprovalStatusHistory]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_ManagementApprovalStatusHistory]
GO
/****** Object:  Table [dbo].[PIDF_IPD_Region]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_IPD_Region]
GO
/****** Object:  Table [dbo].[PIDF_IPD_PatentDetails]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_IPD_PatentDetails]
GO
/****** Object:  Table [dbo].[PIDF_IPD_General]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_IPD_General]
GO
/****** Object:  Table [dbo].[PIDF_IPD_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_IPD_Country]
GO
/****** Object:  Table [dbo].[PIDF_IPD]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_IPD]
GO
/****** Object:  Table [dbo].[PIDF_Finance_Projection]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Finance_Projection]
GO
/****** Object:  Table [dbo].[PIDF_Finance_BatchSizeCoating]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Finance_BatchSizeCoating]
GO
/****** Object:  Table [dbo].[PIDF_Finance]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Finance]
GO
/****** Object:  Table [dbo].[PIDF_Commercial_Years]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Commercial_Years]
GO
/****** Object:  Table [dbo].[PIDF_Commercial_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Commercial_Master]
GO
/****** Object:  Table [dbo].[PIDF_Commercial]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_Commercial]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit_Interested]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_BusinessUnit_Interested]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_BusinessUnit_Country]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_BusinessUnit]
GO
/****** Object:  Table [dbo].[PIDF_API_RnD]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_RnD]
GO
/****** Object:  Table [dbo].[PIDF_API_Outsource_Data]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Outsource_Data]
GO
/****** Object:  Table [dbo].[PIDF_API_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Master]
GO
/****** Object:  Table [dbo].[PIDF_API_IPD]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_IPD]
GO
/****** Object:  Table [dbo].[PIDF_API_Inhouse]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Inhouse]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_TimelineInMonths]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_TimelineInMonths]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_PRDDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_PRDDepartment]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_ManhourEstimates]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_ManhourEstimates]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_HeadwiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_HeadwiseBudget]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_CapitalOtherExpenditure]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_CapitalOtherExpenditure]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_AnalyticalDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter_AnalyticalDepartment]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF_API_Charter]
GO
/****** Object:  Table [dbo].[PIDF]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[PIDF]
GO
/****** Object:  Table [dbo].[pbf_general_TDP]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[pbf_general_TDP]
GO
/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_WorkFlowTasks]
GO
/****** Object:  Table [dbo].[Master_WorkFlow_Tasks]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_WorkFlow_Tasks]
GO
/****** Object:  Table [dbo].[Master_Workflow]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Workflow]
GO
/****** Object:  Table [dbo].[Master_WishListType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_WishListType]
GO
/****** Object:  Table [dbo].[Master_UserRegionMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_UserRegionMapping]
GO
/****** Object:  Table [dbo].[Master_UserDepartmentMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_UserDepartmentMapping]
GO
/****** Object:  Table [dbo].[Master_UserCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_UserCountryMapping]
GO
/****** Object:  Table [dbo].[Master_UserBusinessUnitMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_UserBusinessUnitMapping]
GO
/****** Object:  Table [dbo].[Master_User]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_User]
GO
/****** Object:  Table [dbo].[Master_UnitofMeasurement]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_UnitofMeasurement]
GO
/****** Object:  Table [dbo].[Master_TypeOfSubmission]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_TypeOfSubmission]
GO
/****** Object:  Table [dbo].[Master_Transform]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Transform]
GO
/****** Object:  Table [dbo].[Master_TestType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_TestType]
GO
/****** Object:  Table [dbo].[Master_TestLicense]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_TestLicense]
GO
/****** Object:  Table [dbo].[Master_SubModule]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_SubModule]
GO
/****** Object:  Table [dbo].[Master_Role]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Role]
GO
/****** Object:  Table [dbo].[Master_RegionCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_RegionCountryMapping]
GO
/****** Object:  Table [dbo].[Master_Region]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Region]
GO
/****** Object:  Table [dbo].[Master_ProjectActivities]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ProjectActivities]
GO
/****** Object:  Table [dbo].[Master_Project_Status]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Project_Status]
GO
/****** Object:  Table [dbo].[Master_Project_Priority]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Project_Priority]
GO
/****** Object:  Table [dbo].[Master_ProductType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ProductType]
GO
/****** Object:  Table [dbo].[Master_ProductStrength]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ProductStrength]
GO
/****** Object:  Table [dbo].[Master_PlantLine]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PlantLine]
GO
/****** Object:  Table [dbo].[Master_Plant]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Plant]
GO
/****** Object:  Table [dbo].[Master_PIDFStatus]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PIDFStatus]
GO
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkflow_Task]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkFlow]
GO
/****** Object:  Table [dbo].[Master_Patent_Strategy]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Patent_Strategy]
GO
/****** Object:  Table [dbo].[Master_PackSize]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PackSize]
GO
/****** Object:  Table [dbo].[Master_PackingType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PackingType]
GO
/****** Object:  Table [dbo].[Master_PackagingType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_PackagingType]
GO
/****** Object:  Table [dbo].[Master_Oral]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Oral]
GO
/****** Object:  Table [dbo].[Master_Notification_User]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Notification_User]
GO
/****** Object:  Table [dbo].[Master_Notification]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Notification]
GO
/****** Object:  Table [dbo].[Master_NationApproval_CountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_NationApproval_CountryMapping]
GO
/****** Object:  Table [dbo].[Master_NationApproval]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_NationApproval]
GO
/****** Object:  Table [dbo].[Master_Module]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Module]
GO
/****** Object:  Table [dbo].[Master_MarketExtenstion]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_MarketExtenstion]
GO
/****** Object:  Table [dbo].[Master_Manufacturing]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Manufacturing]
GO
/****** Object:  Table [dbo].[Master_HeadWiseBudgetActivities]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_HeadWiseBudgetActivities]
GO
/****** Object:  Table [dbo].[Master_Formulation]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Formulation]
GO
/****** Object:  Table [dbo].[Master_FormRnDDivision]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_FormRnDDivision]
GO
/****** Object:  Table [dbo].[Master_FinalSelection]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_FinalSelection]
GO
/****** Object:  Table [dbo].[Master_FilingType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_FilingType]
GO
/****** Object:  Table [dbo].[Master_ExtensionApplication]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ExtensionApplication]
GO
/****** Object:  Table [dbo].[Master_ExpenseRegion]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ExpenseRegion]
GO
/****** Object:  Table [dbo].[Master_Exipient]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Exipient]
GO
/****** Object:  Table [dbo].[Master_ExcipientRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ExcipientRequirement]
GO
/****** Object:  Table [dbo].[Master_Exception]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Exception]
GO
/****** Object:  Table [dbo].[Master_EmailLog]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_EmailLog]
GO
/****** Object:  Table [dbo].[Master_DosageForm]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_DosageForm]
GO
/****** Object:  Table [dbo].[Master_Dosage]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Dosage]
GO
/****** Object:  Table [dbo].[Master_DIA]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_DIA]
GO
/****** Object:  Table [dbo].[Master_DepartmentBusinessUnitMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_DepartmentBusinessUnitMapping]
GO
/****** Object:  Table [dbo].[Master_Department]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Department]
GO
/****** Object:  Table [dbo].[Master_CurrencyCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_CurrencyCountryMapping]
GO
/****** Object:  Table [dbo].[Master_Currency]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Currency]
GO
/****** Object:  Table [dbo].[Master_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Country]
GO
/****** Object:  Table [dbo].[Master_BusinessUnitRegionMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_BusinessUnitRegionMapping]
GO
/****** Object:  Table [dbo].[Master_BusinessUnit]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_BusinessUnit]
GO
/****** Object:  Table [dbo].[Master_BERequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_BERequirement]
GO
/****** Object:  Table [dbo].[Master_BatchSizeNumber]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_BatchSizeNumber]
GO
/****** Object:  Table [dbo].[Master_AuditLog]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_AuditLog]
GO
/****** Object:  Table [dbo].[Master_APISourcing]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_APISourcing]
GO
/****** Object:  Table [dbo].[Master_API_Outsource]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Outsource]
GO
/****** Object:  Table [dbo].[Master_API_Inhouse]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Inhouse]
GO
/****** Object:  Table [dbo].[Master_API_Charter_TimelineInMonths]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_TimelineInMonths]
GO
/****** Object:  Table [dbo].[Master_API_Charter_PRDDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_PRDDepartment]
GO
/****** Object:  Table [dbo].[Master_API_Charter_ManhourEstimates]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_ManhourEstimates]
GO
/****** Object:  Table [dbo].[Master_API_Charter_HeadwiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_HeadwiseBudget]
GO
/****** Object:  Table [dbo].[Master_API_Charter_CapitalOtherExpenditure]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_CapitalOtherExpenditure]
GO
/****** Object:  Table [dbo].[Master_API_Charter_AnalyticalDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_API_Charter_AnalyticalDepartment]
GO
/****** Object:  Table [dbo].[Master_Analytical]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_Analytical]
GO
/****** Object:  Table [dbo].[Master_ActivityType]    Script Date: 10/18/2023 5:36:42 AM ******/
DROP TABLE IF EXISTS [dbo].[Master_ActivityType]
GO
/****** Object:  Table [dbo].[Master_ActivityType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ActivityType](
	[ActivityTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityTypeName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_ActivityType] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Analytical]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Analytical](
	[AnalyticalId] [int] IDENTITY(1,1) NOT NULL,
	[AnalyticalName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_AnalyticalGL] PRIMARY KEY CLUSTERED 
(
	[AnalyticalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_AnalyticalDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_AnalyticalDepartment](
	[Master_API_Charter_AnalyticalDepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
	[ARD] [nvarchar](100) NULL,
	[Impurity] [nvarchar](100) NULL,
	[Stability] [nvarchar](100) NULL,
	[AMV] [nvarchar](100) NULL,
	[AMT] [nvarchar](100) NULL,
 CONSTRAINT [PK_Master_API_Charter_AnalyticalDepartment] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_AnalyticalDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_CapitalOtherExpenditure]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_CapitalOtherExpenditure](
	[Master_API_Charter_CapitalOtherExpenditureId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Master_API_Charter_CapitalOtherExpenditure] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_CapitalOtherExpenditureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_HeadwiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_HeadwiseBudget](
	[Master_API_Charter_HeadwiseBudgetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Master_API_Charter_HeadwiseBudget] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_HeadwiseBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_ManhourEstimates]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_ManhourEstimates](
	[Master_API_Charter_ManhourEstimatesId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Master_API_Charter_ManhourEstimates] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_ManhourEstimatesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_PRDDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_PRDDepartment](
	[Master_API_Charter_PRDDepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Master_API_Charter_PRDDepartment] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_PRDDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Charter_TimelineInMonths]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Charter_TimelineInMonths](
	[Master_API_Charter_TimelineInMonthsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
	[NameValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_TimelineInMonths] PRIMARY KEY CLUSTERED 
(
	[Master_API_Charter_TimelineInMonthsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Inhouse]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Inhouse](
	[APIInhouseId] [int] IDENTITY(1,1) NOT NULL,
	[APIInhouseName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[APIInhouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_API_Outsource]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_API_Outsource](
	[APIOutsourceId] [int] IDENTITY(1,1) NOT NULL,
	[APIOutsourceName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[APIOutsourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_APISourcing]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_APISourcing](
	[APISourcingId] [int] IDENTITY(1,1) NOT NULL,
	[APISourcingName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_APISourcing] PRIMARY KEY CLUSTERED 
(
	[APISourcingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_AuditLog]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_AuditLog](
	[AuditLogId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NULL,
	[EntityId] [int] NULL,
	[ActionType] [nvarchar](20) NULL,
	[Log] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Master_AuditLog] PRIMARY KEY CLUSTERED 
(
	[AuditLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_BatchSizeNumber]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_BatchSizeNumber](
	[BatchSizeNumberId] [int] IDENTITY(1,1) NOT NULL,
	[BatchSizeNumberName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_BatchSizeNumber] PRIMARY KEY CLUSTERED 
(
	[BatchSizeNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_BERequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_BERequirement](
	[BERequirementId] [int] IDENTITY(1,1) NOT NULL,
	[BERequirementName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_BERequirement] PRIMARY KEY CLUSTERED 
(
	[BERequirementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_BusinessUnit]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_BusinessUnit](
	[BusinessUnitId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessUnitName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[Latitude] [real] NULL,
	[Longitude] [real] NULL,
	[IsDomestic] [bit] NULL,
 CONSTRAINT [PK_Master_BusinessUnit] PRIMARY KEY CLUSTERED 
(
	[BusinessUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_BusinessUnitRegionMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_BusinessUnitRegionMapping](
	[BusinessUnitCountryMappingId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_BusinessCountryMapping] PRIMARY KEY CLUSTERED 
(
	[BusinessUnitCountryMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[CountryCode] [nvarchar](5) NULL,
	[ISDCountryCode] [nvarchar](5) NULL,
 CONSTRAINT [PK_Master_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Currency]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Currency](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyName] [nvarchar](100) NULL,
	[CurrencyCode] [nvarchar](10) NULL,
	[CurrencySymbol] [nvarchar](5) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_CurrencyCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_CurrencyCountryMapping](
	[CurrencyCountryMappingId] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_CurrencyCountryMapping] PRIMARY KEY CLUSTERED 
(
	[CurrencyCountryMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Department]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_DepartmentBusinessUnitMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_DepartmentBusinessUnitMapping](
	[DepartmentBusinessUnitMappingId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_BusinessBusinessUnitMapping] PRIMARY KEY CLUSTERED 
(
	[DepartmentBusinessUnitMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_DIA]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_DIA](
	[DIAId] [int] IDENTITY(1,1) NOT NULL,
	[DIAName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_DIA_ANDA_ANDS] PRIMARY KEY CLUSTERED 
(
	[DIAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Dosage]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Dosage](
	[DosageId] [int] IDENTITY(1,1) NOT NULL,
	[DosageName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Dosage] PRIMARY KEY CLUSTERED 
(
	[DosageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_DosageForm]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_DosageForm](
	[DosageFormId] [int] IDENTITY(1,1) NOT NULL,
	[DosageFormName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_DosageForm] PRIMARY KEY CLUSTERED 
(
	[DosageFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_EmailLog]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_EmailLog](
	[EmailLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ToEmailAddress] [nvarchar](1000) NULL,
	[Subject] [nvarchar](1000) NULL,
	[SentSuccessfully] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Exception]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Exception](
	[ExceptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](500) NULL,
	[Source] [nvarchar](500) NULL,
	[InnerException] [nvarchar](max) NULL,
	[StrackTrace] [nvarchar](4000) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Master_Exception] PRIMARY KEY CLUSTERED 
(
	[ExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ExcipientRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ExcipientRequirement](
	[ExcipientRequirementId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExcipientRequirementName] [nvarchar](70) NULL,
	[ExcipientRequirementCost] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExcipientRequirementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Exipient]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Exipient](
	[ExipientId] [int] IDENTITY(1,1) NOT NULL,
	[ExipientName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Exipientr] PRIMARY KEY CLUSTERED 
(
	[ExipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ExpenseRegion]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ExpenseRegion](
	[ExpenseRegionId] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseRegionName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_ExpenseRegion] PRIMARY KEY CLUSTERED 
(
	[ExpenseRegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ExtensionApplication]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ExtensionApplication](
	[ExtensionApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[ExtensionApplicationName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_ExtensionApplication] PRIMARY KEY CLUSTERED 
(
	[ExtensionApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_FilingType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_FilingType](
	[FilingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[FilingTypeName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_FilingType] PRIMARY KEY CLUSTERED 
(
	[FilingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_FinalSelection]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_FinalSelection](
	[FinalSelectionID] [int] IDENTITY(1,1) NOT NULL,
	[FinalSelectionName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_FinalSelection] PRIMARY KEY CLUSTERED 
(
	[FinalSelectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_FormRnDDivision]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_FormRnDDivision](
	[FormRnDDivisionId] [int] IDENTITY(1,1) NOT NULL,
	[FormRnDDivisionName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_RNDDivision] PRIMARY KEY CLUSTERED 
(
	[FormRnDDivisionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Formulation]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Formulation](
	[FormulationId] [int] IDENTITY(1,1) NOT NULL,
	[FormulationName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Formulation] PRIMARY KEY CLUSTERED 
(
	[FormulationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_HeadWiseBudgetActivities]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_HeadWiseBudgetActivities](
	[ProjectActivitiesId] [int] NOT NULL,
	[ProjectActivitiesName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Master_HeadWiseBudgetActivities] PRIMARY KEY CLUSTERED 
(
	[ProjectActivitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Manufacturing]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Manufacturing](
	[ManufacturingId] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturingName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Manufacturing] PRIMARY KEY CLUSTERED 
(
	[ManufacturingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_MarketExtenstion]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_MarketExtenstion](
	[MarketExtenstionId] [int] IDENTITY(1,1) NOT NULL,
	[MarketExtenstionName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_MarketExtenstion] PRIMARY KEY CLUSTERED 
(
	[MarketExtenstionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Module]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Module](
	[ModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[ControlName] [nvarchar](200) NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Master_Module] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_NationApproval]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_NationApproval](
	[NationApprovalId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[MinEOP] [int] NULL,
	[MaxEOP] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NationApprovalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_NationApproval_CountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_NationApproval_CountryMapping](
	[NationApprovalCountryId] [int] IDENTITY(1,1) NOT NULL,
	[NationApprovalId] [int] NULL,
	[CountryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NationApprovalCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Notification]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Notification](
	[NotificationId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NULL,
	[StatusId] [int] NULL,
	[NotificationTitle] [nvarchar](100) NOT NULL,
	[NotificationDescription] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IsEmailSent] [bit] NOT NULL,
	[SentDatetime] [datetime] NULL,
 CONSTRAINT [PK_Master_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Notification_User]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Notification_User](
	[NotificationUserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Notification_User] PRIMARY KEY CLUSTERED 
(
	[NotificationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Oral]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Oral](
	[OralId] [int] IDENTITY(1,1) NOT NULL,
	[OralName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Oral] PRIMARY KEY CLUSTERED 
(
	[OralId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PackagingType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PackagingType](
	[PackagingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PackagingTypeName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PackagingType] PRIMARY KEY CLUSTERED 
(
	[PackagingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PackingType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PackingType](
	[PackingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PackingTypeName] [nvarchar](100) NULL,
	[PackingCost] [float] NULL,
	[Ref] [int] NULL,
	[Unit] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PackingType] PRIMARY KEY CLUSTERED 
(
	[PackingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PackSize]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PackSize](
	[PackSizeId] [int] IDENTITY(1,1) NOT NULL,
	[PackSizeName] [nvarchar](100) NOT NULL,
	[PackSize] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_Master_PackSize] PRIMARY KEY CLUSTERED 
(
	[PackSizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Patent_Strategy]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Patent_Strategy](
	[PatentStrategyID] [int] IDENTITY(1,1) NOT NULL,
	[PatentStrategyName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Master_Patent_Strategy] PRIMARY KEY CLUSTERED 
(
	[PatentStrategyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PBFWorkFlow](
	[PBFWorkFlowId] [int] IDENTITY(1,1) NOT NULL,
	[PBFWorkFlowName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PBFWorkFlow] PRIMARY KEY CLUSTERED 
(
	[PBFWorkFlowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PBFWorkflow_Task](
	[PBfWorkFlowTaskId] [int] IDENTITY(1,1) NOT NULL,
	[PBFWorkFlowTaskName] [nvarchar](100) NULL,
	[PBfWorkFlowId] [int] NOT NULL,
	[TaskLevel] [int] NULL,
	[ParentId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PBFWorkflow_Task] PRIMARY KEY CLUSTERED 
(
	[PBfWorkFlowTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PIDFStatus]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PIDFStatus](
	[PIDFStatusID] [int] NOT NULL,
	[PIDFStatus] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[Createdby] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[StatusColor] [varchar](10) NULL,
	[IsDashboard] [bit] NULL,
	[ModuleId] [int] NULL,
	[StatusTextColor] [varchar](10) NULL,
 CONSTRAINT [PK_Master_PIDFStatus] PRIMARY KEY CLUSTERED 
(
	[PIDFStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Plant]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Plant](
	[PlantId] [int] IDENTITY(1,1) NOT NULL,
	[PlantNameName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Plant] PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PlantLine]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PlantLine](
	[LineId] [bigint] IDENTITY(1,1) NOT NULL,
	[PlantId] [int] NOT NULL,
	[LineName] [nvarchar](70) NULL,
	[LineCost] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PlantLine] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ProductStrength]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ProductStrength](
	[ProductStrengthId] [int] IDENTITY(1,1) NOT NULL,
	[ProductStrengthName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_ProductStrength] PRIMARY KEY CLUSTERED 
(
	[ProductStrengthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ProductType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ProductType](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[ProductTypeFactor] [int] NULL,
	[ManPowerFactor] [float] NULL,
 CONSTRAINT [PK_Master_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Project_Priority]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Project_Priority](
	[PriorityId] [int] NOT NULL,
	[PriorityName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Master_Project_Priority] PRIMARY KEY CLUSTERED 
(
	[PriorityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Project_Status]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Project_Status](
	[StatusId] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Master_Project_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_ProjectActivities]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_ProjectActivities](
	[ProjectActivitiesId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectActivitiesName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_ProjectActivities] PRIMARY KEY CLUSTERED 
(
	[ProjectActivitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Region]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Region](
	[RegionId] [int] IDENTITY(1,1) NOT NULL,
	[RegionName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Region] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_RegionCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_RegionCountryMapping](
	[RegionCountryMappingId] [int] IDENTITY(1,1) NOT NULL,
	[RegionId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_RegionCountryMapping] PRIMARY KEY CLUSTERED 
(
	[RegionCountryMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Role]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_SubModule]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_SubModule](
	[SubModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NULL,
	[SubModuleName] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[ControlName] [nvarchar](200) NULL,
 CONSTRAINT [PK_Master_SubModule] PRIMARY KEY CLUSTERED 
(
	[SubModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_TestLicense]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_TestLicense](
	[TestLicenseId] [int] IDENTITY(1,1) NOT NULL,
	[TestLicenseName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_TestLicense] PRIMARY KEY CLUSTERED 
(
	[TestLicenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_TestType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_TestType](
	[TestTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[TestTypeCode] [nvarchar](6) NULL,
	[TestTypePrice] [int] NULL,
 CONSTRAINT [PK_Master_TestType] PRIMARY KEY CLUSTERED 
(
	[TestTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Transform]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Transform](
	[TransformId] [int] IDENTITY(1,1) NOT NULL,
	[TransformName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Transform_form] PRIMARY KEY CLUSTERED 
(
	[TransformId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_TypeOfSubmission]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_TypeOfSubmission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfSubmission] [nvarchar](20) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[MinEOP] [int] NULL,
	[MaxEOP] [int] NULL,
 CONSTRAINT [PK__Master_T__3214EC0780AA3A6E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_UnitofMeasurement]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_UnitofMeasurement](
	[UnitofMeasurementId] [int] IDENTITY(1,1) NOT NULL,
	[UnitofMeasurementName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_UnitofMeasurement] PRIMARY KEY CLUSTERED 
(
	[UnitofMeasurementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_User]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[MobileNumber] [varchar](15) NULL,
	[Password] [varchar](100) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[ForgotPasswordToken] [varchar](100) NULL,
	[ForgotPasswordDateTime] [datetime] NULL,
	[IsManagement] [bit] NULL,
	[APIUser] [bit] NULL,
	[FormulationGL] [bit] NULL,
	[AnalyticalGL] [bit] NULL,
	[DesignationName] [nvarchar](100) NULL,
	[MobileCountryId] [int] NULL,
	[APIGroupLeader] [bit] NULL,
 CONSTRAINT [PK_Master_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [EmailAddress_unique] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_UserBusinessUnitMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_UserBusinessUnitMapping](
	[UserBusinessUnitId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_UserBusinessUnitMapping] PRIMARY KEY CLUSTERED 
(
	[UserBusinessUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_UserCountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_UserCountryMapping](
	[UserCountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_UserCountryMapping] PRIMARY KEY CLUSTERED 
(
	[UserCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_UserDepartmentMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_UserDepartmentMapping](
	[UserDepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_UserDepartmentMapping] PRIMARY KEY CLUSTERED 
(
	[UserDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_UserRegionMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_UserRegionMapping](
	[UserRegionId] [int] IDENTITY(1,1) NOT NULL,
	[RegionId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Master_UserRegionMapping] PRIMARY KEY CLUSTERED 
(
	[UserRegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_WishListType]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_WishListType](
	[WishListTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[WishListTyp] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__Master_W__15F0A6D83940699C] PRIMARY KEY CLUSTERED 
(
	[WishListTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Workflow]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Workflow](
	[WorkflowId] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Workflow] PRIMARY KEY CLUSTERED 
(
	[WorkflowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_WorkFlow_Tasks]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_WorkFlow_Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[TaskName] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[WorkflowId] [int] NULL,
	[Country] [bit] NULL,
	[TaskLevel] [int] NULL,
	[StartDateOffset] [int] NULL,
	[EndDateOffset] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_WorkFlowTasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[TaskName] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[WorkflowId] [int] NULL,
	[Country] [bit] NULL,
	[TaskLevel] [int] NULL,
	[StartDateOffset] [int] NULL,
	[EndDateOffset] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pbf_general_TDP]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pbf_general_TDP](
	[TradeDressProposalId] [bigint] IDENTITY(1,1) NOT NULL,
	[Approch] [varchar](max) NULL,
	[PIDFID] [bigint] NULL,
	[PbfId] [bigint] NULL,
	[PIDFPbfGeneralId] [bigint] NULL,
	[PIDFProductStrngthId] [bigint] NULL,
	[Description] [varchar](max) NULL,
	[Shape] [varchar](200) NULL,
	[Color] [varchar](20) NULL,
	[Engraving] [varchar](200) NULL,
	[Packaging] [varchar](max) NULL,
	[IsPrimaryPackaging] [bit] NULL,
	[IsSecondryPackaging] [bit] NULL,
	[Shelf_Life] [varchar](200) NULL,
	[Storage_Handling] [varchar](200) NULL,
	[IsEmcure] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[FormulaterResponsiblePerson] [nvarchar](100) NULL,
	[PrimaryPackaging] [varchar](255) NULL,
	[SecondryPackaging] [varchar](255) NULL,
 CONSTRAINT [PK__pbf_gene__26222D7F3860C311] PRIMARY KEY CLUSTERED 
(
	[TradeDressProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF](
	[PIDFID] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFNO] [nvarchar](20) NOT NULL,
	[OralId] [int] NULL,
	[UnitofMeasurementId] [int] NULL,
	[DosageFormId] [int] NULL,
	[PackagingTypeId] [int] NULL,
	[BusinessUnitId] [int] NULL,
	[MoleculeName] [nvarchar](100) NULL,
	[BrandName] [nvarchar](100) NULL,
	[ApprovedGenerics] [nvarchar](100) NULL,
	[LaunchedGenerics] [nvarchar](100) NULL,
	[RFDBrand] [nvarchar](100) NULL,
	[RFDApplicant] [nvarchar](100) NULL,
	[RFDCountryId] [int] NULL,
	[RFDIndication] [nvarchar](100) NULL,
	[RFDInnovators] [nvarchar](100) NULL,
	[RFDInitialRevenuePotential] [nvarchar](100) NULL,
	[RFDPriceDiscounting] [nvarchar](100) NULL,
	[RFDCommercialBatchSize] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[StatusId] [int] NOT NULL,
	[LastStatusId] [int] NULL,
	[InHouses] [bit] NULL,
	[MarketExtenstionId] [int] NULL,
	[DIAId] [int] NULL,
	[StatusUpdatedBy] [int] NULL,
	[StatusUpdatedDate] [datetime] NULL,
	[StatusRemark] [nvarchar](300) NULL,
	[TradeNameRequired] [bit] NULL,
	[TradeNameDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF] PRIMARY KEY CLUSTERED 
(
	[PIDFID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter](
	[PIDF_API_CharterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[APIGroupLeader] [nvarchar](100) NULL,
	[ProjectComplexityId] [int] NULL,
	[ManHourRates] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_API_Charter] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_CharterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_AnalyticalDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_AnalyticalDepartment](
	[PIDF_API_Charter_AnalyticalDepartmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[PIDFId] [bigint] NULL,
	[AnalyticalDepartmentId] [int] NULL,
	[AnalyticalDepartmentARDValue] [nvarchar](100) NULL,
	[AnalyticalDepartmentImpurityValue] [nvarchar](100) NULL,
	[AnalyticalDepartmentStabilityValue] [nvarchar](100) NULL,
	[AnalyticalDepartmentAMVValue] [nvarchar](100) NULL,
	[AnalyticalDepartmentAMTValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_AnalyticalDepartment] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_AnalyticalDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_CapitalOtherExpenditure]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_CapitalOtherExpenditure](
	[PIDF_API_Charter_CapitalOtherExpenditureId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[PIDFId] [bigint] NULL,
	[CapitalOtherExpenditureId] [int] NULL,
	[CapitalOtherExpenditureAmountValue] [nvarchar](100) NULL,
	[CapitalOtherExpenditureRemarkValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_CapitalOtherExpenditure] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_CapitalOtherExpenditureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_HeadwiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_HeadwiseBudget](
	[PIDF_API_Charter_HeadwiseBudgetId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[PIDFId] [bigint] NULL,
	[HeadwiseBudgetId] [int] NULL,
	[HeadwiseBudgetValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_HeadwiseBudget] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_HeadwiseBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_ManhourEstimates]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_ManhourEstimates](
	[PIDF_API_Charter_ManhourEstimatesId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[ManhourEstimatesId] [int] NULL,
	[ManhourEstimatesNoOfEmployeeValue] [nvarchar](100) NULL,
	[ManhourEstimatesMonthsValue] [nvarchar](100) NULL,
	[ManhourEstimatesHoursValue] [nvarchar](100) NULL,
	[ManhourEstimatesCostValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_ManhourEstimates] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_ManhourEstimatesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_PRDDepartment]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_PRDDepartment](
	[PIDF_API_Charter_PRDDepartmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[PIDFId] [bigint] NULL,
	[PRDDepartmentId] [int] NULL,
	[PRDDepartmentRawMaterialValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_PRDDepartment] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_PRDDepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Charter_TimelineInMonths]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Charter_TimelineInMonths](
	[PIDF_API_Charter_TimelineInMonthsId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDF_API_CharterId] [bigint] NOT NULL,
	[TimelineInMonthsId] [int] NULL,
	[TimelineInMonthsValue] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_API_Charter_TimelineInMonths_1] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_Charter_TimelineInMonthsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Inhouse]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Inhouse](
	[PIDFAPIInhouseId] [int] IDENTITY(1,1) NOT NULL,
	[APIInhouseId] [int] NULL,
	[PIDFId] [bigint] NULL,
	[Primary] [varchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PIDFAPIInhouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_IPD]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_IPD](
	[PIDF_API_IPD_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NULL,
	[MarketDetailsFileName] [nvarchar](200) NULL,
	[ProductTypeId] [int] NULL,
	[DrugsCategory] [nvarchar](200) NULL,
	[ProductStrength] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[BusinessUnitId] [int] NULL,
 CONSTRAINT [PK__PIDF_API_IPD] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_IPD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Master](
	[PIDFAPIMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[UserId] [int] NULL,
	[Interested] [bit] NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_API_Master] PRIMARY KEY CLUSTERED 
(
	[PIDFAPIMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_Outsource_Data]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_Outsource_Data](
	[APIOutsourceDataId] [int] IDENTITY(1,1) NOT NULL,
	[APIOutsourceId] [int] NULL,
	[PIDFId] [bigint] NULL,
	[Primary] [varchar](255) NULL,
	[Potential_Alt_1] [varchar](255) NULL,
	[Potential_Alt_2] [varchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[APIOutsourceDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_API_RnD]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_API_RnD](
	[PIDF_API_RnD_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[SponsorBusinessPartner] [nvarchar](100) NULL,
	[APIMarketPrice] [nvarchar](100) NULL,
	[APITargetRMC_CCPC] [nvarchar](100) NULL,
	[MarketExtenstionId] [int] NULL,
	[Development] [nvarchar](100) NULL,
	[ScaleUp] [nvarchar](100) NULL,
	[Exhibit] [nvarchar](100) NULL,
	[PlantQC] [nvarchar](100) NULL,
	[Total] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK__PIDF_API_RnD] PRIMARY KEY CLUSTERED 
(
	[PIDF_API_RnD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_BusinessUnit](
	[PIDFBusinessUnitID] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [int] NOT NULL,
	[OralId] [int] NULL,
	[UnitofMeasurementId] [int] NULL,
	[DosageFormId] [int] NULL,
	[PackagingTypeId] [int] NULL,
	[BusinessUnitId] [int] NULL,
	[BrandName] [nvarchar](100) NULL,
	[ApprovedGenerics] [nvarchar](100) NULL,
	[LaunchedGenerics] [nvarchar](100) NULL,
	[RFDBrand] [nvarchar](100) NULL,
	[RFDApplicant] [nvarchar](100) NULL,
	[RFDCountryId] [int] NULL,
	[RFDIndication] [nvarchar](100) NULL,
	[RFDInnovators] [nvarchar](100) NULL,
	[RFDInitialRevenuePotential] [nvarchar](100) NULL,
	[RFDPriceDiscounting] [nvarchar](100) NULL,
	[RFDCommercialBatchSize] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[MarketExtenstionId] [int] NULL,
	[DIAId] [int] NULL,
	[TradeNameRequired] [bit] NULL,
	[TradeNameDate] [datetime] NULL,
 CONSTRAINT [PK_[PIDFBusinessUnitID] PRIMARY KEY CLUSTERED 
(
	[PIDFBusinessUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_BusinessUnit_Country](
	[PIDFBusinessUnitCountryId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_BusinessUnit_Country] PRIMARY KEY CLUSTERED 
(
	[PIDFBusinessUnitCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_BusinessUnit_Interested]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_BusinessUnit_Interested](
	[PIDFBusinessUnitId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[IsInterested] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IPAddress] [varchar](20) NULL,
 CONSTRAINT [PK_PIDF_BusinessUnit_Interested] PRIMARY KEY CLUSTERED 
(
	[PIDFBusinessUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Commercial]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Commercial](
	[PIDFCommercialId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[PIDFProductStrengthId] [bigint] NOT NULL,
	[MarketSizeInUnit] [nvarchar](100) NULL,
	[ShelfLife] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[PackSizeId] [int] NOT NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_PIDF_Commercial] PRIMARY KEY CLUSTERED 
(
	[PIDFCommercialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Commercial_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Commercial_Master](
	[PIDFCommercialMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[Interested] [bit] NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_PIDF_Commercial_Master] PRIMARY KEY CLUSTERED 
(
	[PIDFCommercialMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Commercial_Years]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Commercial_Years](
	[PIDFCommercialYearId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFCommercialId] [bigint] NOT NULL,
	[YearIndex] [int] NOT NULL,
	[PackagingTypeId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CommercialBatchSize] [nvarchar](20) NULL,
	[PriceDiscounting] [nvarchar](20) NULL,
	[TotalAPIReq] [nvarchar](20) NULL,
	[APIReq] [nvarchar](20) NULL,
	[SUIMSVolume] [nvarchar](20) NULL,
	[FreeOfCost] [nvarchar](20) NULL,
	[MarketGrowth] [nvarchar](20) NULL,
	[MarketSize] [nvarchar](20) NULL,
	[PriceErosion] [nvarchar](20) NULL,
	[FinalSelectionId] [int] NULL,
	[MarketSharePercentageLow] [nvarchar](20) NULL,
	[MarketSharePercentageMedium] [nvarchar](20) NULL,
	[MarketSharePercentageHigh] [nvarchar](20) NULL,
	[MarketShareUnitLow] [nvarchar](20) NULL,
	[MarketShareUnitMedium] [nvarchar](20) NULL,
	[MarketShareUnitHigh] [nvarchar](20) NULL,
	[NSPUnitsLow] [nvarchar](20) NULL,
	[NSPUnitsMedium] [nvarchar](20) NULL,
	[NSPUnitsHigh] [nvarchar](20) NULL,
	[NSPLow] [nvarchar](20) NULL,
	[NSPMedium] [nvarchar](20) NULL,
	[NSPHigh] [nvarchar](20) NULL,
	[BrandPrice] [nvarchar](20) NULL,
	[GenericPrice] [nvarchar](20) NULL,
	[TargetCostOfGood] [nvarchar](20) NULL,
 CONSTRAINT [PK_PIDF_Commercial_Year] PRIMARY KEY CLUSTERED 
(
	[PIDFCommercialYearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Finance]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Finance](
	[PIDFFinaceId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[Entity] [nvarchar](70) NULL,
	[Product] [nvarchar](70) NULL,
	[ForecastDate] [datetime] NULL,
	[Currencyid] [nvarchar](50) NULL,
	[DosageFrom] [int] NULL,
	[ManufacturingSiteOrPartner] [nvarchar](70) NULL,
	[SKUs] [nvarchar](70) NULL,
	[MSPersentage] [int] NULL,
	[TargetPriceScenario] [int] NULL,
	[ProjectStartDate] [datetime] NULL,
	[BatchManufacturing] [datetime] NULL,
	[ExpectedFilling] [datetime] NULL,
	[ApprovalPeriodinDays] [nvarchar](20) NULL,
	[ApprovalDate] [datetime] NULL,
	[ProductLaunchDate] [datetime] NULL,
	[GestationPeriodinYears] [numeric](18, 2) NULL,
	[MarketShareErosionrate] [numeric](18, 2) NULL,
	[PriceErosion] [numeric](18, 2) NULL,
	[EscalationinCOGS] [nvarchar](70) NULL,
	[DiscountRate] [numeric](18, 2) NULL,
	[Incometaxrate] [numeric](18, 2) NULL,
	[Opexasapercenttosale] [float] NULL,
	[ExternalProfitSharepercent] [float] NULL,
	[CollectioninDays] [float] NULL,
	[InventoryinDays] [float] NULL,
	[CreditorinDays] [float] NULL,
	[MarketingAllowance] [numeric](18, 2) NULL,
	[RegulatoryMaintenanceCost] [numeric](18, 2) NULL,
	[GrosstoNet] [numeric](18, 2) NULL,
	[Noofbatchestobemanufactured] [float] NULL,
	[NoofbatchestobemanufacturedPhaseEndDate] [datetime] NULL,
	[NoSKUs] [float] NULL,
	[NoSKUsPhaseEndDate] [datetime] NULL,
	[RandDAnalyticalcost] [numeric](18, 2) NULL,
	[RandDAnalyticalcostPhaseEndDate] [datetime] NULL,
	[RLDsamplecost] [numeric](18, 2) NULL,
	[RLDsamplecostPhaseEndDate] [datetime] NULL,
	[BatchmanufacturingcostOrAPIActualsEst] [numeric](18, 2) NULL,
	[BatchmanufacturingcostOrAPIActualsEstPhaseEndDate] [datetime] NULL,
	[Sixmonthsstabilitycost] [numeric](18, 2) NULL,
	[SixmonthsstabilitycostPhaseEndDate] [datetime] NULL,
	[TechTransfer] [numeric](18, 2) NULL,
	[TechTransferPhaseEndDate] [datetime] NULL,
	[BEstudies] [numeric](18, 2) NULL,
	[BEstudiesPhaseEndDate] [datetime] NULL,
	[Filingfees] [numeric](18, 2) NULL,
	[FilingfeesPhaseEndDate] [datetime] NULL,
	[BioStuddyCost] [numeric](18, 2) NULL,
	[BioStuddyCostPhaseEndDate] [datetime] NULL,
	[Capex] [numeric](18, 2) NULL,
	[CapexPhaseEndDate] [datetime] NULL,
	[ToolingAndChangeParts] [numeric](18, 2) NULL,
	[ToolingAndChangePartsPhaseEndDate] [datetime] NULL,
	[Total] [numeric](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_Finance_1] PRIMARY KEY CLUSTERED 
(
	[PIDFFinaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Finance_BatchSizeCoating]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Finance_BatchSizeCoating](
	[PIDFFinaceBatchSizeCoatingId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFFinaceId] [int] NOT NULL,
	[BusinessUnitId] [int] NULL,
	[Batchsize] [float] NULL,
	[Yield] [float] NULL,
	[Batchoutput] [float] NULL,
	[API_CAD] [float] NULL,
	[Excipients_CAD] [float] NULL,
	[PM_CAD] [float] NULL,
	[CCPC_CAD] [float] NULL,
	[Freight_CAD] [float] NULL,
	[EmcureCOGs_pack] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[SKus] [int] NULL,
	[PakeSize] [float] NULL,
	[BrandPrice] [float] NULL,
	[GenericListprice] [float] NULL,
	[EstMAT2016_BY_12Units] [float] NULL,
	[EstMAT2020_BY_12Units] [float] NULL,
	[CAGRover2016_By_12EstMATunits] [float] NULL,
	[Marketinpacks] [float] NULL,
	[Batchsizein_ltr_tabs] [float] NULL,
	[NetRealisation] [float] NULL,
 CONSTRAINT [PK__PIDF_Fin__6C70D23F0E6D1BC3] PRIMARY KEY CLUSTERED 
(
	[PIDFFinaceBatchSizeCoatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Finance_Projection]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Finance_Projection](
	[FinanceProjectionId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFFinaceId] [int] NOT NULL,
	[BusinessUnitId] [int] NULL,
	[PIDFID] [bigint] NULL,
	[Year] [nvarchar](50) NULL,
	[Expiries] [float] NULL,
	[AnnualFee] [float] NULL,
	[AnnualConfirmatoryRelease] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FinanceProjectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_IPD]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_IPD](
	[IPDID] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NULL,
	[MarketName] [nvarchar](200) NULL,
	[DataExclusivity] [nvarchar](200) NULL,
	[FillingType] [nvarchar](200) NULL,
	[ApprovedGenetics] [nvarchar](100) NULL,
	[LaunchedGenetics] [nvarchar](100) NULL,
	[Innovators] [nvarchar](100) NULL,
	[LegalStatus] [nvarchar](100) NULL,
	[CostOfLitication] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[BusinessUnitId] [int] NULL,
	[IsComment] [bit] NULL,
	[PatentStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK__PIDF_IPD__54D2918E72B886FB] PRIMARY KEY CLUSTERED 
(
	[IPDID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_IPD_Country]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_IPD_Country](
	[IPDCountryID] [bigint] IDENTITY(1,1) NOT NULL,
	[IPDID] [bigint] NOT NULL,
	[CountryId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_IPD_Country] PRIMARY KEY CLUSTERED 
(
	[IPDCountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_IPD_General]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_IPD_General](
	[PIDF_IPD_General_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IPDID] [bigint] NOT NULL,
	[BusinessUnitId] [int] NULL,
	[CountryId] [int] NULL,
	[MarketName] [nvarchar](200) NULL,
	[DataExclusivity] [nvarchar](200) NULL,
	[MarketExclusivityDate] [datetime] NULL,
	[ExpectedFilingDate] [datetime] NULL,
	[ExpectedLaunchDate] [datetime] NULL,
	[ApprovedGenetics] [nvarchar](100) NULL,
	[LaunchedGenetics] [nvarchar](100) NULL,
	[LegalStatus] [nvarchar](100) NULL,
	[CostOfLitication] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsComment] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK__PIDF_IPD_General_Id] PRIMARY KEY CLUSTERED 
(
	[PIDF_IPD_General_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_IPD_PatentDetails]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_IPD_PatentDetails](
	[PatentDetailsID] [bigint] IDENTITY(1,1) NOT NULL,
	[IPDID] [bigint] NOT NULL,
	[Type] [varchar](50) NULL,
	[OriginalExpiryDate] [date] NULL,
	[ExtensionExpiryDate] [date] NULL,
	[Comments] [nvarchar](100) NULL,
	[Strategy] [varchar](100) NULL,
	[PatentNumber] [nvarchar](50) NULL,
	[PatentType] [smallint] NULL,
	[BasicPatentExpiry] [date] NULL,
	[OtherLmitingPatentDate1] [date] NULL,
	[OtherLmitingPatentDate2] [date] NULL,
	[EarliestLaunchDate] [date] NULL,
	[AnyPatentstobeFiled] [bit] NULL,
	[EarliestMarketEntry] [date] NULL,
	[stimatedNumberofgenericsinthe] [nvarchar](100) NULL,
	[Lawfirmbeingused] [nvarchar](100) NULL,
	[CountryId] [int] NULL,
	[PatentStrategy] [int] NULL,
	[PatentStrategyOther] [nvarchar](100) NULL,
	[PIDF_IPD_General_Id] [bigint] NULL,
 CONSTRAINT [PK_PIDF_IPD_PatentDetails] PRIMARY KEY CLUSTERED 
(
	[PatentDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_IPD_Region]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_IPD_Region](
	[IPDRegionID] [bigint] IDENTITY(1,1) NOT NULL,
	[IPDID] [bigint] NOT NULL,
	[RegionId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_IPD_Region] PRIMARY KEY CLUSTERED 
(
	[IPDRegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_ManagementApprovalStatusHistory]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_ManagementApprovalStatusHistory](
	[ManagementApprovalStatusHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NULL,
	[StatusId] [int] NULL,
	[Remark] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_ManagementApprovalStatusHistory] PRIMARY KEY CLUSTERED 
(
	[ManagementApprovalStatusHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Medical]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Medical](
	[PIDFMedicalId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[MedicalOpinion] [int] NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_PIDF_Medical] PRIMARY KEY CLUSTERED 
(
	[PIDFMedicalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_Medical_File]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_Medical_File](
	[PIDFMedicalFileId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFMedicalId] [bigint] NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_Medical_File] PRIMARY KEY CLUSTERED 
(
	[PIDFMedicalFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF](
	[PIDFPBFId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[ProjectName] [nvarchar](100) NULL,
	[BusinessRelationable] [nvarchar](100) NULL,
	[BERequirementId] [int] NULL,
	[NumberOfApprovedANDA] [nvarchar](100) NULL,
	[ProductTypeId] [int] NULL,
	[PlantId] [int] NULL,
	[WorkflowId] [int] NULL,
	[DosageId] [int] NULL,
	[PatentStatus] [nvarchar](100) NULL,
	[SponsorBusinessPartner] [nvarchar](100) NULL,
	[FillingTypeId] [int] NULL,
	[ScopeObjectives] [nvarchar](100) NULL,
	[FormRnDDivisionId] [int] NULL,
	[ProjectInitiationDate] [datetime] NULL,
	[RnDHead] [nvarchar](100) NULL,
	[ProjectManager] [nvarchar](100) NULL,
	[PackagingTypeId] [int] NULL,
	[ManufacturingId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[BatchManifacturingDate] [datetime] NULL,
	[FillingDateDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Analytical](
	[PIDFPBFAnalyticalId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[TestTypeId] [int] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[Numberoftests] [int] NULL,
	[PrototypeDevelopment] [nvarchar](100) NULL,
	[CostPerTest] [int] NULL,
	[PrototypeCost] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_Exhibit] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFAnalyticalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_AMVCost]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Analytical_AMVCost](
	[TotalAMVCostId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[TotalAMVCost] [float] NULL,
	[TotalAMVTitle] [nvarchar](100) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_Analytical_AMVCost] PRIMARY KEY CLUSTERED 
(
	[TotalAMVCostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping](
	[PBFAnalyticalCostStrengthId] [bigint] IDENTITY(1,1) NOT NULL,
	[TotalAMVCostId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[IsChecked] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_Analytical_AMVCost_StrengthMapping] PRIMARY KEY CLUSTERED 
(
	[PBFAnalyticalCostStrengthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_Cost]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Analytical_Cost](
	[PBFAnalyticalCostId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[TotalAMVCost] [float] NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_Analytical_Cost] PRIMARY KEY CLUSTERED 
(
	[PBFAnalyticalCostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping](
	[PBFAnalyticalCostStrengthId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFAnalyticalCostId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_Analytical_Cost_StrengthMapping] PRIMARY KEY CLUSTERED 
(
	[PBFAnalyticalCostStrengthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Clinical]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Clinical](
	[PBFClinicalId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[BioStudyTypeId] [int] NOT NULL,
	[FastingOrFed] [float] NULL,
	[NumberofVolunteers] [int] NULL,
	[ClinicalCostAndVolume] [float] NULL,
	[BioAnalyticalCostAndVolume] [float] NULL,
	[DocCostandStudy] [float] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_PilotBioFasting] PRIMARY KEY CLUSTERED 
(
	[PBFClinicalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_General](
	[PBFGeneralId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFPBFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[Capex] [nvarchar](50) NULL,
	[TotalExpense] [float] NULL,
	[ProjectComplexity] [int] NULL,
	[ProductTypeId] [int] NULL,
	[TestLicenseAvailability] [varchar](100) NULL,
	[BudgetTimelineSubmissionDate] [datetime] NULL,
	[ProjectDevelopmentInitialDate] [datetime] NULL,
	[FormulationGLId] [int] NULL,
	[AnalyticalGLId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[BEStudyResults] [nvarchar](50) NULL,
 CONSTRAINT [PK_PIDF_PBF_g] PRIMARY KEY CLUSTERED 
(
	[PBFGeneralId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_RND]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_General_RND](
	[PbfRndDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[PidfId] [bigint] NOT NULL,
	[PbfId] [bigint] NOT NULL,
	[RndResponsiblePerson] [nvarchar](100) NULL,
	[TypeOfDevelopmentDate] [datetime] NULL,
	[PivotalBatchesManufacturedCompleted] [datetime] NULL,
	[StabilityResultsDayZero] [datetime] NULL,
	[StabilityResultsThreeMonth] [datetime] NULL,
	[StabilityResultsSixMonth] [datetime] NULL,
	[NonStandardProduct] [bit] NULL,
	[Pivotals] [nvarchar](100) NULL,
	[BatchSizes] [nvarchar](100) NULL,
	[NoMOfBatchesPerStrength] [bigint] NULL,
	[SiteTransferDate] [datetime] NULL,
	[ApiOrderedDate] [datetime] NULL,
	[ApiReceivedDate] [datetime] NULL,
	[FinalFormulationApproved] [datetime] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[BusinessUnitId] [int] NULL,
 CONSTRAINT [PK__PIDF_PBF__5350EF231EB5A49E] PRIMARY KEY CLUSTERED 
(
	[PbfRndDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_Strength]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_General_Strength](
	[PIDFPBFGeneralStrengthId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ProjectCode] [nvarchar](50) NULL,
	[ImprintingEmbossingCode] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_General_Strength] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFGeneralStrengthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_HeadWiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_HeadWiseBudget](
	[HeadWiseBudgetId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectActivitiesId] [int] NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[Prototype] [float] NULL,
	[ScaleUp] [float] NULL,
	[Exhibit] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_HeadWiseBudget] PRIMARY KEY CLUSTERED 
(
	[HeadWiseBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_MarketMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_MarketMapping](
	[PIDFPBFMarketId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFPBFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_MarketMapping] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFMarketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Outsource](
	[PIDFPBFOutsourceId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[ProjectWorkflowId] [int] NOT NULL,
	[PBFWorkflowId] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF_Outsource] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFOutsourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource_Task]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Outsource_Task](
	[PIDFPBFOutsourceTaskId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFPBFOutsourceId] [int] NOT NULL,
	[PBFWorkFlowTaskName] [nvarchar](100) NULL,
	[PBfWorkFlowId] [int] NOT NULL,
	[TaskLevel] [int] NULL,
	[ParentId] [int] NULL,
	[Cost] [float] NULL,
	[Tentative] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF_Outsource_Task] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFOutsourceTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_PhaseWiseBudget]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_PhaseWiseBudget](
	[PhaseWiseBudgetId] [int] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[FeasabilityCumTotal] [float] NULL,
	[PrototypeCumTotal] [float] NULL,
	[ScaleUpCumTotal] [float] NULL,
	[AMVCumTotal] [float] NULL,
	[ExhibitCumTotal] [float] NULL,
	[FilingCumTotal] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[FeasabilityCumTotalDate] [date] NULL,
	[PrototypeCumTotalDate] [date] NULL,
	[ScaleUpCumTotalDate] [date] NULL,
	[AMVCumTotalDate] [date] NULL,
	[ExhibitCumTotalDate] [date] NULL,
	[FilingCumTotalDate] [date] NULL,
 CONSTRAINT [PK_PIDF_PBF_PhaseWiseBudget] PRIMARY KEY CLUSTERED 
(
	[PhaseWiseBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RA]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RA](
	[PIDFPBFRAId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[PBFId] [bigint] NOT NULL,
	[CountryIdBuId] [int] NOT NULL,
	[PivotalBatchManufactured] [datetime] NULL,
	[LastDataFromRnD] [datetime] NULL,
	[BEFinalReport] [datetime] NULL,
	[BuId] [int] NOT NULL,
	[TypeOfSubmissionId] [int] NULL,
	[DossierReadyDate] [datetime] NULL,
	[EarliestSubmissionDExcl] [datetime] NULL,
	[EarliestLaunchDExcl] [datetime] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LasDateToRegulatory] [datetime] NULL,
	[EndOfProcedureDate] [datetime] NULL,
	[CountryApprovalDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF_RA] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFRAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Reference_Product_detail]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Reference_Product_detail](
	[PIDFPBFReferenceProductdetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[BusinessUnitId] [bigint] NOT NULL,
	[RFDBrand] [nvarchar](100) NULL,
	[RFDApplicant] [nvarchar](100) NULL,
	[RFDCountryId] [int] NULL,
	[RFDIndication] [nvarchar](100) NULL,
	[RFDInnovators] [nvarchar](100) NULL,
	[RFDInitialRevenuePotential] [nvarchar](100) NULL,
	[RFDPriceDiscounting] [nvarchar](100) NULL,
	[RFDCommercialBatchSize] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDFPBFReferenceProductdetail] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFReferenceProductdetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_APIRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_APIRequirement](
	[APIRequirementId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[Prototype] [float] NULL,
	[ScaleUp] [float] NULL,
	[ExhibitBatch1] [float] NULL,
	[ExhibitBatch2] [float] NULL,
	[ExhibitBatch3] [float] NULL,
	[PrototypeCost] [float] NULL,
	[ScaleUpCost] [float] NULL,
	[ExhibitBatchCost] [float] NULL,
	[TotalCost] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_APIRequirement] PRIMARY KEY CLUSTERED 
(
	[APIRequirementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Rnd_BatchSize]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Rnd_BatchSize](
	[BatchSizeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[PrototypeFormulation] [float] NULL,
	[ScaleUpbatch] [float] NULL,
	[ExhibitBatch1] [float] NULL,
	[ExhibitBatch2] [float] NULL,
	[ExhibitBatch3] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Salt] [float] NULL,
 CONSTRAINT [PK_PIDF_PBF_Rnd_BatchSize] PRIMARY KEY CLUSTERED 
(
	[BatchSizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses](
	[CapexMiscellaneousExpensesId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ActivityTypeId] [int] NULL,
	[StrengthMiscellaneousExpense] [float] NULL,
	[MiscellaneousDevelopment] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_CapexMiscellaneousExpenses] PRIMARY KEY CLUSTERED 
(
	[CapexMiscellaneousExpensesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientPrototype]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype](
	[ExicipientProtoypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PidfPbfGeneralId] [bigint] NOT NULL,
	[BusinessUnitId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ExicipientPrototype] [nvarchar](80) NULL,
	[RsPerKg] [float] NULL,
	[MgPerUnitDosage] [nvarchar](80) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_ExicipientPrototype] PRIMARY KEY CLUSTERED 
(
	[ExicipientProtoypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientRequirement]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement](
	[PIDFPBFRNDExicipientId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[ExicipientPrototype] [nvarchar](200) NULL,
	[ExicipientDevelopment] [float] NULL,
	[RsPerKg] [float] NULL,
	[MgPerUnitDosage] [float] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_Exicipient] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFRNDExicipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ExicipientScaleUp]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp](
	[ExicipientScaleUpId] [bigint] IDENTITY(1,1) NOT NULL,
	[PidfPbfGeneralId] [bigint] NOT NULL,
	[BusinessUnitId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ExicipientScaleUp] [nvarchar](80) NULL,
	[RsPerKg] [float] NULL,
	[MgPerUnitDosage] [nvarchar](80) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ExicipientScaleUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_FillingExpenses]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses](
	[FillingExpensesId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[IsChecked] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[TotalCost] [float] NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_FillingExpenses] PRIMARY KEY CLUSTERED 
(
	[FillingExpensesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ManPowerCost]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost](
	[ManPowerCostId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectActivitiesId] [int] NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[DurationInDays] [float] NULL,
	[ManPowerInDays] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_ManPowerCost] PRIMARY KEY CLUSTERED 
(
	[ManPowerCostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_Master]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_Master](
	[RnDMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[BatchSizeId] [bigint] NOT NULL,
	[APIRequirementMarketPrice] [float] NULL,
	[PlanSupportCostRsPerDay] [float] NULL,
	[ManHourRate] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[PlantId] [int] NULL,
	[LineId] [int] NULL,
	[APIRequirementVendorName] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_Master] PRIMARY KEY CLUSTERED 
(
	[RnDMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackagingMaterial]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial](
	[PIDFPBFRNDPackagingId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
	[PackingTypeId] [int] NULL,
	[UnitOfMeasurement] [nvarchar](20) NULL,
	[PackagingDevelopment] [float] NULL,
	[RsPerUnit] [float] NULL,
	[Quantity] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_Packaging] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFRNDPackagingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackSizeStability]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability](
	[PackSizeStabilityId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [int] NULL,
	[PackSizeId] [int] NULL,
	[Value] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK__PIDF_PBF__5C057E65FAB7B4EF] PRIMARY KEY CLUSTERED 
(
	[PackSizeStabilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_PlantSupportCost]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost](
	[PlantSupportCostId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ScaleUp] [float] NULL,
	[Exhibit] [float] NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_PlantSupportCost] PRIMARY KEY CLUSTERED 
(
	[PlantSupportCostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ReferenceProductDetail]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail](
	[ReferenceProductDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ReferenceProductDetailDevelopment] [float] NULL,
	[UnitCostOfReferenceProduct] [float] NULL,
	[FormulationDevelopment] [float] NULL,
	[PilotBE] [float] NULL,
	[PharmasuiticalEquivalence] [float] NULL,
	[PivotalBio] [float] NULL,
	[TotalCost] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_ReferenceProductDetail] PRIMARY KEY CLUSTERED 
(
	[ReferenceProductDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_RnD_ToolingChangepart]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart](
	[ToolingChangepartId] [bigint] IDENTITY(1,1) NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [bigint] NOT NULL,
	[ActivityTypeId] [int] NULL,
	[Cost] [float] NULL,
	[Prototype] [nvarchar](100) NULL,
	[StrengthUnitQuantity] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_PBF_RnD_ToolingChangepart] PRIMARY KEY CLUSTERED 
(
	[ToolingChangepartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDFAPIDetails]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFAPIDetails](
	[PIDFAPIId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[APIName] [nvarchar](100) NULL,
	[APISourcingId] [int] NULL,
	[APIVendor] [nvarchar](100) NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
	[BusinessUnitId] [int] NULL,
 CONSTRAINT [PK_PIDFAPIDetails] PRIMARY KEY CLUSTERED 
(
	[PIDFAPIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDFIMSData]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFIMSData](
	[PIDFIMSDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[IMSValue] [float] NOT NULL,
	[IMSVolume] [float] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
	[BusinessUnitId] [int] NULL,
 CONSTRAINT [PK_PIDFIMSData] PRIMARY KEY CLUSTERED 
(
	[PIDFIMSDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDFProductStrength]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFProductStrength](
	[PIDFProductStrengthId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[Strength] [nvarchar](100) NULL,
	[UnitofMeasurementId] [int] NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
	[BusinessUnitId] [int] NULL,
 CONSTRAINT [PK_PIDFProductStrength] PRIMARY KEY CLUSTERED 
(
	[PIDFProductStrengthId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDFProductStrength_CountryMapping]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFProductStrength_CountryMapping](
	[PIDFProductStrengthCountryId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFProductStrengthId] [bigint] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDFProductStrength_CountryMapping] PRIMARY KEY CLUSTERED 
(
	[PIDFProductStrengthCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDFStatusHistory]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFStatusHistory](
	[PIDFStatusHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[StatusRemark] [nvarchar](300) NULL,
 CONSTRAINT [PK_PIDFStatusHistory] PRIMARY KEY CLUSTERED 
(
	[PIDFStatusHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTask]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTask](
	[ProjectTaskId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[TaskName] [nvarchar](100) NOT NULL,
	[TaskOwnerId] [int] NOT NULL,
	[TaskLevel] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[TaskDuration] [int] NOT NULL,
	[TotalPercentage] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ParentId] [bigint] NULL,
	[PlannedStartDate] [datetime] NULL,
	[PlannedEndDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectTask] PRIMARY KEY CLUSTERED 
(
	[ProjectTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleModulePermission]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleModulePermission](
	[RoleModuleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[ModuleId] [int] NULL,
	[SubModuleId] [int] NULL,
	[View] [bit] NULL,
	[Add] [bit] NULL,
	[Edit] [bit] NULL,
	[Delete] [bit] NULL,
	[Approve] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_RoleModulePermission] PRIMARY KEY CLUSTERED 
(
	[RoleModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_SessionManager]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_SessionManager](
	[TokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TokenIssuedAt] [datetime] NULL,
	[VallidTo] [datetime] NULL,
	[UserToken] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_SessionManager] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_WishList]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_WishList](
	[WishListId] [bigint] IDENTITY(1,1) NOT NULL,
	[WishListTypeId] [int] NULL,
	[GeographyId] [int] NULL,
	[CountryId] [int] NULL,
	[MoleculeName] [nvarchar](100) NULL,
	[Strength] [nvarchar](50) NULL,
	[IsInhouseOrInLicensed] [char](20) NULL,
	[DateOfFiling] [datetime] NULL,
	[DateOfApproval] [datetime] NULL,
	[NameofVendor] [nvarchar](100) NULL,
	[VendorEvaluationRemark] [nvarchar](50) NULL,
	[ReferenceDrugProduct] [nvarchar](200) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__Tbl_Wish__E41F87876701DC93] PRIMARY KEY CLUSTERED 
(
	[WishListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSessionLogMaster]    Script Date: 10/18/2023 5:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessionLogMaster](
	[UserLoginHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserSessionLogMaster] PRIMARY KEY CLUSTERED 
(
	[UserLoginHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Master_EmailLog] ADD  DEFAULT ((0)) FOR [SentSuccessfully]
GO
ALTER TABLE [dbo].[Master_EmailLog] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Master_ExcipientRequirement] ADD  CONSTRAINT [DF_Master_ExcipientRequirement_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Master_NationApproval] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Master_Notification] ADD  CONSTRAINT [DF__Master_No__IsEma__367C1819]  DEFAULT ((0)) FOR [IsEmailSent]
GO
ALTER TABLE [dbo].[Master_PlantLine] ADD  CONSTRAINT [DF_Master_PlantLine_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Master_PlantLine] ADD  CONSTRAINT [DF_Master_PlantLine_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Master_TypeOfSubmission] ADD  CONSTRAINT [DF_Master_TypeOfSubmission_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Master_WishListType] ADD  CONSTRAINT [DF_Master_WishListType_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Master_WishListType] ADD  CONSTRAINT [DF_Master_WishListType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsPri__3A785CDD]  DEFAULT ((0)) FOR [IsPrimaryPackaging]
GO
ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsSec__3B6C8116]  DEFAULT ((0)) FOR [IsSecondryPackaging]
GO
ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsEmc__3C60A54F]  DEFAULT ((0)) FOR [IsEmcure]
GO
ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__Creat__3D54C988]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF] ADD  CONSTRAINT [DF__PIDF__MarketExte__6A50C1DA]  DEFAULT ('FALSE') FOR [MarketExtenstionId]
GO
ALTER TABLE [dbo].[PIDF_Finance] ADD  CONSTRAINT [DF_PIDF_Finance_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_Finance] ADD  CONSTRAINT [DF_PIDF_Finance_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PIDF_Finance_BatchSizeCoating] ADD  CONSTRAINT [DF_PIDF_Finance_BatchSizeCoating_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_Finance_Projection] ADD  CONSTRAINT [DF_PIDF_Finance_Projection_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] ADD  CONSTRAINT [DF_PIDF_ManagementApprovalStatusHistory_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] ADD  CONSTRAINT [DF_PIDF_PBF_General_RND_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PIDF_PBF_RA] ADD  CONSTRAINT [DF_PIDF_PBF_RA_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] ADD  CONSTRAINT [DF_PIDF_PBF_RnD_ExicipientPrototype_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp] ADD  CONSTRAINT [DF_PIDF_PBF_RnD_ExicipientScaleUp_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] ADD  CONSTRAINT [DF_PIDF_PBF_RnD_PackSizeStability_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Tbl_WishList] ADD  CONSTRAINT [DF_Tbl_WishList_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Tbl_WishList] ADD  CONSTRAINT [DF_Tbl_WishList_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_BusinessUnitId] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping] CHECK CONSTRAINT [FK_Master_BusinessUnitId]
GO
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_CountryId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Master_Region] ([RegionId])
GO
ALTER TABLE [dbo].[Master_BusinessUnitRegionMapping] CHECK CONSTRAINT [FK_Master_CountryId]
GO
ALTER TABLE [dbo].[Master_CurrencyCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_CurrencyCountryMapping_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[Master_CurrencyCountryMapping] CHECK CONSTRAINT [FK_Master_CurrencyCountryMapping_Master_Country]
GO
ALTER TABLE [dbo].[Master_CurrencyCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_CurrencyCountryMapping_Master_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Master_Currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[Master_CurrencyCountryMapping] CHECK CONSTRAINT [FK_Master_CurrencyCountryMapping_Master_Currency]
GO
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_DepartmentBusinessUnitMapping_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping] CHECK CONSTRAINT [FK_Master_DepartmentBusinessUnitMapping_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Master_Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Master_DepartmentBusinessUnitMapping] CHECK CONSTRAINT [FK_Master_DepartmentId]
GO
ALTER TABLE [dbo].[Master_NationApproval_CountryMapping]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[Master_NationApproval_CountryMapping]  WITH CHECK ADD FOREIGN KEY([NationApprovalId])
REFERENCES [dbo].[Master_NationApproval] ([NationApprovalId])
GO
ALTER TABLE [dbo].[Master_Notification]  WITH CHECK ADD  CONSTRAINT [FK_Master_Notification_Master_PIDFStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Master_PIDFStatus] ([PIDFStatusID])
GO
ALTER TABLE [dbo].[Master_Notification] CHECK CONSTRAINT [FK_Master_Notification_Master_PIDFStatus]
GO
ALTER TABLE [dbo].[Master_Notification]  WITH CHECK ADD  CONSTRAINT [FK_Master_Notification_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[Master_Notification] CHECK CONSTRAINT [FK_Master_Notification_PIDF]
GO
ALTER TABLE [dbo].[Master_Notification_User]  WITH CHECK ADD  CONSTRAINT [FK_Master_Notification_User_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Master_Notification_User] CHECK CONSTRAINT [FK_Master_Notification_User_Master_User]
GO
ALTER TABLE [dbo].[Master_PlantLine]  WITH CHECK ADD  CONSTRAINT [FK_Master_PlantLine_Master_PlantLine] FOREIGN KEY([LineId])
REFERENCES [dbo].[Master_PlantLine] ([LineId])
GO
ALTER TABLE [dbo].[Master_PlantLine] CHECK CONSTRAINT [FK_Master_PlantLine_Master_PlantLine]
GO
ALTER TABLE [dbo].[Master_RegionCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_RegionCountryMapping_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[Master_RegionCountryMapping] CHECK CONSTRAINT [FK_Master_RegionCountryMapping_Master_Country]
GO
ALTER TABLE [dbo].[Master_RegionCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Master_Region] ([RegionId])
GO
ALTER TABLE [dbo].[Master_RegionCountryMapping] CHECK CONSTRAINT [FK_Master_RegionId]
GO
ALTER TABLE [dbo].[Master_SubModule]  WITH CHECK ADD  CONSTRAINT [FK_Master_SubModule_Master_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Master_Module] ([ModuleId])
GO
ALTER TABLE [dbo].[Master_SubModule] CHECK CONSTRAINT [FK_Master_SubModule_Master_Module]
GO
ALTER TABLE [dbo].[Master_User]  WITH CHECK ADD  CONSTRAINT [FK_Master_User_Master_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Master_Role] ([RoleId])
GO
ALTER TABLE [dbo].[Master_User] CHECK CONSTRAINT [FK_Master_User_Master_Role]
GO
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserBusinessUnitMapping_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping] CHECK CONSTRAINT [FK_Master_UserBusinessUnitMapping_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Master_UserBusinessUnitMapping] CHECK CONSTRAINT [FK_Master_UserId]
GO
ALTER TABLE [dbo].[Master_UserCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserCountryMapping_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[Master_UserCountryMapping] CHECK CONSTRAINT [FK_Master_UserCountryMapping_Master_Country]
GO
ALTER TABLE [dbo].[Master_UserCountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserCountryMapping_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Master_UserCountryMapping] CHECK CONSTRAINT [FK_Master_UserCountryMapping_Master_User]
GO
ALTER TABLE [dbo].[Master_UserDepartmentMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserDepartmentMapping_Master_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Master_Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Master_UserDepartmentMapping] CHECK CONSTRAINT [FK_Master_UserDepartmentMapping_Master_Department]
GO
ALTER TABLE [dbo].[Master_UserDepartmentMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserDepartmentMapping_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Master_UserDepartmentMapping] CHECK CONSTRAINT [FK_Master_UserDepartmentMapping_Master_User]
GO
ALTER TABLE [dbo].[Master_UserRegionMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserRegionMapping_Master_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Master_Region] ([RegionId])
GO
ALTER TABLE [dbo].[Master_UserRegionMapping] CHECK CONSTRAINT [FK_Master_UserRegionMapping_Master_Region]
GO
ALTER TABLE [dbo].[Master_UserRegionMapping]  WITH CHECK ADD  CONSTRAINT [FK_Master_UserRegionMapping_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Master_UserRegionMapping] CHECK CONSTRAINT [FK_Master_UserRegionMapping_Master_User]
GO
ALTER TABLE [dbo].[Master_WorkFlowTasks]  WITH CHECK ADD FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Master_Workflow] ([WorkflowId])
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_Country] FOREIGN KEY([RFDCountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_Country]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_DIA] FOREIGN KEY([DIAId])
REFERENCES [dbo].[Master_DIA] ([DIAId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_DIA]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_DosageForm] FOREIGN KEY([DosageFormId])
REFERENCES [dbo].[Master_DosageForm] ([DosageFormId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_DosageForm]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_MarketExtenstion] FOREIGN KEY([MarketExtenstionId])
REFERENCES [dbo].[Master_MarketExtenstion] ([MarketExtenstionId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_MarketExtenstion]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_Oral] FOREIGN KEY([OralId])
REFERENCES [dbo].[Master_Oral] ([OralId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_Oral]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_PackagingType] FOREIGN KEY([PackagingTypeId])
REFERENCES [dbo].[Master_PackagingType] ([PackagingTypeId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_PackagingType]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_PIDFStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Master_PIDFStatus] ([PIDFStatusID])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_PIDFStatus]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_PIDFStatus1] FOREIGN KEY([LastStatusId])
REFERENCES [dbo].[Master_PIDFStatus] ([PIDFStatusID])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_PIDFStatus1]
GO
ALTER TABLE [dbo].[PIDF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Master_UnitofMeasurement] FOREIGN KEY([UnitofMeasurementId])
REFERENCES [dbo].[Master_UnitofMeasurement] ([UnitofMeasurementId])
GO
ALTER TABLE [dbo].[PIDF] CHECK CONSTRAINT [FK_PIDF_Master_UnitofMeasurement]
GO
ALTER TABLE [dbo].[PIDF_API_Charter]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_API_Charter] CHECK CONSTRAINT [FK_PIDF_API_Charter_PIDF]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_AnalyticalDepartment]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_AnalyticalDepartment_PIDF_API_Charter] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_AnalyticalDepartment] CHECK CONSTRAINT [FK_PIDF_API_Charter_AnalyticalDepartment_PIDF_API_Charter]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_CapitalOtherExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_CapitalOtherExpenditure_PIDF_API_Charter_CapitalOtherExpenditure] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_CapitalOtherExpenditure] CHECK CONSTRAINT [FK_PIDF_API_Charter_CapitalOtherExpenditure_PIDF_API_Charter_CapitalOtherExpenditure]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_HeadwiseBudget]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_HeadwiseBudget_PIDF_API_Charter] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_HeadwiseBudget] CHECK CONSTRAINT [FK_PIDF_API_Charter_HeadwiseBudget_PIDF_API_Charter]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_ManhourEstimates]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_ManhourEstimates_PIDF_API_Charter] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_ManhourEstimates] CHECK CONSTRAINT [FK_PIDF_API_Charter_ManhourEstimates_PIDF_API_Charter]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment] CHECK CONSTRAINT [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter_PRDDepartment] FOREIGN KEY([PIDF_API_Charter_PRDDepartmentId])
REFERENCES [dbo].[PIDF_API_Charter_PRDDepartment] ([PIDF_API_Charter_PRDDepartmentId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_PRDDepartment] CHECK CONSTRAINT [FK_PIDF_API_Charter_PRDDepartment_PIDF_API_Charter_PRDDepartment]
GO
ALTER TABLE [dbo].[PIDF_API_Charter_TimelineInMonths]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Charter_TimelineInMonths_PIDF_API_Charter] FOREIGN KEY([PIDF_API_CharterId])
REFERENCES [dbo].[PIDF_API_Charter] ([PIDF_API_CharterId])
GO
ALTER TABLE [dbo].[PIDF_API_Charter_TimelineInMonths] CHECK CONSTRAINT [FK_PIDF_API_Charter_TimelineInMonths_PIDF_API_Charter]
GO
ALTER TABLE [dbo].[PIDF_API_Inhouse]  WITH CHECK ADD FOREIGN KEY([APIInhouseId])
REFERENCES [dbo].[Master_API_Inhouse] ([APIInhouseId])
GO
ALTER TABLE [dbo].[PIDF_API_Inhouse]  WITH CHECK ADD FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_API_IPD]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_IPD_BusinessUnitId] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_API_IPD] CHECK CONSTRAINT [FK_PIDF_API_IPD_BusinessUnitId]
GO
ALTER TABLE [dbo].[PIDF_API_IPD]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_IPD_PIDFID] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_API_IPD] CHECK CONSTRAINT [FK_PIDF_API_IPD_PIDFID]
GO
ALTER TABLE [dbo].[PIDF_API_IPD]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_IPD_ProductTypeId] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[Master_ProductType] ([ProductTypeId])
GO
ALTER TABLE [dbo].[PIDF_API_IPD] CHECK CONSTRAINT [FK_PIDF_API_IPD_ProductTypeId]
GO
ALTER TABLE [dbo].[PIDF_API_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Master_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_API_Master] CHECK CONSTRAINT [FK_PIDF_API_Master_PIDF]
GO
ALTER TABLE [dbo].[PIDF_API_Outsource_Data]  WITH CHECK ADD FOREIGN KEY([APIOutsourceId])
REFERENCES [dbo].[Master_API_Outsource] ([APIOutsourceId])
GO
ALTER TABLE [dbo].[PIDF_API_Outsource_Data]  WITH CHECK ADD FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_BusinessUnit_Interested_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested] CHECK CONSTRAINT [FK_PIDF_BusinessUnit_Interested_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_BusinessUnit_Interested_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_BusinessUnit_Interested] CHECK CONSTRAINT [FK_PIDF_BusinessUnit_Interested_PIDF]
GO
ALTER TABLE [dbo].[PIDF_Commercial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_Commercial] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_Commercial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_PackSize] FOREIGN KEY([PackSizeId])
REFERENCES [dbo].[Master_PackSize] ([PackSizeId])
GO
ALTER TABLE [dbo].[PIDF_Commercial] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_PackSize]
GO
ALTER TABLE [dbo].[PIDF_Commercial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_Commercial] CHECK CONSTRAINT [FK_PIDF_Commercial_PIDF]
GO
ALTER TABLE [dbo].[PIDF_Commercial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_PIDFProductStrength] FOREIGN KEY([PIDFProductStrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_Commercial] CHECK CONSTRAINT [FK_PIDF_Commercial_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Master] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Master] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_PIDF]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Year_Master_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Master_Currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years] CHECK CONSTRAINT [FK_PIDF_Commercial_Year_Master_Currency]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Year_Master_PackagingType] FOREIGN KEY([PackagingTypeId])
REFERENCES [dbo].[Master_PackagingType] ([PackagingTypeId])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years] CHECK CONSTRAINT [FK_PIDF_Commercial_Year_Master_PackagingType]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Year_PIDF_Commercial] FOREIGN KEY([PIDFCommercialId])
REFERENCES [dbo].[PIDF_Commercial] ([PIDFCommercialId])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years] CHECK CONSTRAINT [FK_PIDF_Commercial_Year_PIDF_Commercial]
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Year_PIDF_Commercial_Year] FOREIGN KEY([FinalSelectionId])
REFERENCES [dbo].[Master_FinalSelection] ([FinalSelectionID])
GO
ALTER TABLE [dbo].[PIDF_Commercial_Years] CHECK CONSTRAINT [FK_PIDF_Commercial_Year_PIDF_Commercial_Year]
GO
ALTER TABLE [dbo].[PIDF_Finance_BatchSizeCoating]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Finance_BatchSizeCoating_PIDF_Finance] FOREIGN KEY([PIDFFinaceId])
REFERENCES [dbo].[PIDF_Finance] ([PIDFFinaceId])
GO
ALTER TABLE [dbo].[PIDF_Finance_BatchSizeCoating] CHECK CONSTRAINT [FK_PIDF_Finance_BatchSizeCoating_PIDF_Finance]
GO
ALTER TABLE [dbo].[PIDF_Finance_Projection]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Finance_Projection_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_Finance_Projection] CHECK CONSTRAINT [FK_PIDF_Finance_Projection_PIDF]
GO
ALTER TABLE [dbo].[PIDF_Finance_Projection]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Finance_Projection_PIDF_Finance] FOREIGN KEY([PIDFFinaceId])
REFERENCES [dbo].[PIDF_Finance] ([PIDFFinaceId])
GO
ALTER TABLE [dbo].[PIDF_Finance_Projection] CHECK CONSTRAINT [FK_PIDF_Finance_Projection_PIDF_Finance]
GO
ALTER TABLE [dbo].[PIDF_IPD]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_BusinessUnitId] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_IPD] CHECK CONSTRAINT [FK_PIDF_IPD_BusinessUnitId]
GO
ALTER TABLE [dbo].[PIDF_IPD]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_PIDFID] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_IPD] CHECK CONSTRAINT [FK_PIDF_IPD_PIDFID]
GO
ALTER TABLE [dbo].[PIDF_IPD_Country]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_Country_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[PIDF_IPD_Country] CHECK CONSTRAINT [FK_PIDF_IPD_Country_Master_Country]
GO
ALTER TABLE [dbo].[PIDF_IPD_Country]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_Country_PIDF_IPD] FOREIGN KEY([IPDID])
REFERENCES [dbo].[PIDF_IPD] ([IPDID])
GO
ALTER TABLE [dbo].[PIDF_IPD_Country] CHECK CONSTRAINT [FK_PIDF_IPD_Country_PIDF_IPD]
GO
ALTER TABLE [dbo].[PIDF_IPD_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_General_IPDID] FOREIGN KEY([IPDID])
REFERENCES [dbo].[PIDF_IPD] ([IPDID])
GO
ALTER TABLE [dbo].[PIDF_IPD_General] CHECK CONSTRAINT [FK_PIDF_IPD_General_IPDID]
GO
ALTER TABLE [dbo].[PIDF_IPD_PatentDetails]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_PatentDetails_IPDID] FOREIGN KEY([IPDID])
REFERENCES [dbo].[PIDF_IPD] ([IPDID])
GO
ALTER TABLE [dbo].[PIDF_IPD_PatentDetails] CHECK CONSTRAINT [FK_PIDF_IPD_PatentDetails_IPDID]
GO
ALTER TABLE [dbo].[PIDF_IPD_Region]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_Region_Master_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Master_Region] ([RegionId])
GO
ALTER TABLE [dbo].[PIDF_IPD_Region] CHECK CONSTRAINT [FK_PIDF_IPD_Region_Master_Region]
GO
ALTER TABLE [dbo].[PIDF_IPD_Region]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_Region_PIDF_IPD] FOREIGN KEY([IPDID])
REFERENCES [dbo].[PIDF_IPD] ([IPDID])
GO
ALTER TABLE [dbo].[PIDF_IPD_Region] CHECK CONSTRAINT [FK_PIDF_IPD_Region_PIDF_IPD]
GO
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_ManagementApprovalStatusHistory_Master_PIDFStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Master_PIDFStatus] ([PIDFStatusID])
GO
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] CHECK CONSTRAINT [FK_PIDF_ManagementApprovalStatusHistory_Master_PIDFStatus]
GO
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_ManagementApprovalStatusHistory_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_ManagementApprovalStatusHistory] CHECK CONSTRAINT [FK_PIDF_ManagementApprovalStatusHistory_PIDF]
GO
ALTER TABLE [dbo].[PIDF_Medical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Medical_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_Medical] CHECK CONSTRAINT [FK_PIDF_Medical_PIDF]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_BERequirement] FOREIGN KEY([BERequirementId])
REFERENCES [dbo].[Master_BERequirement] ([BERequirementId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_BERequirement]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_Dosage1] FOREIGN KEY([DosageId])
REFERENCES [dbo].[Master_Dosage] ([DosageId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_Dosage1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_FilingType1] FOREIGN KEY([FillingTypeId])
REFERENCES [dbo].[Master_FilingType] ([FilingTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_FilingType1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_FormRnDDivision] FOREIGN KEY([FormRnDDivisionId])
REFERENCES [dbo].[Master_FormRnDDivision] ([FormRnDDivisionId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_FormRnDDivision]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_Manufacturing1] FOREIGN KEY([ManufacturingId])
REFERENCES [dbo].[Master_Manufacturing] ([ManufacturingId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_Manufacturing1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_PackagingType1] FOREIGN KEY([PackagingTypeId])
REFERENCES [dbo].[Master_PackagingType] ([PackagingTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_PackagingType1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_Plant1] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Master_Plant] ([PlantId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_Plant1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_ProductType1] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[Master_ProductType] ([ProductTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_ProductType1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Master_Workflow1] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Master_Workflow] ([WorkflowId])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_Master_Workflow1]
GO
ALTER TABLE [dbo].[PIDF_PBF]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_PIDF1] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF] CHECK CONSTRAINT [FK_PIDF_PBF_PIDF1]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_Master_TestType] FOREIGN KEY([TestTypeId])
REFERENCES [dbo].[Master_TestType] ([TestTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_Master_TestType]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDF_PBF_Analytical_AMVCost] FOREIGN KEY([TotalAMVCostId])
REFERENCES [dbo].[PIDF_PBF_Analytical_AMVCost] ([TotalAMVCostId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDF_PBF_Analytical_AMVCost]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_AMVCost_StrengthMapping] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_AMVCost_StrengthMapping_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDF_PBF_Analytical_Cost] FOREIGN KEY([PBFAnalyticalCostId])
REFERENCES [dbo].[PIDF_PBF_Analytical_Cost] ([PBFAnalyticalCostId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDF_PBF_Analytical_Cost]
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Analytical_Cost_StrengthMapping] CHECK CONSTRAINT [FK_PIDF_PBF_Analytical_Cost_StrengthMapping_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_Clinical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Clinical_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Clinical] CHECK CONSTRAINT [FK_PIDF_PBF_Clinical_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_Clinical]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Clinical_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Clinical] CHECK CONSTRAINT [FK_PIDF_PBF_Clinical_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General] CHECK CONSTRAINT [FK_PIDF_PBF_General_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_PBF_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Master_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[Master_ProductType] ([ProductTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General] CHECK CONSTRAINT [FK_PIDF_PBF_General_Master_ProductType]
GO
ALTER TABLE [dbo].[PIDF_PBF_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Master_User] FOREIGN KEY([FormulationGLId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General] CHECK CONSTRAINT [FK_PIDF_PBF_General_Master_User]
GO
ALTER TABLE [dbo].[PIDF_PBF_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Master_User1] FOREIGN KEY([AnalyticalGLId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General] CHECK CONSTRAINT [FK_PIDF_PBF_General_Master_User1]
GO
ALTER TABLE [dbo].[PIDF_PBF_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_PIDF_PBF] FOREIGN KEY([PIDFPBFId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General] CHECK CONSTRAINT [FK_PIDF_PBF_General_PIDF_PBF]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF] FOREIGN KEY([PidfId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] CHECK CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF_PBF] FOREIGN KEY([PbfId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] CHECK CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF_PBF]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_Strength]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Strength_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_Strength] CHECK CONSTRAINT [FK_PIDF_PBF_General_Strength_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_Strength]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_Strength_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_Strength] CHECK CONSTRAINT [FK_PIDF_PBF_General_Strength_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_HeadWiseBudget]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_HeadWiseBudget_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_HeadWiseBudget] CHECK CONSTRAINT [FK_PIDF_PBF_HeadWiseBudget_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_MarketMapping_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping] CHECK CONSTRAINT [FK_PIDF_PBF_MarketMapping_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_MarketMapping_PIDF_PBF] FOREIGN KEY([PIDFPBFId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
ALTER TABLE [dbo].[PIDF_PBF_MarketMapping] CHECK CONSTRAINT [FK_PIDF_PBF_MarketMapping_PIDF_PBF]
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource] CHECK CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID]
GO
ALTER TABLE [dbo].[PIDF_PBF_PhaseWiseBudget]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_PhaseWiseBudget_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_PhaseWiseBudget] CHECK CONSTRAINT [FK_PIDF_PBF_PhaseWiseBudget_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RA]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RA_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_RA] CHECK CONSTRAINT [FK_PIDF_PBF_RA_PIDF]
GO
ALTER TABLE [dbo].[PIDF_PBF_RA]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RA_PIDF_PBF] FOREIGN KEY([PBFId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RA] CHECK CONSTRAINT [FK_PIDF_PBF_RA_PIDF_PBF]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_APIRequirement_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_APIRequirement_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_APIRequirement_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_APIRequirement] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_APIRequirement_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Rnd_BatchSize_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize] CHECK CONSTRAINT [FK_PIDF_PBF_Rnd_BatchSize_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Rnd_BatchSize_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_Rnd_BatchSize] CHECK CONSTRAINT [FK_PIDF_PBF_Rnd_BatchSize_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_CapexMiscellaneousExpenses] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_CapexMiscellaneousExpenses_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDF_PBF_General] FOREIGN KEY([PidfPbfGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientPrototype] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientPrototype_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_Exicipient_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_Exicipient_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_Exicipient_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientRequirement] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_Exicipient_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientScaleUp_PIDF_PBF_General] FOREIGN KEY([PidfPbfGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ExicipientScaleUp] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ExicipientScaleUp_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_Master_BusinessUnit]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_FillingExpenses] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_FillingExpenses_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ManPowerCost_Master_ProjectActivities] FOREIGN KEY([ProjectActivitiesId])
REFERENCES [dbo].[Master_ProjectActivities] ([ProjectActivitiesId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ManPowerCost_Master_ProjectActivities]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ManPowerCost_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ManPowerCost] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ManPowerCost_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_Master_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_Master] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_Master_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_Packaging_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_Packaging_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_Packaging_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_Packaging_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PackagingMaterial_Master_PackingType] FOREIGN KEY([PackingTypeId])
REFERENCES [dbo].[Master_PackingType] ([PackingTypeId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackagingMaterial] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PackagingMaterial_Master_PackingType]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]  WITH CHECK ADD  CONSTRAINT [FK__PIDF_PBF___Count__781FBE44] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] CHECK CONSTRAINT [FK__PIDF_PBF___Count__781FBE44]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PlantSupportCost_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PlantSupportCost_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PlantSupportCost_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_PlantSupportCost] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PlantSupportCost_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ReferenceProductDetail] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ReferenceProductDetail_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ToolingChangepart_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ToolingChangepart_PIDF_PBF_General]
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_ToolingChangepart_PIDFProductStrength] FOREIGN KEY([StrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDF_PBF_RnD_ToolingChangepart] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_ToolingChangepart_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDFAPIDetails]  WITH CHECK ADD  CONSTRAINT [FK_PIDFAPIDetails_Master_APISourcing] FOREIGN KEY([APISourcingId])
REFERENCES [dbo].[Master_APISourcing] ([APISourcingId])
GO
ALTER TABLE [dbo].[PIDFAPIDetails] CHECK CONSTRAINT [FK_PIDFAPIDetails_Master_APISourcing]
GO
ALTER TABLE [dbo].[PIDFAPIDetails]  WITH CHECK ADD  CONSTRAINT [FK_PIDFAPIDetails_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDFAPIDetails] CHECK CONSTRAINT [FK_PIDFAPIDetails_Master_BussinessUnit]
GO
ALTER TABLE [dbo].[PIDFAPIDetails]  WITH CHECK ADD  CONSTRAINT [FK_PIDFAPIDetails_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDFAPIDetails] CHECK CONSTRAINT [FK_PIDFAPIDetails_PIDF]
GO
ALTER TABLE [dbo].[PIDFIMSData]  WITH CHECK ADD  CONSTRAINT [FK_PIDFIMSData_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDFIMSData] CHECK CONSTRAINT [FK_PIDFIMSData_Master_BussinessUnit]
GO
ALTER TABLE [dbo].[PIDFIMSData]  WITH CHECK ADD  CONSTRAINT [FK_PIDFIMSData_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDFIMSData] CHECK CONSTRAINT [FK_PIDFIMSData_PIDF]
GO
ALTER TABLE [dbo].[PIDFProductStrength]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO
ALTER TABLE [dbo].[PIDFProductStrength] CHECK CONSTRAINT [FK_PIDFProductStrength_Master_BussinessUnit]
GO
ALTER TABLE [dbo].[PIDFProductStrength]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_Master_UnitofMeasurement] FOREIGN KEY([UnitofMeasurementId])
REFERENCES [dbo].[Master_UnitofMeasurement] ([UnitofMeasurementId])
GO
ALTER TABLE [dbo].[PIDFProductStrength] CHECK CONSTRAINT [FK_PIDFProductStrength_Master_UnitofMeasurement]
GO
ALTER TABLE [dbo].[PIDFProductStrength]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDFProductStrength] CHECK CONSTRAINT [FK_PIDFProductStrength_PIDF]
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_CountryMapping_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] CHECK CONSTRAINT [FK_PIDFProductStrength_CountryMapping_Master_Country]
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength] FOREIGN KEY([PIDFProductStrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] CHECK CONSTRAINT [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength]
GO
ALTER TABLE [dbo].[PIDFStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_PIDFStatusHistory_Master_PIDFStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Master_PIDFStatus] ([PIDFStatusID])
GO
ALTER TABLE [dbo].[PIDFStatusHistory] CHECK CONSTRAINT [FK_PIDFStatusHistory_Master_PIDFStatus]
GO
ALTER TABLE [dbo].[PIDFStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_PIDFStatusHistory_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDFStatusHistory] CHECK CONSTRAINT [FK_PIDFStatusHistory_PIDF]
GO
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_Master_Project_Priority] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Master_Project_Priority] ([PriorityId])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_Master_Project_Priority]
GO
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_Master_Project_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Master_Project_Status] ([StatusId])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_Master_Project_Status]
GO
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_Master_User] FOREIGN KEY([TaskOwnerId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_Master_User]
GO
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_PIDF]
GO
ALTER TABLE [dbo].[Tbl_SessionManager]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_SessionManager_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Tbl_SessionManager] CHECK CONSTRAINT [FK_Tbl_SessionManager_Master_User]
GO
ALTER TABLE [dbo].[UserSessionLogMaster]  WITH CHECK ADD  CONSTRAINT [FK_UserSessionLogMaster_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[UserSessionLogMaster] CHECK CONSTRAINT [FK_UserSessionLogMaster_Master_User]
GO
