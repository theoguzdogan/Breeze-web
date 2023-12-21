using Microsoft.AspNetCore.Identity;

namespace LoginSolo.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Cities { get; set; }
    }
}
