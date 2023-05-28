using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblTeacher
{
    public string TeacherId { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public string Course { get; set; } = null!;
}
