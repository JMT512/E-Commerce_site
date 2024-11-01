using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAAAAAAAAAA.Pages
{
	[Authorize(Roles = "Admin,superAdmin")]
	public class editAdminPageModel : PageModel
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
        public editAdminPageModel(AppDataContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public void OnGet(string id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnPostUpdate(string? Id, string updateState, string email, string firstName, string lastName, string currentPassword,
			string password, string passwordConfirmation)
        {
			var findUser = await _userManager.FindByIdAsync(Id);
            if (findUser == null)
            {
                return NotFound();
            }

			switch (updateState)
			{
				case "updatename":
					findUser.FirstName = firstName;
					await _userManager.UpdateAsync(findUser);
					break;
				case "updatesurname":
					findUser.LastName = lastName;
					await _userManager.UpdateAsync(findUser);
					break;
				case "updateemail":
					findUser.Email = email;
					await _userManager.UpdateAsync(findUser);
					break;
				case "UpdatePassword":
					if (password == passwordConfirmation)
					{
						
						await _userManager.ChangePasswordAsync(findUser, currentPassword, password);
					}
					else
					{
						//do error throw
					}
					break;

			};
			return RedirectToPage("adminPage");
		}
    }
}
