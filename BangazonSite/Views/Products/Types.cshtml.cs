using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;

namespace BangazonSite.Views.Products
{
    public class TypesModel : PageModel
    {
        private readonly BangazonSite.Data.ApplicationDbContext _context;

        public TypesModel(BangazonSite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.User).ToListAsync();
        }
    }
}
