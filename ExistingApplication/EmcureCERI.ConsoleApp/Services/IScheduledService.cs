using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmcureCERI.ConsoleApp.Services
{
    public interface IScheduledService
    {
        Task SendEmails();
    }
}
