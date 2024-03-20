using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class DocumentViewModel : PageModel
    {
        private readonly ApplicationDbcontext context;
        public IList<Document> documents { get; set; }
        public DocumentViewModel(ApplicationDbcontext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            documents = context.Documents.ToList();
        }
    }
}
