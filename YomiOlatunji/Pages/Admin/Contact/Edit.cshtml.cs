using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Pages.Admin.Contact
{
    public class EditModel : PageModel
    {
        private readonly YomiOlatunji.DataSource.ApplicationDbContext _context;

        public EditModel(YomiOlatunji.DataSource.ApplicationDbContext context)
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

            var contactmessage =  await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            if (contactmessage == null)
            {
                return NotFound();
            }
            ContactMessage = contactmessage;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ContactMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactMessageExists(ContactMessage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactMessageExists(long id)
        {
          return (_context.ContactMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
