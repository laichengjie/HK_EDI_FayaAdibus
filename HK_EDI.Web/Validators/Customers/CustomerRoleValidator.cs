using FluentValidation;
using HK_EDI.Web.Models.Customers;

namespace HK_EDI.Web.Validators.Customers
{
    public class CustomerRoleValidator : AbstractValidator<CustomerRoleModel>
    {
        public CustomerRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("角色名称不能为空！");
        }
    }
}