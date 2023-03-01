using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class FileManagerController : Controller
    {
        private readonly IDRFFinal _dRFFinal;
        private readonly EmcureCERIDBContext _db;
        public FileManagerController(IDRFFinal Final)
        {
            _db = new EmcureCERIDBContext();
          
            this._dRFFinal = Final;
         
        }

        public IActionResult Index(string folderPath,int DRFID)
        {
            string tempName = "";
            clsPath.FolderRootPath = folderPath;
            var tempresult = (from tdi in _db.Tbl_DRF_Initialization
                              where tdi.InitializationID == DRFID
                              select new { tdi.DRFNo }).FirstOrDefault();
            if(tempresult !=null)
            {
                tempName = tempresult.DRFNo;
            }
            else
            {
                tempName = "";
            }
          
            return RedirectToAction("FileTest","Fileman", new { strfolderPath = clsPath.FolderRootPath, drfID=DRFID, ProjectName= tempName });
           
        }

        //[Authorize]
        [HttpGet]
        [ActionName("GetGeneralCheckList")]
        public ActionResult GetGeneralCheckList(int DRFID)
        {
            //var list = _dRFFinal.GetCheckList(DRFID);
            //return Json(new { data = list });

            IList<ParentNationalCheckList> parentNationalCheckLists = new List<ParentNationalCheckList>();

            parentNationalCheckLists = _dRFFinal.ParentNationalCheckList(DRFID);

            if (parentNationalCheckLists.Count > 0)
            {
                for (int i = 0; i < parentNationalCheckLists.Count; i++)
                {
                    IList<ChildrenNationalCheckList> childrens = new List<ChildrenNationalCheckList>();
                    List<ChildrenNationalCheckList> parentchildrens = new List<ChildrenNationalCheckList>();
                    childrens = _dRFFinal.ChildrenNationalCheckList(DRFID,Convert.ToInt32(parentNationalCheckLists[i].TransactionID));

                    if (childrens.Count > 0)
                    {
                        for (int j = 0; j < childrens.Count; j++)
                        {
                            ChildrenNationalCheckList tempchildrens = new ChildrenNationalCheckList();
                            tempchildrens.Name = childrens[j].Name;
                            tempchildrens.Action = childrens[j].Action;
                            parentchildrens.Add(tempchildrens);
                        }

                        parentNationalCheckLists[i].children = parentchildrens;
                    }

                }
            }

            return Json(new { data = parentNationalCheckLists });

        }

        [Authorize]
        [HttpPost]
        [ActionName("UpdateCheckList")]
        public ActionResult UpdateCheckList(CheckList checkList)
        {
            if (checkList.CheckedIDList.Length > 0)
            {
                var IDList = JsonConvert.DeserializeObject<List<string>>(checkList.CheckedIDList);
                _dRFFinal.UncheckedAllDRFwise(checkList.DRFID, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Now);

                for(int i=0;i<IDList.Count;i++)
                {
                    _dRFFinal.checkedIDsDRFwise(Convert.ToInt32(IDList[i]), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")),DateTime.Now);
                }
            }

            return Json(new { data = "success" }, new JsonSerializerSettings());


        }

    }
}
