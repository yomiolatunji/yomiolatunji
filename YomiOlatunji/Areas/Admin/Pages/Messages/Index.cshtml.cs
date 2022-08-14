﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Areas.Admin.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
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
