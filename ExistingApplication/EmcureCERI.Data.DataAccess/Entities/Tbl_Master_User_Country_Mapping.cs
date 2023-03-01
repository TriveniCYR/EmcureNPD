using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
	public class Tbl_Master_User_Country_Mapping
    {

		public int Id { get; set; }
		public int? UserID { get; set; }
		public int? ContinentID { get; set; }
		public int? CountryID { get; set; }
		public string CountryCode { get; set; } 
		public int? DepartmentID { get; set; }
		public bool? IsActive { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }




	}
}
