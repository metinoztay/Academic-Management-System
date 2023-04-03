using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblTeacher
{
    public string TeacherId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;
}
