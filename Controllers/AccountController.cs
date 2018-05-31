using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CatsSystem.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace CatsSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.Method == "POST")
            {
                System.Diagnostics.Debug.WriteLine("CHIAMATA POST");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> CheckLogin()
        {
            Users user = dbContext.Users.Where(s => s.Email == Request.Form["Email"] && s.Password == Request.Form["Password"]).Include(s => s.Role).FirstOrDefault();

            if(user is Users)
            {
                //Roles role = dbContext.Roles.Find(user.role_id);
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName+" "+ user.LastName),
                    new Claim(ClaimTypes.Role, user.Role.Description),
                    new Claim("Email", user.Email),

                      
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("About", "Home");
            }
            else
            {
                ViewData["Error"] = "User not found";
                return View("Login");
            }

            
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            if (HttpContext.Request.Method == "POST")
            {
                System.Diagnostics.Debug.WriteLine(Request.Form["firstName"]);

                Roles role = dbContext.Roles.Find(2);

                Users user = new Users();
                user.FirstName = Request.Form["FirstName"];
                user.LastName = Request.Form["LastName"];
                user.Email = Request.Form["Email"];
                user.Password = Request.Form["Password"];
                user.CreatedAt = DateTime.Now;
                user.Role = role;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Request.Form["FirstName"]+" "+ Request.Form["LastName"]),
                new Claim(ClaimTypes.Role, role.Description),
                new Claim("Email", Request.Form["Email"]),
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("About", "Home");
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return View();
            return RedirectToAction("About", "Home");
        }
    }
}