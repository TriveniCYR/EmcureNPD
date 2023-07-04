using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IWishList
    {
        Task<List<WishListEntity>> GetAll();

        Task<TblWishList> GetById(int id);

        Task<DBOperation> AddUpdateWishList(WishListEntity model);

        Task<dynamic> GetWishListByTypeId(int id = 0);
    }
}
