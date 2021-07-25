using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public int Total { get; set; }

        public List<ATransaction> purchaseHistory { get; set; } = new List<ATransaction>();
        //public List<ViewPurchaseViewModel> purchaseHistory { get; set; } = new List<ViewPurchaseViewModel>();
        // add history to model
    }
}
