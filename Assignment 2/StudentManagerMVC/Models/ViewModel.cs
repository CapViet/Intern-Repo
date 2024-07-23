using System.ComponentModel.DataAnnotations;

namespace StudentManagerMVC.Models
{
    public class ViewModel
    {
        public class StudentFilterViewModel
        {
            public string FilterField { get; set; }
            public string FilterCriteria { get; set; }
            public string FilterValue { get; set; }
            public List<Student> Students { get; set; }
        }
        public class ScoresViewModel
        {
            public int ID { get; set; }
            public int MathScore { get; set; }
            public int ChemScore { get; set; }
            public int PhysScore { get; set; }
        }
        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
    }
}
