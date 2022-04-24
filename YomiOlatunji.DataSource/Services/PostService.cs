using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core;
using YomiOlatunji.Core.DbModel.Post;

namespace YomiOlatunji.DataSource.Services.Interfaces
{
    public class PostService:IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PageModel<Post>> GetPost(int intPageIndex = 1, int intPageSize = 20, Expression<Func<Post, bool>>? filter = null, Func<IQueryable<Post>, IOrderedQueryable<Post>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Post> query = _context.Posts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                             (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            decimal totalCount = await query.CountAsync();

            int pageCount = (int)Math.Ceiling(totalCount / intPageSize);

            var list = await query.Skip((intPageIndex - 1) * intPageSize).Take(intPageSize).ToListAsync();

            return new PageModel<Post> { DataCount = (int)totalCount, PageCount = pageCount, PageNumber = intPageIndex, PageSize = intPageSize, Data = list };
        }

        public async Task<PageModel<Post>> GetAllPost(int intPageIndex, int intPageSize)
        {
            IQueryable<Post> query = _context.Posts;
            decimal totalCount = await query.CountAsync();

            int pageCount = (int)Math.Ceiling(totalCount / intPageSize);
            var list = await query.Skip((intPageIndex - 1) * intPageSize).Take(intPageSize).ToListAsync();

            return new PageModel<Post> { DataCount = (int)totalCount, PageCount = pageCount, PageNumber = intPageIndex, PageSize = intPageSize, Data = list };
        }

        public async Task<IList<Post>> GetAllPost()
        {
            return await _context.Posts.Where(a => !a.IsDeleted).ToListAsync();
        }
    }
}
