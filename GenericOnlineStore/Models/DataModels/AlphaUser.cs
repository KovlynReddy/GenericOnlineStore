using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class AlphaUser
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ImagePath { get; set; }
        public string AccountId { get; set; }
        public string AccountHistory { get; set; }
    
    }
}
