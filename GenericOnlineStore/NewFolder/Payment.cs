using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.NewFolder
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public string PaymentId { get; set; }
        public string cardNumber { get; set; }
       public int Month      { get; set; }
       public int Yeat       { get; set; }
       public int Day        { get; set; }
       public string Cvc        { get; set; }
        public int Value       { get; set; }
    }
}
