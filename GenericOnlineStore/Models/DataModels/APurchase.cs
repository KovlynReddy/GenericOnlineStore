using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class APurchase
    {
        [Key]
        public int Id { get; set; }
        public string Buffer { get; set; }
        public string APurchaseItemID { get; set; } // item in trolley link
        public string APurchaseId { get; set; } // trolley link     
        public string ItemId { get; set; } // item link 
        public string UserId { get; set; } // payer link
        public string PurchaseDetails { get; set; } // details 
        public DateTime TimePurchased { get; set; } // time purched 
        public int Quantity { get; set; } // quantityPurchased

    }
}
