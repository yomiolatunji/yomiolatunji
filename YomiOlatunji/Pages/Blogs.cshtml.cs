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
        public PagedList<Post> Post { get; set; }
        public async Task OnGet(int pageNumber = 1, int pageSize = 20)
        {
            Post = await _postService.GetPost(pageNumber, pageSize, a => a.IsPublished == true, a => a.OrderByDescending(s => s.PublishDate), "Category");
        }
    }
}
