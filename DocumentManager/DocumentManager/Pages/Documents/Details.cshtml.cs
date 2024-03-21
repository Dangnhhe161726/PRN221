using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace DocumentManager.Pages.Documents
{
	[Authorize]
	public class DetailModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DetailModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

      public Document Document { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(u => u.Users)
                .Include(a => a.AgencyIssues)
                .Include(c => c.Category)
                .Include(e => e.Emergency)
                .Include(g => g.GroupDocument)
                .Include(n => n.NameDocument)
                .Include(s => s.Security)
                .Include(s => s.Signer)
                .Include(s => s.Specialized)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }
            else 
            {
                Document = document;
            }
            return Page();
        }
    }
}
