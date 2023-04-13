using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class EmailDetailsVM
    {
        public string ToMail { get; set; }
        public List<string> CCMail { get; set; }
        public List<string> BCCMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string DispalyName { get; set; }
    }

    public class SMTPEntityViewModel
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string EnableSsl { get; set; }
        public string FromEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
