using System.ComponentModel.DataAnnotations;

namespace ProjectExample2.ViewModel
{
    public class ViewCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        [EmailAddress(ErrorMessage = "ใส่เมลล์")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
