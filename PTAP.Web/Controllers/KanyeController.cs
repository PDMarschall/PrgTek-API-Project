using Microsoft.AspNetCore.Mvc;

namespace PTAP.Web.Controllers
{
    // https://api.kanye.rest/
    public class KanyeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
