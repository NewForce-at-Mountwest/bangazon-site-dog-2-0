using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models.ViewModels
{
    public class IndexGroupProductsViewModel
    {
        public List<GroupedProducts> GroupedProducts { get; set; } = new List<GroupedProducts>();
    }
}
