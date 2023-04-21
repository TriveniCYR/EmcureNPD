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
        //ProductRepository productRepository;
        //public DashboardHub(IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("ProductDbContext");
        //    productRepository = new ProductRepository(connectionString);

        //}

        public async Task GetLocation()
        {
            string latitude = "12.12345";
            string longitude = "24.54321";
            await Clients.All.SendAsync("ReceiveLocation", latitude, longitude);
        }
    }
}