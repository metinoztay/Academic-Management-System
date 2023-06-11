using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public class LessonNoteModel
{
    public int Id { get; set; }

    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? LessonCode { get; set; }
    public int? Credit { get; set; }
    public int? Class { get; set; }
    public string? LessonName { get; set; }
    public string? TeacherName { get; set; }

    public int? MidtermNote { get; set; }

    public int? FinalNote { get; set; }

    public int? CompleteNote { get; set; }

    public int? Average { get; set; }

    public string? LetterGrade { get; set; }
}
