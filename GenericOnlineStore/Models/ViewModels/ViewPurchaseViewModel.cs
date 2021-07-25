using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class ViewPurchaseViewModel
    {
        public DateTime TimePurchased { get; set; }
        public List<StoreItemViewModel> ItemsPurchased { get; set; }
    }
}
