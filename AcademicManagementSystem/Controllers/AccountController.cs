using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
