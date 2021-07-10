using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string InventoryId { get; set; }
        public string ItemId { get; set; }
        public int Total { get; set; }
        public int InStock { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
