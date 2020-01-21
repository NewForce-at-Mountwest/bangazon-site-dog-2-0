using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonSite.Models
{
    public class CreateProductViewModel
    {
        public List<SelectListItem> ProductType { get; set; }
        public Product Product { get; set; }


    }
}