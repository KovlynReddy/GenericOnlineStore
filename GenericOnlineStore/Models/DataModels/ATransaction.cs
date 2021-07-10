using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Models.DataModels
{
    public class ATransaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string CardNumber { get; set; }
        public string Password { get; set; }
        public int Value { get; set; }
        public string ExtraDetails { get; set; }
        public string AccountId { get; set; }
        public string AccountHistory { get; set; }
        public string buffer { get; set; }
    }
}
