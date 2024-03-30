using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.AgencyIssues
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
        public AgencyIssue AgencyIssue { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AgencyIssues == null || AgencyIssue == null)
            {
                return Page();
            }

            _context.AgencyIssues.Add(AgencyIssue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
