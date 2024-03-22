using DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Pages.Signers
{
    public class DeleteModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public DeleteModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public Signer Signer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Signers == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signer = await _context.Signers
                                        .Include(s => s.PidNavigation)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (signer == null)
            {
                return NotFound();
            }


            _context.Signers.Remove(signer);




            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }


    }
}
