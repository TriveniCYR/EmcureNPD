using EmcureNPD.Business.Models;

namespace EmcureNPD.Web.Helpers
{
    public interface IHelper
    {
        int GetLoggedInUserId();

        string GetToken();
        string GetAssignedBusinessUnit();
    }
}
