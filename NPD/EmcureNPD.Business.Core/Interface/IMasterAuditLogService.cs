using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterAuditLogService
    {
        Task<DBOperation> CreateAuditLog<TResult>(AuditActionType auditActionType, ModuleEnum moduleEnum, TResult Old, TResult New, int? PrimaryId) where TResult : new();

        Task<List<MasterAuditLogEntity>> GetAllAuditLog();

        Task<MasterAuditLogEntity> GetById(int id);

        Task<IEnumerable<MasterAuditLogWrapperEntity<AuditLog>>> GetByModuleId(int id, int moduleId);

        Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model);

        //Common method to Update PIDF Status
        Task<DBOperation> UpdatePIDFStatusCommon(long PidfId, int StatusId, int StatusUpdatedBy);
    }
}