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
            var studentClass = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == ActiveUser.Username).Class;
            var studentInformations = dbAcademicMsContext.TblStudentsLessons
                    .Where(i => i.StudentId == ActiveUser.Username && i.Class == studentClass && i.Confirmed == true).ToList();
            List<TblLesson> lessonList = new List<TblLesson>();
            foreach (var l in studentInformations)
            {
                var lesson = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(x => x.Code == l.LessonCode);
                lessonList.Add(lesson);
            }
            return View(lessonList);
		}

        public IActionResult ViewNotes()
        {
            var studentClass = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId== ActiveUser.Username).Class;

            var lessons = dbAcademicMsContext.TblStudentsLessons
                .Where(l => l.StudentId == ActiveUser.Username 
                && l.Class == studentClass && l.Confirmed == true).ToList();

            List<LessonNoteModel> lessonsList = new List<LessonNoteModel>();
            foreach (var l in lessons)
            {
                LessonNoteModel less = new LessonNoteModel();
                less.StudentId = ActiveUser.Username;
                less.LessonCode = l.LessonCode;
                less.Class = l.Class;
                less.MidtermNote = l.MidtermNote;
                less.FinalNote = l.FinalNote;
                less.CompleteNote = l.CompleteNote;
                less.Average=l.Average;
                less.LetterGrade = l.LetterGrade;
                less.TeacherName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(t => t.Code == l.LessonCode).TeacherName;
                less.LessonName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(t => t.Code == l.LessonCode).LessonName;

                lessonsList.Add(less);  
            }
            return View(lessonsList);
        }

        public IActionResult Transcript()
        {
            var studentClass = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == ActiveUser.Username).Class;

            var lessons = dbAcademicMsContext.TblStudentsLessons
                .Where(l => l.StudentId == ActiveUser.Username && l.Class != studentClass).ToList();

            List<LessonNoteModel> lessonsList = new List<LessonNoteModel>();
            foreach (var l in lessons)
            {
                LessonNoteModel less = new LessonNoteModel();
                less.StudentId = ActiveUser.Username;
                less.LessonCode = l.LessonCode;
                less.Credit = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(t => t.Code == l.LessonCode).Credit;
                less.Class = l.Class;
                less.MidtermNote = l.MidtermNote;
                less.FinalNote = l.FinalNote;
                less.CompleteNote = l.CompleteNote;
                less.Average = l.Average;
                less.LetterGrade = l.LetterGrade;
                less.TeacherName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(t => t.Code == l.LessonCode).TeacherName;
                less.LessonName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(t => t.Code == l.LessonCode).LessonName;

                lessonsList.Add(less);
            }
            return View(lessonsList);
        }

        public IActionResult CourseSelection()
        {
            bool isCourseSelectionOpen = dbAcademicMsContext.IsCourseSelectionOpens
                .FirstOrDefault(i => i.Id == 1).CourseSelection;
            if (!isCourseSelectionOpen)
                return RedirectToAction("Index");

            var clas = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == ActiveUser.Username).Class;
            var studentsLessons = dbAcademicMsContext.TblStudentsLessons
                .Where(l => l.StudentId == ActiveUser.Username 
                && l.Class == clas).ToList();
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
            addLesson.Class = dbAcademicMsContext.TblLessons
                .FirstOrDefault(l => l.Code == lessonCode).Class;
            dbAcademicMsContext.TblStudentsLessons.Add(addLesson);
            dbAcademicMsContext.SaveChanges();

            var discLesson = new TblDiscontinuity();
            discLesson.StudentId = ActiveUser.Username;
            discLesson.StudentName = ActiveUser.Name + " " + ActiveUser.Surname;
            discLesson.LessonCode = lessonCode;
            discLesson.Class = addLesson.Class;
            discLesson.LessonName = dbAcademicMsContext.TblLessons.Find(lessonCode).LessonName;
            dbAcademicMsContext.TblDiscontinuities.Add(discLesson);
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

            var deleteDiscc = dbAcademicMsContext.TblDiscontinuities
                .FirstOrDefault(x => x.StudentId == ActiveUser.Username
                && x.LessonCode == lessonCode);
            dbAcademicMsContext.TblDiscontinuities.Remove(deleteDiscc);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("CourseSelection");
        }

        [HttpPost]
        public IActionResult SendToConfirm()
        {
            var studentModel = dbAcademicMsContext.TblStudentListForConfirms.FirstOrDefault(s => s.StudentId == ActiveUser.Username);
            if(studentModel == null)
            {
                TblStudentListForConfirm student = new TblStudentListForConfirm();
                student.StudentId = ActiveUser.Username;
                dbAcademicMsContext.TblStudentListForConfirms.Add(student);
                dbAcademicMsContext.SaveChanges();
            }            
            return RedirectToAction("index");
        }

        public IActionResult Discontinuity()
        {
            var clas = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == ActiveUser.Username).Class;
            var discontinuities = dbAcademicMsContext.TblDiscontinuities
                .Where(s => s.StudentId == ActiveUser.Username
                && s.Class == clas && s.Confirmed == true).OrderBy(l => l.LessonName);
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
