using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class StoreItemViewModel
    {
        public BaseItem item { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
        public string ImagePath { get; set; }

        public static StoreItemViewModel ToViewModel(BaseItem baseitem) {

            StoreItemViewModel model = new StoreItemViewModel();
            model.item = baseitem;
            model.Quantity = 0;
            model.SubTotal = 0;
            model.ImagePath = baseitem.ImagePath;

            return model;

        }
    }
}
