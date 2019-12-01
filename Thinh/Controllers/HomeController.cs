using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Thinh.Data;
using Thinh.Models;

namespace Thinh.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IProductRepository _context;
		public HomeController(IProductRepository context,

			UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
			_context = context;
		}

		// GET: HomePage
		public async Task<ViewResult> Index()
		{
			ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
			if(user != null)
			{
				if (user.IsAdmin == 1)
				{
					ViewData["Admin"] = 1;
				}
			}
			return View("Index", _context.Products(1).ToList());
		}

		// GET: HomePage/Details/5
		public ViewResult Details(int id)
		{
			if (id == 0)
			{
				return View("Index");
			}

			var prodDetail = _context.GetProduct(id);
			if (prodDetail == null)
			{
				return View("Index");
			}
			return View("Details",prodDetail);
		}

		[HttpGet]
		public ViewResult Contact()
		{
			return View("Contact");
		}
		[HttpPost]
		public ActionResult Contact([Bind("Email,Feedback")] Feedbacks fb)
		{
			if (ModelState.IsValid)
			{
				_context.AddFeedback(fb);
				return View("Index");
			}
			return View("Contact",fb);
		}
		public ActionResult Search(string search)
		{
			if (search == null)
			{
				return View("Index");
			}
			var result = _context.Search(search);

			string title = "Search results for \"" + search + "\"";
			ViewData["Title"] = title;
			return View("Search", result);
		}

		public ActionResult Category(string cat)
		{
			ViewData["Title"] = cat;
			if (cat == null)
			{
				return View("Index");
			}
			var result = _context.Category(cat);

			return View("Search", result);
		}

		public ViewResult Wish(int id)
		{
			if (id == 0)
			{
				return View("Index");
			}
			var test = _context.GetProduct(id);
			if (test == null)
			{
				return View("Index");
			}
			List<Product> wishlist = HttpContext.Session.GetList();
			bool alreadyExists = wishlist.Any(x => x.productId == id);
			if (!alreadyExists)
			{
				wishlist.Add(test);
			}
			HttpContext.Session.SaveList(wishlist);
			return View("Wishlist", wishlist);
		}
		public IActionResult Wishlist()
		{
				List<Product> wishlist = HttpContext.Session.GetList();
				return View("Wishlist", wishlist);
		}
		public IActionResult DeleteWish(int id)
		{
			List<Product> wishlist = HttpContext.Session.GetList();
			var item = wishlist.SingleOrDefault(x => x.productId == id);
			if (item != null)
				wishlist.Remove(item);
			HttpContext.Session.SaveList(wishlist);
			return View("Wishlist", wishlist);
		}
	}
}
