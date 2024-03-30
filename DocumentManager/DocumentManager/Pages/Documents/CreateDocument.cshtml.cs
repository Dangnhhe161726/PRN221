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
using Microsoft.AspNetCore.Identity;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class CreateDocumentModel : PageModel
    {
        private readonly DocumentManager.Services.ApplicationDbcontext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<Users> _userManager;
        public CreateDocumentModel(DocumentManager.Services.ApplicationDbcontext context, IWebHostEnvironment webHostEnvironment, UserManager<Users> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Category> categories = _context.Categories.ToList();
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
            ViewData["SpecializedId"] = new SelectList(_context.Specializes, "Name", "Name");
            ViewData["Aid"] = new SelectList(_context.Users, "Id", "LastName");
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public IFormFile fileDocument { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        [BindProperty(SupportsGet = true)]
        public String error { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Documents == null || Document == null)
            {
                return Page();
            }
            if (Document.DateSign > Document.DateTo)
            {
                error = "Date sign not more than date to";
                return Page();
            }
            if (Document.DateSign > Document.DateOut)
            {
                error = "Date sign not more than date out";
                return Page();
            }
            if (Document.DateTo > Document.DateOut)
            {
                error = "Date to not more than date out";
                return Page();
            }
            if (fileDocument != null)
            {
                string folder = "data/";
                Document.NameFile = fileDocument.FileName;
                Document.LinkFile = await UploadFile(folder, fileDocument);
            }
            Document.Status = true;
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
