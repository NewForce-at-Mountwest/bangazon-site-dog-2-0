using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;
using Microsoft.AspNetCore.Identity;
using BangazonSite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BangazonSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;

            _userManager = userManager;

        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Products
        public async Task<IActionResult> Index(string searchQuery)
        {
            ApplicationUser loggedInUser = await GetCurrentUserAsync();
            List<Product> products = await _context.Products.Where(p => p.User == loggedInUser).ToListAsync();
            if (searchQuery != null)
            {
                products = products.Where(product => product.Title.ToLower().Contains(searchQuery) || product.Description.ToLower().Contains(searchQuery)).ToList();
            }
            return View(products);
        }



        //// GET: Students
        //public async Task<IActionResult> Index(string searchQuery)
        //{

        //    ApplicationUser loggedInUser = await GetCurrentUserAsync();
        //    List<Student> students = await _context.Student.Include(s => s.Cohort).Where(s => s.User == loggedInUser).ToListAsync();

        //    if (searchQuery != null)
        //    {
        //        students = students.Where(student => student.FirstName.Contains(searchQuery) || student.LastName.Contains(searchQuery)).ToList();
        //    }


        //    return View(students);
        //}










        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel vm = new CreateProductViewModel();
            vm.ProductType = _context.ProductTypes.Select(pt => new SelectListItem
            {
                Value = pt.Id.ToString(),
                Text = pt.Name
            }).ToList();

            vm.ProductType.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "Please choose a product type"
            });
            return View(vm);
        }


        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel viewmodel)
        {

            ModelState.Remove("Product.UserId");

            if (ModelState.IsValid)
            {
                ApplicationUser loggedInUser = await GetCurrentUserAsync();
                viewmodel.Product.UserId = loggedInUser.Id;

                _context.Add(viewmodel.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { viewmodel.Product.Id });
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", product.UserId);
            return View(viewmodel);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,Description,Title,Price,Quantity,UserId,City,ProductImage,LocalDelivery")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // method to see products by category
        public async Task<IActionResult> Types()
        {
            IndexGroupProductsViewModel vm = new IndexGroupProductsViewModel();
            //list of product types
            var productTypes = _context.ProductTypes
            //include the products
            .Include(p => p.Products).ToList();
            //I have alist of product types, need to convert to grouped list
            var groupedProducts = new List<GroupedProducts>();
            foreach (ProductType p in productTypes)
            {
                groupedProducts.Add(new GroupedProducts()
                {
                    CategoryName = p.Name,
                    NumberofProducts = p.Products.Count(),
                    TopProducts = p.Products.Take(3).ToList()
                });
            }

            vm.GroupedProducts = groupedProducts;
            return View(vm);
        }
    }
}
