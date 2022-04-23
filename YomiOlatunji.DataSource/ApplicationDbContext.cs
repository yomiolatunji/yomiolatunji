using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.ActivityLog;
using YomiOlatunji.Core.DbModel.Post;

namespace YomiOlatunji.DataSource
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PublishStatus> PublishStatuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LinkType> LinkTypes { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostTag>()
            .HasKey(p => new { p.PostId, p.TagId });
        }
    }
}