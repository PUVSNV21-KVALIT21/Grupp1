
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace hakims_livs.Models
{
    public class Customer : IdentityUser
    {
        [Display(Name = "First name")]
        [Required(AllowEmptyStrings = true)]
        [System.ComponentModel.DefaultValue("")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required(AllowEmptyStrings = true)]
        [System.ComponentModel.DefaultValue("")]
        public string LastName { get; set; }
        public Address Address { get; set; }
        public List<Product> FavouriteProducts { get; set; }


        public Customer()
        {
            FirstName = "";
            LastName = "";
            Address = new Address();
            FavouriteProducts = new List<Product>();
          
        }
    }

    public class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Address()
        {
            Street = "";
            City = "";
            Country = "";
            PostalCode = "";

        }

    }
}


