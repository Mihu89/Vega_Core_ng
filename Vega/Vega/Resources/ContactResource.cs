using System.ComponentModel.DataAnnotations;

namespace Vega.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}
