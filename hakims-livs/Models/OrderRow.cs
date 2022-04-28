namespace hakims_livs.Models;

public class OrderRow
{
    public int ID { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderRow()
    {
        Product = new Product();
        Order = new Order();

    }
}