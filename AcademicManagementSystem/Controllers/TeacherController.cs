﻿using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NuGet.DependencyResolver;

namespace AcademicManagementSystem.Controllers
{
    

    [Authorize]
	public class TeacherController : Controller
    {
        DbAcademicMsContext dbAcademicMsContext = new DbAcademicMsContext();
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
            var teacherInformations = dbAcademicMsContext.TblTeachers
                    .Find(ActiveUser.Username);
            var lessonList = dbAcademicMsContext.TblLessons
                .Where(l => (l.TeacherId == ActiveUser.Username)
                )
                .OrderBy(d => d.LessonDayIndex);
            return View(lessonList);
        }

        public IActionResult EntryNote()
        {
            return View();
        }

        public IActionResult StudentList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StudentList(string value1, string value2, string value3)
        {
            return View();
        }

        public IActionResult Discontinuity()
        {
            var lessons = dbAcademicMsContext.TblLessons
                .Where(l => l.TeacherId == ActiveUser.Username).ToList();
            TeacherLessons.lessonNames.Clear();
            foreach (var lesson in lessons)
            {
                TeacherLessons.lessonNames.Add(lesson.LessonName);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Discontinuity(TblLesson lesson)
        {
            string LessonCode = dbAcademicMsContext.TblLessons
                .FirstOrDefault(l => l.LessonName == lesson.LessonName).Code;
            var discontinuityTable = dbAcademicMsContext.TblDiscontinuities
                .Where(s => s.LessonCode == LessonCode)
                .OrderBy(s => s.StudentName);

            return View(discontinuityTable);
        }

        [HttpPost]
        public IActionResult DiscontinuitySave(TblDiscontinuity discontinuity)
        {

            return View();
        }

        public IActionResult Announcements()
        {
            var announcements = dbAcademicMsContext.TblAnnouncements
                .Where(d => d.LastDate >= DateTime.Now)
                .OrderBy(d => d.PostDate);
            return View(announcements);
        }

        public IActionResult MakeAnnouncement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeAnnouncement(TblAnnouncement announce)
        {
            announce.PostDate = DateTime.Now;
            announce.TeacherName = ActiveUser.Name + " " + ActiveUser.Surname;
            dbAcademicMsContext.TblAnnouncements.Add(announce);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("Announcements");
        }

        public IActionResult MyProfile()
        {
            var teacher = dbAcademicMsContext.TblUsers.Find(ActiveUser.Username);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult MyProfile(TblUser teacher)
        {
            teacher.Authority = "Teacher";
            if (teacher.Province == null)
                teacher.Province = ActiveUser.Province;

            if (teacher.Password == null)
                teacher.Password = ActiveUser.Password;

            dbAcademicMsContext.TblUsers.Update(teacher);
            dbAcademicMsContext.SaveChanges();
            return View(teacher);
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
