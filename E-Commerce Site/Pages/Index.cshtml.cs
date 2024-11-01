using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Site.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        public readonly AppDataContext _db;

        public List<Product> ProductList { get; set; }

        public Product product { get; set; }

        public IndexModel(AppDataContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
