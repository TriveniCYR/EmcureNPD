using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    
    public class PIDFFinanceProjectionEntity
	{
		public int FinanceProjectionId { get; set; }
		public int PidffinaceId { get; set; }
		public string BusinessUnitId { get; set; }
		public string Pidfid { get; set; }
		public string Year { get; set; }
		public double? Expiries { get; set; }
		public double? AnnualFee { get; set; }
		public double? AnnualConfirmatoryRelease { get; set; }

	}
}
