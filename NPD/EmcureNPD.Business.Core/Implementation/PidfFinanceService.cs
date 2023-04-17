using Dapper;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PidfFinanceService : IPidfFinanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext _dbContext;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<Pidf> _pidfrepository { get; set; }
        private IRepository<PidfFinance> _repository { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMasterAuditLogService _auditLogService;
        private IRepository<PidfFinanceBatchSizeCoating> _childrepository { get; set; }
        private readonly INotificationService _notificationService;
        private readonly IExceptionService _ExceptionService;

        public PidfFinanceService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory,
             INotificationService notificationService,
            IConfiguration configuration, DbContext dbContext, IMasterAuditLogService auditLogService, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<PidfFinance>();
            _childrepository = _unitOfWork.GetRepository<PidfFinanceBatchSizeCoating>();
            _configuration = configuration;
            _dbContext = dbContext;
            _auditLogService = auditLogService;
            _pidfrepository = _unitOfWork.GetRepository<Pidf>();
            _notificationService = notificationService;
            _ExceptionService = exceptionService;
        }

        public async Task<List<PidfFinance>> GetAll()
        {
            return await _repository.GetAllQuery().ToListAsync();
        }

        public async Task<dynamic> GetPidfFinance(int Pidfid = 0)
        {
            try
            {
                SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFId", Pidfid)
            };

                DataSet dsPidfFinance = await _repository.GetDataSetBySP("ProcGetPidfFinance", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dsPidfFinance;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }

        public async Task<dynamic> GetFinanceBatchSizeCoating(int PidffinaceId = 0)
        {
            try
            {
                SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFFinaceId", PidffinaceId)
            };

                DataSet dsFinanceBatchSizeCoating = await _repository.GetDataSetBySP("PRocGetFinanceBatchSizeCoating", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dsFinanceBatchSizeCoating;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }

        public async Task<PidfFinance> GetById(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<DBOperation> AddUpdatePidfFinance(FinanceModel entityPidfFinance)
        {
            try
            {
                bool IsUpdateFinancePIDF = false;
                SqlConnection con = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
                PidfFinance _previousFinanceEntity = new PidfFinance();
                if (entityPidfFinance.PidffinaceId > 0)
                {
                    IsUpdateFinancePIDF = true;
                    _previousFinanceEntity = await _repository.GetAsync(entityPidfFinance.PidffinaceId);
                }                  

                DynamicParameters data = new DynamicParameters();
                data.Add("@PIDFId", entityPidfFinance.Pidfid);
                data.Add("@Entity", entityPidfFinance.Entity);
                data.Add("@Product", entityPidfFinance.Product);
                data.Add("@ForecastDate", entityPidfFinance.ForecastDate);
                data.Add("@Currencyid", entityPidfFinance.Currencyid);
                data.Add("@DosageFrom", entityPidfFinance.DosageFrom);
                data.Add("@ManufacturingSiteOrPartner", entityPidfFinance.ManufacturingSiteOrPartner);
                data.Add("@SKUs", entityPidfFinance.Skus);
                data.Add("@MSPersentage", entityPidfFinance.Mspersentage);
                data.Add("@TargetPriceScenario", entityPidfFinance.TargetPriceScenario);
                data.Add("@ProjectStartDate", entityPidfFinance.ProjectStartDate);
                data.Add("@BatchManufacturing", entityPidfFinance.BatchManufacturing);
                data.Add("@ExpectedFilling", entityPidfFinance.ExpectedFilling);
                data.Add("@ApprovalPeriodinDays", entityPidfFinance.ApprovalPeriodinDays);
                data.Add("@ApprovalDate", entityPidfFinance.ApprovalDate);
                data.Add("@ProductLaunchDate", entityPidfFinance.ProductLaunchDate);
                data.Add("@GestationPeriodinYears", entityPidfFinance.GestationPeriodinYears);
                data.Add("@MarketShareErosionrate", entityPidfFinance.MarketShareErosionrate);
                data.Add("@PriceErosion", entityPidfFinance.PriceErosion);
                data.Add("@EscalationinCOGS", entityPidfFinance.EscalationinCOGS);
                data.Add("@DiscountRate", entityPidfFinance.DiscountRate);
                data.Add("@Incometaxrate", entityPidfFinance.Incometaxrate);
                data.Add("@Opexasapercenttosale", entityPidfFinance.Opexasapercenttosale);
                data.Add("@ExternalProfitSharepercent", entityPidfFinance.ExternalProfitSharepercent);
                data.Add("@CollectioninDays", entityPidfFinance.CollectioninDays);
                data.Add("@InventoryinDays", entityPidfFinance.InventoryinDays);
                data.Add("@CreditorinDays", entityPidfFinance.CreditorinDays);
                data.Add("@MarketingAllowance", entityPidfFinance.MarketingAllowance);
                data.Add("@RegulatoryMaintenanceCost", entityPidfFinance.RegulatoryMaintenanceCost);
                data.Add("@GrosstoNet", entityPidfFinance.GrosstoNet);
                data.Add("@Noofbatchestobemanufactured", entityPidfFinance.Noofbatchestobemanufactured);
                data.Add("@NoofbatchestobemanufacturedPhaseEndDate", entityPidfFinance.NoofbatchestobemanufacturedPhaseEndDate);
                data.Add("@NoSKUs", entityPidfFinance.NoSkus);
                data.Add("@NoSKUsPhaseEndDate", entityPidfFinance.NoSkusPhaseEndDate);
                data.Add("@RandDAnalyticalcost", entityPidfFinance.RandDanalyticalcost);
                data.Add("@RandDAnalyticalcostPhaseEndDate", entityPidfFinance.RandDanalyticalcostPhaseEndDate);
                data.Add("@RLDsamplecost", entityPidfFinance.Rldsamplecost);
                data.Add("@RLDsamplecostPhaseEndDate", entityPidfFinance.RldsamplecostPhaseEndDate);
                data.Add("@BatchmanufacturingcostOrAPIActualsEst", entityPidfFinance.BatchmanufacturingcostOrApiactualsEst);
                data.Add("@BatchmanufacturingcostOrAPIActualsEstPhaseEndDate", entityPidfFinance.BatchmanufacturingcostOrApiactualsEstPhaseEndDate);
                data.Add("@Sixmonthsstabilitycost", entityPidfFinance.Sixmonthsstabilitycost);
                data.Add("@SixmonthsstabilitycostPhaseEndDate", entityPidfFinance.SixmonthsstabilitycostPhaseEndDate);
                data.Add("@TechTransfer", entityPidfFinance.TechTransfer);
                data.Add("@TechTransferPhaseEndDate", entityPidfFinance.TechTransferPhaseEndDate);
                data.Add("@BEstudies", entityPidfFinance.Bestudies);
                data.Add("@BEstudiesPhaseEndDate", entityPidfFinance.BestudiesPhaseEndDate);
                data.Add("@Filingfees", entityPidfFinance.Filingfees);
                data.Add("@FilingfeesPhaseEndDate", entityPidfFinance.FilingfeesPhaseEndDate);
                data.Add("@BioStuddyCost", entityPidfFinance.BioStuddyCost);
                data.Add("@BioStuddyCostPhaseEndDate", entityPidfFinance.BioStuddyCostPhaseEndDate);
                data.Add("@Capex", entityPidfFinance.Capex);
                data.Add("@CapexPhaseEndDate", entityPidfFinance.CapexPhaseEndDate);
                data.Add("@ToolingAndChangeParts", entityPidfFinance.ToolingAndChangeParts);
                data.Add("@ToolingAndChangePartsPhaseEndDate", entityPidfFinance.ToolingAndChangePartsPhaseEndDate);
                data.Add("@Total", entityPidfFinance.Total);
                data.Add("@CreatedBy", entityPidfFinance.CreatedBy);
                data.Add("@Success", "", direction: ParameterDirection.Output);
                await con.ExecuteAsync("ProcAddUpdatePidfFinance", data, commandType: CommandType.StoredProcedure);
                var result = data.Get<string>("Success").Trim();
                if (result.Split('-')[0] == "success")
                {
                    PidfFinance newFinanceEntity = new PidfFinance();//await _repository.GetAsync(Convert.ToInt32(result.Split('-')[1]));
                    newFinanceEntity.PidffinaceId = entityPidfFinance.PidffinaceId;
                    newFinanceEntity.Pidfid = Convert.ToInt32(entityPidfFinance.Pidfid);
                    newFinanceEntity.Entity = entityPidfFinance.Entity;
                    newFinanceEntity.Product = entityPidfFinance.Product;
                    newFinanceEntity.ForecastDate = entityPidfFinance.ForecastDate;
                    newFinanceEntity.Currencyid = entityPidfFinance.Currencyid;
                    newFinanceEntity.DosageFrom = entityPidfFinance.DosageFrom;
                    newFinanceEntity.ManufacturingSiteOrPartner = entityPidfFinance.ManufacturingSiteOrPartner;
                    newFinanceEntity.Skus = entityPidfFinance.Skus;
                    newFinanceEntity.Mspersentage = entityPidfFinance.Mspersentage;
                    newFinanceEntity.TargetPriceScenario = entityPidfFinance.TargetPriceScenario;
                    newFinanceEntity.ProjectStartDate = entityPidfFinance.ProjectStartDate;
                    newFinanceEntity.BatchManufacturing = entityPidfFinance.BatchManufacturing;
                    newFinanceEntity.ExpectedFilling = entityPidfFinance.ExpectedFilling;
                    newFinanceEntity.ApprovalPeriodinDays = entityPidfFinance.ApprovalPeriodinDays;
                    newFinanceEntity.ApprovalDate = entityPidfFinance.ApprovalDate;
                    newFinanceEntity.ProductLaunchDate = entityPidfFinance.ProductLaunchDate;
                    newFinanceEntity.GestationPeriodinYears = entityPidfFinance.GestationPeriodinYears;
                    newFinanceEntity.MarketShareErosionrate = entityPidfFinance.MarketShareErosionrate;
                    newFinanceEntity.PriceErosion = entityPidfFinance.PriceErosion;
                    newFinanceEntity.DiscountRate = entityPidfFinance.DiscountRate;
                    newFinanceEntity.Incometaxrate = entityPidfFinance.Incometaxrate;
                    newFinanceEntity.Opexasapercenttosale = entityPidfFinance.Opexasapercenttosale;
                    newFinanceEntity.ExternalProfitSharepercent = entityPidfFinance.ExternalProfitSharepercent;
                    newFinanceEntity.CollectioninDays = entityPidfFinance.CollectioninDays;
                    newFinanceEntity.InventoryinDays = entityPidfFinance.InventoryinDays;
                    newFinanceEntity.CreditorinDays = entityPidfFinance.CreditorinDays;
                    newFinanceEntity.MarketingAllowance = entityPidfFinance.MarketingAllowance;
                    newFinanceEntity.RegulatoryMaintenanceCost = entityPidfFinance.RegulatoryMaintenanceCost;
                    newFinanceEntity.GrosstoNet = entityPidfFinance.GrosstoNet;
                    newFinanceEntity.Noofbatchestobemanufactured = entityPidfFinance.Noofbatchestobemanufactured;
                    newFinanceEntity.NoofbatchestobemanufacturedPhaseEndDate = entityPidfFinance.NoofbatchestobemanufacturedPhaseEndDate;
                    newFinanceEntity.NoSkus = entityPidfFinance.NoSkus;
                    newFinanceEntity.NoSkusPhaseEndDate = entityPidfFinance.NoSkusPhaseEndDate;
                    newFinanceEntity.RandDanalyticalcost = entityPidfFinance.RandDanalyticalcost;
                    newFinanceEntity.RandDanalyticalcostPhaseEndDate = entityPidfFinance.RandDanalyticalcostPhaseEndDate;
                    newFinanceEntity.Rldsamplecost = entityPidfFinance.Rldsamplecost;
                    newFinanceEntity.RldsamplecostPhaseEndDate = entityPidfFinance.RldsamplecostPhaseEndDate;
                    newFinanceEntity.BatchmanufacturingcostOrApiactualsEst = entityPidfFinance.BatchmanufacturingcostOrApiactualsEst;
                    newFinanceEntity.BatchmanufacturingcostOrApiactualsEstPhaseEndDate = entityPidfFinance.BatchmanufacturingcostOrApiactualsEstPhaseEndDate;
                    newFinanceEntity.Sixmonthsstabilitycost = entityPidfFinance.Sixmonthsstabilitycost;
                    newFinanceEntity.SixmonthsstabilitycostPhaseEndDate = entityPidfFinance.SixmonthsstabilitycostPhaseEndDate;
                    newFinanceEntity.TechTransfer = entityPidfFinance.TechTransfer;
                    newFinanceEntity.TechTransferPhaseEndDate = entityPidfFinance.TechTransferPhaseEndDate;
                    newFinanceEntity.Bestudies = entityPidfFinance.Bestudies;
                    newFinanceEntity.BestudiesPhaseEndDate = entityPidfFinance.BestudiesPhaseEndDate;
                    newFinanceEntity.Filingfees = entityPidfFinance.Filingfees;
                    newFinanceEntity.FilingfeesPhaseEndDate = entityPidfFinance.FilingfeesPhaseEndDate;
                    newFinanceEntity.BioStuddyCost = entityPidfFinance.BioStuddyCost;
                    newFinanceEntity.BioStuddyCostPhaseEndDate = entityPidfFinance.BioStuddyCostPhaseEndDate;
                    newFinanceEntity.Capex = entityPidfFinance.Capex;
                    newFinanceEntity.CapexPhaseEndDate = entityPidfFinance.CapexPhaseEndDate;
                    newFinanceEntity.ToolingAndChangeParts = entityPidfFinance.ToolingAndChangeParts;
                    newFinanceEntity.ToolingAndChangePartsPhaseEndDate = entityPidfFinance.ToolingAndChangePartsPhaseEndDate;
                    newFinanceEntity.Total = entityPidfFinance.Total;
                    newFinanceEntity.CreatedDate = Convert.ToDateTime(entityPidfFinance.CreatedDate);
                    newFinanceEntity.CreatedBy = entityPidfFinance.CreatedBy;
                    List<PidfFinanceBatchSizeCoating> ls = new List<PidfFinanceBatchSizeCoating>();
                    var PidffinaceId = entityPidfFinance.PidffinaceId > 0 ? entityPidfFinance.PidffinaceId : Convert.ToInt32(result.Split('-')[1]);
                    if (entityPidfFinance.lsPidfFinanceBatchSizeCoating.Count > 0)
                    {
                        await AddUpdatePidfFinanceBatchSizeCoating(entityPidfFinance.lsPidfFinanceBatchSizeCoating, Convert.ToInt32(entityPidfFinance.CreatedBy), PidffinaceId);
                    }
                    //*Audit log//
                    if (IsUpdateFinancePIDF)
                    {
                        var isAuditSuccess = await _auditLogService.CreateAuditLog<PidfFinance>(Utility.Audit.AuditActionType.Update,
                                            ModuleEnum.Finance, _previousFinanceEntity, newFinanceEntity, Convert.ToInt32(entityPidfFinance.Pidfid));

                    }
                    //*Status update start//
                    try
                    {
                        int saveTId = 0;
                        if (entityPidfFinance.SaveType == "submit")
                            saveTId = (Int32)Master_PIDFStatus.FinanceSubmitted;
                        else if (entityPidfFinance.SaveType == "draft")
                            saveTId = (Int32)Master_PIDFStatus.FinanceInProgres;
                        else if (entityPidfFinance.SaveType == "approved")
                            saveTId = (Int32)Master_PIDFStatus.FinanceApproved;
                        else if (entityPidfFinance.SaveType == "rejected")
                            saveTId = (Int32)Master_PIDFStatus.FinanceRejected;

                        Pidf objPidf = await _pidfrepository.GetAsync((long)newFinanceEntity.Pidfid);
                        objPidf.LastStatusId = objPidf.StatusId;
                        objPidf.StatusId = saveTId;
                        objPidf.StatusUpdatedBy = entityPidfFinance.CreatedBy;
                        objPidf.StatusUpdatedDate = DateTime.Now;
                        //objPidf.StatusRemark = entityPidfFinance.StatusRemark;
                        _pidfrepository.UpdateAsync(objPidf);
                        await _unitOfWork.SaveChangesAsync();
                        await _notificationService.CreateNotification(objPidf.Pidfid, objPidf.StatusId, string.Empty, string.Empty, (int)objPidf.StatusUpdatedBy);
                    }
                    catch (Exception ex)
                    {
                        await _ExceptionService.LogException(ex);
                        return DBOperation.Error;
                    }
                    //Status update end//
                    return DBOperation.Success;
                }
                else
                { return DBOperation.Error; }
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return DBOperation.Error;
            }
        }

        public async Task<bool> AddUpdatePidfFinanceBatchSizeCoating(List<ChildPidfFinanceBatchSizeCoating> ls, int CreatedBy, int PIDFFinaceId = 0)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
                int count = 0;
                var recordsTable = new DataTable();
                recordsTable.Columns.Add("PIDFFinaceBatchSizeCoatingId", typeof(int));
                recordsTable.Columns.Add("PIDFFinaceId", typeof(int));
                recordsTable.Columns.Add("BusinessUnitId", typeof(int));
                recordsTable.Columns.Add("Batchsize", typeof(double));
                recordsTable.Columns.Add("Yield", typeof(double));
                recordsTable.Columns.Add("Batchoutput", typeof(double));
                recordsTable.Columns.Add("API_CAD", typeof(double));
                recordsTable.Columns.Add("Excipients_CAD", typeof(double));
                recordsTable.Columns.Add("PM_CAD", typeof(double));
                recordsTable.Columns.Add("CCPC_CAD", typeof(double));
                recordsTable.Columns.Add("Freight_CAD", typeof(double));
                recordsTable.Columns.Add("EmcureCOGs_pack", typeof(double));
                recordsTable.Columns.Add("CreatedDate", typeof(DateTime));
                recordsTable.Columns.Add("CreatedBy", typeof(int));
                DataRow row;
                foreach (var item in ls)
                {
                    row = recordsTable.NewRow();
                    row["PIDFFinaceBatchSizeCoatingId"] = item.PidffinaceBatchSizeCoatingId;
                    row["PIDFFinaceId"] = PIDFFinaceId;
                    row["BusinessUnitId"] = item.BusinessUnitId == null ? DBNull.Value : item.BusinessUnitId;
                    row["Batchsize"] = item.Batchsize == null ? DBNull.Value : item.Batchsize;
                    row["Yield"] = item.Yield == null ? DBNull.Value : item.Yield;
                    row["Batchoutput"] = item.Batchoutput == null ? DBNull.Value : item.Batchoutput;
                    row["API_CAD"] = item.ApiCad == null ? DBNull.Value : item.ApiCad;
                    row["Excipients_CAD"] = item.ExcipientsCad == null ? DBNull.Value : item.ExcipientsCad;
                    row["PM_CAD"] = item.PmCad == null ? DBNull.Value : item.PmCad;
                    row["CCPC_CAD"] = item.CcpcCad == null ? DBNull.Value : item.CcpcCad;
                    row["Freight_CAD"] = item.FreightCad == null ? DBNull.Value : item.FreightCad;
                    row["EmcureCOGs_pack"] = item.EmcureCogsPack == null ? DBNull.Value : item.EmcureCogsPack;
                    row["CreatedDate"] = DateTime.Now;
                    row["CreatedBy"] = CreatedBy;
                    recordsTable.Rows.Add(row);
                }

                recordsTable.EndLoadData();
                var data = new DynamicParameters(new
                {
                    table = recordsTable.AsTableValuedParameter("PIDF_Finance_BatchSizeCoatingTable_Type")
                });
                data.Add("@Success", "", direction: ParameterDirection.Output);
                count = await con.ExecuteAsync("AddUpdatePIDF_Finance_BatchSizeCoating", data, commandType: CommandType.StoredProcedure);
                if (count > 0 && data.Get<string>("Success").Trim() == "success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return false;
            }
        }
    }
}