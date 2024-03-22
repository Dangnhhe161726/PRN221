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
    public class DetailsModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DetailsModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

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
    }
}
