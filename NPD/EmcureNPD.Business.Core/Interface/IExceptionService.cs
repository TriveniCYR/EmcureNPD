using System;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IExceptionService
    {
        Task<DBOperation> LogException(Exception exception);
    }
}