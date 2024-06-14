using System.Collections.Generic;

namespace ProjectExample2.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer? customer { get; set; }
        public ICollection<Orderdetial>? Orderdetials { get; set; }
    }
}
