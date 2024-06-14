using System.ComponentModel.DataAnnotations;

namespace ProjectExample2.Model
{
    public class Customer
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        public string Address { get; set; } = null!;
        
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Order>?  Orders { get; set; }

    }
}
