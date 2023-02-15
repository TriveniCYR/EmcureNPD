 
namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Models;
    using EmcureCERI.Data.DataAccess.Entities;
    public interface ITbl_Master_DepartmentService
    {
        ServiceResponseList<Tbl_Master_Department> GetAllDepartment();

    }
}
