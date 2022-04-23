#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages.Admin.Category
{
    public class DeleteModel : PageModel
    {

        private readonly ICategoryService _categoryService;
        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory((int)id);

            return RedirectToPage("./Index");
        }
    }
}
