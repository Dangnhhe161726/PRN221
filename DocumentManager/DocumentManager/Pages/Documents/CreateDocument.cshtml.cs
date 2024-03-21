using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class CreateDocumentModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateDocumentModel(DocumentManager.Services.ApplicationDbcontext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["AgencyIssuesId"] = new SelectList(_context.AgencyIssues, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["EmergencyId"] = new SelectList(_context.Emergencies, "Id", "Name");
        ViewData["GroupDocumentId"] = new SelectList(_context.GroupDocuments, "Id", "Name");
        ViewData["NameDocumentId"] = new SelectList(_context.NameDocuments, "Id", "Name");
        ViewData["SecurityId"] = new SelectList(_context.Sercurities, "Id", "Name");
        ViewData["SignerId"] = new SelectList(_context.Signers, "Id", "Name");
        ViewData["SpecializedId"] = new SelectList(_context.Specializes, "Name", "Name");
        ViewData["Aid"] = new SelectList(_context.Users, "Id", "LastName");
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public IFormFile fileDocument { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD


        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Documents == null || Document == null)
            {
                return Page();
            }
            if(fileDocument != null)
            {
                string folder = "data/";
                Document.NameFile = fileDocument.FileName;
                Document.LinkFile = await UploadFile(folder, fileDocument);
            }

            _context.Documents.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./DocumentView");
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
