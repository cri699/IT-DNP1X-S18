using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatsSystem;
using CatsSystem.Data.Entities;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CatsSystem.Controllers
{
    public class CatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public CatsController(ApplicationDbContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;
            _environment = IHostingEnvironment;
        }

       

        // GET: Cats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cats.ToListAsync());
        }

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cats = await _context.Cats
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cats == null)
            {
                return NotFound();
            }

            return View(cats);
        }

        // GET: Cats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Photo,Status")] Cats cats)
        {
            if (ModelState.IsValid)
            {
                String newFileName = String.Empty;
                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    string PathDB = string.Empty;

                    var files = HttpContext.Request.Form.Files;

                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //Getting file Extension
                            var FileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            newFileName = myUniqueFileName + FileExtension;

                            // Combines two strings into a path.
                            fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            //PathDB = "demoImages/" + newFileName;
                            cats.Photo = newFileName;

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }


                }

                _context.Add(cats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cats);
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cats = await _context.Cats.SingleOrDefaultAsync(m => m.Id == id);
            if (cats == null)
            {
                return NotFound();
            }
            return View(cats);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Photo,Status")] Cats cats)
        {
            if (id != cats.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    String newFileName = String.Empty;

                    if (HttpContext.Request.Form.Files != null)
                    {
                        var fileName = string.Empty;
                        string PathDB = string.Empty;

                        var files = HttpContext.Request.Form.Files;

                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                //Getting FileName
                                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                                //Assigning Unique Filename (Guid)
                                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                //Getting file Extension
                                var FileExtension = Path.GetExtension(fileName);

                                // concating  FileName + FileExtension
                                newFileName = myUniqueFileName + FileExtension;

                                // Combines two strings into a path.
                                fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";

                                // if you want to store path of folder in database
                                //PathDB = "demoImages/" + newFileName;
                                cats.Photo = newFileName;

                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }
                        }


                    }


                    _context.Update(cats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatsExists(cats.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cats);
        }

        // GET: Cats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cats = await _context.Cats
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cats == null)
            {
                return NotFound();
            }

            return View(cats);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cats = await _context.Cats.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cats.Remove(cats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatsExists(int id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}
