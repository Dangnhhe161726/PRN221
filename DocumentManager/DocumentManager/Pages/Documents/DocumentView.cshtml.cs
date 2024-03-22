using DocumentManager.Models;
using DocumentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.Documents
{
	[Authorize]
	public class DocumentViewModel : PageModel
	{
		private readonly ApplicationDbcontext context;
		public IList<Document> documents { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int CategoryIdSelected { get; set; }
		public string NameSelected { get; set; }
		public List<Category> categories { get; set; }
		public Category GetCategory { get; set; }
		public string search { get; set; }

		public DocumentViewModel(ApplicationDbcontext context)
		{
			this.context = context;
		}
		public void OnGet(int? PageIndex)
		{
			if (PageIndex == 0 || PageIndex == null)
			{
				PageIndex = 1;
			}
			const int pageSize = 3;
			CurrentPage = (int)PageIndex;
			documents = context.Documents
				.Where(d => d.Status == true)
				.OrderBy(x => x.Id)
				.Skip((int)((PageIndex - 1) * pageSize))
				.Take(pageSize)
				.ToList();
			var totalCount = context.Documents.Where(d => d.Status == true).Count();
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


			categories = context.Categories.ToList();
		}

		public void OnPost(int CategoryId = 0, int PageIndex = 0, string searchname = "")
		{
			List<Document> query = context.Documents.Where(d => d.Status == true).ToList();

			if (!string.IsNullOrEmpty(searchname))
			{
				query = query.Where(d => d.Describe.Contains(searchname)).ToList();
				search = searchname;
			}

			if (CategoryId != 0)
			{
				query = query.Where(d => d.CategoryId == CategoryId).ToList();
				GetCategory = context.Categories.FirstOrDefault(s => s.Id == CategoryId);
			}

			const int pageSize = 3;

			if (PageIndex <= 0)
			{
				PageIndex = 1;
			}

			CurrentPage = PageIndex;

			documents = query.OrderBy(x => x.Id)
							 .Skip((PageIndex - 1) * pageSize)
							 .Take(pageSize)
							 .ToList();

			var totalCount = query.Count();
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

			categories = context.Categories.ToList();
		}

	}
}
