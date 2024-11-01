using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AAAAAAAAAAA.Pages
{
    public class CartModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;

		public readonly AppDataContext _db;

		public string ProductName { get; set; }
		public decimal OrderTotal {  get; set; }
		public Cart cart { get; set; }
        public List<Product> ProductList { get; set; }
		public List<Cart> CartList { get; set; }
		public CartModel(AppDataContext db, UserManager<AppUser> userManager) {
			_db = db;
			_userManager = userManager;
		}
		public void OnGet()
        {
		    var user =  _userManager.GetUserId(User);
			Guid userId = Guid.Parse(user);

			CartList = _db.ShoppingCart
				.Include(c => c.Product)
				.Where(s => s.AppUserId == userId).ToList();

			OrderTotal = 0;

			foreach (var item in CartList)
			{
				decimal productPrice = item.Product.Price;

				OrderTotal += productPrice;

			}
		}

		public async Task<IActionResult> OnGetRemove(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var removeFromCart = await _db.ShoppingCart.FindAsync(id);

			if (removeFromCart == null)
			{
				return NotFound();
			}

			_db.ShoppingCart.Remove(removeFromCart);
			await _db.SaveChangesAsync();
			return RedirectToPage("Index");	
		}

    }
}
