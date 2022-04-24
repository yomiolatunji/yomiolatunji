using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages.Admin.Post
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;

        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }
        public IList<Core.DbModel.Post.Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _postService.GetAllPost();
        }
    }
}
