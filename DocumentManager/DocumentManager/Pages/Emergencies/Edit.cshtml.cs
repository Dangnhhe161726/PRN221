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

namespace DocumentManager.Pages.Emergencies
{
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public EditModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public Emergency Emergency { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency =  await _context.Emergencies.FirstOrDefaultAsync(m => m.Id == id);
            if (emergency == null)
            {
                return NotFound();
            }
            Emergency = emergency;
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

            _context.Attach(Emergency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmergencyExists(Emergency.Id))
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

        private bool EmergencyExists(int id)
        {
          return (_context.Emergencies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
