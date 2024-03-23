using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativePart1.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentFname { get; set; }

        public string StudentLname { get; set; }

        public string StudentNum { get; set; }

        public DateTime EnrolDate { get; set; }
    }
}