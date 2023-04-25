using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EmcureNPD.Utility.Helpers
{
    // [Route("/notificationHub")]
    public class NotificationHub : Hub
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        //public static void Show()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    context.Clients.All.ShowAllNotifications();
        //}
        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            //_repository = repository;
            _hubContext = hubContext;
        }
        public async Task GetNotification(int count)
        {
          await _hubContext.Clients.All.SendAsync("ReceiveNotification", count);
        }
    }
}