using Lazy.SlideCaptcha.Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lazy.SlideCaptcha.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("jquery")]
        public IActionResult Jquery()
        {
            return View();
        }

        [Route("vue")]
        public IActionResult Vue()
        {
            return View();
        }
    }
}