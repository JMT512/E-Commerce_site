using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace AAAAAAAAAAA.Pages
{
    public class ProductModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;

		public readonly AppDataContext _db;
		public Product product { get; set; }
		public Cart cart { get; set; }

		public List<Cart> CartList { get; set; }
		public List<Product> ProductList { get; set; }
        public ProductModel(AppDataContext db, UserManager<AppUser> userManager)
        {
            _db= db;
			_userManager = userManager;
		}
		

		public void OnGet()
        {
            ProductList=_db.Products.ToList();
        }

		public async Task<IActionResult> OnGetCart(int? id)
		{
			var user = await _userManager.GetUserAsync(User);
			Guid userId = Guid.Parse(user?.Id);

			var productAlreadyAdded = _db.ShoppingCart.Where(s => s.AppUserId == userId)
				                      .Where(p => p.ProductId == id)
									  .Select(i => i.CartId)
									  .FirstOrDefault();

            if (productAlreadyAdded == 0) {
				var cart = new Cart()
				{
					quantity = 1,
					AppUserId = userId,
					ProductId = (int)id
				};

				_db.ShoppingCart.Add(cart);
				await _db.SaveChangesAsync();
				return RedirectToPage("Index");
			}
			else
			{
                var findExistingCart = await _db.ShoppingCart.FindAsync(productAlreadyAdded);

                int existingQuantity = findExistingCart.quantity;
				var newQuantity = existingQuantity + 1;

                findExistingCart.quantity = newQuantity;
				await _db.SaveChangesAsync();
				return RedirectToPage("Index");
			}
		}

		public async Task<IActionResult> OnGetDelete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var findProduct = await _db.Products.FindAsync(id);
			Debug.WriteLine("I AM WRITING THIS OUT" + findProduct);
			if (findProduct == null)
			{
				return NotFound();
			}

			_db.Products.Remove(findProduct);
			await _db.SaveChangesAsync();
			return RedirectToPage("Index");

		}

		public async Task<IActionResult> OnPostSubmit(int? id, string updateState)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productToUpdate = await _db.Products.FindAsync(id);
			Debug.WriteLine("I AM WRITING THIS OUT"+productToUpdate);

			if (productToUpdate == null)
			{
				return NotFound();
			}

			switch (updateState)
			{
				case "updatename":
					if(await TryUpdateModelAsync<Product>(productToUpdate, "product", s=> s.Name)){
						await _db.SaveChangesAsync();
						return RedirectToPage("Index");
					}
					break;
                case "updateprice":
                    if (await TryUpdateModelAsync<Product>(productToUpdate, "product", s => s.Price))
                    {
                        await _db.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                    break;
                case "updatedesc":
                    if (await TryUpdateModelAsync<Product>(productToUpdate, "product", s => s.Description))
                    {
                        await _db.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                    break;
                case "updateimage":
                    if (await TryUpdateModelAsync<Product>(productToUpdate, "product", s => s.ProductImage))
                    {
                        await _db.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                    break;

            };
			return Page();
		}
	}
}
