using DocumentManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.User
{
	public class DeleteModel : PageModel
	{
		private readonly UserManager<Users> userManager;

		public DeleteModel(UserManager<Users> userManager)
		{
			this.userManager = userManager;
		}
		public async Task<IActionResult> OnGetAsync(string? Id)
		{
			var user = await userManager.FindByIdAsync(Id);
			if (user != null)
			{
				user.Enabled = false;
				await userManager.UpdateAsync(user);
			}

			return RedirectToPage("./Index");
		}
	}
}
