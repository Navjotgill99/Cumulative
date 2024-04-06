using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativePart1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public string TeacherFname { get; set; }

        public string TeacherLname { get; set; }

        public string EmployeeNum { get; set; }

        public DateTime HireDate { get; set; }

        public decimal TeacherSalary { get; set; }

        public List<Class> Classes { get; set; }

    }
}