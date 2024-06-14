using Microsoft.AspNetCore.Identity;

namespace ProjectExample2.Model
{
    public class Register:IdentityUser
    {
        public string GivenName { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
