using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativePart1.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        public string ClassCode { get; set; }

        public string StartDate { get; set; }

        public string FinishDate { get; set; }

        public string ClassName { get; set; }

        public int TeacherId { get; set; }
    }
}