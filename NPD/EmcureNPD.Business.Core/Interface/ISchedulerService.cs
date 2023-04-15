using EmcureNPD.Business.Models;
using EmcureNPD.Web.Models;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface ISchedulerService
    {
        Task<SendReminderModel> SendReminder();

        void SendReminderMail(SendReminderModel sendReminderModel);

        Task<SendReminderModel> AutoUpdatePIDFStatus();

        void AutoUpdatePIDFStatusMail(AutoUpdatePIDFStatusModel autoUpdatePIDFStatusModel);
    }
}