using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
