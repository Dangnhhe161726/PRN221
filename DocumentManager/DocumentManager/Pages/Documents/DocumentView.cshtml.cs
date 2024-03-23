using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Pages.Documents
{
    [Authorize]
    public class DocumentViewModel : PageModel
    {
        private readonly ApplicationDbcontext context;
        private readonly UserManager<Users> _userManager;
        public IList<Document> documents { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int CategoryIdSelected { get; set; }
        public string NameSelected { get; set; }
        //public List<Category> categories { get; set; }
        public Category GetCategory { get; set; }

		[BindProperty(SupportsGet = true)]
		public string search { get; set; }

        public DocumentViewModel(ApplicationDbcontext context, UserManager<Users> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? PageIndex)
        {
            IQueryable<Document> query = null;
            if (PageIndex == 0 || PageIndex == null)
            {
                PageIndex = 1;
            }
            const int pageSize = 3;
            CurrentPage = (int)PageIndex;

            var user = await _userManager.GetUserAsync(User);
            var role = user != null ? await _userManager.GetRolesAsync(user) : new List<string>();
            if (!String.IsNullOrEmpty(search) || search != null)
            {
                query = context.Documents
                                   .Include(c => c.Category)
                                   .Include(d => d.NameDocument)
                                   .Where(d => d.Status == true)
               .Where(s => s.Describe.Contains(search));
            }
            else
            {
                query = context.Documents
                                   .Include(c => c.Category)
                                   .Include(d => d.NameDocument)
                                   .Where(d => d.Status == true);
            }

            if (query != null)
            {
                if (role.Contains("senior"))
                {
                    query = query.Where(c => (c.Category.Id == 1 || c.Category.Name.ToLower().Equals("Đang chờ xác nhận".ToLower())) ||
                                             (c.Category.Id == 2 || c.Category.Name.ToLower().Equals("Đang chờ ký".ToLower())) ||
                                             (c.Category.Id == 4 || c.Category.Name.ToLower().Equals("Đã hết hạn".ToLower())) ||
                                             (c.Category.Id == 5 || c.Category.Name.ToLower().Equals("Không được duyệt".ToLower()))
                    );
                }
                else if (role.Contains("signer"))
                {
                    query = query.Where(c => (c.Category.Id == 2 || c.Category.Name.ToLower().Equals("đang chờ ký".ToLower())) ||
                                              (c.Category.Id == 3 || c.Category.Name.ToLower().Equals("đã ký".ToLower())) ||
                                              (c.Category.Id == 6 || c.Category.Name.ToLower().Equals("đang huỷ".ToLower())));
                }

                documents = await query.OrderBy(x => x.Id)
                                       .Skip((int)((PageIndex - 1) * pageSize))
                                       .Take(pageSize)
                                       .ToListAsync();

                var totalCount = await query.CountAsync();
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }

            return Page();
        }
    }
}
