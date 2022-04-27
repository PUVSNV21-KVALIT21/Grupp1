namespace hakims_livs.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        
        public Order()
        {
        OrderRows = new List<OrderRow>();
        }

    }



    public class OrderRow
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
