namespace ProjectExample2.Model
{
    public class Orderdetial
    {
        public int Id { get; set; }
        public DateTime datetime { get; set; }
        public int Quanlity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order? Orders { get; set; }
        public Product? Products { get; set; }
    }
}
