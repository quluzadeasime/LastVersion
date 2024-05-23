using System.ComponentModel.DataAnnotations;

namespace Bilet16.DTO_s
{
    public class LoginDto
    {
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
