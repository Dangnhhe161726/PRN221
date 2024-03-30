using DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Pages.Signers
{
    public class DetailsModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DetailsModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public Signer Signer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Signers == null)
            {
                return NotFound();
            }
            //FirstOrDefaultAsync(m => m.Id == id);
            var signer = await _context.Signers.Include(x => x.PidNavigation).FirstOrDefaultAsync(m => m.Id == id);
            if (signer == null)
            {
                return NotFound();
            }
            else
            {
                Signer = signer;
            }
            return Page();
        }
    }
}
