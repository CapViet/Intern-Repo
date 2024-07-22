using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagerMVC.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Birthplace")]
        public string PlaceOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        [Display(Name = "Is Graduated?")]
        public bool IsGraduated { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{LastName} {FirstName}";
    }
}
