using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.NameDocuments
{
    public class CreateModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public CreateModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NameDocument NameDocument { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.NameDocuments == null || NameDocument == null)
            {
                return Page();
            }

            _context.NameDocuments.Add(NameDocument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
