using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        //we are creating these properties because they are not available in the Identity user class
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
