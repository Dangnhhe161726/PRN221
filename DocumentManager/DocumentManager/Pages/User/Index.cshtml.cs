using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbcontext context;

        public IList<Users> user { get; set; }
        
        public IndexModel(ApplicationDbcontext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            user = context.Users.ToList();
        }
    }
}
