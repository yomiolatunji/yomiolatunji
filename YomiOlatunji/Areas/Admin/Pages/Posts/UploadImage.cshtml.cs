using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core.ViewModel;

namespace YomiOlatunji.Areas.Admin.Pages.Posts
{
    public class UploadImageModel : PageModel
    {
        public async Task<JsonResult> OnPost(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new JsonResult(null);

            UploadFileResult result = new UploadFileResult();

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        GetUniqueFileName(file.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            result.Location = path;
            return new JsonResult(result);
        }
        public async Task<JsonResult> OnGet(IFormFile file)
        {
            return new JsonResult("test");
        }
            private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
