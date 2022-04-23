#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Core.DbModel.Post.Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _categoryService.GetAllCategories();
        }
    }
}
