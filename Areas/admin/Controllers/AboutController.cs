using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylish.Data;
using Stylish.Models;

namespace Stylish.Areas.admin.Controllers
{
    [Area("admin")]

    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var about=_context.Abouts.FirstOrDefault(); 
            return View(about);
        }
        public IActionResult Create()
        { 
            var about = _context.Abouts.FirstOrDefault();
            if (about != null)
            {
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
           var about = _context.Abouts.FirstOrDefault(a => a.Id == id);
            if (about == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return View(about);
        }
        [HttpPost]
        public IActionResult Delete(About about)
        {
            if (about == null)
            {
                return RedirectToAction("Index");
            
             }
            _context.Abouts.Remove(about);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var about = _context.Abouts.FirstOrDefault(a => a.Id == id);    
            return View(about);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var about=_context.Abouts.FirstOrDefault(a => a.Id == id);
            if(about == null)
            {
                return NotFound();
            }
            return View(about);
        }
        [HttpPost]
        public IActionResult Edit(About About)
        {
            _context.Abouts.Update(About);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
