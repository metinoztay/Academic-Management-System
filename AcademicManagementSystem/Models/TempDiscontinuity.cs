using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public class TempDiscontinuity
{
    public int Id { get; set; }

    public string LessonCode { get; set; } = null!;
    public int? Class { get; set; }
    public string LessonName { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public string? Week1 { get; set; }

    public string? Week2 { get; set; }

    public string? Week3 { get; set; }

    public string? Week4 { get; set; }

    public string? Week5 { get; set; }

    public string? Week6 { get; set; }

    public string? Week7 { get; set; }

    public string? Week8 { get; set; }

    public string? Week9 { get; set; }

    public string? Week10 { get; set; }

    public string? Week11 { get; set; }

    public string? Week12 { get; set; }

    public string? Week13 { get; set; }

    public string? Week14 { get; set; }
}
