using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface {
    public interface IExceptionService {
        Task<DBOperation> LogException(Exception exception);
    }
}
