using Microsoft.AspNetCore.Mvc;

namespace PTAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KanyeAPIController : ControllerBase
    {
        private readonly Random _random = new Random();

        [HttpGet]
        public IActionResult GetImage()
        {
            string imagePath = GetRandomImagePath();

            if (imagePath != "")
            {
                return File(GetImage(imagePath), "image/jpeg");
            }

            return NoContent();

        }

        private FileStream GetImage(string path)
        {
            FileStream image = System.IO.File.Open(path, FileMode.Open);
            return image;
        }

        private string GetRandomImagePath()
        {
            DirectoryInfo imageDirectoryInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"Images/"));
            FileInfo[] imagesFileInfo = imageDirectoryInfo.GetFiles().Where(x => x.Extension == ".jpg").ToArray();

            if (imagesFileInfo.Count() > 0)
            {
                string randomImagePath = imagesFileInfo[_random.Next(0, imagesFileInfo.Length)].FullName;
                return randomImagePath;
            }

            return "";
        }
    }
}