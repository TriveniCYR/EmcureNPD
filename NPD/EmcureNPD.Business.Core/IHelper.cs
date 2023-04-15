using EmcureNPD.Business.Models;

namespace EmcureNPD.Business.Core
{
    public interface IHelper
    {
        UserSessionEntity GetLoggedInUser();
    }
}