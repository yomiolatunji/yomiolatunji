using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using YomiOlatunji.Core;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Migrations;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.DataSource.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostManager _postManager;

        public PostService(ApplicationDbContext context,IPostManager postManager)
        {
            _context = context;
            _postManager = postManager;
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

        public async Task<bool> AddPost(Post post, string userName)
        {
            post.CanComment = true;
            post.IsDeleted = false;
            post.IsPublished = false;
            post.IsArchived = false;
            post.CreateTime=DateTimeOffset.Now;
            post.CreateBy = userName;
            post.Content = await _postManager.ExtractImageFromPost(post.Content);
            post.Slug = _postManager.ExtractUrlFromTitle(post.Title);
            post.Excerpt = post.Content.Substring(0, Math.Min(200, post.Content.Length));

            _context.Add(post);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> PublishPost(long postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(a => a.Id == postId);
            if (post == null)
            {
                return false;
            }
            post.PublishDate = DateTimeOffset.Now;
            post.IsPublished = true;

            //_repository.Update(category);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
