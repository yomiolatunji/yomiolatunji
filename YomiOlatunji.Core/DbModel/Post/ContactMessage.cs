using System.ComponentModel.DataAnnotations;
using YomiOlatunji.Core.DbModel.Audit;

namespace YomiOlatunji.Core.DbModel.Post
{
    public class ContactMessage : AuditTrail
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

        [MaxLength(400)]
        public string Title { get; set; }

        public string Message { get; set; }

        public bool? Read { get; set; }
        public DateTime? DateRead { get; set; }
    }
}