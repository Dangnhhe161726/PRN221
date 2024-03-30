using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.Documents
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

      //  [BindProperty]
      //public Document Document { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FirstOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }
            //else 
            //{
            //    Document = document;
            //}
            document.Status = false;
            _context.Documents.Update(document);
            _context.SaveChanges();
			   return RedirectToPage("./DocumentView");
		}

		//public async Task<IActionResult> OnPostAsync(int? id)
		//{
		//    if (id == null || _context.Documents == null)
		//    {
		//        return NotFound();
		//    }
		//    var document = await _context.Documents.FindAsync(id);

		//    if (document != null)
		//    {
		//        Document = document;
		//        _context.Documents.Remove(Document);
		//        await _context.SaveChangesAsync();
		//    }

		//    return RedirectToPage("./Index");
		//}
	}
}
