using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public record BookCreateUpdateVm
    {
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }

        [Required]
        [StringLength(1500)]
        public string? Description { get; set; }

        [StringLength(20)]
        public string? Isbn { get; set; }

        [Required]
        public int? AuthorId { get; set; }
    }
}
