using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinh.Data;
using Thinh.Models;

namespace Thinh.Controllers
{
    public class SellerController : Controller
	{
		private readonly ProductDbContext _context;
		public SellerController(ProductDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            return View();

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
		public async Task<IActionResult> Create([Bind("productName,productCategory,productUrl,productDescription,productImgUrl,Price")] Product homePageModel)
		{
			if (ModelState.IsValid)
			{																
				homePageModel.DateAdded = DateTime.Now;						
				_context.Add(homePageModel);								
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}																
			return View(homePageModel);										
		}																	
	}																		
}																			
																			
																			