using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YomiOlatunji.Areas.Admin.Pages.Posts
{
    [Authorize]
    public class PublishModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
