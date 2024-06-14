using System.ComponentModel.DataAnnotations;

namespace ProjectExample2.ViewModel
{
    public class ViewRegister
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string GivenName { get; set; } = null!;
        public string Surname { get; set; } = null!;
       
       
        [EmailAddress(ErrorMessage ="กรุณากรอกอีเมลล์")]
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string[] Roles { get; set; } = null!;
    }
}
