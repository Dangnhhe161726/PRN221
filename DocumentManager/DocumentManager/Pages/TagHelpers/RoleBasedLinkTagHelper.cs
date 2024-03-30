using DocumentManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocumentManager.Pages.TagHelpers
{
	[HtmlTargetElement("a", Attributes = "asp-role")]
	public class RoleBasedLinkTagHelper : TagHelper
	{
		private readonly UserManager<Users> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RoleBasedLinkTagHelper(UserManager<Users> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		[HtmlAttributeName("asp-role")]
		public string Role { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			bool isInAnyRole = false;

			if (user != null)
			{
				var rolesToCheck = Role.Split(',');
				foreach (var role in rolesToCheck)
				{
					if (await _userManager.IsInRoleAsync(user, role.Trim()))
					{
						isInAnyRole = true;
						break; 
					}
				}
			}

			if (!isInAnyRole)
			{
				output.SuppressOutput(); 
			}
		}
	}
}
