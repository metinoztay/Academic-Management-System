using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

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
            
            var teacherModel = dbAcademicMsContext.TblTeachers
                .FirstOrDefault( t => t.TeacherId == teacherID);

            TempTeacherModel tempTeacherModel = new TempTeacherModel();
            tempTeacherModel.Name = teacher.Name;
            tempTeacherModel.Surname = teacher.Surname;
            tempTeacherModel.Adress = teacher.Adress;
            tempTeacherModel.Email = teacher.Email;
            tempTeacherModel.Username = teacher.Username;
            tempTeacherModel.Phone = teacher.Phone;
            tempTeacherModel.District= teacher.District;
            tempTeacherModel.Password = teacher.Password;
            tempTeacherModel.Province = teacher.Province;
            tempTeacherModel.SecurityKey = teacher.SecurityKey;

            tempTeacherModel.Faculty = teacherModel.Faculty;
            tempTeacherModel.Course = teacherModel.Course;


            return View(tempTeacherModel);
        }

        [HttpPost]
        public IActionResult TeacherSave(TempTeacherModel tempTeacherModel)
        {
            TblUser teacher = new TblUser();
            teacher.Authority = "Teacher";
            teacher.Username = tempTeacherModel.Username;
            teacher.Name = tempTeacherModel.Name;
            teacher.Surname = tempTeacherModel.Surname;
            teacher.Adress = tempTeacherModel.Adress;
            teacher.District = tempTeacherModel.District;
            teacher.Province = tempTeacherModel.Province;
            teacher.Phone = tempTeacherModel.Phone;
            teacher.Email= tempTeacherModel.Email;
            teacher.Password = tempTeacherModel.Password;
            teacher.SecurityKey = tempTeacherModel.SecurityKey;
            dbAcademicMsContext.TblUsers.Update(teacher);
            TblTeacher teacherModel = new TblTeacher();
            teacherModel.Faculty = tempTeacherModel.Faculty;
            teacherModel.Course = tempTeacherModel.Course;
            teacherModel.TeacherId = tempTeacherModel.Username;
            dbAcademicMsContext.TblTeachers.Update(teacherModel);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("TeacherControl");
        }

        [HttpPost]
        public IActionResult DeleteTeacher(string username)
        {
            var user = dbAcademicMsContext.TblUsers
                .FirstOrDefault(u => u.Username == username);
            dbAcademicMsContext.TblUsers.Remove(user);
            var teacher = dbAcademicMsContext.TblTeachers
                .FirstOrDefault(t => t.TeacherId== username);
            dbAcademicMsContext.TblTeachers.Remove(teacher);
            var lessons = dbAcademicMsContext.TblLessons
                .Where(l => l.TeacherId == username);
            foreach(var lesson in lessons)
            {
                lesson.TeacherId = null;
                lesson.TeacherName = null;
            }
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("TeacherControl");
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

        [HttpPost]
        public IActionResult StudentControl(string username)
        {
            var student = dbAcademicMsContext.TblUsers
                .FirstOrDefault(s => s.Username == username);
            var studentModel = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == username);

            TempStudentModel tempStudentModel = new TempStudentModel();
            tempStudentModel.Name = student.Name;
            tempStudentModel.Surname = student.Surname;
            tempStudentModel.Adress = student.Adress;
            tempStudentModel.Email = student.Email;
            tempStudentModel.Username = student.Username;
            tempStudentModel.Phone = student.Phone;
            tempStudentModel.District = student.District;
            tempStudentModel.Password = student.Password;
            tempStudentModel.Province = student.Province;
            tempStudentModel.SecurityKey = student.SecurityKey;

            tempStudentModel.Faculty = studentModel.Faculty;
            tempStudentModel.Course = studentModel.Course;
            tempStudentModel.Class = studentModel.Class;

            return View(tempStudentModel);
        }

        [HttpPost]
        public IActionResult SaveStudent(TempStudentModel tempStudentModel)
        {
            TblUser student = new TblUser();
            student.Authority = "Student";
            student.Username = tempStudentModel.Username;
            student.Name = tempStudentModel.Name;
            student.Surname = tempStudentModel.Surname;
            student.Adress = tempStudentModel.Adress;
            student.District = tempStudentModel.District;
            student.Province = tempStudentModel.Province;
            student.Phone = tempStudentModel.Phone;
            student.Email = tempStudentModel.Email;
            student.Password = tempStudentModel.Password;
            student.SecurityKey = tempStudentModel.SecurityKey;
            dbAcademicMsContext.TblUsers.Update(student);
            TblStudent studentModel = new TblStudent();
            studentModel.Faculty = tempStudentModel.Faculty;
            studentModel.Course = tempStudentModel.Course;
            studentModel.Class = (byte)tempStudentModel.Class;
            studentModel.StudentId = tempStudentModel.Username;
            dbAcademicMsContext.TblStudents.Update(studentModel);
            dbAcademicMsContext.SaveChanges();
            return RedirectToAction("StudentControl");
        }

        [HttpPost]
        public IActionResult DeleteStudent(string username)
        {
            var student = dbAcademicMsContext.TblUsers
                .FirstOrDefault(s => s.Username == username);
            dbAcademicMsContext.TblUsers.Remove(student);

            var studentModel = dbAcademicMsContext.TblStudents
                .FirstOrDefault(s => s.StudentId == username);
            dbAcademicMsContext.TblStudents.Remove(studentModel);

            var studentLessons = dbAcademicMsContext.TblStudentsLessons
                .Where(s => s.StudentId == username);
            foreach (var lesson in studentLessons)
            {
                dbAcademicMsContext.TblStudentsLessons.Remove(lesson);
            }

            dbAcademicMsContext.SaveChanges();

            return RedirectToAction("StudentControl");
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

        public IActionResult LessonSelectionControl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LessonSelectionControl(int courseSelection)
        {
            IsCourseSelectionOpen selection = new IsCourseSelectionOpen();
            selection.CourseSelection = (courseSelection == 1) ? true : false;
            selection.Id = 1;
            dbAcademicMsContext.IsCourseSelectionOpens.Update(selection);
            dbAcademicMsContext.SaveChanges();
            return View();
        }

        public IActionResult LessonSelectionConfirm()
        {
            var list = dbAcademicMsContext.TblStudentListForConfirms.ToList();
            List<TempStudentModel> students = new List<TempStudentModel>();
            
            foreach (var s in list)
            {
                TempStudentModel student = new TempStudentModel();
                student.Username = s.StudentId;
                student.Name = dbAcademicMsContext.TblUsers
                    .FirstOrDefault(a => a.Username == s.StudentId).Name;
                student.Surname = dbAcademicMsContext.TblUsers
                    .FirstOrDefault(a => a.Username == s.StudentId).Surname;
                student.Course = dbAcademicMsContext.TblStudents
                    .FirstOrDefault(a => a.StudentId == s.StudentId).Course;
                student.Class = dbAcademicMsContext.TblStudents
                    .FirstOrDefault(a => a.StudentId == s.StudentId).Class;
                students.Add(student);
            }

            return View(students);
        }


        [HttpPost]
        public IActionResult SelectStudent(string studentID)
        {   var studentLessons = dbAcademicMsContext.TblStudentsLessons
                .Where(s => s.StudentId == studentID && s.Confirmed == false).ToList();
            TempStudentID.tempStudentID = studentID;
            List<TblLesson> lessons = new List<TblLesson>();
            
            foreach (var item in studentLessons)
            {
                TblLesson lesson = new TblLesson();
                lesson.Code = item.LessonCode;
                lesson.LessonName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(l => l.Code == item.LessonCode).LessonName;
                lesson.TeacherName = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(l => l.Code == item.LessonCode).TeacherName;
                lesson.Credit = dbAcademicMsContext.TblLessons
                    .FirstOrDefault(l => l.Code == item.LessonCode).Credit;
                lessons.Add(lesson);
            }

            return View(lessons);
        }

        [HttpPost]
        public IActionResult SaveLessonSelection(List<string> lessonCode, List<string> selection)
        {
            int index = 0;
            foreach (var item in lessonCode)
            {
                var lesson = dbAcademicMsContext.TblStudentsLessons
                    .FirstOrDefault(l => l.LessonCode == item);
                var disc = dbAcademicMsContext.TblDiscontinuities
                    .FirstOrDefault(l => l.LessonCode == item);                
                lesson.Confirmed = (selection[index] == "true") ? true : false;
                disc.Confirmed = (selection[index] == "true") ? true : false;
                if (lesson.Confirmed)
                {
                    dbAcademicMsContext.TblDiscontinuities.Update(disc);
                    dbAcademicMsContext.TblStudentsLessons.Update(lesson);
                    dbAcademicMsContext.SaveChanges();
                }
                else
                {
                    dbAcademicMsContext.TblDiscontinuities.Remove(disc);
                    dbAcademicMsContext.TblStudentsLessons.Remove(lesson);
                    dbAcademicMsContext.SaveChanges();
                }                
                index++;
            }

            var student = dbAcademicMsContext.TblStudentListForConfirms
                .FirstOrDefault(s => s.StudentId == TempStudentID.tempStudentID);
            dbAcademicMsContext.TblStudentListForConfirms.Remove(student);
            dbAcademicMsContext.SaveChanges();

            return RedirectToAction("LessonSelectionConfirm");
        }

        [HttpPost]
        public IActionResult SaveLesson(TblLesson lesson)
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            lesson.LessonDay = days[(int)lesson.LessonDayIndex-1];
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
