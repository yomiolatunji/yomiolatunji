#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages.Admin.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Core.DbModel.Post.Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _categoryService.AddCategory(Category);
            //_context.Category.Add(Category);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
