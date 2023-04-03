using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
