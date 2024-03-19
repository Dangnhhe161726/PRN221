using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.Test
{
    [Authorize(Roles = "employee")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
