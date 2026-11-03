using FluentValidation;
using FluentValidation.Results;
using HK_EDI.Web.Models.Customers;

namespace HK_EDI.Web.Validators.Customers
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {

        }
    }
}