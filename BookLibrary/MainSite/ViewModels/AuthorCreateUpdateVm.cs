using System.ComponentModel.DataAnnotations;

namespace MainSite.ViewModels
{
    public record AuthorCreateUpdateVm
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
    }
}
