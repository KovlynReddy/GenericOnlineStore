using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class TrolleyViewModel
    {
        public List<StoreItemViewModel> ItemsPurchased { get; set; } = new List<StoreItemViewModel>();
        public int FinalTotal { get; set; }
        public DateTime DatePurchased { get; set; }
    }

    public class ChecoutViewModel : TrolleyViewModel
    {

        public bool Confirm { get; set; }
        public DateTime Date { get; set; }

    }
}
