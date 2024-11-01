using GroceryStore.Data;
using GroceryStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace GroceryStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppDbContext _Db;
		private SignInManager<IdentityUser> _signInManager;
		public HomeController(ILogger<HomeController> logger, AppDbContext DB, SignInManager<IdentityUser> signInManager)
		{
			_logger = logger;
			_Db = DB;
			_signInManager = signInManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Product(int id)
		{
			ViewBag.Category = _Db.categories.ToList();
			List<Product> result;
			if (id == 0)
			{
				//All products
				result = _Db.products.ToList();
			}
			else
			{
				result = _Db.products.Where(x => x.CategoryId == id).ToList();
			}
			return View(result);
		}
		[Authorize]
        [Authorize]
        public IActionResult Cart(int? id)
        {
            ViewData["id"] = id;
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the pending order or create a new one if needed
            var currentOrder = _Db.orders
                .Where(x => x.UserId == userId && x.OrderStatus == "Pending")
                .OrderBy(x => x.CreationDate)
                .LastOrDefault() ?? CreateNewOrder(userId);

            ViewData["idOrder"] = currentOrder.OrderId;

            // If no product ID is provided, show the current cart contents
            if (!id.HasValue)
            {
                var cartItems = _Db.carts.Where(x => x.OrderId == currentOrder.OrderId).ToList();
                return View(cartItems);
            }

            // Otherwise, add the specified product to the cart
            var product = _Db.products.FirstOrDefault(x => x.ProductId == id.Value);
            if (product == null)
                return NotFound("Product not found.");

            // Check if the product is already in the cart
            var existingCartItem = _Db.carts
                .FirstOrDefault(x => x.OrderId == currentOrder.OrderId && x.ProductId == product.ProductId);

            if (existingCartItem != null)
            {
                // Product already exists in the cart; increase the quantity
                existingCartItem.Quantity += 1;
                existingCartItem.Total = existingCartItem.Quantity * existingCartItem.price;
            }
            else
            {
                // Product is not in the cart; create a new cart item
                var newCartItem = new Cart
                {
                    OrderId = currentOrder.OrderId,
                    ProductId = product.ProductId,
                    Productname = product.ProductName,
                    ProductImage = product.ProductImg,
                    Quantity = 1,
                    Total = float.Parse(product.ProductPrice),
                    price = float.TryParse(product.ProductPrice, out var price) ? price : 0
                };
                _Db.carts.Add(newCartItem);
            }

            _Db.SaveChanges();

            // Retrieve updated cart items and display them
            var cart = _Db.carts.Where(x => x.OrderId == currentOrder.OrderId).ToList();
            return View(cart);
        }

        // Action to increase the quantity of a cart item
        [Authorize]
        public IActionResult IncreaseQuantity(int id)
        {
            var cartItem = _Db.carts.FirstOrDefault(x => x.CartId == id);
            if (cartItem != null)
            {
                cartItem.Quantity += 1;
                cartItem.Total = cartItem.Quantity * cartItem.price;
                _Db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        // Action to decrease the quantity of a cart item
        [Authorize]
        public IActionResult DecreaseQuantity(int id)
        {
            var cartItem = _Db.carts.FirstOrDefault(x => x.CartId == id);
            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                cartItem.Total = cartItem.Quantity * cartItem.price;
                _Db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        // Helper method to create a new order if no pending order exists
        private Order CreateNewOrder(string userId)
        {
            var order = new Order
            {
                CreationDate = DateTime.Now,
                UserId = userId,
                OrderStatus = "Pending",
            };
            _Db.orders.Add(order);
            _Db.SaveChanges();
            return order;
        }
        [Authorize]
	
		[Authorize]

		public IActionResult checkout(int? id)
		{
			var result = _Db.orders.Where(x => x.OrderId == id).FirstOrDefault();
			if (result != null)
			{
				result.OrderStatus = "checkout";
				_Db.Update(result);
				_Db.SaveChanges();
			}

			return View();
		}

		// POST action for checkout
		[HttpPost]
		public IActionResult checkout()
		{
			TempData["SuccessMessage"] = "Payment has been completed successfully.";
			return RedirectToAction("checkout");
		}


		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost]

		public IActionResult Contact(Contact model)
		{
			_Db.contacts.Add(model);
			_Db.SaveChanges();
            TempData["SuccessMessage"] = "Your message has been sent successfully!";

            return RedirectToAction("Contact"); 
        }
        public IActionResult DetailsProduct(int id)
		{
			var result = _Db.products.Where(x => x.ProductId == id).FirstOrDefault();
			return View(result);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


        
         public IActionResult RemoveitemFormCart(int id)
        {
			var Result=_Db.carts.Where(x => x.CartId == id).FirstOrDefault();
			_Db.Remove(Result);
			_Db.SaveChanges();
            return RedirectToAction("Cart");
        }


    }
}
