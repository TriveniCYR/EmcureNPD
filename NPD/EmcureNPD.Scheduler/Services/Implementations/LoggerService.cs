﻿using EmcureNPD.Scheduler.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EmcureNPD.Scheduler.Services.Implementations
{
    public class LoggerService : ILoggerService
    {
        private IConfiguration configuration;
        public LoggerService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void Log(Exception ex)
        {

            string strPath = configuration.GetSection("Logs:LogFile").Value;//Environment.CurrentDirectory +
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }

        public void ServiceLog(string message)
        {
            string strPath = configuration.GetSection("Logs:LogFile").Value;//Environment.CurrentDirectory + 
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine(message);
            }
        }
    }
}
