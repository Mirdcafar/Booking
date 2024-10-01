using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Models;

namespace Shop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Id");
            ViewData["HotelIdValue"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, string Name,DateTime CheckInDate,DateTime CheckOutDate, string Address, string Email, string Phone, string Comment, int TotalPrice)
        {

            var order = new Orders()
            {
                HotelId = id,
                Name = Name,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate,
                Address = Address,
                Email = Email,
                Phone = Phone,
                Comment = Comment,
                TotalPrice = TotalPrice
            };
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
