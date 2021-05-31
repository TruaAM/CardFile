using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.Models;
using System.Diagnostics;

namespace PL.Controllers
{
    /// <summary>
    /// This controller is first to be reprisented
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method returns view of Home(index)
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method returns view of Home(privace) but only if you are authenticated
        /// </summary>
        [Authorize]
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
