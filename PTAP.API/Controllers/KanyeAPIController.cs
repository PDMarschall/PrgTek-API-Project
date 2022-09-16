using Microsoft.AspNetCore.Mvc;

namespace PTAP.API.Controllers
{
    public class KanyeAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
