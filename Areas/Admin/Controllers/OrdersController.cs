using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers;
[Authorize(Roles = "Admin")]
[Area("Admin")]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Orders.Include(o => o.Hotel);
        return View(await applicationDbContext.ToListAsync());
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Hotel)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order != null)
        {
            if (order.CheckOutDate >= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Orders with past check-in dates cannot be deleted.");
                return View(order);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
