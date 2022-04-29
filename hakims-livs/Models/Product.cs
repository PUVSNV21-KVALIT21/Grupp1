using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace hakims_livs.Models
{

    public enum Unit { gram, ml, st }
    public class Product
    {
        public int ID { get; set; }
        [Display(Name="Namn")]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Display(Name="Beskrivning")]
        [MaxLength(255)]
        public string? Description { get; set; }
        [Display(Name="Bild")]
        [MaxLength(255)]
        public byte[]? Image { get; set; }
        [Display(Name="Pris")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Display(Name="Lagersaldo")]
        public int Stock { get; set; }
        [Display(Name="Volym")]
        public int Volume { get; set; }
        public Category? Category { get; set; }
        [Display(Name="Produkt inlagd")]
        public DateTime CreatedDateTime { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        [Display(Name="Enhet")]
        public Unit Unit { get; set; }

        public Product()
        {
            CreatedDateTime = DateTime.Now;
        }

    }
}
