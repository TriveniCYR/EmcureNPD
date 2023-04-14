using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class MasterAuditLogService : IMasterAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IHelper _helper;
        private IRepository<MasterAuditLog> _repository { get; set; }
        private IRepository<MasterModule> _moduleRepository { get; set; }
        private IRepository<MasterUser> _userRepository { get; set; }
        private IRepository<Pidf> _pidfrepository { get; set; }
        public MasterAuditLogService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterAuditLog>();
            _moduleRepository = _unitOfWork.GetRepository<MasterModule>();
            _userRepository = _unitOfWork.GetRepository<MasterUser>();
            _pidfrepository = unitOfWork.GetRepository<Pidf>();
            _helper = helper;
        }
        public async Task<DBOperation> CreateAuditLog<TResult>(AuditActionType auditActionType,
            ModuleEnum moduleEnum, TResult Old, TResult New, int? PrimaryId) where TResult : new()
        {
            try
            {
                MasterAuditLog objAuditLog;
                var entityAuditLog = new MasterAuditLogEntity
                {
                    CreatedBy = _helper.GetLoggedInUser().UserId,
                    ActionType = Enum.GetName(typeof(AuditActionType), auditActionType),
                    Log = Old.ToAuditLog(New),
                    ModuleId = (int)moduleEnum,
                    EntityId = (int)PrimaryId
                };
                if (entityAuditLog.Log != "[]")
                {
                    objAuditLog = _mapperFactory.Get<MasterAuditLogEntity, MasterAuditLog>(entityAuditLog);
                    _repository.AddAsync(objAuditLog);

                    await _unitOfWork.SaveChangesAsync();

                    if (objAuditLog.AuditLogId == 0)
                        return DBOperation.Error;
                }
                return DBOperation.Success;
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }
        }
        public async Task<DBOperation> UpdatePIDFStatusCommon(long PidfId, int StatusId, int StatusUpdatedBy)
        {
            try
            {
                var _objExistingPIDF = _pidfrepository.Get(x => x.Pidfid == PidfId);
                if (_objExistingPIDF != null)
                {
                    _objExistingPIDF.LastStatusId = _objExistingPIDF.StatusId;
                    _objExistingPIDF.StatusId = StatusId;
                    _objExistingPIDF.StatusUpdatedBy = StatusUpdatedBy;
                    _objExistingPIDF.StatusUpdatedDate = DateTime.Now;
                    _pidfrepository.UpdateAsync(_objExistingPIDF);
                }
                await _unitOfWork.SaveChangesAsync();
                return DBOperation.Success;
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }
        }
        public Task<List<MasterAuditLogEntity>> GetAllAuditLog()
        {
            throw new NotImplementedException();
        }

        public Task<MasterAuditLogEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MasterAuditLogWrapperEntity<AuditLog>>> GetByModuleId(int id, int moduleId)
        {
            var entityAuditLog = await _repository.FindAllAsync(x => x.EntityId == id && x.ModuleId == moduleId);
            var auditLog = entityAuditLog.Select(xx => new MasterAuditLogWrapperEntity<AuditLog>
            {
                ActionType = xx.ActionType,
                AuditLogId = xx.AuditLogId,
                CreatedBy = xx.CreatedBy,
                CreatedDate = xx.CreatedDate,
                EntityId = xx.EntityId,
                ModuleId = xx.ModuleId,
                Log = JsonConvert.DeserializeObject<IEnumerable<AuditLog>>(xx.Log)
            });
            return auditLog;
        }

        public async Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model)
        {

            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                    new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var PIDFList = await _repository.GetBySP("stp_npd_GetAuditLogList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (PIDFList != null && PIDFList.Rows.Count > 0 ? Convert.ToInt32(PIDFList.Rows[0]["TotalCount"]) : 0);

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, PIDFList);

            return oDataTableResponseModel;

        }
    }
}
