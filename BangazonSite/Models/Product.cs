using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string City { get; set; }

        [Display(Name = "Image")]
        public byte[] ProductImage { get; set; }

        [Display(Name = "Local Delivery")]
        public bool LocalDelivery { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        

        
       
        // Need to add: relationships between ProductType and Product and relationships between OrderProducts and Products


    }
}
