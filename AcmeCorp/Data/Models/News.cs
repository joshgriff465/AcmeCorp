using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AcmeCorp.Data.Models
{
    public class News
    {
        public Guid NewsId { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Seo { get; set; }
        [Required]
        [StringLength(250)]
        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
    }
}
