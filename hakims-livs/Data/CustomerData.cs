using hakims_livs.Models;
using System;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using hakims_livs;

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

        public async Task SetFirstNameAsync(Customer user, string firstName)
        {
            Customer customer = await _database.Users.FindAsync(user.Id);
            customer.FirstName = firstName;
            _database.Attach(customer.FirstName).State = EntityState.Modified;
        }
    }
}
