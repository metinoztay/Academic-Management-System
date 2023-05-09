using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	
	[Authorize]
    public class StudentController : Controller
    {		
		TblUser activeStudent = new TblUser();
		public IActionResult Index(TblUser student)
        {
			if (!ActiveUser.isSettedInformations)
                SetActiveUserInformations(student);      

            GetActiveUserInformations();
            return View(activeStudent);
        }

        public IActionResult Timetable()
        {
			GetActiveUserInformations();
            return View(activeStudent);
		}

        public IActionResult ViewNotes()
        {
            GetActiveUserInformations();
            return View(activeStudent);
        }

        public IActionResult CourseSelection()
        {
            GetActiveUserInformations();
            return View(activeStudent);
        }

        public IActionResult Discontinuity()
        {
            GetActiveUserInformations();
            return View(activeStudent);
        }

        public IActionResult Announcements()
        {
            GetActiveUserInformations();
            return View(activeStudent);
        }

        public IActionResult MyProfile()
        {
            GetActiveUserInformations();
            return View(activeStudent);
        }

		public IActionResult Logout()
		{
            ActiveUser.isSettedInformations = false;
            return RedirectToAction("Logout","Account");
		}

		private void GetActiveUserInformations()
		{
			activeStudent.Username = ActiveUser.Username;
			activeStudent.Name=ActiveUser.Name;
			activeStudent.Surname=ActiveUser.Surname;
			activeStudent.Email=ActiveUser.Email;
			activeStudent.Phone=ActiveUser.Phone;
			activeStudent.Password=ActiveUser.Password;
			activeStudent.Authority = ActiveUser.Authority;
			activeStudent.Adress = ActiveUser.Adress;
			activeStudent.District= ActiveUser.District;
			activeStudent.Province=ActiveUser.Province;
			activeStudent.SecurityKey=ActiveUser.SecurityKey;
		}

		private void SetActiveUserInformations(TblUser studentModel)
		{
			ActiveUser.Username = studentModel.Username;
			ActiveUser.Name = studentModel.Name;
			ActiveUser.Surname = studentModel.Surname;
			ActiveUser.Email=studentModel.Email;
			ActiveUser.Phone=studentModel.Phone;
			ActiveUser.Password=studentModel.Password;
			ActiveUser.Authority = studentModel.Authority;
			ActiveUser.Adress=studentModel.Adress;
			ActiveUser.District=studentModel.District;
			ActiveUser.Province=studentModel.Province;
			ActiveUser.SecurityKey=studentModel.SecurityKey;
			ActiveUser.isSettedInformations = true;
		}

    }
}
