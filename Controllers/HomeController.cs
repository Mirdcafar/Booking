using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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

    public IActionResult Index( Country country)
    {
        var hotelList = _context.Hotels.Where(h => h.Country == country).ToList();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



