using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Schedule.Services.Interfaces {
    public interface ISchedulerService {
        void GetToken();
        void SendReminderAPI();
    }
}
