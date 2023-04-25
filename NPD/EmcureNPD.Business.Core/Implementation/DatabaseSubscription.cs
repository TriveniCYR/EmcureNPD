using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility;
using EmcureNPD.Utility.Helpers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace EmcureNPD.Business.Core.Implementation
{
    public class DatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IHubContext<NotificationHub> _hubContext;
        SqlTableDependency<MasterNotification> _tableDependency;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IDatabaseSubscription _databaseSubscription;
        private IRepository<MasterNotification> _repository { get; set; }
        NotificationHub notificationHub;
        //private readonly string _connectionstring;
        public DatabaseSubscription(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext,INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<MasterNotification>();
            _hubContext = hubContext;
            _notificationService = notificationService;
            _databaseSubscription.Configure(DatabaseConnection.NPDDatabaseConnection);
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<MasterNotification>(connectionString, "Master_Notification", null, null, null, null, DmlTriggerType.Insert);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            //Console.WriteLine("Waiting for receiving notifications...");
        }

        public void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            //Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        public async void Changed(object sender, RecordChangedEventArgs<MasterNotification> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity
                var changedEntity = e.Entity;
               // _hubContext.Clients.All.SendAsync("UpdateCatalog", _repository.GetAllAsync());
                var pendingnotification = await _notificationService.NotificationCountForUser();
                //await _hubContext.Clients.All.SendAsync("ReceiveNotification", pendingnotification.Count);
               await notificationHub.GetNotification(pendingnotification.Count);
            }
        }

        #region IDisposable

        ~DatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}

