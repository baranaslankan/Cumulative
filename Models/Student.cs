using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentFname { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentLname { get; set; }

        [Required]
        [StringLength(50)]
        public string StudentNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime EnrollDate { get; set; }
    }
}
