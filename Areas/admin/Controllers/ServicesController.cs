using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylish.Data;
using Stylish.Models;

namespace Stylish.Areas.admin.Controllers
{
    [Area("admin")]
   
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

       public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Services services)
        {
            _context.Services.Add(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Edit(int id)
        {
            var services=_context.Services.FirstOrDefault(x=>x.Id==id);
            return View(services);
        }
        [HttpPost]
        public IActionResult Edit(int id,Services services)
        {
           _context.Services.Update(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            return View(service);
        }
        [HttpPost]
        public IActionResult Delete(int id,Services services)
        {
            _context.Services.Remove(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
