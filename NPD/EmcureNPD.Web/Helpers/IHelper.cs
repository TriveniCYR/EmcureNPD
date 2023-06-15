using EmcureNPD.Data.DataAccess.Entity;
using System;
namespace EmcureNPD.Web.Helpers
{
    public interface IHelper
    {
        int GetLoggedInUserId();

        string GetToken();

        string GetAssignedBusinessUnit();

        string IsManagementUser();

        int GetLoggedInRoleId();
        void LogExceptions(Exception ex);
        bool _isEmptyOrInvalid(string token, DateTime VallidTo);
        bool IsAccessToPIDF(int ModuleId, int? PidfId);


    }
}