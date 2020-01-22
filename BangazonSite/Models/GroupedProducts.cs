using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
        //custom class to hold data for reports for products grouped by category
    public class GroupedProducts
    {
        public string CategoryName { get; set;}
        public int NumberofProducts { get; set;}

        public List<Product> TopProducts { get; set; } = new List<Product>();
    }
}
