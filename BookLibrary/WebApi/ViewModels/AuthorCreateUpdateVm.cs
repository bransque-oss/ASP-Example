using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public record AuthorCreateUpdateVm
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength (2000)]
        public string? Description { get; set; }
    }
}
