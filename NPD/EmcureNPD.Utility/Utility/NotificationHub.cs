using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EmcureNPD.Utility.Helpers
{
    // [Route("/notificationHub")]
    public class NotificationHub : Hub
    {
        //public static void Show()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    context.Clients.All.ShowAllNotifications();
        //}
        
        public async Task GetNotification()
        {
          await Clients.All.SendAsync("ReceiveNotification",100);
        }
    }
}