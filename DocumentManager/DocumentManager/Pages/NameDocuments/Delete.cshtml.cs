using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.NameDocuments
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
      public NameDocument NameDocument { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.NameDocuments == null)
            {
                return NotFound();
            }

            var namedocument = await _context.NameDocuments.FirstOrDefaultAsync(m => m.Id == id);

            if (namedocument == null)
            {
                return NotFound();
            }
            else 
            {
                NameDocument = namedocument;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.NameDocuments == null)
            {
                return NotFound();
            }
            var namedocument = await _context.NameDocuments.FindAsync(id);

            if (namedocument != null)
            {
                NameDocument = namedocument;
                _context.NameDocuments.Remove(NameDocument);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
