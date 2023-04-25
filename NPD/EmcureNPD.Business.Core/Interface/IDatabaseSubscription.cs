using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient.Base.EventArgs;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
        void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e);
        void Changed(object sender, RecordChangedEventArgs<MasterNotification> e);
        void Dispose();
    }
}
