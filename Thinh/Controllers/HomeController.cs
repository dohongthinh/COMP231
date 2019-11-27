using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Thinh.Data;
using Thinh.Models;

namespace Thinh.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomePageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HomePage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: HomePage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var homePageModel = await _context.Products
                .FirstOrDefaultAsync(m => m.productId == id);
            if (homePageModel == null)
            {
                return NotFound();
            }

            return View(homePageModel);
        }

        // GET: HomePage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomePage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productId,productCode,productName,productTypeName,productUrl,productDescription,productImg")] Product homePageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homePageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homePageModel);
        }

        // GET: HomePage/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homePageModel = await _context.Products.FindAsync(id);
            if (homePageModel == null)
            {
                return NotFound();
            }
            return View(homePageModel);
        }

        // POST: HomePage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,productCode,productName,productTypeName,productUrl,productDescription,productImg")] Product homePageModel)
        {
            if (id != homePageModel.productId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homePageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageModelExists(homePageModel.productId))
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
            return View(homePageModel);
        }

        // GET: HomePage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var homePageModel = await _context.Products
                .FirstOrDefaultAsync(m => m.productId == id);
            if (homePageModel == null)
            {
                return NotFound();
            }

            return View(homePageModel);
        }

        // POST: HomePage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homePageModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(homePageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomePageModelExists(int id)
        {
            return _context.Products.Any(e => e.productId == id);
        }
    }
}
