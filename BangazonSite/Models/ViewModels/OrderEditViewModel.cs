using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BangazonSite.Models.ViewModels
{
    public class OrderEditViewModel
    {
        public List<SelectListItem> PaymentTypes { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
