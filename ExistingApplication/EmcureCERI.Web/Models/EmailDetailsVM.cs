using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
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
}
