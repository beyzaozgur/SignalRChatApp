using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using SignalRChatClient.Models;
using System.Diagnostics;

namespace SignalRChatClient.Controllers
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
