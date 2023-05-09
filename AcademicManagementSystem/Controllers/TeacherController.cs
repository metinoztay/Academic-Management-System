using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace AcademicManagementSystem.Controllers
{
	[Authorize]
	public class TeacherController : Controller
    {
        TblUser activeTeacher = new TblUser();
        public IActionResult Index(TblUser teacher)
        {
            if (!ActiveUser.isSettedInformations)
                SetActiveUserInformations(teacher);

            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult Lessons()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult EntryNote()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult ByFaculty()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult BySection()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult ByLesson()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult Discontinuity()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult Announcements()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult MakeAnnouncement()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }

        public IActionResult MyProfile()
        {
            GetActiveUserInformations();
            return View(activeTeacher);
        }
		public IActionResult Logout()
		{
            ActiveUser.isSettedInformations = false;
            return RedirectToAction("Logout", "Account");
		}

        private void GetActiveUserInformations()
        {
            activeTeacher.Username = ActiveUser.Username;
            activeTeacher.Name = ActiveUser.Name;
            activeTeacher.Surname = ActiveUser.Surname;
            activeTeacher.Email = ActiveUser.Email;
            activeTeacher.Phone = ActiveUser.Phone;
            activeTeacher.Password = ActiveUser.Password;
            activeTeacher.Authority = ActiveUser.Authority;
            activeTeacher.Adress = ActiveUser.Adress;
            activeTeacher.District = ActiveUser.District;
            activeTeacher.Province = ActiveUser.Province;
            activeTeacher.SecurityKey = ActiveUser.SecurityKey;
        }

        private void SetActiveUserInformations(TblUser studentModel)
        {
            ActiveUser.Username = studentModel.Username;
            ActiveUser.Name = studentModel.Name;
            ActiveUser.Surname = studentModel.Surname;
            ActiveUser.Email = studentModel.Email;
            ActiveUser.Phone = studentModel.Phone;
            ActiveUser.Password = studentModel.Password;
            ActiveUser.Authority = studentModel.Authority;
            ActiveUser.Adress = studentModel.Adress;
            ActiveUser.District = studentModel.District;
            ActiveUser.Province = studentModel.Province;
            ActiveUser.SecurityKey = studentModel.SecurityKey;
            ActiveUser.isSettedInformations = true;
        }
    }
}
