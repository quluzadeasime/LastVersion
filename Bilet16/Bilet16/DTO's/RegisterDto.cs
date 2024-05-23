using System.ComponentModel.DataAnnotations;

namespace Bilet16.DTO_s
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(40)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
