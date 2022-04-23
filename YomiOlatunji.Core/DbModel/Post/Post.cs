using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YomiOlatunji.Core.DbModel.Audit;

namespace YomiOlatunji.Core.DbModel.Post
{
    public class Post : AuthAuditTrail
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public string HeaderImage { get; set; }
        public bool CanComment { get; set; } = true;
        public short PublishStatusId { get; set; }

        public PublishStatus PublishStatus { get; set; }

        public DateTimeOffset? PublishDate { get; set; }
        public IList<PostTag> Tags { get; set; } = new List<PostTag>();
        public IList<PostLink> Links { get; set; } = new List<PostLink>();
        public IList<Comment> Comments { get; set; }
    }

    public class Comment : AuthAuditTrail
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Post")]
        public long PostId { get; set; }

        public Post Post { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public bool IsAnonymous { get; set; }
        public string Message { get; set; }
        public string ParentId { get; set; }
    }

    public class PostTag
    {
        //[Key]
        //public Guid Id { get; set; } = new Guid();

        [ForeignKey("Post")]
        public long PostId { get; set; }

        public Post Post { get; set; }

        [ForeignKey("Tag")]
        public long TagId { get; set; }

        public Tag Tag { get; set; }

        [NotMapped]
        public string Name { get; set; }
    }
}