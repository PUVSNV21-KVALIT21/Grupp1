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
}
