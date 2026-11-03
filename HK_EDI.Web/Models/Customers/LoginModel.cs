using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using HK_EDI.Web;
using HK_EDI.Web.Mvc;
using HK_EDI.Web.Validators.Customers;

namespace HK_EDI.Web.Models.Customer
{
    [Validator(typeof(LoginValidator))]
    public partial class LoginModel : BaseNopModel
    {
        public bool CheckoutAsGuest { get; set; }

        [Display(Name = "邮箱地址")]
        [AllowHtml]
        public string Email { get; set; }

        [Display(Name = "用户名")]
        [AllowHtml]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [AllowHtml]
        public string Password { get; set; }

        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}