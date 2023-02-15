//using Microsoft.Data.SqlClient;

using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;








namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class PIDFServiceNew : IPidfServiceNew
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public PIDFServiceNew(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public int AddPIDFDetails(PIDFHeaderAndDetails entity)
        {
            try
            {
                IList<Tbl_PIDF_CountryDetails> tbl_PIDF_CountryDetails = entity.objPIDF_CountryDetails;
                DataTable tblDetails = new DataTable();
                tblDetails.Columns.Add(new DataColumn("RowID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfDetailID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfNo", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("ContinentID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("CountryID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PatentStatusID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PackingID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("EBatchSize", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("PackSize", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("COGS", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("Freight", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("TotalCIFCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerUnit", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerPack", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ProfitPerPack", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("PercentCont", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriOne", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriTwo", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriThree", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS1", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS2", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS3", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContributionThreeYear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CostofThreeBatches", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RandDCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("FilingCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("StabilityCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("TotalInvest", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ROI", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RejectionReason", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("AnalyticalCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("BECost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RLDCost", typeof(decimal)));                

                if (tbl_PIDF_CountryDetails.Count > 0)
                {
                    int intRowID = 1;
                    foreach (var ddata in tbl_PIDF_CountryDetails)
                    {
                        tblDetails.Rows.Add(intRowID, ddata.PidfDetailID, ddata.PidfID, ddata.PidfNo, ddata.ContinentID, ddata.CountryID, ddata.PatentStatusID, ddata.PackingID
                                            ,ddata.BatchSize,ddata.PackSize,ddata.COGS,ddata.Freight,ddata.TotalCIFCost
                                            ,ddata.CIFPricePerUnit,ddata.CIFPricePerPack,ddata.ProfitPerPack,ddata.PercentCont
                                            ,ddata.QtyOneyear,ddata.QtyTwoyear,ddata.QtyThreeyear,ddata.VolOneyear
                                            ,ddata.VolTwoyear,ddata.VolThreeyear,ddata.ContriOne,ddata.ContriTwo,ddata.ContriThree
                                            ,ddata.COGS1,ddata.COGS2,ddata.COGS3,ddata.ContributionThreeYear,ddata.CostofThreeBatches
                                            ,ddata.RandDCost,ddata.FilingCost,ddata.StabilityCost,ddata.TotalInvest,ddata.ROI
                                            ,ddata.RejectionReason,ddata.AnalyticalCost,ddata.BECost,ddata.RLDCost);
                        intRowID++;
                    }
                }
                SqlParameter tblParameter = new SqlParameter("pIDFCountryDetails", SqlDbType.Structured);
                tblParameter.TypeName = "dbo.PIDFCountryDetailsList";
                tblParameter.Value = tblDetails;
                //Add pidfdetails
                var testoutput = _db.LoadStoredProc("USP_TblPidfHeader_Insert")
                .WithSqlParam("@pIDFCountryDetails", tblParameter)

                .WithSqlParam("@pIDFNo", "p1")
                .WithSqlParam("@pidfDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                .WithSqlParam("@projectorProductID", entity.ProjectorProductID)
                .WithSqlParam("@projectorProductName", entity.ProjectorProductName)
                .WithSqlParam("@strengths", entity.Strengths)
                .WithSqlParam("@pidfStatusID", entity.PidfStatusID)
                .WithSqlParam("@pidfStatus", entity.PidfStatus)
                .WithSqlParam("@approvedById1", entity.ApprovedById1)
                .WithSqlParam("@approvedDate1", entity.ApprovedByDate1)
                .WithSqlParam("@approvedById2", entity.ApprovedById2)
                .WithSqlParam("@approvedByDate2", entity.ApprovedByDate2)
                .WithSqlParam("@approvedById3", entity.ApprovedById3)
                .WithSqlParam("@approvedByDate3", entity.ApprovedByDate3)
                .WithSqlParam("@approvedById4", entity.ApprovedById4)
                .WithSqlParam("@approvedByDate4", entity.ApprovedByDate4)
                .WithSqlParam("@pidfLastStatusID", entity.PidfStatusID)
                .WithSqlParam("@pidfLastStatus", entity.PidfStatus)
                .WithSqlParam("@isActive", true)                
                .WithSqlParam("@createdby", entity.Createdby)
                .WithSqlParam("@createdDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                .WithSqlParam("@modifiedby", entity.Modifiedby)
                .WithSqlParam("@modifiedDate",System.DateTime.Now.ToString("yyyy-MM-dd"))
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<Tbl_PIDF_Header> GetAllApprovalPIDF(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels)
        {
            IList<Tbl_PIDF_Header> result = new List<Tbl_PIDF_Header>();
            try
            {
                _db.LoadStoredProc("USP_GetPIDF_List")
                    .WithSqlParam("@PidfID", getPIDFApprovalListRequestModels.PidfID)
                    .WithSqlParam("@userID", getPIDFApprovalListRequestModels.userID)
                    .WithSqlParam("@StatusId", getPIDFApprovalListRequestModels.StatusId)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_PIDF_Header>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<PidfCountryDetailsNewModel> GetAllDetailedApprovalPIDF(GetPIDFApprovalListRequestModels getPIDFApprovalListRequestModels)
        {
            IList<PidfCountryDetailsNewModel> result = new List<PidfCountryDetailsNewModel>();
            try
            {
                _db.LoadStoredProc("USP_GetPIDFDetails_List")
                    .WithSqlParam("@PidfID", getPIDFApprovalListRequestModels.PidfID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<PidfCountryDetailsNewModel>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<PIDFDetailsNew> GetAllPIDF(string PidfNumber)
        {
            IList<PIDFDetailsNew> result = new List<PIDFDetailsNew>();
            try
            {
                _db.LoadStoredProc("USP_TblPidfCountrydetails_SelectAll")
                    .WithSqlParam("@PIDFNo", PidfNumber == null ? "-1" : PidfNumber)
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

        public int InsertUploadFileDetails(UploadedFileModel entity)
        {
            try {
                _db.LoadStoredProc("USP_PIDF_Insert_UploadFileDetails")
                .WithSqlParam("@PIDFID", entity.PIDFID)
                .WithSqlParam("@FilePath", entity.SaveFilePath)
                .WithSqlParam("@FileName", entity.SaveFileName)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateApprovalPIDFStatus(UpdateApprovalPIDFStatusRequestModels updateApprovalPIDFStatusRequestModels)
        {
            try
            {
                var testoutput = _db.LoadStoredProc("USP_UpdatePIDF_Status")
                .WithSqlParam("@pidfID", updateApprovalPIDFStatusRequestModels.pidfID)
                .WithSqlParam("@pIDFNo", updateApprovalPIDFStatusRequestModels.pIDFNo)
                .WithSqlParam("@userID", updateApprovalPIDFStatusRequestModels.userID)
                .WithSqlParam("@pidfStatusID", updateApprovalPIDFStatusRequestModels.pidfStatusID)
                .WithSqlParam("@approvalRemark", updateApprovalPIDFStatusRequestModels.approvalRemark)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public Tbl_PIDF_Header GetPIDF(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        public int UpdatePIDFDetails(PIDFHeaderAndDetails entity)
        {
            try
            {
                IList<Tbl_PIDF_CountryDetails> tbl_PIDF_CountryDetails = entity.objPIDF_CountryDetails;
                DataTable tblDetails = new DataTable();
                tblDetails.Columns.Add(new DataColumn("RowID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfDetailID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfNo", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("ContinentID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("CountryID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PatentStatusID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PackingID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("BatchSize", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("PackSize", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("COGS", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("Freight", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("TotalCIFCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerUnit", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerPack", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ProfitPerPack", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("PercentCont", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriOne", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriTwo", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContriThree", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS1", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS2", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("COGS3", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ContributionThreeYear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CostofThreeBatches", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RandDCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("FilingCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("StabilityCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("TotalInvest", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("ROI", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RejectionReason", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("AnalyticalCost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("BECost", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("RLDCost", typeof(decimal)));

                if (tbl_PIDF_CountryDetails.Count > 0)
                {
                    int intRowID = 1;
                    foreach (var ddata in tbl_PIDF_CountryDetails)
                    {
                        tblDetails.Rows.Add(intRowID, ddata.PidfDetailID, ddata.PidfID, ddata.PidfNo, ddata.ContinentID, ddata.CountryID, ddata.PatentStatusID, ddata.PackingID
                                            , ddata.BatchSize, ddata.PackSize, ddata.COGS, ddata.Freight, ddata.TotalCIFCost
                                            , ddata.CIFPricePerUnit, ddata.CIFPricePerPack, ddata.ProfitPerPack, ddata.PercentCont
                                            , ddata.QtyOneyear, ddata.QtyTwoyear, ddata.QtyThreeyear, ddata.VolOneyear
                                            , ddata.VolTwoyear, ddata.VolThreeyear, ddata.ContriOne, ddata.ContriTwo, ddata.ContriThree
                                            , ddata.COGS1, ddata.COGS2, ddata.COGS3, ddata.ContributionThreeYear, ddata.CostofThreeBatches
                                            , ddata.RandDCost, ddata.FilingCost, ddata.StabilityCost, ddata.TotalInvest, ddata.ROI
                                            , ddata.RejectionReason, ddata.AnalyticalCost, ddata.BECost, ddata.RLDCost);
                        intRowID++;
                    }
                }
                SqlParameter tblParameter = new SqlParameter("pIDFCountryDetails", SqlDbType.Structured);
                tblParameter.TypeName = "dbo.PIDFCountryDetailsList";
                tblParameter.Value = tblDetails;
                //SqlParameter tblParameter1 = new SqlParameter("NewRequestNumber", SqlDbType.Char, 50);
                //tblParameter1.Direction = ParameterDirection.Output;
                object testoutput = _db.LoadStoredProc("USP_TblPidfHeader_Update")
                .WithSqlParam("@pIDFCountryDetails", tblParameter)
                .WithSqlParam("@pidfID", entity.PidfID)
                .WithSqlParam("@pIDFNo", entity.PIDFNo)
                .WithSqlParam("@pidfDate", System.DateTime.Now)
                .WithSqlParam("@projectorProductID", entity.ProjectorProductID)
                .WithSqlParam("@projectorProductName", entity.ProjectorProductName)
                .WithSqlParam("@strengths", entity.Strengths)
                .WithSqlParam("@pidfStatusID", entity.PidfStatusID)
                .WithSqlParam("@pidfStatus", entity.PidfStatus)
                .WithSqlParam("@approvedById1", entity.ApprovedById1)
                .WithSqlParam("@approvedDate1", entity.ApprovedByDate1)
                .WithSqlParam("@approvedById2", entity.ApprovedById2)
                .WithSqlParam("@approvedByDate2", entity.ApprovedByDate2)
                .WithSqlParam("@approvedById3", entity.ApprovedById3)
                .WithSqlParam("@approvedByDate3", entity.ApprovedByDate3)
                .WithSqlParam("@approvedById4", entity.ApprovedById4)
                .WithSqlParam("@approvedByDate4", entity.ApprovedByDate4)
                .WithSqlParam("@pidfLastStatusID", entity.PidfStatusID)
                .WithSqlParam("@pidfLastStatus", entity.PidfStatus)
                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", entity.Createdby)
                .WithSqlParam("@createdDate", System.DateTime.Now)
                .WithSqlParam("@modifiedby", entity.Modifiedby)
                .WithSqlParam("@modifiedDate", System.DateTime.Now)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddInitialPidfDetails(Tbl_PIDF_HeaderNew entity, DataTable tblDetails)
        {
            try
            {                          
                SqlParameter tblParameter = new SqlParameter("pIDFStrengthDetails", SqlDbType.Structured);
                tblParameter.TypeName = "dbo.PIDFStrengthList";
                tblParameter.Value = tblDetails;

                    SqlParameter tblParameter1 = new SqlParameter("NewPidfNumber", SqlDbType.Char, 50);
                    tblParameter1.Direction = ParameterDirection.Output;
                    //Add pidfdetails
                    var testoutput = _db.LoadStoredProc("USP_TblPidfheadernew_Insert_Update")
                    .WithSqlParam("@pIDFStrengthDetails", tblParameter)
                    .WithSqlParam("@pIDFID", 0)
                    .WithSqlParam("@pIDFNo", "p1")
                    .WithSqlParam("@pidfDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                    .WithSqlParam("@projectorProductID", entity.ProjectorProductID)
                    .WithSqlParam("@projectorProductName", entity.ProjectorProductName)
                    .WithSqlParam("@productID", entity.ProductID)
                    .WithSqlParam("@productName", entity.ProductName)
                    .WithSqlParam("@plantID", entity.PlantID)
                    .WithSqlParam("@plantName", entity.PlantName)
                    .WithSqlParam("@strengthID", entity.StrengthID)
                    .WithSqlParam("@strengthName", entity.StrengthName)
                    .WithSqlParam("@uomid", entity.UnitID)
                    .WithSqlParam("@unitName", entity.UnitName)
                    .WithSqlParam("@formulationID", entity.FormulationID)
                    .WithSqlParam("@formulationName", entity.FormulationName)
                    .WithSqlParam("@workflowID", entity.WorkflowID)
                    .WithSqlParam("@workflowName", entity.WorkflowName)
                    .WithSqlParam("@strengths", entity.Strengths)
                    .WithSqlParam("@pidfStatusID", entity.PidfStatusID)
                    .WithSqlParam("@pidfStatus", entity.PidfStatus)
                    .WithSqlParam("@batchSize", entity.BatchSize)
                    .WithSqlParam("@packSize", entity.PackSize)
                    .WithSqlParam("@packSizeID", entity.PackSizeID)
                    .WithSqlParam("@packSizeName", entity.PackSizeName)
                    .WithSqlParam("@packingID", entity.PackingID)
                    .WithSqlParam("@packingName", entity.PackingName)
                    .WithSqlParam("@currencyID", entity.CurrencyID)
                    .WithSqlParam("@currencyName", entity.CurrencyName)
                    .WithSqlParam("@cogs", entity.COGS)
                    .WithSqlParam("@cogsFreightPer", 0)
                    .WithSqlParam("@freight", entity.Freight)
                    .WithSqlParam("@totalCIFCost", entity.TotalCIFCost)
                    .WithSqlParam("@cIFPricePerUnit", entity.CIFPricePerUnit)
                    .WithSqlParam("@cIFPricePerPack", entity.CIFPricePerPack)
                    .WithSqlParam("@profitPerPack", entity.ProfitPerPack)
                    .WithSqlParam("@percentCont", entity.PercentCont)
                    .WithSqlParam("@qtyOneyear", entity.QtyOneyear)
                    .WithSqlParam("@qtyTwoyear", entity.QtyTwoyear)
                    .WithSqlParam("@qtyThreeyear", entity.QtyThreeyear)
                    .WithSqlParam("@volOneyear", entity.VolOneyear)
                    .WithSqlParam("@volTwoyear", entity.VolTwoyear)
                    .WithSqlParam("@volThreeyear", entity.VolThreeyear)
                    .WithSqlParam("@contriOne", entity.ContriOne)
                    .WithSqlParam("@contriTwo", entity.ContriTwo)
                    .WithSqlParam("@contriThree", entity.ContriThree)
                    .WithSqlParam("@cogs1", entity.COGS1)
                    .WithSqlParam("@cogs2", entity.COGS2)
                    .WithSqlParam("@cogs3", entity.COGS3    )
                    .WithSqlParam("@contributionThreeYear", entity.ContributionThreeYear    )
                    .WithSqlParam("@costofThreeBatches", entity.CostofThreeBatches)
                    .WithSqlParam("@randDCost", entity.RandDCost)
                    .WithSqlParam("@filingCost", entity.FilingCost)
                    .WithSqlParam("@stabilityCost", entity.StabilityCost)
                    .WithSqlParam("@totalInvest", entity.TotalInvest)
                    .WithSqlParam("@analyticalCost", entity.AnalyticalCost)
                    .WithSqlParam("@bECost", entity.BECost)
                    .WithSqlParam("@rLDCost", entity.RLDCost)
                    .WithSqlParam("@otherCost", entity.OtherCost)
                    .WithSqlParam("@aPISource", entity.APISource)
                    .WithSqlParam("@roi", entity.ROI)
                    .WithSqlParam("@rejectionReason", entity.RejectionReason)                           
                    .WithSqlParam("@approvedById1", entity.ApprovedById1)
                    .WithSqlParam("@approvedDate1", entity.ApprovedDate1)
                    .WithSqlParam("@approvedByID1StatusID", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedByID1Remark", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedById2", entity.ApprovedById2)
                    .WithSqlParam("@approvedByDate2", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedByID2StatusID", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedByID2Remark", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedById3", entity.ApprovedById3)
                    .WithSqlParam("@approvedByDate3", entity.ApprovedByDate3)
                    .WithSqlParam("@approvedByID3StatusID", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedByID3Remark", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedById4", entity.ApprovedById4)
                    .WithSqlParam("@approvedByDate4", entity.ApprovedByDate4)
                    .WithSqlParam("@approvedByID4StatusID", entity.ApprovedByDate2)
                    .WithSqlParam("@approvedByID4Remark", entity.ApprovedByDate2)
                    .WithSqlParam("@pidfLastStatusID", entity.PidfStatusID)
                    .WithSqlParam("@pidfLastStatus", entity.PidfStatus)
                    .WithSqlParam("@dueDate", entity.DueDate)
                    .WithSqlParam("@isActive", true)
                    .WithSqlParam("@createdby", entity.Createdby)
                    .WithSqlParam("@createdDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                    .WithSqlParam("@modifiedby", entity.Modifiedby)
                    .WithSqlParam("@modifiedDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                    .WithSqlParam("@action", "insert")
                    .WithSqlParam("@NewPidfNumber", tblParameter1)
                    .ExecuteStoredNonQuery();
                    string RequestNumber;
                    RequestNumber = Convert.ToString(tblParameter1.Value).Trim();

                    string tempRequestNumber = RequestNumber;

                    //Add code for mail to country managers

                    return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int UpdateInitialPidfDetails(Tbl_PIDF_HeaderNew tbl_PIDF_HeaderNew)
        {
            throw new NotImplementedException();
        }

        public object GetPIDFStrengthList()
        {
            IList<PIDFStrengthList> result = new List<PIDFStrengthList>();
            //try
            //{
            _db.LoadStoredProc("USP_GetPIDFStrengthList")
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<PIDFStrengthList>();
            });

           // List<Object> result1 = (from x in result select (Object)new { projectTaskMappingID = x.ProjectTaskMappingID, parentID = x.ParentID, taskName = x.TaskName, empName = x.EmpName, duration = x.Duration, totalPercentage = "<div class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='" + Convert.ToInt32(x.TotalPercentage) + "' aria-valuemin='0' aria-valuemax='100' style='width:" + Convert.ToInt32(x.TotalPercentage) + "% '>" + Convert.ToInt32(x.TotalPercentage) + "%</div></div>", taskStatus = x.TaskStatus, priority = x.Priority, startDate = x.StartDate.ToString("dd/MM/yyyy"), endDate = x.EndDate.ToString("dd/MM/yyy"), modifiedDate = x.ModifiedDate.ToString("dd/MM/yyyy"), action = "<div class='btn-group'><a href='javascript:void(0)' class='btn btn-primary btn-sm ttip' title='EDIT' onclick=Edit('" + x.ProjectTaskMappingID + "')><i class='fa fa-edit'></i></a><a href='javascript:void(0)' class='btn btn-danger btn-sm ttip' title='DELETE' onclick=Delete('" + x.ProjectTaskMappingID + "')><i class='fa fa-trash-alt'></i></a></div> " }).ToList();
            List<Object> result1 = (from x in result select (Object)new {
                PIDFID = x.PIDFID,
                PIDFNo = "<a href='PIDFShowDetails?ID="+ x.PIDFID+"'  title='Show PIDF details'>" + x.PIDFNo + "</a>" ,
                //PIDFNo = x.PIDFNo ,
                ProjectorProductName = x.ProjectorProductName,
                ProductName = x.ProductName,
                PlantName = x.PlantName, 
               // totalPercentage = "<div class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='" + Convert.ToInt32(x.TotalPercentage) + "' aria-valuemin='0' aria-valuemax='100' style='width:" + Convert.ToInt32(x.TotalPercentage) + "% '>" + Convert.ToInt32(x.TotalPercentage) + "%</div></div>",
                FormulationName = x.FormulationName,
                ParentID =x.ParentID,
                StatusID=x.StatusID,
                Status= x.Status,
                Action=x.Action
                //"<div class='btn-group'><a href='PIDFNewCountry?ID=" + x.PIDFID + "' class='btn btn-primary btn-sm ttip' title='Add Details' ><i class='fa fa-plus-circle' aria-hidden='true'></i></a></div>"
              }).ToList();

            return result1;
            //}
            //catch (Exception ex)
            //{
            //    return result1;
            //}
        }

        public IList<clsPidfStrength> GetPidfStrengthDetails(long PidfID)
        {
            IList<clsPidfStrength> result = new List<clsPidfStrength>();
            try
            {
                _db.LoadStoredProc("USP_GetStrengthsByPidfID")
                    .WithSqlParam("@pidfid", PidfID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<clsPidfStrength>();
                 });

                return result;
            }
            catch (Exception ex)            
            {
                return result;

            }
        }

        public int AddPidfCountryDetails(DataTable tblDetails)
        {
            try
            {
                SqlParameter tblParameter = new SqlParameter("pIDFNewCountryDetailsList", SqlDbType.Structured);
                tblParameter.TypeName = "dbo.PIDFNewCountryDetailsList";
                tblParameter.Value = tblDetails;               
                //Add pidfdetails
                var testoutput = _db.LoadStoredProc("USP_TblPidfCountrydetailsnew_Insert")
                .WithSqlParam("@pIDFNewCountryDetailsList", tblParameter)                
                .ExecuteStoredNonQuery();                

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public object GetPIDFCountrywiseList(int ID)
        {
            IList<PIDFCountrywiseList> result = new List<PIDFCountrywiseList>();
            //try
            //{
            _db.LoadStoredProc("USP_GetPIDFCountrywiseList")
            .WithSqlParam("@PIDFID", ID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<PIDFCountrywiseList>();
            });

            // List<Object> result1 = (from x in result select (Object)new { projectTaskMappingID = x.ProjectTaskMappingID, parentID = x.ParentID, taskName = x.TaskName, empName = x.EmpName, duration = x.Duration, totalPercentage = "<div class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='" + Convert.ToInt32(x.TotalPercentage) + "' aria-valuemin='0' aria-valuemax='100' style='width:" + Convert.ToInt32(x.TotalPercentage) + "% '>" + Convert.ToInt32(x.TotalPercentage) + "%</div></div>", taskStatus = x.TaskStatus, priority = x.Priority, startDate = x.StartDate.ToString("dd/MM/yyyy"), endDate = x.EndDate.ToString("dd/MM/yyy"), modifiedDate = x.ModifiedDate.ToString("dd/MM/yyyy"), action = "<div class='btn-group'><a href='javascript:void(0)' class='btn btn-primary btn-sm ttip' title='EDIT' onclick=Edit('" + x.ProjectTaskMappingID + "')><i class='fa fa-edit'></i></a><a href='javascript:void(0)' class='btn btn-danger btn-sm ttip' title='DELETE' onclick=Delete('" + x.ProjectTaskMappingID + "')><i class='fa fa-trash-alt'></i></a></div> " }).ToList();
            List<Object> result1 = (from x in result
                                    select (Object)new
                                    {
                                        PIDFID = x.PIDFID,
                                        //PIDFNo = "<a href='PIDF/PIDFCountryWiseList?ID=" + x.PIDFID + "'title='Show Country wise details'>" + x.PIDFNo + "</a>",
                                        //PIDFNo =  x.PIDFNo ,
                                        PIDFNo = "<a href='PIDFShowDetails?ID=" + x.PIDFID + "' title='Show PIDF details'>" + x.PIDFNo + "</a>",
                                        ProjectorProductName = x.ProjectorProductName,
                                        CountryName=x.CountryName,
                                        PackSizeName = x.PackSizeName,
                                        PackingName = x.PackingName,
                                        PidfStrength = x.PidfStrength,
                                        CIFPricePerPack1 = x.CIFPricePerPack1,
                                        CIFPricePerPack2 = x.CIFPricePerPack2,
                                        CIFPricePerPack3 = x.CIFPricePerPack3,
                                        QtyOneyear = x.QtyOneyear,
                                        QtyTwoyear = x.QtyTwoyear,
                                        QtyThreeyear = x.QtyThreeyear,
                                        VolOneyear = x.VolOneyear,
                                        VolTwoyear = x.VolTwoyear,
                                        VolThreeyear = x.VolThreeyear,
                                        ParentID = x.ParentID,
                                        Action =x.Action
//"<div class='btn-group'><a href='PIDFNewCountry?ID=" + x.PIDFID + "' class='btn btn-primary btn-sm ttip' title='Edit Details' ><i class='fa fa-edit' aria-hidden='true'></i></a></div>"
                                    }).ToList();

            return result1;
            //}
            //catch (Exception ex)
            //{
            //    return result1;
            //}
        }

        public clsAllContinentData GetAllPidfNewDetails(long PidfID)
        {
            IList<PidfCountryDetailsNew> result = new List<PidfCountryDetailsNew>();
            clsAllContinentData _clsAllContinentData = new clsAllContinentData();
            try
            {
                DataSet dsRequest1 = new DataSet();

               string myconn = _config.GetSection("ConnectionStrings:DefaultConnection").Value;
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(myconn))
                {
                    System.Data.SqlClient.SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("USP_TblPidfCountrydetailsnew_SelectAll_ByPidfID", conn);
                    sqlComm.Parameters.Add(new SqlParameter("pidfID", PidfID));
                    sqlComm.CommandTimeout = 0;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter();

                    da1.SelectCommand = sqlComm;
                    da1.Fill(dsRequest1);
                }

                List<PidfCountryDetailsNew> CISDetails = new List<PidfCountryDetailsNew>();
                List<PidfCountryDetailsNew> LATAMDetails = new List<PidfCountryDetailsNew>();
                List<PidfCountryDetailsNew> ASIADetails = new List<PidfCountryDetailsNew>();
                List<PidfCountryDetailsNew> AFRICADetails = new List<PidfCountryDetailsNew>();
                List<PidfCountryDetailsNew> MENADetails = new List<PidfCountryDetailsNew>();

                CISDetails = ConvertDataTable<PidfCountryDetailsNew>(dsRequest1.Tables[0]);
                LATAMDetails = ConvertDataTable<PidfCountryDetailsNew>(dsRequest1.Tables[1]);
                ASIADetails = ConvertDataTable<PidfCountryDetailsNew>(dsRequest1.Tables[2]);
                AFRICADetails = ConvertDataTable<PidfCountryDetailsNew>(dsRequest1.Tables[3]);
                MENADetails = ConvertDataTable<PidfCountryDetailsNew>(dsRequest1.Tables[4]);

                _clsAllContinentData.CIS_ContinentCountries = CISDetails;
                _clsAllContinentData.LATAM_ContinentCountries = LATAMDetails;
                _clsAllContinentData.ASIA_ContinentCountries = ASIADetails;
                _clsAllContinentData.AFRICA_ContinentCountries = AFRICADetails;
                _clsAllContinentData.MENA_ContinentCountries = MENADetails;

                //_db.LoadStoredProc("USP_TblPidfCountrydetailsnew_SelectAll_ByPidfID")
                //    .WithSqlParam("@pidfID", PidfID)
                // .ExecuteStoredProc((handler) =>
                // {
                //     result = handler.ReadToList<PidfCountryDetailsNew>();
                // });

                return _clsAllContinentData;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            try
            {               

                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            object value = dr[column.ColumnName];
                            if (value == DBNull.Value) value = null;
                            pro.SetValue(obj, value, null);                            
                            //pro.SetValue(obj, dr[column.ColumnName], null);                            
                        }                           
                        else
                            continue;
                    }
                }
                return obj;
            }
            catch(Exception ex)
            {
                return obj;
            }
            
        }

        public PidfCountryDetailsNew GetCountryDetails(long PidfID, int CountryID,int StrengthID)
        {
            IList<PidfCountryDetailsNew> result = new List<PidfCountryDetailsNew>();
            try
            {
                _db.LoadStoredProc("USP_TblPidfCountrydetailsnew_ByPidfIDCountryID")
                    .WithSqlParam("@pidfid", PidfID)
                    .WithSqlParam("@countryID", CountryID)
                    .WithSqlParam("@strengthID", StrengthID)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<PidfCountryDetailsNew>();
                 });
                if(result.Count>0)
                {
                    return result[0];
                }
                else { return null; }
                
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        
        public int UpdatePidfCountryDetails(PidfCountryDetailsNew entity)
        {
            try
            {
                var testoutput = _db.LoadStoredProc("USP_TblPidfCountrydetailsNew_Update")
                .WithSqlParam("@pidfDetailID", entity.PidfDetailID)
                .WithSqlParam("@pIDFID", entity.PidfID)
                .WithSqlParam("@pIDFNo", entity.PidfNo)
                .WithSqlParam("@continentID", entity.ContinentID)
                .WithSqlParam("@continentName", entity.ContinentName)
                .WithSqlParam("@countryID", entity.CountryID)
                .WithSqlParam("@countryName", entity.CountryName)
                .WithSqlParam("@strengthID", entity.StrengthID)
                .WithSqlParam("@patentStatusID", entity.PatentStatusID)
                .WithSqlParam("@patentStatus", entity.PatentStatus)
                .WithSqlParam("@packSizeID", entity.PackSizeID)
                .WithSqlParam("@packSizeName", entity.PackSizeName)
                .WithSqlParam("@packingID", entity.PackingID)
                .WithSqlParam("@packingName", entity.PackingName)
                .WithSqlParam("@cIFPricePerPack", entity.CIFPricePerPack)
                .WithSqlParam("@cIFPricePerPack1", entity.CIFPricePerPack1)
                .WithSqlParam("@cIFPricePerPack2", entity.CIFPricePerPack2)
                .WithSqlParam("@cIFPricePerPack3", entity.CIFPricePerPack3)
                .WithSqlParam("@qtyOneyear", entity.QtyOneyear)
                .WithSqlParam("@qtyTwoyear", entity.QtyTwoyear)
                .WithSqlParam("@qtyThreeyear", entity.QtyThreeyear)
                .WithSqlParam("@volOneyear", entity.VolOneyear)
                .WithSqlParam("@volTwoyear", entity.VolTwoyear)
                .WithSqlParam("@volThreeyear", entity.VolThreeyear)
                .WithSqlParam("@contriOne", entity.ContriOne)
                .WithSqlParam("@contriTwo", entity.ContriTwo)
                .WithSqlParam("@contriThree", entity.ContriThree)
                .WithSqlParam("@cogs1", entity.COGS1)
                .WithSqlParam("@cogs2", entity.COGS2)
                .WithSqlParam("@cogs3", entity.COGS3)
                .WithSqlParam("@batchSize", entity.BatchSize)
                .WithSqlParam("@packSize", entity.PackSize)
                .WithSqlParam("@currencyID", entity.CurrencyID)
                .WithSqlParam("@currencyName", entity.CurrencyName)
                .WithSqlParam("@cogs", entity.COGS)               
                .WithSqlParam("@freight", entity.Freight)
                .WithSqlParam("@totalCIFCost", entity.TotalCIFCost)
                .WithSqlParam("@cIFPricePerUnit", entity.CIFPricePerUnit)
                .WithSqlParam("@profitPerPack", entity.ProfitPerPack)
                .WithSqlParam("@percentCont", entity.PercentCont)
                .WithSqlParam("@contributionThreeYear", entity.ContributionThreeYear)
                .WithSqlParam("@costofThreeBatches", entity.CostofThreeBatches)
                .WithSqlParam("@randDCost", entity.RandDCost)
                .WithSqlParam("@filingCost", entity.FilingCost)
                .WithSqlParam("@stabilityCost", entity.StabilityCost)
                .WithSqlParam("@totalInvest", entity.TotalInvest)
                .WithSqlParam("@rejectionReason", entity.RejectionReason)
                .WithSqlParam("@analyticalCost", entity.AnalyticalCost)
                .WithSqlParam("@bECost", entity.BECost)
                .WithSqlParam("@rLDCost", entity.RLDCost)
                .WithSqlParam("@otherCost", entity.OtherCost)
                .WithSqlParam("@aPISource", entity.APISource)
                .WithSqlParam("@roi", entity.ROI)
                .WithSqlParam("@isActive", true)
                .WithSqlParam("@createdby", entity.Createdby)
                .WithSqlParam("@createdDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                .WithSqlParam("@modifiedby", entity.Modifiedby)
                .WithSqlParam("@modifiedDate", System.DateTime.Now.ToString("yyyy-MM-dd"))
                
                .ExecuteStoredNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<PIDFParentStrengthList> PIDFParentStrengthList()
        {
            IList<PIDFParentStrengthList> result = new List<PIDFParentStrengthList>();
            
            _db.LoadStoredProc("USP_GetParentPIDFStrengthList")
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<PIDFParentStrengthList>();
            });

            return result;
        }

        public IList<children> PIDFChildrenStrengthList(int PIDFID)
        {
            IList<children> result = new List<children>();

            _db.LoadStoredProc("USP_GetChildPIDFStrengthList")
             .WithSqlParam("@PIDFID", PIDFID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<children>();
            });

            return result;
        }

        public IList<PIDFParentCountrywiseList> PIDFParentCountrywiseList(int PIDFID)
        {
            IList<PIDFParentCountrywiseList> result = new List<PIDFParentCountrywiseList>();

            _db.LoadStoredProc("USP_GetParentPIDFCountryList")
            .WithSqlParam("@PIDFID", PIDFID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<PIDFParentCountrywiseList>();
            });

            return result;
        }

        public IList<countryChildren> PIDFChildrenCountrywiseList(int PIDFID)
        {
            IList<countryChildren> result = new List<countryChildren>();

            _db.LoadStoredProc("USP_GetChildPIDFCountryList")
            .WithSqlParam("@PIDFID", PIDFID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<countryChildren>();
            });

            return result;
        }

        public int DeleteAllUploadFileDetails(int PidfID)
        {
            try
            {
                _db.LoadStoredProc("USP_Tbl_PIDF_UploadFileDetails_DeleteByPidfID")
                .WithSqlParam("@pidfID", PidfID)                
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<DRFTaskSubTaskOutputNew> GetMixedTaskSubTaskListForPIDFInsertion(string strAction)
        {
            IList<DRFTaskSubTaskOutputNew> result = new List<DRFTaskSubTaskOutputNew>();
            try
            {
                _db.LoadStoredProc("USP_GetMixedTaskSubTaskListForPIDFInsertion")
                    .WithSqlParam("@action", strAction)
                  .ExecuteStoredProc((handler) =>
                  {
                      result = handler.ReadToList<DRFTaskSubTaskOutputNew>();
                  });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<ApprovedPIDFParentList> GetAllApprovedPidfList()
        {
            IList<ApprovedPIDFParentList> result = new List<ApprovedPIDFParentList>();

            _db.LoadStoredProc("USP_Get_Parent_PIDF_Header_List")            
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<ApprovedPIDFParentList>();
            });

            return result;
        }

        public IList<AprovedPidfCountryList> GetAllApprovedPidfCountryList(int PidID)
        {
            IList<AprovedPidfCountryList> result = new List<AprovedPidfCountryList>();

            _db.LoadStoredProc("USP_Get_Child_PIDF_Country_List")
            .WithSqlParam("@PIDFID", PidID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<AprovedPidfCountryList>();
            });

            return result;
        }

        public object GetPIDFTAskSubTaskList(string PidfID)
        {
            IList<DRFTaskSubTaskListOutput> result = new List<DRFTaskSubTaskListOutput>();
           
            _db.LoadStoredProc("USP_Get_PIDF_Task_SubTask_List")
            .WithSqlParam("@DRFID", PidfID)
            .ExecuteStoredProc((handler) =>
            {
                result = handler.ReadToList<DRFTaskSubTaskListOutput>();
            });

            List<Object> result1 = (from x in result select (Object)new { projectTaskMappingID = x.ProjectTaskMappingID, parentID = x.ParentID, taskName = x.TaskName, empName = x.EmpName, duration = x.Duration, totalPercentage = "<div class='progress'><div class='progress-bar' role='progressbar' aria-valuenow='" + Convert.ToInt32(x.TotalPercentage) + "' aria-valuemin='0' aria-valuemax='100' style='width:" + Convert.ToInt32(x.TotalPercentage) + "% '>" + Convert.ToInt32(x.TotalPercentage) + "%</div></div>", taskStatus = x.TaskStatus, priority = x.Priority, startDate = x.StartDate.ToString("dd/MM/yyyy"), endDate = x.EndDate.ToString("dd/MM/yyy"), modifiedDate = x.ModifiedDate.ToString("dd/MM/yyyy"), action = "<div class='btn-group'><a href='javascript:void(0)' class='btn btn-primary btn-sm ttip' title='EDIT' onclick=Edit('" + x.ProjectTaskMappingID + "')><i class='fa fa-edit'></i></a><a href='javascript:void(0)' class='btn btn-danger btn-sm ttip' title='DELETE' onclick=Delete('" + x.ProjectTaskMappingID + "')><i class='fa fa-trash-alt'></i></a></div> " }).ToList();

            return result1;
            
        }

        public IList<Tbl_DRF_Initialization> GetDRFList(int PidfID)
        {
            IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();
            try
            {
                _db.LoadStoredProc("USP_GetFinalApprovedProjectList_ByPIDFID")
                    .WithSqlParam("@pidfID", Convert.ToInt64(PidfID))
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

        public IList<PidfCountryDetailsNew> GetAllCountryDetails(int PidfID)
        {
            IList<PidfCountryDetailsNew> result = new List<PidfCountryDetailsNew>();
            try
            {
                _db.LoadStoredProc("USP_AllPidfCountrydetails_ByPidfID")
                    .WithSqlParam("@pidfid", Convert.ToInt64(PidfID))                    
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<PidfCountryDetailsNew>();
                 });
                if (result.Count > 0)
                {
                    return result;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }


}
