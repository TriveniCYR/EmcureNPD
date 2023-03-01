using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public  interface IManagementApproval
    {
        Task<dynamic> GetProjectNameAndStrength(int Pidfid = 0);
    }
}
