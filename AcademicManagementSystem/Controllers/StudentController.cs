using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Timetable()
        {
            return View();
        }

        public IActionResult ViewNotes()
        {
            return View();
        }

        public IActionResult CourseSelection()
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

        public IActionResult MyProfile()
        {
            return View();
        }
    }
}
