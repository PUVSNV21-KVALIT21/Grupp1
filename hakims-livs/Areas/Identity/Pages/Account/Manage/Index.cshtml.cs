// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using hakims_livs.Models;
using hakims_livs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hakims_livs.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICustomer _customerData;

        public IndexModel(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            ApplicationDbContext dbContext,
            ICustomer customer
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerData = customer;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 

        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Customer user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var firstName = await _customerData.GetFirstNameAsync(user);
            var lastName = await _customerData.GetLastNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var street = await _customerData.GetStreetAsync(user);
            var city = await _customerData.GetCityAsync(user);
            var postalCode = await _customerData.GetPostalCodeAsync(user);
            var country = await _customerData.GetCountryAsync(user);

            Username = userName;

            Customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName
            };

            Address = new Address
            {
                Street = street,
                City = city,
                PostalCode = postalCode,
                Country = country
            };

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            await _customerData.SetFirstNameAsync(user, Customer.FirstName);
            await _customerData.SetLastNameAsync(user, Customer.LastName);
            await _customerData.SetStreetAsync(user, Address.Street);
            await _customerData.SetCityAsync(user, Address.City);
            await _customerData.SetPostalCodeAsync(user, Address.PostalCode);
            await _customerData.SetCountryAsync(user, Address.Country);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
