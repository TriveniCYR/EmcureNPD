using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DashboardReportService : IDashboardReportService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public DashboardReportService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }
        public DashboardDetails dashboardDetails (string FromDate, string Todate,string CountryName, int CompanyID)
        {
           
            DashboardDetails result = new DashboardDetails();
            IList<DashboardTabData> dashboardTabData = new List<DashboardTabData>();
            IList<DashboardWorldMapData> dashboardWorldMapDatas = new List<DashboardWorldMapData>();
            try
            {
                DataSet dsRequest1 = new DataSet();

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(_db.Database.GetDbConnection().ConnectionString))
                {
                    System.Data.SqlClient.SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("USP_Report_Dashboard", conn);
                    sqlComm.Parameters.Add(new SqlParameter("FromDate", FromDate));
                    sqlComm.Parameters.Add(new SqlParameter("ToDate", Todate));
                    sqlComm.Parameters.Add(new SqlParameter("CountryName", CountryName));
                    sqlComm.Parameters.Add(new SqlParameter("CompanyID", CompanyID));

                    sqlComm.CommandTimeout = 0;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter();

                    da1.SelectCommand = sqlComm;
                    da1.Fill(dsRequest1);
                }

                dashboardTabData = ConvertDataTable<DashboardTabData>(dsRequest1.Tables[0]);
                dashboardWorldMapDatas = ConvertDataTable<DashboardWorldMapData>(dsRequest1.Tables[1]);
                List<DashboardWorldMapData> test = ConvertDataTable<DashboardWorldMapData>(dsRequest1.Tables[1]);
                foreach (var ddata in dashboardTabData)
                {
                    result.dashboardTabDatas.totalInitial = ddata.totalInitial;
                    result.dashboardTabDatas.totalRejected = ddata.totalRejected;
                    result.dashboardTabDatas.totalInitialApproved = ddata.totalInitialApproved;
                    result.dashboardTabDatas.totalPartialApproved = ddata.totalPartialApproved;
                    result.dashboardTabDatas.totalPendingFinanceApproval = ddata.totalPendingFinanceApproval;
                    result.dashboardTabDatas.totalFinanceApproved = ddata.totalFinanceApproved;
                    result.dashboardTabDatas.totalFinalApproved = ddata.totalFinalApproved;
                    result.dashboardTabDatas.totalFinalRejected = ddata.totalFinalRejected;
                }
                result.dashboardWorldMapDatas = dashboardWorldMapDatas;

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
           
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
