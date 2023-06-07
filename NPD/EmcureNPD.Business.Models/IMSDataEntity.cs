using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class IMSDataEntity
    {
        public long PidfimsdataId { get; set; }
        public long Pidfid { get; set; }
        public double Imsvalue { get; set; }
        public double Imsvolume { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
       //public List<IMSDataEntity> IMSDataEntities { get; set; }
    }
}
