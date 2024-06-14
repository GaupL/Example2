
namespace ProjectExample2.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set;}
        public ICollection<Orderdetial>?  Orderdetials;
    }
}
