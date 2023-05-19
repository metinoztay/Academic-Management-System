using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
	
	[Authorize]
    public class StudentController : Controller
    {		
        DbAcademicMsContext dbAcademicMsContext = new DbAcademicMsContext();
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
            var studentInformations = dbAcademicMsContext.TblStudents
                    .Find(ActiveUser.Username);
            var lessonList = dbAcademicMsContext.TblLessons
                .Where(l => (
                l.Faculty == studentInformations.Faculty &&
                l.Class == studentInformations.Class &&
                l.Course == studentInformations.Course
                ))
                .OrderBy(l => l.LessonTime);
            return View(lessonList);
		}

        public IActionResult ViewNotes()
        {
            return View();
        }

        public IActionResult CourseSelection()
        {
            var studentsLessons = dbAcademicMsContext.TblStudentsLessons
                .Where(l => l.StudentId == ActiveUser.Username).ToList();
            StudentLessons.studentLessons.Clear();
            foreach (var studentLesson in studentsLessons)
            {
                var lessonInformations = dbAcademicMsContext.TblLessons.Find(studentLesson.LessonCode);
                StudentLessons.studentLessons.Add(lessonInformations);
            }

            var studentInformations = dbAcademicMsContext.TblStudents
                .Find(ActiveUser.Username);

            var studentLessons = dbAcademicMsContext.TblLessons
                .Where(l => l.Class == studentInformations.Class
                        && l.Course == studentInformations.Course)
                .OrderBy(l=>l.LessonName).ToList();
            
            List<TblLesson> deleteLessons = new List<TblLesson>();
            foreach(var lesson in studentLessons)
            {
                foreach(var l in StudentLessons.studentLessons)
                {
                    if (l.Code == lesson.Code)
                    {
                        deleteLessons.Add(lesson);
                    }
                }
            }

            foreach (var lesson in deleteLessons)
            {
                studentLessons.Remove(lesson);
            }

            return View(studentLessons);
        }

        [HttpPost]
        public IActionResult CourseAdd(String lessonCode)
        {
            var addLesson = new TblStudentsLesson();
            addLesson.StudentId = ActiveUser.Username;
            addLesson.LessonCode = lessonCode;
            dbAcademicMsContext.TblStudentsLessons.Add(addLesson);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("CourseSelection");
        }

        [HttpPost]
        public IActionResult CourseDelete(String lessonCode)
        {
            var deleteLesson = dbAcademicMsContext.TblStudentsLessons
                .FirstOrDefault(x => x.StudentId == ActiveUser.Username
                && x.LessonCode == lessonCode);
            dbAcademicMsContext.TblStudentsLessons.Remove(deleteLesson);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("CourseSelection");
        }

        public IActionResult Discontinuity()
        {
            var discontinuities = dbAcademicMsContext.TblDiscontinuities
                .Where(s => s.StudentId == ActiveUser.Username)
                .OrderBy(l => l.LessonName);
            return View(discontinuities);
        }

        public IActionResult Announcements()
        {
            var announcements = dbAcademicMsContext.TblAnnouncements
                .Where(d => d.LastDate >= DateTime.Now)
                .OrderBy(d => d.PostDate);
            return View(announcements);
        }

        public IActionResult MyProfile()
        {
            var student = dbAcademicMsContext.TblUsers.Find(ActiveUser.Username);
            return View(student);
        }

        [HttpPost]
        public IActionResult MyProfile(TblUser student)
        {
            student.Authority = "Student";
            if (student.Province == null)
                student.Province = ActiveUser.Province;

            if (student.Password == null)
                student.Password = ActiveUser.Password;           

            dbAcademicMsContext.TblUsers.Update(student);
            dbAcademicMsContext.SaveChanges();
            return View(student);
        }

        public IActionResult Logout()
		{
            ActiveUser.isSettedInformations = false;
            return RedirectToAction("Logout","Account");
		}

		private void GetActiveUserInformations()
		{
			activeStudent.Username = ActiveUser.Username;
			activeStudent.Name = ActiveUser.Name;
			activeStudent.Surname = ActiveUser.Surname;
			activeStudent.Email = ActiveUser.Email;
			activeStudent.Phone = ActiveUser.Phone;
			activeStudent.Password = ActiveUser.Password;
			activeStudent.Authority = ActiveUser.Authority;
			activeStudent.Adress = ActiveUser.Adress;
			activeStudent.District = ActiveUser.District;
			activeStudent.Province = ActiveUser.Province;
			activeStudent.SecurityKey = ActiveUser.SecurityKey;
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
