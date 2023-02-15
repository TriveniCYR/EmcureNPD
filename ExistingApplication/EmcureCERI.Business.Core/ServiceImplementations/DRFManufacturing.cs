using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFManufacturing : IDRFManufacturing
    {
        private readonly EmcureCERIDBContext _db;

        public DRFManufacturing()
        {
            _db = new EmcureCERIDBContext();
        }

        public IList<Tbl_DRF_Manufacturing> GetManufacturingFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_Manufacturing> result = new List<Tbl_DRF_Manufacturing>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "MANUFACTURE")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Manufacturing>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFManufacturing(Tbl_DRF_Manufacturing entity)
        {
            int result = 0;
            try
            {
                _db.LoadStoredProc("USP_InsertDRFManufacturingDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ManufacturingSiteId", entity.ManufacturingSiteId)
                .WithSqlParam("@ManufacturingSiteName", entity.ManufacturingSiteName)
                .WithSqlParam("@APIId", entity.APIId)
                .WithSqlParam("@APISite", entity.APISiteName)
                .WithSqlParam("@Batchsize", entity.Batchsize)
                .WithSqlParam("@Leadtime", entity.Leadtime)
                .WithSqlParam("@UnitEXW", entity.UnitEXW)
                .WithSqlParam("@ArtworkTypeId", entity.ArtworkTypeId)
                //  .WithSqlParam("@TentativeSchedule", entity.TentativeSchedule)
                .WithSqlParam("@Tentative_Artwork_Lead_Time", entity.Tentative_Artwork_Lead_Time)

                .WithSqlParam("@PackorShipper", entity.PackorShipper)
                .WithSqlParam("@GrossWeight", entity.GrossWeight)
                .WithSqlParam("@Dimensions", entity.Dimensions)

                .WithSqlParam("@MWidth", entity.MWidth)
                .WithSqlParam("@MHeight", entity.MHeight)
                .WithSqlParam("@MLength", entity.MLength)

                .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                   .ExecuteStoredProc((handler) =>
                   {
                       result = Convert.ToInt32(handler.ReadToValue<int>());
                   });
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public int updateDRFManufacturing(Tbl_DRF_Manufacturing entity)
        //{
        //    try
        //    {
        //        _db.LoadStoredProc("USP_UpdateDRFManufacturingDetails")
        //        .WithSqlParam("@InitializationId", entity.InitializationId)
        //        .WithSqlParam("@ManufacturingSiteId", entity.ManufacturingSiteId)
        //        .WithSqlParam("@ManufacturingSiteName", entity.ManufacturingSiteName)
        //        .WithSqlParam("@APIId", entity.APIId)
        //        .WithSqlParam("@APISite", entity.APISiteName)
        //        .WithSqlParam("@Batchsize", entity.Batchsize)
        //        .WithSqlParam("@Leadtime", entity.Leadtime)
        //        .WithSqlParam("@UnitEXW", entity.UnitEXW)
        //        .WithSqlParam("@ArtworkTypeId", entity.ArtworkTypeId)
        //        //.WithSqlParam("@TentativeSchedule", entity.TentativeSchedule)
        //         .WithSqlParam("@Tentative_Artwork_Lead_Time", entity.Tentative_Artwork_Lead_Time)
        //         .WithSqlParam("@PackorShipper", entity.PackorShipper)
        //        .WithSqlParam("@GrossWeight", entity.GrossWeight)
        //        .WithSqlParam("@Dimensions", entity.Dimensions)
        //        .WithSqlParam("@Remark", entity.Remark)
        //        .WithSqlParam("@Createdby", entity.CreatedBy)
        //        .WithSqlParam("@CreatedDate", entity.CreatedDate)
        //        .ExecuteStoredNonQuery();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}


        //NEW CODE BY SONALI GORE

        public int updateDRFManufacturing(DRFManufHeaderAndDetails entity)
        {

            int result = 0;
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFManufacturingDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ManufacturingSiteId", entity.ManufacturingSiteId)
                .WithSqlParam("@ManufacturingSiteName", entity.ManufacturingSiteName)
                .WithSqlParam("@APIId", entity.APIId)
                .WithSqlParam("@APISite", entity.APISiteName)
                .WithSqlParam("@Batchsize", entity.Batchsize)
                .WithSqlParam("@Leadtime", entity.Leadtime)
                .WithSqlParam("@UnitEXW", entity.UnitEXW)
                .WithSqlParam("@ArtworkTypeId", entity.ArtworkTypeId)
                //.WithSqlParam("@TentativeSchedule", entity.TentativeSchedule)
                 .WithSqlParam("@Tentative_Artwork_Lead_Time", entity.Tentative_Artwork_Lead_Time)
                 .WithSqlParam("@PackorShipper", entity.PackorShipper)
                .WithSqlParam("@GrossWeight", entity.GrossWeight)
                .WithSqlParam("@Dimensions", entity.Dimensions)
                .WithSqlParam("@MWidth", entity.MWidth)
                .WithSqlParam("@MHeight", entity.MHeight)
                .WithSqlParam("@MLength", entity.MLength)

                .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = Convert.ToInt32(handler.ReadToValue<int>());
                 });

                for (int i = 0; i < entity.Tbl_DRF_Manufacturing_APISite.Count; i++)
                {
                    _db.LoadStoredProc("USP_InsertManufacturing_APISite")
                  .WithSqlParam("@MAPIID", entity.Tbl_DRF_Manufacturing_APISite[i].MAPIID)
                  .WithSqlParam("@ManufacturingSiteId", entity.Tbl_DRF_Manufacturing_APISite[i].ManufacturingSiteId)
                 .WithSqlParam("@APIId", entity.Tbl_DRF_Manufacturing_APISite[i].APIId)
                 .WithSqlParam("@APISiteName", entity.Tbl_DRF_Manufacturing_APISite[i].APISiteName)
                 .WithSqlParam("@APIName", entity.Tbl_DRF_Manufacturing_APISite[i].APIName)
                 .WithSqlParam("@IsActive", entity.Tbl_DRF_Manufacturing_APISite[i].IsActive)
                 .WithSqlParam("@CreatedBy", entity.Tbl_DRF_Manufacturing_APISite[i].CreatedBy)
                 .WithSqlParam("@ModifiedBy", entity.Tbl_DRF_Manufacturing_APISite[i].ModifiedBy)

                    .ExecuteStoredNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public IList<Tbl_DRF_Manufacturing_APISite> GetManufacturingAPIListDetails(int InitializationID)
        {
            IList<Tbl_DRF_Manufacturing_APISite> result = new List<Tbl_DRF_Manufacturing_APISite>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "MANUFACTUREAPI")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Manufacturing_APISite>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public int insertDRFManufacturingAPISite(List<Tbl_DRF_Manufacturing_APISite> entity)
        {
            int result = 0;
            try
            {
                for (int i = 0; i < entity.Count; i++)
                {
                    _db.LoadStoredProc("USP_InsertManufacturing_APISite")
                        .WithSqlParam("@MAPIID", entity[i].MAPIID)
                        .WithSqlParam("@ManufacturingSiteId", entity[i].ManufacturingSiteId)
                 .WithSqlParam("@APIId", entity[i].APIId)
                 .WithSqlParam("@APISiteName", entity[i].APISiteName)
                 .WithSqlParam("@APIName", entity[i].APIName)
                 .WithSqlParam("@IsActive", entity[i].IsActive)
                 .WithSqlParam("@CreatedBy", entity[i].CreatedBy)
                 .WithSqlParam("@ModifiedBy", entity[i].ModifiedBy)
                  .ExecuteStoredProc((handler) =>
                  {
                      result = Convert.ToInt32(handler.ReadToValue<int>());
                  });
                }
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


    }
}
