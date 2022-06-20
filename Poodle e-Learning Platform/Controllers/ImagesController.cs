using Microsoft.AspNetCore.Mvc;
using Poodle_e_Learning_Platform.Services.Contracts;

namespace Poodle_e_Learning_Platform.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        public IActionResult GetOriginal(string id)
        {
            byte[] content = imageService.GetOrignal(id);

            return File(content, "image/jpeg");
        }


        [HttpGet]
        public IActionResult GetVisualize(string id)
        {
            byte[] content = imageService.GetVisualize(id);

            return File(content, "image/jpeg");
        }

        [HttpGet]
        public IActionResult GetThumbnail (string id)
        {
            byte[] content = imageService.GetThumbnail(id);

            return File(content, "image/jpeg");
        }
    }
}
