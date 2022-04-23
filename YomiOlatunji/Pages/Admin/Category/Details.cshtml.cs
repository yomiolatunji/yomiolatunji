#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages.Admin.Category
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DetailsModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Core.DbModel.Post.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _categoryService.GetCategoryById((int)id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
