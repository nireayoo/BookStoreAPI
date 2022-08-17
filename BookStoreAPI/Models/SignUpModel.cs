using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }

    }
}
