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
    public class IndexModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public IndexModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public IList<Emergency> Emergency { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Emergencies != null)
            {
                Emergency = await _context.Emergencies.ToListAsync();
            }
        }
    }
}
