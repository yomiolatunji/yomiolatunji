using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Pages.Admin.Contact
{
    public class IndexModel : PageModel
    {
        private readonly YomiOlatunji.DataSource.ApplicationDbContext _context;

        public IndexModel(YomiOlatunji.DataSource.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ContactMessage> ContactMessage { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ContactMessages != null)
            {
                ContactMessage = await _context.ContactMessages.ToListAsync();
            }
        }
    }
}
