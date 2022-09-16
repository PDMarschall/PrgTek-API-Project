using Microsoft.AspNetCore.Mvc;

namespace PTAP.API.Controllers
{
    public class KanyeAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetImage()
        {
            FileStream stream = System.IO.File.Open(@"E:\\Test.jpg", FileMode.Open);
            return File(stream, "image/jpeg");
        }
    }
}