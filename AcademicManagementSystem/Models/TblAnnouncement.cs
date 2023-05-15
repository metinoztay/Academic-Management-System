using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblAnnouncement
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public DateTime LastDate { get; set; }
}
