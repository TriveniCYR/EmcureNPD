using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Scheduler.Services.Interfaces {
    public interface ILoggerService 
    {
        void Log(Exception exception);
        void ServiceLog(string message);
    }
}
