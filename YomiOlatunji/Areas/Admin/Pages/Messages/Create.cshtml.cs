using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource;

namespace YomiOlatunji.Areas.Admin.Pages.Messages
{
    public class CreateModel : PageModel
    {
        private readonly YomiOlatunji.DataSource.ApplicationDbContext _context;

        public CreateModel(YomiOlatunji.DataSource.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContactMessage ContactMessage { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ContactMessages == null || ContactMessage == null)
            {
                return Page();
            }

            _context.ContactMessages.Add(ContactMessage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
