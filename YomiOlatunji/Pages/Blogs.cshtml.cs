using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Interface;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages
{
    public class BlogsModel : PageModel
    {
        public BlogsModel(IPostService postService)
        {
            _postService = postService;
        }
        private readonly IPostService _postService;
        public PageModel<Post> Post { get; set; }
        public async Task OnGet()
        {
            Post = await _postService.GetPost(1,20,a=>a.IsPublished==true,null,"Category");
        }
    }
}
