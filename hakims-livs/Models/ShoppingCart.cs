namespace hakims_livs.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }

        public ShoppingCart()
        {
            Customer = new Customer();
            Products = new List<Product>();
        }
    }
}
