using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.Core.ViewModel;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Areas.Admin.Pages.Posts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IMapper mapper,IPostService postService,ICategoryService categoryService)
        {
            _mapper = mapper;
            _postService = postService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return Page();
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public CreatePost Post { get; set; }

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //StatusMessage = ModelState.Values.Where(a=>a.va);
                return Page();
            }
            var mappedPost = _mapper.Map<Post>(Post);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var saved = await _postService.AddPost(mappedPost, userName);
            if (saved)
            {
                StatusMessage = "Post successfully added";
            }

            return RedirectToPage("./Index");
        }
    }
}
