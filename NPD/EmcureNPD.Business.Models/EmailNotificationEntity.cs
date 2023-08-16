using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public class EmailNotificationEntity
	{
		public int NotificationId { get; set; }
		public string NotificationTitle { get; set; }
		public DateTime CreatedDate { get; set; }
		public string PIDFStatus { get; set; }
		public string PidfNo { get; set; }
		public string CreatedByName { get; set; }
		public string EmailAddress { get; set; }
		public string SendToName { get; set; }
		public bool IsEmailSent { get; set; }
		public DateTime? SentDatetime { get; set; }

		public string LogMessage { get; set; }
	}
}
