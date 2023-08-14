using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface ICommercialFormService
    {
        //Task<IPDEntity> FillDropdown();
        // Task<DBOperation> AddUpdateIPD(PIDFormEntity entityIPD);
        Task<DBOperation> AddUpdateCommercialPIDF(PIDFCommercialViewModel entityIPD);

        //Task<PIDFormEntity> GetIPDFormData(long pidfId, int buid);
        Task<dynamic> GetCommercialFormData(long pidfId, int buid, int? strengthid);

        //Task<DataTableResponseModel> GetAllIPDPIDFList(DataTableAjaxPostModel model);

        Task<IEnumerable<dynamic>> GetAllRegion(int userId);

        Task<IEnumerable<dynamic>> GetCountryRefByRegionIds(string regionIds);

        //Task<DBOperation> ApproveRejectIpdPidf(EntryApproveRej oApprRej);

        Task<List<MasterFinalSelectionEntity>> GetAllFinalSelection();
        Task<List<MasterPackSizeViewModelEntity>> GetAllPackSize();
        Task<dynamic> GetPBFOutsourcingTabDropDownData();
        Task<dynamic> GetPBFWorkFlowTaskNames(int pbfWorkFlowid);
        Task<DBOperation> AddUpdatePBFoutsourceData(PidfpbfoutsourceEntity entityPBFOutsource);
    }
}