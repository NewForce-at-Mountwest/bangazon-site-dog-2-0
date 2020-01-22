using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Data;
using BangazonSite.Models;
using BangazonSite.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BangazonSite.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.PaymentType).Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.PaymentType)
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "ApplicationUserId");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentTypeId,ApplicationUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "ApplicationUserId", order.PaymentTypeId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", order.ApplicationUserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            OrderEditViewModel vm = new OrderEditViewModel();
            vm.Order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            vm.PaymentTypes = _context.Orders.Where(o => o.PaymentType.Name != null).Select(o => new SelectListItem
            {
                Value = o.PaymentType.Id.ToString(),
                Text = o.PaymentType.Name
            }).ToList();

            vm.PaymentTypes.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "Please Choose a Payment Type"
            });
            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderEditViewModel vm)
        {
            ModelState.Remove("Order.Id");
            ModelState.Remove("Order.ApplicationUserId");
            ModelState.Remove("PaymentType.Id");
            ModelState.Remove("PaymentType.ApplicationUserId");
            if (ModelState.IsValid)
            {


                //remove any studentexercises from context by id
                // _context.Student.Remove(StudentExercises);
                //add any student exercises to context
                
                    _context.Update(vm.Order);
                    await _context.SaveChangesAsync();
                
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["Payment Type Id"] = new SelectList(_context.Orders, "Id", "Id", vm.PaymentType.Id);
            ViewData["Payment Type Name"] = new SelectList(_context.Orders, "Id", "Id", vm.PaymentType.Name);
            return View(vm);
        }
    


        private Task<ApplicationUser> GetCurrentUserAsync() =>
        
            _userManager.GetUserAsync(HttpContext.User);
        

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.PaymentType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
