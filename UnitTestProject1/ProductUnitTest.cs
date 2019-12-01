using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thinh.Controllers;
using Thinh.Models;
using Thinh.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace UnitTest
{
	[TestClass]
	public class ProductUnitTest
	{
		Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
		MockHttpSession mockSession = new MockHttpSession();
		Mock<FakeSignInManager> signInManager = new Mock<FakeSignInManager>();
		Mock<FakeUserManager> userManager = new Mock<FakeUserManager>();

		List<Product> faqs = new List<Product>
				{ new Product
					{
						productId= 1 ,
						productImgUrl = "https://images-na.ssl-images-amazon.com/images/I/61SVEUsMepL._SL1024_.jpg",
						productName = "Samsung Galaxy",
						productUrl = "https://www.amazon.com/Samsung-DS-Unlocked-Smartphone-International/dp/B07QV2JKGQ/ref=sxin_0_ac_d_rm?ac_md=3-3-c2Ftc3VuZyBnYWxheHk%3D-ac_d_rm&crid=101VGUI07M0WW&keywords=phone&pd_rd_i=B07QV2JKGQ&pd_rd_r=0ce275e5-3539-49ea-bca7-1f86ba0c21b3&pd_rd_w=Qdnd7&pd_rd_wg=nx2FJ&pf_rd_p=e2f20af2-9651-42af-9a45-89425d5bae34&pf_rd_r=7WXJGQHR1JTJRGWP4FD4&psc=1&qid=1574825321&sprefix=phone%2Caps%2C236",
						productCategory = "Electronics",
						productDescription = "Shoot more awesomeness with the 123° Ultra Wide lens."
						+ "Enhance your viewing experience with the next-gen Super AMOLED 16.20cm (6.4”) HD+ Infinity-V Display." +
						"Quickly power up your phone for the day with the 15W Fast Charging." +
						"Play games, stream videos and multitask with ease." +
						"Flaunt the sleek design and the ergonomically placed rear Fingerprint Sensor.",
						Price = "$178.90",
						DateAdded = DateTime.Now,
						IsApproved = 1
					},
					new Product
					{
						productId = 2,
						productImgUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcSt1WHfo9cZnj5-EdIS0rosoqDJeQjXjVc16Hd-RwLqQ8F_Jw56nQ&usqp=CAc",
						productName = "Google Pixel",
						productUrl = "https://www.bestbuy.ca/en-ca/product/google-pixel-xl-32gb-gsm-unlocked-4g-lte-smartphone-in-quite-black-new-in-box/14193690",
						productCategory = "Electronics",
						productDescription = "Enjoy a pure Android experience with the unlocked Google Pixel XL G-2PW2100 128GB Smartphone in Quite " +
						"Black. This phone features an aluminum body and is powered by a quad-core Qualcomm Snapdragon 821 " +
						"processor with two 2.15 GHz cores and two 1.6 GHz cores as well as 4GB of RAM. It comes equipped with "+
						"32GB of storage for your media, apps, and more. The reversible USB Type-C interface allows you to connect "+
						"the device for mass-storage purposes and also serves as a charging port. ",
						Price = "$499",
						DateAdded = DateTime.Now,
						IsApproved = 1
					},
					new Product
					{
						productId = 3,
						productImgUrl = "https://www.bell.ca/Styles/wireless/all_languages/all_regions/catalog_images/large/iPhone_11_Pro_Space_Gray_lrg1.png",
						productName = "Apple iPhone",
						productUrl = "https://www.bestbuy.ca/en-ca/product/bell-apple-iphone-11-pro-512gb-space-grey-select-2-year-agreement/13897367",
						productCategory = "Electronics",
						productDescription = "A transformative triple‑camera system that adds tons of capability without complexity. An unprecedented " +
						"leap in battery life. And a mind‑blowing chip that doubles down on machine learning and pushes the " +
						"boundaries of what a smartphone can do. Welcome to the first iPhone powerful enough to be called Pro. ",
						Price = "$949.99",
						DateAdded = DateTime.Now,
						IsApproved = 0
					}
				};
		Mock<IProductRepository> faqRepository = new Mock<IProductRepository>();
		[TestMethod]
		public async Task TestIndexAsync()
		{
			faqRepository.Setup(e => e.Products(1)).Returns(faqs.Where(f=> f.IsApproved==1).AsQueryable());
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
			var controller = new HomeController(faqRepository.Object, userManager.Object);
			controller.ControllerContext.HttpContext = mockHttpContext.Object;
			ViewResult result = await controller.Index() as ViewResult;
			var x = result.ViewData.Model as IEnumerable<Product>;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, x.Count());
		}
		[TestMethod]
		public void TestDetails()
		{
			faqRepository.Setup(e => e.GetProduct(2)).Returns(faqs[1]);
			var controller = new HomeController(faqRepository.Object, null);
			ViewResult result = controller.Details(2) as ViewResult;
			Product prod = result.ViewData.Model as Product;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Google Pixel", prod.productName);
		}
		[TestMethod]
		public void TestContact()
		{
			var controller = new HomeController(null, null);
			ViewResult result = controller.Contact() as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Contact", result.ViewName);
		}
		[TestMethod]
		public void TestContactPost()
		{
			Feedbacks x = new Feedbacks()
			{
				FeedbackId = 1,
				Email = "test",
				Feedback = "test fb"
			};
			faqRepository.Setup(e => e.AddFeedback(x));
			var controller = new HomeController(faqRepository.Object, null);
			ViewResult result = controller.Contact(x) as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ViewName);
		}
		[TestMethod]
		public void TestSearch()
		{
			faqRepository.Setup(e => e.Search("Google Pixel")).Returns(faqs.AsQueryable());
			var controller = new HomeController(faqRepository.Object, null);
			ViewResult result = controller.Search("Google Pixel") as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Search results for \"Google Pixel\"", result.ViewData["Title"]);
			IEnumerable<Product> x = result.ViewData.Model as IEnumerable<Product>;
			Assert.AreEqual("Electronics", x.First().productCategory);
		}

		[TestMethod]
		public void TestCategory()
		{
			faqRepository.Setup(e => e.Category("Electronics")).Returns(faqs.AsQueryable());
			var controller = new HomeController(faqRepository.Object, null);
			ViewResult result = controller.Category("Electronics") as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			IEnumerable<Product> x = result.ViewData.Model as IEnumerable<Product>;
			Assert.AreEqual("Electronics", result.ViewData["Title"]);
			Assert.AreEqual("Electronics", x.First().productCategory);
		}

		[TestMethod]
		public void TestWish()
		{
			faqRepository.Setup(e => e.GetProduct(1)).Returns(faqs[0]);
			mockSession["Wishlist"] = null;
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
			var controller = new HomeController(faqRepository.Object, null);
			controller.ControllerContext.HttpContext = mockHttpContext.Object;
			ViewResult result = controller.Wish(1) as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Wishlist", result.ViewName);
			List<Product> test = result.Model as List<Product>;
			Assert.AreEqual("Samsung Galaxy", test[0].productName);

		}
		[TestMethod]
		public void TestWishlist()
		{
			mockSession["Wishlist"] = null;
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
			var controller = new HomeController(faqRepository.Object, null);
			controller.ControllerContext.HttpContext = mockHttpContext.Object;
			ViewResult result = controller.Wishlist() as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Wishlist", result.ViewName);
		}
		[TestMethod]
		public void TestWishlistDelete()
		{
			mockSession["Wishlist"] = null;
			mockHttpContext.Setup(s => s.Session).Returns(mockSession);
			var controller = new HomeController(faqRepository.Object, null);
			controller.ControllerContext.HttpContext = mockHttpContext.Object;
			ViewResult result = controller.DeleteWish(1) as ViewResult;
			// Assert
			Assert.AreEqual("Wishlist", result.ViewName);
		}

		[TestMethod]
		public void TestCreate()
		{
			Product x = new Product()
			{
				productId = 1,
				productName = "test"
			};
			faqRepository.Setup(e => e.SaveProduct(x));
			var controller = new AccountController(faqRepository.Object, null, null, null);
			var result = controller.Create(x) as RedirectToActionResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual("Home", result.ControllerName);
		}
		[TestMethod]
		public void TestFeedbackList()
		{
			List<Feedbacks> fb = new List<Feedbacks>()
			{
				new Feedbacks()
				{
				FeedbackId = 1,
				Email = "test",
				Feedback = "test fb"
				}
			};
			faqRepository.Setup(e => e.AddFeedback(fb[0]));
			faqRepository.Setup(e => e.FeedbackList()).Returns(fb.AsQueryable);
			var controller = new AccountController(faqRepository.Object, null, null, null);
			ViewResult result = controller.FeedbackList() as ViewResult;
			var x = result.ViewData.Model as IEnumerable<Feedbacks>;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("test", x.First().Email);
		}
		[TestMethod]
		public void TestApproveList()
		{
			faqRepository.Setup(e => e.Products(0)).Returns(faqs.Where(f => f.IsApproved == 0).AsQueryable());
			var controller = new AccountController(faqRepository.Object, null, null, null);
			ViewResult result = controller.ApproveList() as ViewResult;
			var x = result.ViewData.Model as IEnumerable<Product>;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, x.Count());
			Assert.AreEqual(0, x.First().IsApproved);
		}
		[TestMethod]
		public void TestApproveProduct()
		{
			faqRepository.Setup(e => e.GetProduct(3)).Returns(faqs[2]);
			var controller = new AccountController(faqRepository.Object, null, null, null);
			var result = controller.Approve(3) as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("ApproveList", result.ViewName);
		}
		[TestMethod]
		public void TestDelete()
		{
			faqRepository.Setup(e => e.DeleteProduct(3)).Returns(faqs[2]);
			var controller = new AccountController(faqRepository.Object, null, null, null);
			var result = controller.DeleteProduct(3) as RedirectToActionResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual("Home", result.ControllerName);
		}
		public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
		{
			var store = new Mock<IUserStore<TUser>>();
			var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
			mgr.Object.UserValidators.Add(new UserValidator<TUser>());
			mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

			mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
			mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
			mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

			return mgr;
		}

		[TestMethod]
		public async Task TestRegisterAsync()
		{
			RegisterViewModel model = new RegisterViewModel()
			{
				Email = "test@gmail.com",
				Password = "123456",
				ConfirmPassword = "123456"
			};
			LoginViewModel loginModel = new LoginViewModel()
			{
				Email = "test@gmail.com",
				Password = "123456",
			};
			ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			var userManager1 = MockUserManager<ApplicationUser>();
			userManager1.Setup(e => e.CreateAsync(user, model.Password));
			var controller = new AccountController(faqRepository.Object, userManager1.Object, signInManager.Object, null);
			var result = await controller.Register(model, null) as ViewResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Login", result.ViewName);
		}

		[TestMethod]
		public async Task TestLoginAsync()
		{
			RegisterViewModel model = new RegisterViewModel()
			{
				Email = "test@gmail.com",
				Password = "123456",
				ConfirmPassword = "123456"
			};
			LoginViewModel loginModel = new LoginViewModel()
			{
				Email = "test@gmail.com",
				Password = "123456",
			};
			ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			userManager.Setup(u => u.CreateAsync(user, model.Password));
			var controller = new AccountController(faqRepository.Object, userManager.Object, signInManager.Object, null);
			userManager.Setup(e => e.FindByEmailAsync(loginModel.Email));
			signInManager.Setup(s => s.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false)).ReturnsAsync(SignInResult.Success);
			var result = await controller.Login(loginModel, null) as RedirectToActionResult;
			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
		}

	}

	public class FakeSignInManager : SignInManager<ApplicationUser>
	{
		public FakeSignInManager()
				: base(new FakeUserManager(),
					 new Mock<IHttpContextAccessor>().Object,
					 new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
					 new Mock<IOptions<IdentityOptions>>().Object,
					 new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
					 new Mock<IAuthenticationSchemeProvider>().Object)
		{ }
	}

	public class FakeUserManager : UserManager<ApplicationUser>
	{
		public FakeUserManager()
			: base(new Mock<IUserStore<ApplicationUser>>().Object,
			  new Mock<IOptions<IdentityOptions>>().Object,
			  new Mock<IPasswordHasher<ApplicationUser>>().Object,
			  new IUserValidator<ApplicationUser>[0],
			  new IPasswordValidator<ApplicationUser>[0],
			  new Mock<ILookupNormalizer>().Object,
			  new Mock<IdentityErrorDescriber>().Object,
			  new Mock<IServiceProvider>().Object,
			  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
		{ }

		public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
		{
			return Task.FromResult(IdentityResult.Success);
		}
		public override Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
		{
			return Task.FromResult(IdentityResult.Success);
		}

		public override Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
		{
			return Task.FromResult(Guid.NewGuid().ToString());
		}
		public override Task<ApplicationUser> FindByEmailAsync(string email)
		{
			return Task.FromResult(new ApplicationUser { Email = email });
		}

	}
	public class MockHttpSession : ISession
	{
		Dictionary<string, object> sessionStorage = new Dictionary<string, object>();

		public object this[string name]
		{
			get { return sessionStorage[name]; }
			set { sessionStorage[name] = value; }
		}

		string ISession.Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISession.IsAvailable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		IEnumerable<string> ISession.Keys
		{
			get { return sessionStorage.Keys; }
		}

		public Task CommitAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task LoadAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		void ISession.Clear()
		{
			sessionStorage.Clear();
		}

		void ISession.Remove(string key)
		{
			sessionStorage.Remove(key);
		}

		void ISession.Set(string key, byte[] value)
		{
			sessionStorage[key] = value;
		}

		bool ISession.TryGetValue(string key, out byte[] value)
		{
			if (sessionStorage[key] != null)
			{
				value = Encoding.ASCII.GetBytes(sessionStorage[key].ToString());
				return true;
			}
			else
			{
				value = null;
				return false;
			}
		}
	}

}