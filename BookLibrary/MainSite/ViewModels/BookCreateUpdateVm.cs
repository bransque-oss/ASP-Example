using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MainSite.ViewModels
{
    public record BookCreateUpdateVm
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(1500)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Isbn { get; set; }

        public IEnumerable<SelectListItem>? Authors { get; set; }

        public int? AuthorId { get; set; }
    }
}
