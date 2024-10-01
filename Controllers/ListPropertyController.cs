using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers
{
    public class ListPropertyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListPropertyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["HotelCategoriesId"] = new SelectList(_context.HotelCatigories, "Id", "Id");
            ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)).Cast<Country>().Select(c => new SelectListItem
            {
                Value = c.ToString(),
                Text = c.ToString()
            }), "Value", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Index([Bind("Id,Name,Price,ImageUrl,Description,PeopleCount,RoomCount,Rating,Country,HotelCategoriesId")] HotelsDTOModel hotelsDTOModel)
        {
            Hotels hotel;
            if (ModelState.IsValid)
            {
                hotel = new Hotels
                {
                    Name = hotelsDTOModel.Name,
                    Price = hotelsDTOModel.Price,
                    ImageUrl = hotelsDTOModel.ImageUrl,
                    Description = hotelsDTOModel.Description,
                    PeopleCount = hotelsDTOModel.PeopleCount,
                    RoomCount = hotelsDTOModel.RoomCount,
                    Rating = hotelsDTOModel.Rating,
                    Country = hotelsDTOModel.Country,
                    HotelCategoriesId = hotelsDTOModel.HotelCategoriesId
                };

                _context.Add(hotel);
                await _context.SaveChangesAsync();
                ViewData["HotelCategoriesId"] = new SelectList(_context.HotelCatigories, "Id", "Id", hotel.HotelCategoriesId);
                ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)).Cast<Country>().Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                }), "Value", "Text", hotel.Country);
                return RedirectToAction(nameof(Index));
            }

            return View(hotelsDTOModel);
        }

    }
}
