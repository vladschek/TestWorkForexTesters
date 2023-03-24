using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}
