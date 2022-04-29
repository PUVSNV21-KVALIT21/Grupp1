using System.ComponentModel.DataAnnotations.Schema;

namespace hakims_livs.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        
        public Order()
        {
            Customer = new Customer();
        OrderRows = new List<OrderRow>();
        }

    }
}
