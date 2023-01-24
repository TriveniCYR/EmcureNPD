using EmcureNPD.Business.Models;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IAPIListService
    {
        Task<DataTableResponseModel> GetAllAPIList(DataTableAjaxPostModel model);
    }
}
