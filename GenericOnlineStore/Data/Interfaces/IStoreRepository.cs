using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Data.Interfaces
{
    public interface IStoreRepository
    {
        #region Items
        BaseItem AddItem(BaseItem newItem);
        BaseItem DeleteItem(BaseItem Item);
        BaseItem UpdateItem(BaseItem updatedItem);
        List<BaseItem> GetAllItems( );
    #endregion

    #region purchases
        APurchase AddPurchase(APurchase newItem);
        APurchase DeletePurchase(APurchase Item);
        APurchase DeletePurchase(string ItemId);
        APurchase UpdatePurchase(APurchase updatedItem);
        List<APurchase> GetAllPurchases();
    #endregion


    } 

}