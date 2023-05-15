using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblLesson
{
    public string Code { get; set; } = null!;

    public string LessonName { get; set; } = null!;

    public string TeacherId { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public byte Credit { get; set; }

    public string Faculty { get; set; } = null!;

    public string Course { get; set; } = null!;

    public byte Class { get; set; }

    public string LessonDay { get; set; } = null!;

    public byte LessonDayIndex { get; set; }

    public string LessonTime { get; set; } = null!;

    public string LessonClass { get; set; } = null!;
}
