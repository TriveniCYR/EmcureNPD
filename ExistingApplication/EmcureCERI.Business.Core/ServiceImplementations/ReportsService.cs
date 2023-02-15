using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    class ReportsService : IReportsService
    {


        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public ReportsService(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env)
        {
            _db = db;
            _config = config;
            _env = env;
        }


        public object GetAllReportList()
        {
            try
            {
                IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();

                _db.LoadStoredProc("USP_Reports_MainPage")

                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Initialization>();
                });

                List<Object> result1 = (from x in result
                                        select (Object)new
                                        {
                                            SrNo = x.SrNo,
                                            //MoleculeNo = "<a href = '/NewProject/summary?MoleculeID=" + x.MoleculeID + "' data-toggle='tooltip' title='Molecule Details' >" + x.MoleculeNo + "</a>",
                                            //Molecule_Name = "<a href = '/NewProject/summary?MoleculeID=" + x.InitializationID + "' data-toggle='tooltip' title='Molecule Details' >" + x.GenericName + "</a>",
                                            DRFNO = x.DRFNo,
                                            Molecule_Name = x.GenericName,
                                            Country = x.Country,
                                            Pharmaceutical_FormName = x.Pharmaceutical_Form,


                                            //Priority = x.Priority,
                                            Strength = x.Strength_Name,
                                            InHouse_or_third_Party = x.Partner,
                                            Project_Status = x.Status,
                                            FirstYearSaleQty = x.FirstYearForecastUnitsPacks,
                                            SecondYearSaleQty = x.SecondYearForecastUnitsPacks,
                                            ThirdYearSaleQty = x.ThirdYearForecastUnitsPacks,

                                            FirstYearSaleUnitPrice = x.FirstYearForecastPriceToPatient,
                                            SecondYearSaleUnitPrice = x.SecondYearForecastPriceToPatient,
                                            ThirdYearSaleUnitPrice = x.ThirdYearForecastPriceToPatient,
                                            FirstYearAPIQuantity = x.FirstYearAPIQuantity,
                                            SecondYearAPIQuantity = x.SecondYearAPIQuantity,
                                            ThirdYearAPIQuantity = x.ThirdYearAPIQuantity,

                                        }).ToList();

                return result1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public IList<Tbl_DRF_Initialization> GetAllReportList1()
        {
            IList<Tbl_DRF_Initialization> result = new List<Tbl_DRF_Initialization>();
            try
            {
                _db.LoadStoredProc("USP_Reports_MainPage")
                 //.WithSqlParam("@ProjectName", "")
                 //.WithSqlParam("@ProjectID", 0)
                 //.WithSqlParam("@StatusID", 0)
                 //.WithSqlParam("@TaskName", "")
                 //.WithSqlParam("@TaskDateTime", "")
                 //.WithSqlParam("@workhours", "")
                 //.WithSqlParam("@InTime", "")
                 //.WithSqlParam("@OutTime", "")
                 //.WithSqlParam("@Comment", "")

                 //.WithSqlParam("@IsActive", false)
                 //.WithSqlParam("@CreatedBy", 0)
                 //.WithSqlParam("@ModifiedBy", 0)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_DRF_Initialization>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public IList<DRFHistoryDetails> GetDRFHistoryDetailsByID(int DRFID)
        {
            IList<DRFHistoryDetails> result = new List<DRFHistoryDetails>();
            try
            {
                _db.LoadStoredProc("USP_Report_DRF_History_Details")
                    .WithSqlParam("@InitializationID", DRFID)                   
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<DRFHistoryDetails>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<TBL_Initialization_ListForManF> GetReportofManfacturing()
        {
            IList<TBL_Initialization_ListForManF> result = new List<TBL_Initialization_ListForManF>();
            try
            {
                _db.LoadStoredProc("USP_ReporT_ManufacturingDetails")

                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<TBL_Initialization_ListForManF>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<TBL_Initialization_List> GetReportofMoleculeTimeline()
        {
            IList<TBL_Initialization_List> result = new List<TBL_Initialization_List>();
            try
            {
                _db.LoadStoredProc("USP_GetReportsMoleculeStatusTimeLine")
                
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<TBL_Initialization_List>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}

