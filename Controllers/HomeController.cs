using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using CatsSystem.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CatsSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Cart()
        {
            String email = User.FindFirst("Email").Value;
            Users user = dbContext.Users.Where(s => s.Email == email).FirstOrDefault();
            if(!(user is Users))
            {
                ViewData["Message"] = "Your cart is empty.";
            }
            //List<Carts> carts = await dbContext.Carts.Where(s => s.UserId == user.Id).Include(s => s.Cat).ToListAsync();
            return View(await dbContext.Carts.Where(s => s.UserId == user.Id).Include(s => s.Cat).ToListAsync());

            //return View();
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            String email = User.FindFirst("Email").Value;
           
            Users user = dbContext.Users.Where(s => s.Email == email).FirstOrDefault();
            Carts cart = new Carts();
            cart.CatId = (int)id;
            cart.UserId = user.Id;

            dbContext.Carts.Add(cart);
            dbContext.SaveChanges();

            //return View("~/Views/Cats/Index.cshtml");
            return RedirectToAction("Index", "Cats");

        }


        public async Task<IActionResult> RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            String email = User.FindFirst("Email").Value;

            Users user = dbContext.Users.Where(s => s.Email == email).FirstOrDefault();
            Carts cart = dbContext.Carts.Where(s => s.UserId == user.Id && s.CatId == id).FirstOrDefault();
            
            //var cats = await _context.Cats.SingleOrDefaultAsync(m => m.Id == id);
            dbContext.Carts.Remove(cart);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Cart", "Home");
            
            //return View("~/Views/Cats/Index.cshtml");
            //return RedirectToAction("Index", "Cats");

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
