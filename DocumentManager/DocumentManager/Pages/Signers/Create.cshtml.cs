using DocumentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentManager.Pages.Signers
{
    public class CreateModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;

        public CreateModel(DocumentManager.Services.ApplicationDbcontext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Pid"] = new SelectList(_context.Positions, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Signer Signer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Signers == null || Signer == null)
            {
                return Page();
            }

            _context.Signers.Add(Signer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
