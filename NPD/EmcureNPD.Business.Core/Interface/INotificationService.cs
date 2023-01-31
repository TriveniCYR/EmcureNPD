using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface {
    public interface INotificationService {
        Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model);
    }
}
