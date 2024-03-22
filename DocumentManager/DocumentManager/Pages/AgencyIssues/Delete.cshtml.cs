using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.AgencyIssues
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
      public AgencyIssue AgencyIssue { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AgencyIssues == null)
            {
                return NotFound();
            }

            var agencyissue = await _context.AgencyIssues.FirstOrDefaultAsync(m => m.Id == id);

            if (agencyissue == null)
            {
                return NotFound();
            }
            else 
            {
                AgencyIssue = agencyissue;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AgencyIssues == null)
            {
                return NotFound();
            }
            var agencyissue = await _context.AgencyIssues.FindAsync(id);

            if (agencyissue != null)
            {
                AgencyIssue = agencyissue;
                _context.AgencyIssues.Remove(AgencyIssue);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
