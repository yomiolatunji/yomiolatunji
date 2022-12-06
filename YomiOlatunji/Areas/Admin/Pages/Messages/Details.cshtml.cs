using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Areas.Admin.Pages.Messages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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

            ContactMessage = contactmessage;
            return Page();
        }
    }
}
