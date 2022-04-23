using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YomiOlatunji.Core.DbModel.Audit
{
    public class AuditTrail
    {
        public AuditTrail()
        {
            CreateTime = DateTimeOffset.Now;
        }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public Guid? CreateBy { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid? UpdateBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
