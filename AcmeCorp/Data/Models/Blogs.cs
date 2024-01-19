using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AcmeCorp.Data.Models
{
    public class Blogs
    {
        public Guid BlogsId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(250)]
        [DisplayName("Image URL")]

        public string ImageUrl { get; set; }
        public DateTime? ReleaseDate {get; set;}
        [Required]
        public DateTime CreationDate { get; set; }

        public bool Active {get; set;}

    }
}
