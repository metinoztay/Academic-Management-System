using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	[Authorize]
	public class TeacherController : Controller
    {
        TblUser teacher = new TblUser();
        public IActionResult Index(TblUser t)
        {
            teacher = t;
            return View(teacher);
        }

        public IActionResult Lessons()
        {
			return View(teacher);
		}

        public IActionResult EntryNote()
        {
			return View(teacher);
		}

        public IActionResult ByFaculty()
        {
			return View(teacher);
		}

        public IActionResult BySection()
        {
			return View(teacher);
		}

        public IActionResult ByLesson()
        {
			return View(teacher);
		}

        public IActionResult Discontinuity()
        {
			return View(teacher);
		}

        public IActionResult Announcements()
        {
			return View(teacher);
		}

        public IActionResult MakeAnnouncement()
        {
			return View(teacher);
		}

        public IActionResult MyProfile()
        {
			return View(teacher);
		}
		public IActionResult Logout()
		{
			return RedirectToAction("Logout", "Account");
		}
	}
}
