using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task2.Models
{

    public class Student
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [Range(0,3)]
        public int Year { get; set; }
        [Required]
        public String Course { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public String Email { get; set; }
    }
}