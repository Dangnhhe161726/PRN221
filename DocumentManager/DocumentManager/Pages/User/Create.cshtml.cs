using DocumentManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DocumentManager.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using DocumentManager.Services;

namespace DocumentManager.Pages.User
{
    public class CreateModel : PageModel
    {

		private readonly SignInManager<Users> _signInManager;
		private readonly UserManager<Users> _userManager;
		private readonly IUserStore<Users> _userStore;
		private readonly IUserEmailStore<Users> _emailStore;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbcontext _context;

		public CreateModel(
			UserManager<Users> userManager,
			IUserStore<Users> userStore,
			SignInManager<Users> signInManager,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbcontext context)
		{
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
			_roleManager = roleManager;
			_context = context;
		}

		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; set; }
		[BindProperty]
		public string roleName {  get; set; }
		[BindProperty]
		public string PositionId { get; set; }


		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public IList<IdentityRole> Roles { get; set; }
		public IList<Position> positions { get; set; }

		public class InputModel
		{
			[Required]
			public string FristName { get; set; }

			[Required]
			public string LastName { get; set; }

			[Required]
			public string Address { get; set; }

			[Required]
			public string PhoneNumber { get; set; }

			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			await LoadAsync(returnUrl);
		}

		private Users CreateUser()
		{
			try
			{
				return Activator.CreateInstance<Users>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(Users)}'. " +
					$"Ensure that '{nameof(Users)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/users");
			ModelState.Remove("ReturnUrl");
			ModelState.Remove("PositionId");
			if (ModelState.IsValid)
			{
				var user = new Users()
				{
					FristName = Input.FristName,
					LastName = Input.LastName,
					UserName = Input.Email,
					Email = Input.Email,
					PhoneNumber = Input.PhoneNumber,
					Address = Input.Address,
					CreatedAt = DateTime.Now,
					Enabled = true
				};
				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");
					await _userManager.AddToRoleAsync(user, roleName);
					var UserId = await _userManager.GetUserIdAsync(user);
					if (roleName.Equals("signer"))
					{
						if (!String.IsNullOrEmpty(PositionId))
						{
							Signer signer = new Signer
							{
								Name = Input.FristName + " " + Input.LastName,
								userId = user.Id,
								Pid = int.Parse(PositionId)
							};
							if(signer != null)
							{
								_context.Signers.Add(signer);
								_context.SaveChanges();
							}
						}
					}
					//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					//var callbackUrl = Url.Page(
					//	"/Account/ConfirmEmail",
					//	pageHandler: null,
					//	values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
					//	protocol: Request.Scheme);

					//await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
					//	$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					//if (_userManager.Options.SignIn.RequireConfirmedAccount)
					//{
					//	return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
					//}
					//else
					//{
						//await _signInManager.SignInAsync(user, isPersistent: false);
						return LocalRedirect(returnUrl);
					//}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			else
			{
				await LoadAsync();
			}

			return Page();
		}

		private IUserEmailStore<Users> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<Users>)_userStore;
		}
		public async Task LoadAsync(string returnUrl = null)
		{
			positions = _context.Positions.ToList();
			Roles = _roleManager.Roles.ToList();
			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}
	}
}
