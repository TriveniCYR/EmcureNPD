using Microsoft.AspNet.SignalR;
using System.Runtime.InteropServices;

namespace EmcureNPD.Utility.Helpers
{
    // [Route("/notificationHub")]
    public class NotificationHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.ShowAllNotifications();
        }
    }
}