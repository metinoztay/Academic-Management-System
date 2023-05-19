using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblStudentsLesson
{
    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string LessonCode { get; set; } = null!;

    public int? MidtermNote { get; set; }

    public int? FinalNote { get; set; }
}
