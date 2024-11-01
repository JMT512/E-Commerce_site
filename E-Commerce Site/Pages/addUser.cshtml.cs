using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAAAAAAAAAA.Pages
{
	[Authorize(Roles = "Admin,superAdmin")]
	public class addUserModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;

		public readonly AppDataContext _db;
		public string Id { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public string email { get; set; }

		public string currentPassword { get; set; }
		public string password { get; set; }

		public string passwordConfirmation { get; set; }

		public string selectedRole { get; set; }

		public addUserModel(AppDataContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

		public void OnGet(string id)
        {
		}

		public async Task<IActionResult> OnPostCreate(string firstName, string lastName, string email, string password, 
			string passwordConfirmation, string selectedRole)
		{
			AppUser appuser = new AppUser
			{
				FirstName= firstName,
				LastName= lastName,
				Email= email,
				UserName= email,
			};

			if (password == passwordConfirmation)
			{
                await _userManager.CreateAsync(appuser, password);

                switch (selectedRole)
				{
					case "Admin":
						var attachRoleAdmin= await _userManager.FindByEmailAsync(email);
						await _userManager.AddToRoleAsync(attachRoleAdmin, selectedRole);
						break;
                    case "Editor":
                        var attachRoleEditor = await _userManager.FindByEmailAsync(email);
                        await _userManager.AddToRoleAsync(attachRoleEditor, selectedRole);
                        break;
                    case "Visitor":
                        var attachRoleVisitor = await _userManager.FindByEmailAsync(email);
                        await _userManager.AddToRoleAsync(attachRoleVisitor, selectedRole);
                        break;
                };
                return RedirectToPage("adminPage");
            }
            return RedirectToPage("adminPage");
        }


    }
}
