using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	[Authorize]
	public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Lessons()
        {
            return View();
        }

        public IActionResult EntryNote()
        {
            return View();
        }

        public IActionResult ByFaculty()
        {
            return View();
        }

        public IActionResult BySection()
        {
            return View();
        }

        public IActionResult ByLesson()
        {
            return View();
        }

        public IActionResult Discontinuity()
        {
            return View();
        }

        public IActionResult Announcements()
        {
            return View();
        }

        public IActionResult MakeAnnouncement()
        {
            return View();
        }

        public IActionResult MyProfile()
        {
            return View();
        }
		public IActionResult Logout()
		{
			return RedirectToAction("Logout", "Account");
		}
	}
}
