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
using Microsoft.AspNetCore.Identity;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<Users> _userManager;
        public EditModel(DocumentManager.Services.ApplicationDbcontext context, IWebHostEnvironment webHostEnvironment, UserManager<Users> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [BindProperty]
        public Document Document { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public IFormFile fileDocument { get; set; }

        [BindProperty(SupportsGet = true)]
        public string roleName { get; set; }

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
            var user = await _userManager.GetUserAsync(User);
            List<Category> categories = _context.Categories.ToList();
            Document = document;
            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);

                foreach (var r in role)
                {
                    switch (r)
                    {
                        case "admin":
                        case "employee":
                            categories = categories.Where(c => c.Id == 1 || c.Name.Equals("Đang chờ xác nhận", StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case "senior":
                            categories = categories.Where(c => (c.Id == 1 || c.Name.Equals("Đang chờ xác nhận", StringComparison.OrdinalIgnoreCase)) ||
                                                               (c.Id == 2 || c.Name.Equals("Đang chờ ký", StringComparison.OrdinalIgnoreCase)) ||
                                                               (c.Id == 4 || c.Name.Equals("Đã hết hạn", StringComparison.OrdinalIgnoreCase)) ||
                                                               (c.Id == 5 || c.Name.Equals("Không được duyệt", StringComparison.OrdinalIgnoreCase))).ToList();
                            break;
                        case "signer":
                            categories = categories.Where(c => (c.Id == 3 || c.Name.Equals("Đã ký", StringComparison.OrdinalIgnoreCase)) ||
                                                               (c.Id == 6 || c.Name.Equals("Huỷ", StringComparison.OrdinalIgnoreCase))).ToList();
                            break;
                    }
                }
            }

            ViewData["AgencyIssuesId"] = new SelectList(_context.AgencyIssues, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["EmergencyId"] = new SelectList(_context.Emergencies, "Id", "Name");
            ViewData["GroupDocumentId"] = new SelectList(_context.GroupDocuments, "Id", "Name");
            ViewData["NameDocumentId"] = new SelectList(_context.NameDocuments, "Id", "Name");
            ViewData["SecurityId"] = new SelectList(_context.Sercurities, "Id", "Name");
            ViewData["SignerId"] = new SelectList(_context.Signers, "Id", "Name");
            ViewData["SpecializedId"] = new SelectList(_context.Specializes, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);
                foreach (var r in role)
                {
                    roleName = r;
                }
            }

            Document documentExisting = await _context.Documents.FirstOrDefaultAsync(m => m.Id == Document.Id);

            switch (roleName)
            {
                case "admin":
                case "employee":

                    if (fileDocument != null)
                    {
                        string folder = "data/";
                        documentExisting.NameFile = fileDocument.FileName;
                        documentExisting.LinkFile = await UploadFile(folder, fileDocument);
                    }
                    documentExisting.Describe = Document.Describe;
                    documentExisting.Quantity = Document.Quantity;
                    documentExisting.AgencyIssuesId = Document.AgencyIssuesId;
                    documentExisting.EmergencyId = Document.EmergencyId;
                    documentExisting.GroupDocumentId = Document.GroupDocumentId;
                    documentExisting.NameDocumentId = Document.NameDocumentId;
                    documentExisting.SecurityId = Document.SecurityId;
                    documentExisting.SpecializedId = Document.SpecializedId;
                    //_context.Attach(Document).State = EntityState.Modified;
                    _context.Documents.Update(documentExisting);
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
                    break;
                case "signer":
                case "senior":
                    documentExisting.CategoryId = Document.CategoryId;
                    _context.Attach(documentExisting).State = EntityState.Modified;
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
                    break;
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
