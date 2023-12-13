using Microsoft.AspNetCore.Mvc;
using ResilienceWithPolly.HttpClients;
using ResilienceWithPolly.Models;
using System.Diagnostics;

namespace ResilienceWithPolly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostClient postClient;

        public HomeController(ILogger<HomeController> logger, IPostClient postClient)
        {
            _logger = logger;
            this.postClient = postClient;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await postClient.GetPostsAsync();
            return View(posts);
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
