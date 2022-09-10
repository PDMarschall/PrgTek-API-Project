using Microsoft.AspNetCore.Mvc;
using PTAP.Infrastructure;
using PTAP.Web.Models;
using System.Diagnostics;

namespace PTAP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KanyeClient _kanyeClient;

        public HomeController(ILogger<HomeController> logger, KanyeClient kanye)
        {
            _logger = logger;
            _kanyeClient = kanye;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}