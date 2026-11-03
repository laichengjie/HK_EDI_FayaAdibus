using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using HK_EDI.Web.Mvc;
using HK_EDI.Web.Validators.Customers;
using System.ComponentModel.DataAnnotations;

namespace HK_EDI.Web.Models.Customers
{
    [Validator(typeof(CustomerRoleValidator))]
    public partial class CustomerRoleModel : BaseNopEntityModel
    {
        [Display(Name = "角色名称")]
        [AllowHtml]
        public string Name { get; set; }

        [Display(Name = "是否免邮")]
        [AllowHtml]
        public bool FreeShipping { get; set; }

        [Display(Name = "是否免税")]
        public bool TaxExempt { get; set; }

        [Display(Name = "是否启用")]
        public bool Active { get; set; }

        [Display(Name = "是否系统角色")]
        public bool IsSystemRole { get; set; }

        [Display(Name = "系统角色名")]
        public string SystemName { get; set; }

    }
}