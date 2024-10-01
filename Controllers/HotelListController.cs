using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers
{
    public class HotelListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelListController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(Country country, DateTime? checkInDate, DateTime? checkOutDate, int minPrice = 0, int maxPrice = 10000)
        {
            var hotelsQuery = _context.Hotels
                                      .Where(h => h.Price >= minPrice && h.Price <= maxPrice)
                                      .AsQueryable();

            if (country != 0) 
            {
                hotelsQuery = hotelsQuery.Where(h => h.Country == country);
            }


            var filteredHotels = hotelsQuery.ToList();

            var categoryList = _context.HotelCatigories.ToList();

            var viewModel = new HotelCategoryFilterViewModel
            {
                Hotels = filteredHotels,
                Categories = categoryList,
            };

            ViewBag.Message = DateTime.Now.Day;
            ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country))
                                              .Cast<Country>()
                                              .Select(c => new SelectListItem
                                              {
                                                  Value = c.ToString(),
                                                  Text = c.ToString()
                                              }), "Value", "Text");

            return View(viewModel);
        }


    }
}
