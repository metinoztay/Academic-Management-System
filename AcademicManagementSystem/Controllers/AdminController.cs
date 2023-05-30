using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        DbAcademicMsContext dbAcademicMsContext = new DbAcademicMsContext();
        TblUser activeAdmin = new TblUser();
        public IActionResult Index(TblUser admin)
        {
            if (!ActiveUser.isSettedInformations)
                SetActiveUserInformations(admin);

            GetActiveUserInformations();
            return View(activeAdmin);
        }

        public IActionResult TeacherAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeacherAdd(TblUser user, TblTeacher teacher)
        {
            user.Authority = "Teacher";
            user.Password = user.Username;
            dbAcademicMsContext.TblUsers.Add(user);
            teacher.TeacherId = user.Username;
            dbAcademicMsContext.TblTeachers.Add(teacher);
            dbAcademicMsContext.SaveChanges();
            return View();
        }
        public IActionResult TeacherControl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TeacherControl(string teacherID)
        {
            var teacher = dbAcademicMsContext.TblUsers
                .FirstOrDefault(t => t.Username == teacherID);
            return View(teacher);
        }

        public IActionResult StudentAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StudentAdd(TblUser user, TblStudent student)
        {
            user.Authority = "Student";
            user.Password = user.Username;
            dbAcademicMsContext.TblUsers.Add(user);
            student.StudentId = user.Username;
            dbAcademicMsContext.TblStudents.Add(student);
            dbAcademicMsContext.SaveChanges();
            return View();
        }

        public IActionResult StudentControl()
        {
            return View();
        }

        public IActionResult LessonAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LessonAdd(TblLesson lesson)
        {
            dbAcademicMsContext.TblLessons.Add(lesson);
            dbAcademicMsContext.SaveChanges();
            return View();
        }

        public IActionResult LessonControl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LessonControl(string code)
        {
            TeachersList.teachers.Clear();
            TeachersList.teachers = dbAcademicMsContext.TblUsers
                .Where(t => t.Authority =="Teacher").ToList();

            var lesson = dbAcademicMsContext.TblLessons
                .FirstOrDefault(l => l.Code == code);

            return View(lesson);
        }

        [HttpPost]
        public IActionResult SaveLesson(TblLesson lesson)
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            lesson.LessonDay = days[(int)lesson.LessonDayIndex];
            lesson.TeacherName = dbAcademicMsContext.TblUsers
                .FirstOrDefault(t => t.Username == lesson.TeacherId).Name + " " +
                dbAcademicMsContext.TblUsers
                .FirstOrDefault(t => t.Username == lesson.TeacherId).Surname;
            dbAcademicMsContext.TblLessons.Update(lesson);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("LessonControl");
        }

        [HttpPost]
        public IActionResult DeleteLesson(string code)
        {
            var lesson = dbAcademicMsContext.TblLessons
                .FirstOrDefault(l => l.Code == code);
            dbAcademicMsContext.TblLessons.Remove(lesson);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("LessonControl");
        }

        public IActionResult Logout()
        {
            ActiveUser.isSettedInformations = false;
            return RedirectToAction("Logout", "Account");
        }

        private void GetActiveUserInformations()
        {
            activeAdmin.Username = ActiveUser.Username;
            activeAdmin.Name = ActiveUser.Name;
            activeAdmin.Surname = ActiveUser.Surname;
            activeAdmin.Email = ActiveUser.Email;
            activeAdmin.Phone = ActiveUser.Phone;
            activeAdmin.Password = ActiveUser.Password;
            activeAdmin.Authority = ActiveUser.Authority;
            activeAdmin.Adress = ActiveUser.Adress;
            activeAdmin.District = ActiveUser.District;
            activeAdmin.Province = ActiveUser.Province;
            activeAdmin.SecurityKey = ActiveUser.SecurityKey;
        }

        private void SetActiveUserInformations(TblUser adminModel)
        {
            ActiveUser.Username = adminModel.Username;
            ActiveUser.Name = adminModel.Name;
            ActiveUser.Surname = adminModel.Surname;
            ActiveUser.Email = adminModel.Email;
            ActiveUser.Phone = adminModel.Phone;
            ActiveUser.Password = adminModel.Password;
            ActiveUser.Authority = adminModel.Authority;
            ActiveUser.Adress = adminModel.Adress;
            ActiveUser.District = adminModel.District;
            ActiveUser.Province = adminModel.Province;
            ActiveUser.SecurityKey = adminModel.SecurityKey;
            ActiveUser.isSettedInformations = true;
        }
    }
}
