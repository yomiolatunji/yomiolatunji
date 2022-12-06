using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
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

        public Post Post { get; set; }
        public async Task<IActionResult> OnGet(string slug)
        {
            var post = await _context.Posts.Include(s=>s.Category).FirstOrDefaultAsync(a => a.Slug.ToLower() == slug.ToLower());
            if (post == null)
            {
                return NotFound();
            }

            Post = post;
            return Page();
        }
    }
}
