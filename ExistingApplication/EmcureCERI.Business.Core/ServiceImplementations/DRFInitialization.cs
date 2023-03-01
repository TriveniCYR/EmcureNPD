
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Data.Repository;
using Microsoft.AspNetCore.Http; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFInitialization : IDRFInitialization
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IEntityBaseRepository<Tbl_DRF_PIDF_Mapping> _DRFPRDF;
        public DRFInitialization(IEntityBaseRepository<Tbl_DRF_PIDF_Mapping> drfPIDF)
        {
            _db = new EmcureCERIDBContext();
            _DRFPRDF = drfPIDF;
        }

        public int deleteDRFInitialization(Tbl_DRF_Initialization entity)
        {
            try
            {
                _db.LoadStoredProc("USP_DeleteDRFInitializationDetails")
               .WithSqlParam("@InitializationID", entity.InitializationID)
               .WithSqlParam("@ModifiedBy", entity.Modifiedby)
                .WithSqlParam("@ModifiedDate", entity.ModifiedDate)
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void DRFPIDFMapping(Tbl_DRF_PIDF_Mapping entity)
        {
            _DRFPRDF.Add(entity);
            _DRFPRDF.Commit();
        }

        public IList<Tbl_DRF_Initialization> GetDRFInitializationLists(int Userid,int CompanyID)
        {
            IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFInitializationList")
                    .WithSqlParam("@UserID", Userid)
                    .WithSqlParam("@CompanyID", CompanyID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_DRF_Initialization>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_DRF_Initialization> GetDRFInitializationSingleRecord(int InitializationID)
        {
            IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFInitializationForEdit")
                     .WithSqlParam("@InitializationID", InitializationID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_DRF_Initialization>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFInitialization(Tbl_DRF_Initialization entity)
        {
            try
            {

                SqlParameter tblParameter1 = new SqlParameter("InitializationID", SqlDbType.Int);
                tblParameter1.Direction = ParameterDirection.Output;

                _db.LoadStoredProc("USP_InsertDRFInitializationDetails")
                .WithSqlParam("@DRFNo", entity.DRFNo)
                .WithSqlParam("@CompanyID", entity.CompanyID)
                .WithSqlParam("@CountryID", entity.CountryID)
                .WithSqlParam("@GenericName", entity.GenericName)
                .WithSqlParam("@BrandName", entity.BrandName)
                .WithSqlParam("@TreadmarkApprovedInternal", entity.TreadmarkApprovedInternal)
                .WithSqlParam("@TreadmarkSuggestedInternal", entity.TreadmarkSuggestedInternal)
                .WithSqlParam("@TreadmarkOwnerInternal", entity.TreadmarkOwnerInternal)
                .WithSqlParam("@Form", entity.Form)
                .WithSqlParam("@Strength", entity.Strength)
                .WithSqlParam("@PackSize", entity.PackSize)
                .WithSqlParam("@PackStyle", entity.PackStyle)
                .WithSqlParam("@Plant", entity.Plant)
                .WithSqlParam("@ProductTypeId", entity.ProductTypeId)
                .WithSqlParam("@DossierTemplate", entity.DossierTemplateID)
                .WithSqlParam("@CurrencyID", entity.CurrencyID)
                .WithSqlParam("@RegistrationFees", entity.RegistrationFees)
                .WithSqlParam("@FeesToBePaidByID", entity.FeesToBePaidByID)
                .WithSqlParam("@FeesToBePaidBy", entity.FeesToBePaidBy)
                .WithSqlParam("@ModeOfFeesPayment", entity.ModeOfFeesPayment)
                .WithSqlParam("@MAHolderID", entity.MAHolder)
                .WithSqlParam("@ProposedMarketingStatusID", entity.ProposedMarketingStatusID)
                .WithSqlParam("@ShippingPort", entity.ShippingPort)
                .WithSqlParam("@ModeOfShipment", entity.ModeOfShipment)
                .WithSqlParam("@Incoterms", entity.Incoterms)
                .WithSqlParam("@DossierSubmittedToMOHBy", entity.DossierSubmittedToMOHBy)
                .WithSqlParam("@OwnerOfRegistration", entity.OwnerOfRegistration)
                .WithSqlParam("@AvailabilityofCDA", entity.AvailabilityofCDA)
                .WithSqlParam("@MarketSize", entity.MarketSize)
                .WithSqlParam("@ThreeYearCAGR", entity.ThreeYearCAGR)
                .WithSqlParam("@NumberOfCurrentPlayer", entity.NumberOfCurrentPlayer)
                .WithSqlParam("@InnovatorBrand", entity.InnovatorBrand)
                .WithSqlParam("@FirstBrand", entity.FirstBrand)
                .WithSqlParam("@SecondBrand", entity.SecondBrand)
                .WithSqlParam("@ThirdBrand", entity.ThirdBrand)
                .WithSqlParam("@ExpectedMarketValueGrowth", entity.ExpectedMarketValueGrowth)
                .WithSqlParam("@InnavotorName", entity.InnavotorName)
                .WithSqlParam("@MSFirstBrand", entity.MSFirstBrand)
                .WithSqlParam("@MSSecondBrand", entity.MSSecondBrand)
                .WithSqlParam("@MSThirdBrand", entity.MSThirdBrand)
                .WithSqlParam("@Partner", entity.Partner)
                .WithSqlParam("@FirstYearForecastUnitsPacks", entity.FirstYearForecastUnitsPacks)
                .WithSqlParam("@SecondYearForecastUnitsPacks", entity.SecondYearForecastUnitsPacks)
                .WithSqlParam("@ThirdYearForecastUnitsPacks", entity.ThirdYearForecastUnitsPacks)
                .WithSqlParam("@FirstYearForecastPriceToPatient", entity.FirstYearForecastPriceToPatient)
                .WithSqlParam("@SecondYearForecastPriceToPatient", entity.SecondYearForecastPriceToPatient)
                .WithSqlParam("@ThirdYearForecastPriceToPatient", entity.ThirdYearForecastPriceToPatient)
                .WithSqlParam("@FirstYearAPIQuantity", entity.FirstYearAPIQuantity)
                .WithSqlParam("@SecondYearAPIQuantity", entity.SecondYearAPIQuantity)
                .WithSqlParam("@ThirdYearAPIQuantity", entity.ThirdYearAPIQuantity)
                .WithSqlParam("@FirstYearForecastCIF", entity.FirstYearForecastCIF)
                .WithSqlParam("@SecondYearForecastCIF", entity.SecondYearForecastCIF)
                .WithSqlParam("@ThirdYearForecastCIF", entity.ThirdYearForecastCIF)
                .WithSqlParam("@FirstYearForecastValue", entity.FirstYearForecastValue)
                .WithSqlParam("@SecondYearForecastValue", entity.SecondYearForecastValue)
                .WithSqlParam("@ThirdYearForecastValue", entity.ThirdYearForecastValue)
                .WithSqlParam("@OrderFrequency", entity.OrderFrequencyID)
                .WithSqlParam("@NameDossierSend", entity.NameDossierSend)
                .WithSqlParam("@AddressDossierSend", entity.AddressDossierSend)
                .WithSqlParam("@StrategyAlignment", entity.StrategyAlignment)
                .WithSqlParam("@ExceptionExplained", entity.ExceptionExplained)
                //.WithSqlParam("@IsActive", entity.IsActive)
                .WithSqlParam("@Createdby", entity.Createdby)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@EmailDossierSend", entity.EmailDossierSend)
                .WithSqlParam("@PhoneDossierSend", entity.PhoneDossierSend)
                .WithSqlParam("@TSExcecuted", entity.TSExcecuted)
                .WithSqlParam("@DAExcecuted", entity.DAExcecuted)
                .WithSqlParam("@IsSamples_Required", entity.IsSamples_Required)
                .WithSqlParam("@Samples_Required", entity.Samples_Required)
                .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@NoofShipmnets", entity.NoofShipmnets)
                .WithSqlParam("@InitializationID", tblParameter1)
                .ExecuteStoredNonQuery();

                int InitializationID = Convert.ToInt32(tblParameter1.Value);

                return InitializationID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateDRFInitialApproval(Tbl_DRF_Initialization entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFInitialApprovel")
               .WithSqlParam("@InitializationID", entity.InitializationID)
               .WithSqlParam("@StatusID", entity.StatusID)
               .WithSqlParam("@Status", entity.Status)
               .WithSqlParam("@Comment", entity.InitialApproveRejectComment)
               .WithSqlParam("@ModifiedBy", entity.Modifiedby)
                .WithSqlParam("@ModifiedDate", entity.ModifiedDate)
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateDRFInitialization(Tbl_DRF_Initialization entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFInitializationDetails")
                .WithSqlParam("@InitializationID", entity.InitializationID)
                .WithSqlParam("@CompanyID", entity.CompanyID)
                .WithSqlParam("@CountryID", entity.CountryID)
                .WithSqlParam("@GenericName", entity.GenericName)
                .WithSqlParam("@BrandName", entity.BrandName)
                .WithSqlParam("@TreadmarkApprovedInternal", entity.TreadmarkApprovedInternal)
                .WithSqlParam("@TreadmarkSuggestedInternal", entity.TreadmarkSuggestedInternal)
                .WithSqlParam("@TreadmarkOwnerInternal", entity.TreadmarkOwnerInternal)
                .WithSqlParam("@Form", entity.Form)
                .WithSqlParam("@Strength", entity.Strength)
                .WithSqlParam("@PackSize", entity.PackSize)
                .WithSqlParam("@PackStyle", entity.PackStyle)
                .WithSqlParam("@Plant", entity.Plant)
                .WithSqlParam("@RegistrationFees", entity.RegistrationFees)
                .WithSqlParam("@FeesToBePaidByID", entity.FeesToBePaidByID)
                .WithSqlParam("@FeesToBePaidBy", entity.FeesToBePaidBy)
                .WithSqlParam("@ModeOfFeesPayment", entity.ModeOfFeesPayment)
                .WithSqlParam("@MAHolder", entity.MAHolder)
                .WithSqlParam("@ProposedMarketingStatusID", entity.ProposedMarketingStatusID)
                .WithSqlParam("@ShippingPort", entity.ShippingPort)
                .WithSqlParam("@ModeOfShipment", entity.ModeOfShipment)
                .WithSqlParam("@Incoterms", entity.Incoterms)
                .WithSqlParam("@DossierSubmittedToMOHBy", entity.DossierSubmittedToMOHBy)
                .WithSqlParam("@OwnerOfRegistration", entity.OwnerOfRegistration)
                .WithSqlParam("@AvailabilityofCDA", entity.AvailabilityofCDA)
                .WithSqlParam("@TSExcecuted", entity.TSExcecuted)
                .WithSqlParam("@DAExcecuted", entity.DAExcecuted)
                .WithSqlParam("@MarketSize", entity.MarketSize)
                .WithSqlParam("@ThreeYearCAGR", entity.ThreeYearCAGR)
                .WithSqlParam("@NumberOfCurrentPlayer", entity.NumberOfCurrentPlayer)
                .WithSqlParam("@InnovatorBrand", entity.InnovatorBrand)
                .WithSqlParam("@FirstBrand", entity.FirstBrand)
                .WithSqlParam("@SecondBrand", entity.SecondBrand)
                .WithSqlParam("@ThirdBrand", entity.ThirdBrand)
                .WithSqlParam("@ExpectedMarketValueGrowth", entity.ExpectedMarketValueGrowth)
                .WithSqlParam("@InnavotorName", entity.InnavotorName)
                .WithSqlParam("@MSFirstBrand", entity.MSFirstBrand)
                .WithSqlParam("@MSSecondBrand", entity.MSSecondBrand)
                .WithSqlParam("@MSThirdBrand", entity.MSThirdBrand)
                .WithSqlParam("@Partner", entity.Partner)
                .WithSqlParam("@FirstYearForecastUnitsPacks", entity.FirstYearForecastUnitsPacks)
                .WithSqlParam("@SecondYearForecastUnitsPacks", entity.SecondYearForecastUnitsPacks)
                .WithSqlParam("@ThirdYearForecastUnitsPacks", entity.ThirdYearForecastUnitsPacks)
                .WithSqlParam("@FirstYearForecastPriceToPatient", entity.FirstYearForecastPriceToPatient)
                .WithSqlParam("@SecondYearForecastPriceToPatient", entity.SecondYearForecastPriceToPatient)
                .WithSqlParam("@ThirdYearForecastPriceToPatient", entity.ThirdYearForecastPriceToPatient)
                 .WithSqlParam("@FirstYearAPIQuantity", entity.FirstYearAPIQuantity)
                .WithSqlParam("@SecondYearAPIQuantity", entity.SecondYearAPIQuantity)
                .WithSqlParam("@ThirdYearAPIQuantity", entity.ThirdYearAPIQuantity)
                .WithSqlParam("@FirstYearForecastCIF", entity.FirstYearForecastCIF)
                .WithSqlParam("@SecondYearForecastCIF", entity.SecondYearForecastCIF)
                .WithSqlParam("@ThirdYearForecastCIF", entity.ThirdYearForecastCIF)
                .WithSqlParam("@FirstYearForecastValue", entity.FirstYearForecastValue)
                .WithSqlParam("@SecondYearForecastValue", entity.SecondYearForecastValue)
                .WithSqlParam("@ThirdYearForecastValue", entity.ThirdYearForecastValue)
                .WithSqlParam("@OrderFrequency", entity.OrderFrequencyID)
                .WithSqlParam("@NameDossierSend", entity.NameDossierSend)
                .WithSqlParam("@AddressDossierSend", entity.AddressDossierSend)
                .WithSqlParam("@EmailDossierSend", entity.EmailDossierSend)
                .WithSqlParam("@PhoneDossierSend", entity.PhoneDossierSend)
                .WithSqlParam("@StrategyAlignment", entity.StrategyAlignment)
                .WithSqlParam("@ExceptionExplained", entity.ExceptionExplained)
                .WithSqlParam("@IsSamples_Required", entity.IsSamples_Required)
                .WithSqlParam("@Samples_Required", entity.Samples_Required)
                .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@NoofShipmnets", entity.NoofShipmnets)
                .WithSqlParam("@UpdateRemark", entity.UpdateRemark)
               // .WithSqlParam("@IsActive", entity.IsActive)
               //.WithSqlParam("@Createdby", entity.Createdby)
               //.WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@ModifiedBy", entity.Modifiedby)
                .WithSqlParam("@ModifiedDate", entity.ModifiedDate)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<PIDFDetailsNew> GetAttachedPIDFList(int DRFID, int CountryID)
        {
            IList<PIDFDetailsNew> result = new List<PIDFDetailsNew>();
            try
            {
                _db.LoadStoredProc("USP_GetInitializationDRFAttachedPIDFDetails")
                    .WithSqlParam("@DRFId", DRFID)
                    .WithSqlParam("@CountryId", CountryID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<PIDFDetailsNew>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_DRF_Initialization> GetFinalApprovedProjectList()
        {
            IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();
            try
            {
                _db.LoadStoredProc("USP_GetFinalApprovedProjectList")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_DRF_Initialization>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public object GetFinalApprovedDRFList(int Userid,int CompanyID)
        {
            IList<ApprovedDRFList> result = new List<ApprovedDRFList>();

            _db.LoadStoredProc("USP_GetFinalApprovedProjectList")
               .WithSqlParam("@UserID", Userid)
               .WithSqlParam("@CompanyID", CompanyID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<ApprovedDRFList>();
            });

            List<Object> result1 = (from x in result
                                    select (Object)new
                                    {
                                        InitializationID = x.InitializationID,
                                        DRFNo = "<a href = '/DRFInitialization/ProjectShowDetails?Id=" + x.InitializationID + "' data-toggle='tooltip' title='Dossier Details' >" + x.DRFNo + "</a>",
                                        ProjectName = x.ProjectName,
                                        CountryName = x.CountryName,
                                       // GenericName = x.GenericName,
                                        GenericName = "<span style='width: 100px !important;display: inline-block;white-space: normal;'>" + x.GenericName + "</span>",
                                        BrandName = x.BrandName,
                                        OrderFrequency = x.OrderFrequency,
                                        StrengthName = x.StrengthName,
                                        PackSizeName = x.PackSizeName,
                                        PackStyleName = x.PackStyleName,
                                        FormName = x.FormName,
                                        PlantName = x.PlantName,
                                        IncotermsName = x.IncotermsName,
                                        MaHolder = x.MAHolder,
                                        NameDossierSend = x.NameDossierSend,
                                        ProjectManager = "Binnu Singh",
                                        DossierTemplate = x.DossierTemplate,
                                        Status = x.Status,
                                        Final_Approved_Date=x.Final_Approved_Date

                                    }).ToList();

            return result1;
        }

        public int UpdateDRFApprovals(Tbl_DRF_FormApprovals entity,string UserRole, Int32 CurrentStatusID)
        {
            try
            {
                if(UserRole.Contains("Country Manager"))
                {
                    _db.LoadStoredProc("USP_UpdateDRFApprovel")
              .WithSqlParam("@InitializationID", entity.InitializationID)
              .WithSqlParam("@UserRole", UserRole)
              .WithSqlParam("@Approval", entity.CountryManagerApproval)
              .WithSqlParam("@CreatedBy", entity.CMCreatedBy)
              .WithSqlParam("@CreatedDate", entity.CMCreatedDate)
              .WithSqlParam("@Comment", entity.Comment)
                .ExecuteStoredNonQuery();
                }
                else if (UserRole == "Line Manager")
                {
                    _db.LoadStoredProc("USP_UpdateDRFApprovel")
              .WithSqlParam("@InitializationID", entity.InitializationID)
              .WithSqlParam("@UserRole", UserRole)
              .WithSqlParam("@Approval", entity.LineManagerApproval)
              .WithSqlParam("@CreatedBy", entity.LMCreatedBy)
              .WithSqlParam("@CreatedDate", entity.LMCreatedDate)
               .WithSqlParam("@Comment", entity.Comment)
                .ExecuteStoredNonQuery();
                }
                else if (UserRole == "HOD Of Dossier")
                {
                    _db.LoadStoredProc("USP_UpdateDRFApprovel")
              .WithSqlParam("@InitializationID", entity.InitializationID)
              .WithSqlParam("@UserRole", UserRole)
              .WithSqlParam("@Approval", entity.HODofDossierApproval)
              .WithSqlParam("@CreatedBy", entity.HODCreatedBy)
              .WithSqlParam("@CreatedDate", entity.HODCreatedDate)
               .WithSqlParam("@Comment", entity.Comment)
                .ExecuteStoredNonQuery();
                }
                else
                {
                    //prescriber(admin)

                    if(CurrentStatusID==8)
                    {
                        _db.LoadStoredProc("USP_UpdateDRFApprovel")
                      .WithSqlParam("@InitializationID", entity.InitializationID)
                      .WithSqlParam("@UserRole", UserRole)
                      .WithSqlParam("@Approval", entity.CountryManagerApproval)
                      .WithSqlParam("@CreatedBy", entity.CMCreatedBy)
                      .WithSqlParam("@CreatedDate", entity.CMCreatedDate)
                       .WithSqlParam("@Comment", entity.Comment)
                        .ExecuteStoredNonQuery();
                    }
                    else if(CurrentStatusID == 22 || CurrentStatusID == 28)
                    {
                        _db.LoadStoredProc("USP_UpdateDRFApprovel")
                     .WithSqlParam("@InitializationID", entity.InitializationID)
                     .WithSqlParam("@UserRole", UserRole)
                     .WithSqlParam("@Approval", entity.LineManagerApproval)
                     .WithSqlParam("@CreatedBy", entity.LMCreatedBy)
                     .WithSqlParam("@CreatedDate", entity.LMCreatedDate)
                     .WithSqlParam("@Comment", entity.Comment)
                       .ExecuteStoredNonQuery();
                    }
                    else if (CurrentStatusID == 23 || CurrentStatusID == 29)
                    {
                        _db.LoadStoredProc("USP_UpdateDRFApprovel")
                     .WithSqlParam("@InitializationID", entity.InitializationID)
                     .WithSqlParam("@UserRole", UserRole)
                     .WithSqlParam("@Approval", entity.HODofDossierApproval)
                     .WithSqlParam("@CreatedBy", entity.HODCreatedBy)
                     .WithSqlParam("@CreatedDate", entity.HODCreatedDate)
                     .WithSqlParam("@Comment", entity.Comment)
                       .ExecuteStoredNonQuery();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<ProductMasterDropdownData> GetAllProductMasterNameList()
        {
            IList<ProductMasterDropdownData> result = new List<ProductMasterDropdownData>();
            try
            {
                _db.LoadStoredProc("USP_Get_All_ProductMaster_Details")
                    .WithSqlParam("@Action", "genericname")
                    .WithSqlParam("@GenericName", "genericname")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<ProductMasterDropdownData>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<DropdownDetails> GetAllDetailsByProductMasterName(string DropdownType ,string GenericName)
        {
            IList<DropdownDetails> result = new List<DropdownDetails>();
            try
            {
                _db.LoadStoredProc("USP_Get_All_ProductMaster_Details")
                    .WithSqlParam("@Action", DropdownType)
                    .WithSqlParam("@GenericName", GenericName)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<DropdownDetails>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<DRFInitializationDraftModel> GetAllDraftInitializationDataByUserID(int UserID)
        {
            IList<DRFInitializationDraftModel> result = new List<DRFInitializationDraftModel>();
            try
            {
                _db.LoadStoredProc("USP_GetAllDraftInitializationDataByUserID")
                    .WithSqlParam("@UserID", UserID)
                    .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<DRFInitializationDraftModel>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public DRFInitializationDraftModel GetDraftInitializationDataByUserID_DraftID(int UserID, int DraftID)
        {
            IList<DRFInitializationDraftModel> result = new List<DRFInitializationDraftModel>();
            try
            {
                _db.LoadStoredProc("USP_GetDraftInitializationDataByUserID_DraftID")
                    .WithSqlParam("@UserID", UserID)
                    .WithSqlParam("@DraftID", DraftID)
                    .ExecuteStoredProc((handler) =>
                    {
                        result = handler.ReadToList<DRFInitializationDraftModel>();
                    });
                if(result.Count >0 )
                {
                    return result[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteDraftInitializationDataByUserID_DraftID(int UserID, int DraftID)
        {
            IList<DRFInitializationDraftModel> result = new List<DRFInitializationDraftModel>();
            try
            {
                _db.LoadStoredProc("USP_DeleteDraftInitializationDataByUserID_DraftID")
                    .WithSqlParam("@UserID", UserID)
                    .WithSqlParam("@DraftID", DraftID)
                    .ExecuteStoredNonQuery();              
               
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertUpdateDraft_InitializationData(DRFInitializationDraftModel entity)
        {
            try
            {
                _db.LoadStoredProc("USP_Insert_Draft_InitializationData")
                .WithSqlParam("@DraftID", entity.DraftID)
                .WithSqlParam("@CompanyID", entity.CompanyID)
                .WithSqlParam("@CountryID", entity.CountryID)
                .WithSqlParam("@CountryName", entity.CountryName)
                .WithSqlParam("@GenericName", entity.GenericName)
                .WithSqlParam("@BrandName", entity.BrandName)
                .WithSqlParam("@TreadmarkApprovedInternal", entity.TreadmarkApprovedInternal)
                .WithSqlParam("@TreadmarkSuggestedInternal", entity.TreadmarkSuggestedInternal)
                .WithSqlParam("@TreadmarkOwnerInternal", entity.TreadmarkOwnerInternal)
                .WithSqlParam("@Form", entity.Form)
                .WithSqlParam("@FormulationName", entity.FormulationName)
                .WithSqlParam("@Strength", entity.Strength)
                .WithSqlParam("@StrengthName", entity.StrengthName)
                .WithSqlParam("@PackSize", entity.PackSize)
                .WithSqlParam("@PackSizeName", entity.PackSizeName)
                .WithSqlParam("@PackStyle", entity.PackStyle)
                .WithSqlParam("@PackStyleName", entity.PackStyleName)
                .WithSqlParam("@Plant", entity.Plant)
                .WithSqlParam("@ProductTypeId", entity.ProductTypeId)                
                .WithSqlParam("@CurrencyID", entity.CurrencyID)
                .WithSqlParam("@RegistrationFees", entity.RegistrationFees)
                .WithSqlParam("@FeesToBePaidByID", entity.FeesToBePaidByID)
                .WithSqlParam("@FeesToBePaidBy", entity.FeesToBePaidBy)
                .WithSqlParam("@ModeOfFeesPayment", entity.ModeOfFeesPayment)
                .WithSqlParam("@MAHolder", entity.MAHolder)
                .WithSqlParam("@ProposedMarketingStatusID", entity.ProposedMarketingStatusID)
                .WithSqlParam("@ShippingPort", entity.ShippingPort)
                .WithSqlParam("@ModeOfShipment", entity.ModeOfShipment)
                .WithSqlParam("@Incoterms", entity.Incoterms)
                .WithSqlParam("@DossierSubmittedToMOHBy", entity.DossierSubmittedToMOHBy)                
                .WithSqlParam("@AvailabilityofCDA", entity.AvailabilityofCDA)
                .WithSqlParam("@TSExcecuted", entity.TSExcecuted)
                .WithSqlParam("@DAExcecuted", entity.DAExcecuted)
                .WithSqlParam("@MarketSize", entity.MarketSize)
                .WithSqlParam("@ThreeYearCAGR", entity.ThreeYearCAGR)
                .WithSqlParam("@NumberOfCurrentPlayer", entity.NumberOfCurrentPlayer)
                .WithSqlParam("@InnovatorBrand", entity.InnovatorBrand)
                .WithSqlParam("@FirstBrand", entity.FirstBrand)
                .WithSqlParam("@SecondBrand", entity.SecondBrand)
                .WithSqlParam("@ThirdBrand", entity.ThirdBrand)
                .WithSqlParam("@ExpectedMarketValueGrowth", entity.ExpectedMarketValueGrowth)
                .WithSqlParam("@InnavotorName", entity.InnavotorName)
                .WithSqlParam("@MSFirstBrand", entity.MSFirstBrand)
                .WithSqlParam("@MSSecondBrand", entity.MSSecondBrand)
                .WithSqlParam("@MSThirdBrand", entity.MSThirdBrand)
                .WithSqlParam("@Partner", entity.Partner)
                .WithSqlParam("@FirstYearForecastUnitsPacks", entity.FirstYearForecastUnitsPacks)
                .WithSqlParam("@SecondYearForecastUnitsPacks", entity.SecondYearForecastUnitsPacks)
                .WithSqlParam("@ThirdYearForecastUnitsPacks", entity.ThirdYearForecastUnitsPacks)
                .WithSqlParam("@FirstYearForecastPriceToPatient", entity.FirstYearForecastPriceToPatient)
                .WithSqlParam("@SecondYearForecastPriceToPatient", entity.SecondYearForecastPriceToPatient)
                .WithSqlParam("@ThirdYearForecastPriceToPatient", entity.ThirdYearForecastPriceToPatient)
                .WithSqlParam("@FirstYearAPIQuantity", entity.FirstYearAPIQuantity)
                .WithSqlParam("@SecondYearAPIQuantity", entity.SecondYearAPIQuantity)
                .WithSqlParam("@ThirdYearAPIQuantity", entity.ThirdYearAPIQuantity)
                .WithSqlParam("@FirstYearForecastCIF", entity.FirstYearForecastCIF)
                .WithSqlParam("@SecondYearForecastCIF", entity.SecondYearForecastCIF)
                .WithSqlParam("@ThirdYearForecastCIF", entity.ThirdYearForecastCIF)
                .WithSqlParam("@FirstYearForecastValue", entity.FirstYearForecastValue)
                .WithSqlParam("@SecondYearForecastValue", entity.SecondYearForecastValue)
                .WithSqlParam("@ThirdYearForecastValue", entity.ThirdYearForecastValue)
                .WithSqlParam("@OrderFrequencyID", entity.OrderFrequencyID)
                .WithSqlParam("@NameDossierSend", entity.NameDossierSend)
                .WithSqlParam("@AddressDossierSend", entity.AddressDossierSend)
                .WithSqlParam("@EmailDossierSend", entity.EmailDossierSend)
                .WithSqlParam("@PhoneDossierSend", entity.PhoneDossierSend)
                .WithSqlParam("@StrategyAlignment", entity.StrategyAlignment)
                .WithSqlParam("@ExceptionExplained", entity.ExceptionExplained)
                .WithSqlParam("@IsSamples_Required", entity.IsSamples_Required)
                .WithSqlParam("@Samples_Required", entity.Samples_Required)
                .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@NoofShipmnets", entity.NoofShipmnets)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)                                       
                .ExecuteStoredNonQuery();              

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DRFInitializationDraftModel Check_Exists_DraftInitializationData(int? CountryID, string GenericName, int? FormulationID, int? StrengthID, int? PackSizeID, int? PackStyleID, int? PlantID, int? ProductTypeID)
        {
            IList<DRFInitializationDraftModel> result = new List<DRFInitializationDraftModel>();
            try
            {
                _db.LoadStoredProc("USP_Check_Exists_DraftInitializationData")
                    .WithSqlParam("@CountryID", CountryID)
                    .WithSqlParam("@GenericName", GenericName)
                    .WithSqlParam("@Form", FormulationID)
                    .WithSqlParam("@Strenth", StrengthID)
                    .WithSqlParam("@PackSize", PackSizeID)
                    .WithSqlParam("@PackStyle", PackStyleID)
                    .WithSqlParam("@Plant", PlantID)
                    .WithSqlParam("@ProductTypeID", ProductTypeID)
                    .ExecuteStoredProc((handler) =>
                    {
                        result = handler.ReadToList<DRFInitializationDraftModel>();
                    });
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    
}
