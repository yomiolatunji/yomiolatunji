using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.Core.ViewModel;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BlogModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public BlogResponse Post { get; set; }
        public async Task<IActionResult> OnGet(string slug)
        {
            var post = await _context.Posts.Include(s=>s.Category).FirstOrDefaultAsync(a => a.Slug.ToLower() == slug.ToLower());
            if (post == null)
            {
                return NotFound();
            }

            Post = new BlogResponse
            {
                CanComment=post.CanComment,
                Category=post.Category,
                CategoryId=post.CategoryId,
                Content=post.Content,
                Excerpt=post.Excerpt,
                HeaderImage = post.HeaderImage,
                Id=post.Id,
                PublishDate=post.PublishDate,
                Slug=post.Slug,
                Title = post.Title,
                Tags=post.Tags,
                CreateBy=post.CreateBy
            };
            return Page();
        }
    }
}
