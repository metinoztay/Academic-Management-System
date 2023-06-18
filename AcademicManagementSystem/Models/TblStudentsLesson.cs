using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblStudentsLesson
{
    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string LessonCode { get; set; } = null!;

    public byte Class { get; set; }

    public int? MidtermNote { get; set; }

    public int? FinalNote { get; set; }

    public int? CompleteNote { get; set; }

    public int? Average { get; set; }

    public string? LetterGrade { get; set; }

    public bool Confirmed { get; set; }
}
