using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class GolbalSettingModel
    {
        /// <summary>
        /// 公司字符串
        /// </summary>
        public List<CompanyModel> CompanyIds { get; set; }
        /// <summary>
        /// 保持网站运行URL
        /// </summary>
        public string KeepWebUrl { get; set; }
        /// <summary>
        /// 类型字符串
        /// </summary>
        public List<BillTypeModel> BillTypeIds { get; set; }
        /// <summary>
        /// 商品字符串
        /// </summary>
        public string SkuCodeStr { get; set; }
        /// <summary>
        /// 品牌字符串
        /// </summary>
        public string BrandStr { get; set; }
        /// <summary>
        /// 设置计划任务停止执行的时间段
        /// </summary>
        public string TaskStopExecuteTime { get; set; }
        /// <summary>
        /// 商城订单是否生成发票
        /// </summary>
        public bool TurnOnShopncInvoice { get; set; }

        /// <summary>
        /// 网络退货单是否自动收货
        /// </summary>
        public bool TurnOnAutoReceive { get; set; }
    }
    public class CompanyModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsSelect { get; set; }
    }
    
    public class BillTypeModel
    {
        public string BillTypeId{get;set;}
        public string BillTypeName{get;set;}
        public bool IsSelect { get; set; }
    }
}