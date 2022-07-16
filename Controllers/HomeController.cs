using Microsoft.AspNetCore.Mvc;
using Stylish.Data;
using Stylish.ViewModel;

namespace Stylish.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
       public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           var banner=_context.Banners.FirstOrDefault();
           var about=_context.Abouts.FirstOrDefault();
           var portfolios=_context.Portfolios.ToList();
           var services = _context.Services.ToList();
            HomeVM vm = new()
            {
                Banner= banner,
                About= about,
                Portfolios= portfolios,
                Services= services,
            };
           return View(vm);
        }
    }
}
