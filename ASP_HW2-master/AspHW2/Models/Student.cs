using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspHW2.Models
{
    // POCO - Plain Old CLR Object
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Display(Name = "Оценка")]
        [Range(1, 5, ErrorMessage = "Оценка не является валидной")]
        public double Mark { get; set; }

        public List<Course> Courses { get; set; } = new List<Course>();
    }
}