using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YomiOlatunji.Core.ViewModel;

namespace YomiOlatunji.Areas.Admin.Pages.Posts
{
    public class UploadModel : PageModel
    {
        public void OnGet()
        {
           // return new JsonResult("test");
        }
        public async Task<JsonResult> OnPostAsync(IFormFile file)
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
