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

namespace DocumentManager.Pages.AgencyIssues
{
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public EditModel(DocumentManager.Services.ApplicationDbcontext context)
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

            var agencyissue =  await _context.AgencyIssues.FirstOrDefaultAsync(m => m.Id == id);
            if (agencyissue == null)
            {
                return NotFound();
            }
            AgencyIssue = agencyissue;
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

            _context.Attach(AgencyIssue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyIssueExists(AgencyIssue.Id))
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

        private bool AgencyIssueExists(int id)
        {
          return (_context.AgencyIssues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
