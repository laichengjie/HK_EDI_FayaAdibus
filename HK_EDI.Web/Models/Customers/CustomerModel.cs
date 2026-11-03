using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using HK_EDI.Web.Validators.Customers;
using HK_EDI.Web.Mvc;

namespace HK_EDI.Web.Models.Customers
{
    [Validator(typeof(CustomerValidator))]
    public partial class CustomerModel : BaseNopEntityModel
    {
        public CustomerModel()
        {
            this.AvailableCustomerRoles = new List<CustomerRoleModel>();
        }

        public bool AllowUsersToChangeUsernames { get; set; }
        public bool UsernamesEnabled { get; set; }

        [Display(Name = "账号")]
        [AllowHtml]
        public string Username { get; set; }

        [Display(Name = "邮件地址")]
        [AllowHtml]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [AllowHtml]
        public string Password { get; set; }

        [Display(Name = "是否启用")]
        public bool Active { get; set; }






        //registration date
        [Display(Name = "创建时间")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "最后活动时间")]
        public DateTime LastActivityDate { get; set; }



        //customer roles
        [Display(Name = "角色")]
        public string CustomerRoleNames { get; set; }
        public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }
        public int[] SelectedCustomerRoleIds { get; set; }




    }
}