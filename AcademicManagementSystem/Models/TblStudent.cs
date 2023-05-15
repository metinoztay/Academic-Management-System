using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblStudent
{
    public string StudentId { get; set; } = null!;

    public byte Class { get; set; }

    public string Faculty { get; set; } = null!;

    public string Course { get; set; } = null!;
}
