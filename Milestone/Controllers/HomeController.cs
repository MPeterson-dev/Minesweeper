using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Diagnostics;

namespace Milestone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // The code `public HomeController(ILogger<HomeController> logger)` is a constructor for the HomeController class.
        // It takes in a parameter of type `ILogger<HomeController>` named `logger`.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The function returns a view for the Index page.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The function returns a view for the Privacy page.
        /// </summary>
        /// <returns>
        /// The method is returning a View.
        /// </returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The above function is an error handler in C# that returns a view with an ErrorViewModel object.
        /// </summary>
        /// <returns>
        /// The method is returning a View with an ErrorViewModel object as the model.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
