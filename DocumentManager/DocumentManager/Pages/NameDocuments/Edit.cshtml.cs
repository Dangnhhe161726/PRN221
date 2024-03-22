using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.NameDocuments
{
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public EditModel(DocumentManager.Services.ApplicationDbcontext context)
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

            var namedocument =  await _context.NameDocuments.FirstOrDefaultAsync(m => m.Id == id);
            if (namedocument == null)
            {
                return NotFound();
            }
            NameDocument = namedocument;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NameDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NameDocumentExists(NameDocument.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NameDocumentExists(int id)
        {
          return (_context.NameDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
