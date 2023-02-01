using EmcureNPD.Schedule.Services.Implementations;
using EmcureNPD.Schedule.Services.Interfaces;
using EmcureNPD.Scheduler.Services.Implementations;
using EmcureNPD.Scheduler.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmcureNPD.Scheduler {
    public class Program {
        public static void Main(string[] args) {

            var configBuilder = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);

            var config = configBuilder.Build();

            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(services => {
                services.AddHttpContextAccessor();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<ISchedulerService, SchedulerService>();
                services.AddScoped<ILoggerService, LoggerService>();
                services.AddScoped<IAPIService, APIService>();
            });
            
            var app = builder.Build();
            using (var serviceScope = app.Services.CreateScope()) {
                var services = serviceScope.ServiceProvider;
                var schedulerService = services.GetRequiredService<ISchedulerService>();
                schedulerService.GetToken();
            }
            app.Run();

        }
        //public static void RunSchedule() {

        //    string path = @"D:\EmcureNPDRepo\NPD\EmcureNPD.Scheduler\SchedulerTask.txt";

        //    using (StreamWriter writer = new StreamWriter(path, true)) {
        //        writer.WriteLine($"{DateTime.Now.ToString()} - running from task");
        //    }
        //}
    }
}
