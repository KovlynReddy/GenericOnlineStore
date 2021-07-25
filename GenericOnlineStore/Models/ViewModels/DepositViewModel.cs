using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class DepositViewModel
    {
        [Key]
        public int Key { get; set; }
        public string AccountId { get; set; }
        public DateTime TimeDeposited { get; set; }
        public int Amount { get; set; }
    }
}
