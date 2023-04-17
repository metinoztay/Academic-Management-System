using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	
	[Authorize]
    public class StudentController : Controller
    {
		TblUser student = new TblUser();
		public IActionResult Index(TblUser s)
        {
			student = s;
			return View(student);
        }

        public IActionResult Timetable()
        {
			return View(student);
		}

        public IActionResult ViewNotes()
        {
			return View(student);
		}

        public IActionResult CourseSelection()
        {
			return View(student);
		}

        public IActionResult Discontinuity()
        {
			return View(student);
		}

        public IActionResult Announcements()
        {
			return View(student);
		}

        public IActionResult MyProfile(TblUser s)
        {
			return View(s);
		}

		public IActionResult Logout()
		{
			return RedirectToAction("Logout","Account");
		}
	}
}
