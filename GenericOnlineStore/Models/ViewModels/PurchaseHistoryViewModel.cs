using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class PurchaseHistoryViewModel
    {
        public List<ViewPurchaseViewModel> PurchaseHistory { get; set; } = new List<ViewPurchaseViewModel>();
    }
}
