using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class BasCompany : BaseEntity
    {
        public string CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public string ForeignName { get; set; }
        public string CurrencyID { get; set; }
        public string AccperiodSchemeID { get; set; }
        public decimal? OwnerShareate { get; set; }
        public string Trade { get; set; }
        public string Manager { get; set; }
        public string Admin { get; set; }
        public string Linkman { get; set; }
        public string BusinessAddress { get; set; }
        public string Address { get; set; }
        public int? TaxpayerType { get; set; }
        public string Telphone { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Http { get; set; }
        public string License { get; set; }
        public string TaxCode { get; set; }
        public string RegisterBank { get; set; }
        public string BankAccount { get; set; }
        public bool IsAccount { get; set; }
        public bool IsHold { get; set; }
        public string ParentID { get; set; }
        public Int16? Level { get; set; }
        public bool? IsDetail { get; set; }
        public string FullName { get; set; }
        public string FullParentID { get; set; }
        public string DownLoadSchemeID { get; set; }
        public string UpLoadSchemeID { get; set; }
        public string Remark { get; set; }
        public string CompanyLOG { get; set; }
        public bool? AllowUsed { get; set; }
        public string AllowFutures { get; set; }
        public DateTime? ModifyDTM { get; set; }
        public string CardNo { get; set; }
        public bool? EnterpriseType { get; set; }
        public bool? IsGroup { get; set; }
        public bool? IsActuals { get; set; }
        public int Lan { get; set; }
        public string T_CompanyCode { get; set; }
        public string T_CompanyName { get; set; }
        public string T_ShortName { get; set; }
        public string Steuernummer { get; set; }
        public string SteuernummerOfHouse { get; set; }
        public string IRSID_Substation { get; set; }
        public string IRSID { get; set; }
        public string AreaID { get; set; }
        public string TaxPeriodSchemeID { get; set; }
        public string CompanyNature { get; set; }
        public decimal? ShareholdingRatio { get; set; }
        public string BigAreaID { get; set; }
        public string SmallAreaID { get; set; }
    }
}
