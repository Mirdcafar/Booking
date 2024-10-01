using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetailsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, string name, string description)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var comment = new Comments
            {
                HotelId = id,
                Name = name,
                Description = description,
                logoImage = "https://ukulell.ru/wp-content/uploads/2023/12/default_logo_user-395.jpg"
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = id });
        }
    }
}
