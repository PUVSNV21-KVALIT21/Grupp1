using hakims_livs.Models;
using Microsoft.EntityFrameworkCore;

namespace hakims_livs.Data
{
    public class CustomerData : ICustomer
    {
        private readonly ApplicationDbContext _database;

        public CustomerData(ApplicationDbContext dbContext)
        {
            _database = dbContext;
        }

        public async Task<string> GetFirstNameAsync(Customer user)
        {
            var customer = await _database.Users.FindAsync(user.Id);
            return customer.FirstName;
        }

        public async Task<string> GetLastNameAsync(Customer user)
        {
            var customer = await _database.Users.FindAsync(user.Id);
            return customer.LastName;
        }

        public async Task<string?> GetStreetAsync(Customer user)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);
            if (customer.Address == null)
            {
                return null;
            }
            else
            {
                return customer.Address.Street;
            }
        }

        public async Task<string?> GetCityAsync(Customer user)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);
            if (customer.Address == null)
            {
                return null;
            }
            else
            {
                return customer.Address.City;
            }
        }
        public async Task<string?> GetPostalCodeAsync(Customer user)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);
            if (customer.Address == null)
            {
                return null;
            }
            else
            {
                return customer.Address.PostalCode;
            }
        }

        public async Task<string?> GetCountryAsync(Customer user)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);
            if (customer.Address == null)
            {
                return null;
            }
            else
            {
                return customer.Address.Country;
            }
        }


        public async Task SetFirstNameAsync(Customer user, string firstName)
        {
            Customer customer = await _database.Users.FindAsync(user.Id);
            customer.FirstName = firstName;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task SetLastNameAsync(Customer user, string lastName)
        {
            Customer customer = await _database.Users.FindAsync(user.Id);
            customer.LastName = lastName;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task SetStreetAsync(Customer user, string street)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);

            if (customer.Address is null)
            {
                customer.Address = new Address();
            }
            customer.Address.Street = street;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task SetCityAsync(Customer user, string city)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);

            if (customer.Address is null)
            {
                customer.Address = new Address();
            }
            customer.Address.City = city;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task SetPostalCodeAsync(Customer user, string postalCode)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);

            if (customer.Address is null)
            {
                customer.Address = new Address();
            }
            customer.Address.PostalCode = postalCode;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task SetCountryAsync(Customer user, string country)
        {
            var customer = await _database.Users.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == user.Id);

            if (customer.Address is null)
            {
                customer.Address = new Address();
            }
            customer.Address.Country = country;
            _database.Attach(customer).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
