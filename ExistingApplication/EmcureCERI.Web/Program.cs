using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Web.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmcureCERI.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

            //--<set upload large files> ---
            .UseIISIntegration()

            .UseKestrel(options =>
            {
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(240);
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(240);
            })

                //--< /set upload large files> ---


                .UseStartup<Startup>();

    }
}
