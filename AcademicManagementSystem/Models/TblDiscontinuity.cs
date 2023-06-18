using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblDiscontinuity
{
    public int Id { get; set; }

    public string LessonCode { get; set; } = null!;

    public string LessonName { get; set; } = null!;

    public byte Class { get; set; }

    public string StudentId { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public bool? Week1 { get; set; }

    public bool? Week2 { get; set; }

    public bool? Week3 { get; set; }

    public bool? Week4 { get; set; }

    public bool? Week5 { get; set; }

    public bool? Week6 { get; set; }

    public bool? Week7 { get; set; }

    public bool? Week8 { get; set; }

    public bool? Week9 { get; set; }

    public bool? Week10 { get; set; }

    public bool? Week11 { get; set; }

    public bool? Week12 { get; set; }

    public bool? Week13 { get; set; }

    public bool? Week14 { get; set; }

    public bool? Confirmed { get; set; }
}
