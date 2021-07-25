using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Models.DataModels;
using Microsoft.EntityFrameworkCore;
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
            _DB.purchases.Remove(Item);
            _DB.SaveChanges();
            return Item;    
        }

        public APurchase UpdatePurchase(APurchase updatedItem)
        {


            _DB.Update(updatedItem);
            _DB.Entry(updatedItem).State = EntityState.Modified;

            _DB.SaveChanges();

            return updatedItem;
        }

        public List<APurchase> GetAllPurchases()
        {
            return _DB.purchases.ToList();
        }

        public APurchase DeletePurchase(string ItemId)
        {
            var item = GetAllPurchases().FirstOrDefault(m => m.APurchaseId == ItemId);

            _DB.purchases.Remove(item);
            _DB.SaveChanges();
            return item;
        }
    }
}
