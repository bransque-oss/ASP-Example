using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public record UserVm
    {
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"[a-z]*")]
        public string? Login { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"[a-zA-Z0-9]*")]
        public string? Password { get; set; }
    }
}
