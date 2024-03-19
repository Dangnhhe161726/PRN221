using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.Test
{
    [Authorize(Roles ="admin")]
    public class Index1Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
