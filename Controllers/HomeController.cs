using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Data;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

namespace Shop.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var hotelList = _context.Hotels.ToList();
        var categoryList = _context.HotelCatigories.ToList();
        var viewModel = new HotelCategoryViewModel
        {
            Hotels = hotelList,
            Categories = categoryList
        };
        ViewBag.Message = DateTime.Now.Day;
        ViewBag.Country = new SelectList(Enum.GetValues(typeof(Country)).Cast<Country>().Select(c => new SelectListItem
        {
            Value = c.ToString(),
            Text = c.ToString()
        }), "Value", "Text");
        return View(viewModel);
    }

    public IActionResult HotelList( Country country, DateTime? checkInDate, DateTime? checkOutDate, int minPrice = 0, int maxPrice = 10000)
    {
        var hotelsQuery = _context.Hotels
                                  .Where(h => h.Price >= minPrice && h.Price <= maxPrice)
                                  .AsQueryable();

        if (country != 0) // Assuming 0 is a default value indicating "no selection"
        {
            hotelsQuery = hotelsQuery.Where(h => h.Country == country);
        }

        // Filter by check-in and check-out dates if provided
       
        //// Filter by date range (availability)
        //if (checkInDate.HasValue && checkOutDate.HasValue)
        //{
        //    hotelsQuery = hotelsQuery.Where(h => h.AvailableFrom <= checkInDate && h.AvailableTo >= checkOutDate);
        //}


        //hotelsQuery = hotelsQuery.Where(h => selectedFilters.Contains(h.Name));

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


    [HttpGet]
    public async Task<IActionResult> Details(int id)
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
    public async Task<IActionResult> Details(int id, string name, string description)
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

        return RedirectToAction(nameof(Details), new { id = id });
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("Id,Icon,Name,Email,Password,IsActive,IsAdmin")] User user)
    {
        if (ModelState.IsValid)
        {
            user.IsActive = true;
            user.IsAdmin = false;
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public IActionResult ListProperty()
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


    public async Task<IActionResult> ListProperty([Bind("Id,Name,Price,ImageUrl,Description,PeopleCount,RoomCount,Rating,Country,HotelCategoriesId")] HotelsDTOModel hotelsDTOModel)
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
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }
        }
        return View();
    }

    public IActionResult FilterHotels()
    {
        return View();
    }
    public async Task<IActionResult> ViewOrders()
    {
        var orders = await _context.Orders.Include(o => o.Hotel).ToListAsync();

        return View(orders);
    }

    public IActionResult Orders(int id)
    {
        ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Id");
        ViewData["HotelIdValue"] = id;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Orders(int id, string Name, string Address, string Email, string Phone, string Comment , int TotalPrice)
    {

        var order = new Orders()
        {
            HotelId = id,
            Name = Name,
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
