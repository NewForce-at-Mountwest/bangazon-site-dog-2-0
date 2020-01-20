using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BangazonSite.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? PaymentTypeId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public PaymentType PaymentType { get; set; }
        public ApplicationUser User { get; set; }
        [Display (Name = "Order Products")]
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
       
    }
}
