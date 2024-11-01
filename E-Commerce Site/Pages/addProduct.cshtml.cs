using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAAAAAAAAAA.Pages
{
    [Authorize(Roles = "Admin,superAdmin")]
    public class addProductModel : PageModel
    {
        public readonly AppDataContext _db;

        [BindProperty]

        public Product product { get; set; }

		public addProductModel(AppDataContext db)
        {
            _db = db;
        }

        public IActionResult OnPost()
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }

        public void OnGet()
        {
        }
    }
}
