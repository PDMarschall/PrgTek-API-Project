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
            GetRandomImage();
            FileStream stream = System.IO.File.Open(GetRandomImage(), FileMode.Open);
            return File(stream, "image/jpeg");
        }

        private string GetRandomImage()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"Images/"));
            var files = directoryInfo.GetFiles();

            return files[_random.Next(0, files.Length)].FullName;
        }
    }
}