using EmcureNPD.Business.Models;
using EmcureNPD.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface ISchedulerService
    {
        Task<SendReminderModel> SendReminder();

       // void SendReminderMail(List<SendReminderModel> sendReminderModel_list);

        Task<SendReminderModel> AutoUpdatePIDFStatus();

       // void AutoUpdatePIDFStatusMail(AutoUpdatePIDFStatusModel autoUpdatePIDFStatusModel);
    }
}