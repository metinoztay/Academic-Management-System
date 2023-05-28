using System;
using System.Collections.Generic;

namespace AcademicManagementSystem.Models;

public partial class TblUser
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Authority { get; set; } = null!;

    public string? Adress { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? SecurityKey { get; set; }
}
