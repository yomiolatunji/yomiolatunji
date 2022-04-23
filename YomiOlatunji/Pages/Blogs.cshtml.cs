using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Interface;

namespace YomiOlatunji.Pages
{
    public class BlogsModel : PageModel
    {
        public BlogsModel(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        private readonly IPostRepository _postRepository;
        PageModel<Post> Posts { get; set; }
        public async Task OnGet()
        {
            //GetPage(Expression<Func<TEntity, bool>> ? filter = null, int intPageIndex = 1, int intPageSize = 20, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ? orderBy = null, string includeProperties = "")
            Posts = await _postRepository.GetPage();
        }
    }
}
