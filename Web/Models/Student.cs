﻿using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Student
{
    public int Id { get; set; }

    public int? ProgramId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Program? Program { get; set; }
}
