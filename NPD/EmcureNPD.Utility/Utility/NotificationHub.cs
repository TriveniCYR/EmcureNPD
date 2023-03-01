
using Microsoft.AspNet.SignalR;
using System.Runtime.InteropServices;

namespace EmcureNPD.Utility.Helpers {
   // [Route("/notificationHub")]
    public class NotificationHub :Hub {
        public static void Show() {
            IHubContext context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.ShowAllNotifications();
        }
    }
}
