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

namespace DocumentManager.Pages.GroupDocuments
{
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public EditModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupDocument GroupDocument { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GroupDocuments == null)
            {
                return NotFound();
            }

            var groupdocument =  await _context.GroupDocuments.FirstOrDefaultAsync(m => m.Id == id);
            if (groupdocument == null)
            {
                return NotFound();
            }
            GroupDocument = groupdocument;
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

            _context.Attach(GroupDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupDocumentExists(GroupDocument.Id))
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

        private bool GroupDocumentExists(int id)
        {
          return (_context.GroupDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
