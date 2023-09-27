using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Class
{
    public int Id { get; set; }

    public int? StaffId { get; set; }

    public int? CourseId { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
}
