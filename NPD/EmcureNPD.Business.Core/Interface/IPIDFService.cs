using EmcureNPD.Business.Models;
using System;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IPIDFService
    {
        Task<dynamic> FillDropdown(int userid);
        Task<DataTableResponseModel> GetAllPIDFList(DataTableAjaxPostModel model);
        Task<DBOperation> AddUpdatePIDF(PIDFEntity entityPIDF);
        Task<PIDFEntity> GetById(int id);
        Task<DBOperation> ApproveRejectDeletePidf(EntryApproveRej oApprRej);
        Task<DataTableResponseModel> GetCommonPIDFList(DataTableAjaxPostModel model, string ScrrenName);
        Task<DBOperation> CommonApproveRejectDeletePidf(EntryApproveRej oApprRej,string ScreenName);
        
    }
}
