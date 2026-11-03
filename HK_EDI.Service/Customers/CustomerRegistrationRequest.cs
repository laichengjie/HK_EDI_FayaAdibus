using HK_EDI.Core.Domain.Customers;

namespace HK_EDI.Services.Customers
{
    public class CustomerRegistrationRequest
    {
        public Customer Customer { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IAdibusproved { get; set; }

        public CustomerRegistrationRequest(Customer customer, string email, string username,
            string password, 
            bool iAdibusproved = true)
        {
            this.Customer = customer;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.IAdibusproved = iAdibusproved;
        }

        //public bool IsValid  
        //{
        //    get 
        //    {
        //        return (!CommonHelper.AreNullOrEmpty(
        //                    this.Email,
        //                    this.Password
        //                    ));
        //    }
        //}
    }
}
