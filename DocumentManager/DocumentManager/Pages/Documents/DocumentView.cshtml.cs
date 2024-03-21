using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class DocumentViewModel : PageModel
    {
        private readonly ApplicationDbcontext context;
		public IList<Document> documents { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public DocumentViewModel(ApplicationDbcontext context)
        {
            this.context = context;
        }
        public void OnGet(int? PageIndex)
        {
            if(PageIndex == 0 || PageIndex == null)
            {
                PageIndex = 1;
            }
			const int pageSize = 3;
			CurrentPage = (int)PageIndex;
			documents = context.Documents
                .Where(d => d.Status == true)
                .OrderBy(x => x.Id)
                .Skip((int)((PageIndex - 1) * pageSize))
                .Take(pageSize)
                .ToList();
            var totalCount = context.Documents.Where(d => d.Status == true).Count();
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		}
    }
}
