using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [StringLength(100)]
        public string TeacherFname { get; set; }

        [Required]
        [StringLength(100)]
        public string TeacherLname { get; set; }

        [StringLength(50)]
        public string EmployeeNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}
