using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Data.Repository
{
    public class StoreRepository : IStoreRepository
    {
        public ApplicationDbContext _DB { get; set; }
        public StoreRepository(ApplicationDbContext DB)
        {
            _DB = DB;
        }
        public BaseItem AddItem(BaseItem newItem)
        {
            _DB.items.Add(newItem);
            _DB.SaveChanges();

            var dbItem = _DB.items.FirstOrDefault(m =>m.ItemId == newItem.ItemId);
                
            return dbItem;
        }

        public BaseItem DeleteItem(BaseItem Item)
        {
            throw new NotImplementedException();
        }

        public List<BaseItem> GetAllItems()
        {
            return _DB.items.ToList();
        }

        public BaseItem UpdateItem(BaseItem updatedItem)
        {
            throw new NotImplementedException();
        }

        public APurchase AddPurchase(APurchase newItem)
        {
            _DB.purchases.Add(newItem);
            _DB.SaveChanges();

            var dbItem = _DB.purchases.FirstOrDefault(m => m.APurchaseItemID == newItem.APurchaseItemID);

            return dbItem;
        }

        public APurchase DeletePurchase(APurchase Item)
        {
            throw new NotImplementedException();
        }

        public APurchase UpdatePurchase(APurchase updatedItem)
        {
            throw new NotImplementedException();
        }

        public List<APurchase> GetAllPurchases()
        {
            return _DB.purchases.ToList();
        }
    }
}
