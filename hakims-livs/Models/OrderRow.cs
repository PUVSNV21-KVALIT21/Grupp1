using System.ComponentModel.DataAnnotations.Schema;

namespace hakims_livs.Models;

public class OrderRow
{
    public int ID { get; set; }
    public Product? Product { get; set; }
    public Order? Order { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
    
}