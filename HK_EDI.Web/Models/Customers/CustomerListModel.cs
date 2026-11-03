using HK_EDI.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.Customers
{
    public partial class CustomerListModel : BaseNopModel
    {
        [Display(Name="角色列表")]
        [AllowHtml]
        public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }

        [Display(Name = "角色")]
        public int[] SearchCustomerRoleIds { get; set; }

        [Display(Name = "邮件地址")]
        [AllowHtml]
        public string SearchEmail { get; set; }

        [Display(Name = "用户名")]
        [AllowHtml]
        public string SearchUsername { get; set; }

        
    }
}