using Microsoft.AspNetCore.Identity;
using System;
namespace hakims_livs.Models
{
    public interface ICustomer
    {
        /// <summary>
        /// Gets the first name for the specified user from Hakims Livs database using ApplicationDbContext
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>The task that respresents the asyncronous operation, containing the first name for the specified user</returns>
        Task<string> GetFirstNameAsync(Customer customer);
        /// <summary>
        /// Gets the last name for the specified user from Hakims Livs database using ApplicationDbContext
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>The task that respresents the asyncronous operation, containing the last name for the specified user</returns>
        Task<string> GetLastNameAsync(Customer customer);
        /// <summary>
        /// Sets the first name for the specified user in Hakims Livs database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task SetFirstNameAsync(Customer customer, string firstName);
    }
}
