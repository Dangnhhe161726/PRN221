using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class CreateDocumentModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public CreateDocumentModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AgencyIssuesId"] = new SelectList(_context.AgencyIssues, "Id", "Id");
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
        ViewData["EmergencyId"] = new SelectList(_context.Emergencies, "Id", "Id");
        ViewData["GroupDocumentId"] = new SelectList(_context.GroupDocuments, "Id", "Id");
        ViewData["NameDocumentId"] = new SelectList(_context.NameDocuments, "Id", "Id");
        ViewData["SecurityId"] = new SelectList(_context.Sercurities, "Id", "Id");
        ViewData["SignerId"] = new SelectList(_context.Signers, "Id", "Id");
        ViewData["SpecializedId"] = new SelectList(_context.Specializes, "Id", "Id");
        ViewData["Aid"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Documents == null || Document == null)
            {
                return Page();
            }

            _context.Documents.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
