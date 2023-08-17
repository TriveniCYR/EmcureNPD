//using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.NotificationScheduler.Interfaces {
    public interface INotificationScheduler {
        void GetToken();
        void SendNotificationAPI();
    }
}
