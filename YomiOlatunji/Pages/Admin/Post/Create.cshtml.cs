using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YomiOlatunji.Pages.Admin.Post
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
