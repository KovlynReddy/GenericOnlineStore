using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class BaseItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Value { get; set; }
        public int Genre { get; set; }
        public string StockId { get; set; }
        public string ImagePath { get; set; }

    }
}
