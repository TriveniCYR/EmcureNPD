using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class UploadService : IUploadService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        IHostingEnvironment _env;
        private readonly IUploadService _uploadService;
        public UploadService(IConfiguration config, IHostingEnvironment env, IUploadService uploadService)
        {
            _db = new EmcureCERIDBContext();
            _config = config;
            _uploadService = uploadService;
            _env = env;
        }

        public bool ProductMasterDataDataUpload(DataSet productDataSet,int UserID)
        {
            try
            {
                int tempUserID = UserID;
                int batchsize = 1000;
                SqlConnection con;
                string myconn = _config.GetSection("ConnectionStrings:DefaultConnection").Value;

                con = new SqlConnection(myconn);

                foreach (DataTable table in productDataSet.Tables)
                {
                    string tablename = table.TableName;
                    DataTable dataTable = table;

                    if (tablename.ToLower() == "product master")
                    {
                        SqlBulkCopy objbulk = new SqlBulkCopy(con);
                        objbulk.DestinationTableName = "Tbl_UploadProductMasterData";
                        objbulk.BatchSize = batchsize;

                        objbulk.ColumnMappings.Add("Generic Name", "GenericName");//
                        objbulk.ColumnMappings.Add("Brand Name", "BrandName");//
                        objbulk.ColumnMappings.Add("Form", "FormName");//
                        objbulk.ColumnMappings.Add("Strength", "Strength");//
                        objbulk.ColumnMappings.Add("Pack Style", "PackStyle");//
                        objbulk.ColumnMappings.Add("Pack Size", "PackSize");//
                        objbulk.ColumnMappings.Add("Plant", "PlantName");//
                        objbulk.ColumnMappings.Add("Product Type", "ProductType");//

                        objbulk.ColumnMappings.Add(tempUserID, "CreatedBy");//
                        objbulk.ColumnMappings.Add(Convert.ToString(DateTime.Now), "CreatedDate");//
                        
                        //objbulk.ColumnMappings.Add("", "");//

                        //inserting Datatable Records to DataBase    
                        con.Open();
                        objbulk.WriteToServer(dataTable);
                        con.Close();
                    }                                                           
                }                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int TruncateProductMasterBulkData(string typeData)
        {
            throw new NotImplementedException();
        }
    }
}
