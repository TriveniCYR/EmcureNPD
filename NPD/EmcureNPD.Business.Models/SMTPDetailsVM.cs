namespace EmcureNPD.Web.Models
{
    public class SMTPDetailsVM
    {
        public string AliasName { get; set; }
        public string HostName { get; set; }
        public string FromMail { get; set; }
        public string FromPassword { get; set; }
        public bool IsEnableSSL { get; set; }
        public int PortNumber { get; set; }
        public bool IsMailStatus { get; set; }
        public bool IsDefaultCredentials { get; set; }
        public bool IsWithoutPassword { get; set; }
    }
}
