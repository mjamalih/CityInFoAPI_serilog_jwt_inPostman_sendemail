using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInFoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }
        [HttpGet]
        public ActionResult GetFile(string fileName)
        {
            string path=Path.Combine("Files",fileName);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            else
            {
                var bytes=System.IO.File.ReadAllBytes(path);
                if(!fileExtensionContentTypeProvider.TryGetContentType(path, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                return File(bytes, contentType, Path.GetFileName(path));
            }
        }
    }
}
