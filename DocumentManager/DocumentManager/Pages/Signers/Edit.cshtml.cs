using DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Pages.Signers
{
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public EditModel(DocumentManager.Services.ApplicationDbcontext context)
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
            Signer = signer;
            ViewData["Pid"] = new SelectList(_context.Positions, "Id", "Name");
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

            _context.Attach(Signer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignerExists(Signer.Id))
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

        private bool SignerExists(int id)
        {
            return (_context.Signers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
