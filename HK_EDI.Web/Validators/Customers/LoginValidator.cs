using FluentValidation;
using HK_EDI.Core.Domain.Customers;
using HK_EDI.Web.Models.Customer;

namespace HK_EDI.Web.Validators.Customers
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {

        }
    }
}