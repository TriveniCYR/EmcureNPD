﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public class PidfPbfRnDExicipientPrototypeEntity
	{
		public long ExicipientProtoypeId { get; set; }
		public long PidfPbfGeneralId { get; set; }
		public long BusinessUnitId { get; set; }
		public long StrengthId { get; set; }
		public string ExicipientPrototype { get; set; }
		public double? RsPerKg { get; set; }
		public string MgPerUnitDosage { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}