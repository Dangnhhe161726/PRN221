using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;

namespace DocumentManager.Pages.GroupDocuments
{
    public class DetailsModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DetailsModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

      public GroupDocument GroupDocument { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GroupDocuments == null)
            {
                return NotFound();
            }

            var groupdocument = await _context.GroupDocuments.FirstOrDefaultAsync(m => m.Id == id);
            if (groupdocument == null)
            {
                return NotFound();
            }
            else 
            {
                GroupDocument = groupdocument;
            }
            return Page();
        }
    }
}
