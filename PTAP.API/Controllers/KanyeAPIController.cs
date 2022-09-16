using Microsoft.AspNetCore.Mvc;

namespace PTAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KanyeAPIController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetImage()
        {
            FileStream stream = System.IO.File.Open(@"./Images/Kanye-West.jpg", FileMode.Open);
            return File(stream, "image/jpeg");
        }
    }
}