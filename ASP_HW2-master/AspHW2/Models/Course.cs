using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspHW2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}