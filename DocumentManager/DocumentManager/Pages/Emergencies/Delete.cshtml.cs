using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.Emergencies
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
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

            var emergency = await _context.Emergencies.FirstOrDefaultAsync(m => m.Id == id);

            if (emergency == null)
            {
                return NotFound();
            }
            else 
            {
                Emergency = emergency;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }
            var emergency = await _context.Emergencies.FindAsync(id);

            if (emergency != null)
            {
                Emergency = emergency;
                _context.Emergencies.Remove(Emergency);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
