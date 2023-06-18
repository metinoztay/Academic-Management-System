using AcademicManagementSystem.Models;
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
            var lessons = dbAcademicMsContext.TblLessons
                .Where(l => l.TeacherId == ActiveUser.Username).ToList();
            TeacherLessons.lessonNames.Clear();
            foreach (var lesson in lessons)
            {
                TeacherLessons.lessonCodes.Add(lesson.Code);
                TeacherLessons.lessonNames.Add(lesson.LessonName);
            }
            return View();
        }

        [HttpPost]
        public IActionResult EntryNote(TblLesson lesson)
        {
            var studentList = dbAcademicMsContext.TblStudentsLessons
                .Where(l => l.LessonCode == lesson.Code && l.Confirmed==true).ToList();

            List<LessonNoteModel> studentModelList = new List<LessonNoteModel>();

            foreach (var student in studentList)
            {
                LessonNoteModel less = new LessonNoteModel();
                less.StudentId = student.StudentId;
                less.StudentName =dbAcademicMsContext.TblUsers
                    .FirstOrDefault(s => s.Username == student.StudentId).Name + " " +
                    dbAcademicMsContext.TblUsers
                    .FirstOrDefault(s => s.Username == student.StudentId).Surname;
                less.MidtermNote = student.MidtermNote;
                less.FinalNote = student.FinalNote;
                less.CompleteNote = student.CompleteNote;
                less.Average = student.Average;
                less.LetterGrade = student.LetterGrade;
                less.LessonCode = lesson.Code;
                less.Class = student.Class;
                less.Id = student.Id;
                studentModelList.Add(less);
            }

            return View(studentModelList);
        }

        [HttpPost]
        public IActionResult SaveNote(LessonNoteModel lessonNote)
        {
            TblStudentsLesson studentNote = new TblStudentsLesson();
            studentNote.StudentId = lessonNote.StudentId;
            studentNote.MidtermNote = lessonNote.MidtermNote;
            studentNote.FinalNote = lessonNote.FinalNote;
            studentNote.CompleteNote = lessonNote.CompleteNote;
            studentNote.LessonCode = lessonNote.LessonCode;
            studentNote.Class = (byte)lessonNote.Class;
            studentNote.Id = lessonNote.Id;
            studentNote.Confirmed = true;

            float average;
            if (lessonNote.FinalNote == null)
            {
                average = 0;
            }
            else if (lessonNote.CompleteNote == null)
            {
                average = (float)lessonNote.MidtermNote *0.4f + (float)lessonNote.FinalNote *0.6f;
            }
            else
            {
                average = (float)lessonNote.MidtermNote * 0.4f + (float)lessonNote.CompleteNote * 0.6f;
            }
            studentNote.Average = (int)average;
            

            string letterNote;
            if ( studentNote.FinalNote < 45 && lessonNote.CompleteNote == null)
                letterNote = "DC";
            else if (average >= 88)
                letterNote = "AA";
            else if (average >= 81)
                letterNote = "BA";
            else if (average >= 74)
                letterNote = "BB";
            else if (average >= 67)
                letterNote = "CB";
            else if (average >= 60)
                letterNote = "CC";
            else if (average >= 53)
                letterNote = "DC";
            else if (average >= 46)
                letterNote = "DD";
            else if (average >= 0)
                letterNote = "FD";
            else
                letterNote = "FF";

            studentNote.LetterGrade = letterNote;

            dbAcademicMsContext.TblStudentsLessons.Update(studentNote);
            dbAcademicMsContext.SaveChanges();

            TblLesson lesson = dbAcademicMsContext.TblLessons
                .FirstOrDefault(l => l.Code == lessonNote.LessonCode);
            return RedirectToAction("EntryNote","Teacher", lesson);
            //not kaydedildiğinde seçili olan dersin tekrar seçilmesi gerek?
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
                .Where(s => s.LessonCode == LessonCode && s.Confirmed == true)
                .OrderBy(s => s.StudentName);

            return View(discontinuityTable);
        }

        [HttpPost]
        public IActionResult DiscontinuitySave(TempDiscontinuity temp)
        {
            TblDiscontinuity discontinuity = new TblDiscontinuity();
            discontinuity.Id = temp.Id;
            discontinuity.StudentName = temp.StudentName;
            discontinuity.StudentId = temp.StudentId;
            discontinuity.LessonCode= temp.LessonCode;
            discontinuity.Class = (byte)temp.Class;
            discontinuity.LessonName = temp.LessonName;
            discontinuity.Confirmed = true;
            discontinuity.Week1 = (temp.Week1 == "on") ? true : false;
            discontinuity.Week2 = (temp.Week2 == "on") ? true : false;
            discontinuity.Week3 = (temp.Week3 == "on") ? true : false;
            discontinuity.Week4 = (temp.Week4 == "on") ? true : false;
            discontinuity.Week5 = (temp.Week5 == "on") ? true : false;
            discontinuity.Week6 = (temp.Week6 == "on") ? true : false;
            discontinuity.Week7 = (temp.Week7 == "on") ? true : false;
            discontinuity.Week8 = (temp.Week8 == "on") ? true : false;
            discontinuity.Week9 = (temp.Week9 == "on") ? true : false;
            discontinuity.Week10 = (temp.Week10 == "on") ? true : false;
            discontinuity.Week11 = (temp.Week11 == "on") ? true : false;
            discontinuity.Week12 = (temp.Week12 == "on") ? true : false;
            discontinuity.Week13 = (temp.Week13 == "on") ? true : false;
            discontinuity.Week14 = (temp.Week14 == "on") ? true : false;
            dbAcademicMsContext.TblDiscontinuities.Update(discontinuity);
            dbAcademicMsContext.SaveChanges();

            return RedirectToAction("Discontinuity");
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
