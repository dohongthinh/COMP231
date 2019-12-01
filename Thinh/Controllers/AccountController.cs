using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Thinh.Data;
using Thinh.Models;

namespace Thinh.Controllers
{
    [Authorize]
    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IProductRepository _context;

		private readonly ILogger _logger;

        public AccountController(IProductRepository context,

			UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
			_context = context;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

					//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					//var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
					//await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

					//diallowed signin for self registration, email should be confirmed first
					//await _signInManager.SignInAsync(user, isPersistent: false);
					//_logger.LogInformation("User created a new account with password.");
					return View("Login");
				}
                AddErrors(result);
            }

            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login", "Account");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Login");
            }
        }

		public async Task<IActionResult> UserInfo()
		{
			ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
			return View(user);
		}

		// GET: HomePage/Create
		public ActionResult Create()
		{
			return View("Create");
		}
		[HttpPost]
		public ActionResult Create([Bind("productName,productCategory,productUrl,productDescription,productImgUrl,Price")] Product addProduct)
		{
			if (ModelState.IsValid)
			{
				addProduct.DateAdded = DateTime.Now;
				_context.SaveProduct(addProduct);
				return RedirectToAction("Index", "Home");
			}
			return View("Create", addProduct);
		}

		public ViewResult FeedbackList()
		{
			return View("FeedbackList", _context.FeedbackList().ToList());
		}

		public ViewResult ApproveList()
		{
			return View("Approve", _context.Products(0).ToList());
		}

		public ViewResult Approve(int id)
		{
			Product approve = _context.GetProduct(id);
			approve.IsApproved = 1;
			approve.DateAdded = DateTime.Now;
			if (ModelState.IsValid)
			{
				_context.SaveProduct(approve);
				return View("ApproveList");
			}
			return View();
		}
		public ActionResult DeleteProduct(int id)
		{
			if (ModelState.IsValid)
			{
				_context.DeleteProduct(id);
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}