using System.ComponentModel.DataAnnotations.Schema;

namespace hakims_livs.Models
{
    public enum Unit { gram, ml, st }
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int Volume { get; set; }
        public Category Category { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public Unit Unit { get; set; }


    }
}
