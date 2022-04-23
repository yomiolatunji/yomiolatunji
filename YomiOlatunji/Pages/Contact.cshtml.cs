using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactModel(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public ContactMessage ContactMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var response=await _contactMessageService.AddMessage(ContactMessage);
            if (response)
            {
                TempData["success"] = "Message Sent";
                ContactMessage=new ContactMessage();
            }
            else
            {
                TempData["error"] = "Error sending message";
            }
            return Page();
        }
    }
}
