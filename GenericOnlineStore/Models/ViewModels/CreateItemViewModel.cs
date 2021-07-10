using GenericOnlineStore.Models.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.ViewModels
{
    public class CreateItemViewModel : BaseItem
    {
        public IFormFile uploadedImage { get; set; }
    }
}
