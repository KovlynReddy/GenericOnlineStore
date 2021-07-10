using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class BaseAccount
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public string Buffer { get; set; }
        public string AccountHistory { get; set; }
        public int Balance { get; set; }
    }
}
