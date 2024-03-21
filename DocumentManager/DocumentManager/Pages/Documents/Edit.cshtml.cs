using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DocumentManager.Pages.Documents
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly DocumentManager.Services.ApplicationDbcontext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EditModel(DocumentManager.Services.ApplicationDbcontext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		[BindProperty]
		public Document Document { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public IFormFile fileDocument { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Documents == null)
			{
				return NotFound();
			}

			var document = await _context.Documents.FirstOrDefaultAsync(m => m.Id == id);
			if (document == null)
			{
				return NotFound();
			}
			Document = document;
			ViewData["AgencyIssuesId"] = new SelectList(_context.AgencyIssues, "Id", "Name");
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			ViewData["EmergencyId"] = new SelectList(_context.Emergencies, "Id", "Name");
			ViewData["GroupDocumentId"] = new SelectList(_context.GroupDocuments, "Id", "Name");
			ViewData["NameDocumentId"] = new SelectList(_context.NameDocuments, "Id", "Name");
			ViewData["SecurityId"] = new SelectList(_context.Sercurities, "Id", "Name");
			ViewData["SignerId"] = new SelectList(_context.Signers, "Id", "Name");
			ViewData["SpecializedId"] = new SelectList(_context.Specializes, "Id", "Name");
			ViewData["Aid"] = new SelectList(_context.Users, "Id", "LastName");
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
   //         var document = await _context.Documents.FirstOrDefaultAsync(m => m.Id == Document.Id);
			//Document.NameFile = document.NameFile;
			//Document.LinkFile = document.LinkFile;
            if (fileDocument != null)
            {
				//if(!string.IsNullOrEmpty(Document.LinkFile) && !string.IsNullOrEmpty(Document.NameFile))
				//{
    //                FileInfo file = new FileInfo(Document.LinkFile);
    //                if (!file.Exists)
    //                {
    //                    file.Delete();
    //                }
    //            }
                string folder = "data/";
                Document.NameFile = fileDocument.FileName;
                Document.LinkFile = await UploadFile(folder, fileDocument);
            }
			_context.Attach(Document).State = EntityState.Modified;
			try
            {
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DocumentExists(Document.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./DocumentView");
		}

		private bool DocumentExists(int id)
		{
			return (_context.Documents?.Any(e => e.Id == id)).GetValueOrDefault();
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
