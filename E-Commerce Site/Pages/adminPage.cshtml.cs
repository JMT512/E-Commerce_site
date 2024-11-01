using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace AAAAAAAAAAA.Pages
{
	[Authorize(Roles = "Admin,superAdmin")]
	public class adminPageModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;

		public readonly AppDataContext _db;
		public AppUser user { get; set; }
		public List<AppUser> userAdmin { get; set; }
		public List<AppUser> userEditor { get; set; }
		public List<AppUser> userVisitor { get; set; }
		public adminPageModel(AppDataContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public async Task<IActionResult> OnGet()
        {

			if (User.IsInRole("superAdmin"))
			{
				var usersAdmin = await _userManager.GetUsersInRoleAsync("Admin"); //Returns a list of users from the user store who are members of the specified roleName.

				userAdmin = usersAdmin.ToList();

				var usersEditor = await _userManager.GetUsersInRoleAsync("Editor");

				userEditor = usersEditor.ToList();

				var usersVisitor = await _userManager.GetUsersInRoleAsync("Visitor");

				userVisitor = usersVisitor.ToList();

				return Page();
			}
			else if (User.IsInRole("Admin"))
			{
				var usersEditor = await _userManager.GetUsersInRoleAsync("Editor");

				userEditor = usersEditor.ToList();

				var usersVisitor = await _userManager.GetUsersInRoleAsync("Visitor");

				userVisitor = usersVisitor.ToList();

				return Page();
			}
			return Page();
		}

		public async Task<IActionResult> OnGetDelete(string? id)   //DeleteAsync  FindByIdAsync
		{
			if (id == null)
			{
				return NotFound();
			}

			var userToDelete = await _userManager.FindByIdAsync(id);
			


				await _userManager.DeleteAsync(userToDelete);
				return RedirectToPage("Index");
		}
    }
}
