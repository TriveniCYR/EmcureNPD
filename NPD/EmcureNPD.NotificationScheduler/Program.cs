using EmcureNPD.NotificationScheduler.Interfaces;
using EmcureNPD.NotificationScheduler.Services.Implementations;
using EmcureNPD.NotificationScheduler.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
public class Program
{
	public static void Main(string[] args)
	{

		var configBuilder = new ConfigurationBuilder()
			   .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);

		var config = configBuilder.Build();

		var builder = Host.CreateDefaultBuilder(args);
		builder.ConfigureServices(services => {
			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<INotificationScheduler, NotificationScheduler>();
			services.AddScoped<ILoggerService, LoggerService>();
			services.AddScoped<IAPIService, APIService>();
		});

		var app = builder.Build();
		using (var serviceScope = app.Services.CreateScope())
		{
			var services = serviceScope.ServiceProvider;
			var schedulerService = services.GetRequiredService<INotificationScheduler>();
			schedulerService.GetToken();
		}
		app.Run();
	}

}