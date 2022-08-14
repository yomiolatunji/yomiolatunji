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
    public class DeleteModel : PageModel
    {
        private readonly YomiOlatunji.DataSource.ApplicationDbContext _context;

        public DeleteModel(YomiOlatunji.DataSource.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ContactMessage ContactMessage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.ContactMessages == null)
            {
                return NotFound();
            }

            var contactmessage = await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);

            if (contactmessage == null)
            {
                return NotFound();
            }
            else 
            {
                ContactMessage = contactmessage;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.ContactMessages == null)
            {
                return NotFound();
            }
            var contactmessage = await _context.ContactMessages.FindAsync(id);

            if (contactmessage != null)
            {
                ContactMessage = contactmessage;
                _context.ContactMessages.Remove(ContactMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
