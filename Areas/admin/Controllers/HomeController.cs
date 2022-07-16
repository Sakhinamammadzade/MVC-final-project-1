using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Stylish.Areas.admin.Controllers
{
    [Area("admin")]
   
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
