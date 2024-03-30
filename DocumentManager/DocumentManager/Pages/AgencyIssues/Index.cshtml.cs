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
    public class IndexModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public IndexModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public IList<AgencyIssue> AgencyIssue { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AgencyIssues != null)
            {
                AgencyIssue = await _context.AgencyIssues.ToListAsync();
            }
        }
    }
}
