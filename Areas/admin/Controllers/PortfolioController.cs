using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylish.Data;
using Stylish.Models;

namespace Stylish.Areas.admin.Controllers
{
    [Area("admin")]
   
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var portfolio = _context.Portfolios.ToList();
            return View(portfolio);
        }


        //create

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Portfolio portfolio,IFormFile myPhoto)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);
            //get url
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                myPhoto.CopyTo(fs);
            }
           portfolio.PhotoUrl = "image/" + imageName;
            _context.Portfolios.Add(portfolio);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
                 
        }
        //delete
        public IActionResult Delete(int id)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);
            return View(portfolio);
        }

        [HttpPost]
        public IActionResult Delete(int id,Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //detail

        public IActionResult Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);
            if (portfolio == null)
            {

                return NotFound();
            }
            return View(portfolio);
        }

        //edit

        public IActionResult Edit (int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);    
            if (portfolio == null)
            {
                return NotFound();
            }
            return View(portfolio);
        }

        [HttpPost]

        public IActionResult Edit(int id,Portfolio portfolio,IFormFile myPhoto,string? oldPhoto)
        {
            if(myPhoto != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(myPhoto.FileName);
               
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    myPhoto.CopyTo(fs);
                }
                portfolio.PhotoUrl = "image/" + imageName;

            }
            else
            {
                portfolio.PhotoUrl = oldPhoto;
            }

            _context.Portfolios.Update(portfolio);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

    }
}
