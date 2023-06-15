using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public class SessionManagerEntity
	{
		public int TokenId { get; set; }
		public int UserId { get; set; }
		public string Email { get; set; }
		public DateTime? TokenIssuedAt { get; set; }
		public DateTime? VallidTo { get; set; }
		public string UserToken { get; set; }
	}
}
