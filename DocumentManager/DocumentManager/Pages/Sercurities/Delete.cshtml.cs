using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.Sercurities
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
      public Sercurity Sercurity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sercurities == null)
            {
                return NotFound();
            }

            var sercurity = await _context.Sercurities.FirstOrDefaultAsync(m => m.Id == id);

            if (sercurity == null)
            {
                return NotFound();
            }
            else 
            {
                Sercurity = sercurity;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sercurities == null)
            {
                return NotFound();
            }
            var sercurity = await _context.Sercurities.FindAsync(id);

            if (sercurity != null)
            {
                Sercurity = sercurity;
                _context.Sercurities.Remove(Sercurity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
