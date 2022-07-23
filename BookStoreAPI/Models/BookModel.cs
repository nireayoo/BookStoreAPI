//using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="pls add title property")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
