using HK_EDI.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.SettingModel
{
    public class GolbalSetting : ISettings
    {
        /// <summary>
        /// 公司字符串
        /// </summary>
        public string CompanyIds { get; set; }
        /// <summary>
        /// 保持网站运行URL
        /// </summary>
        public string KeepWebUrl { get; set; }
        /// <summary>
        /// 单据类型字符串
        /// </summary>
        public string BillTypeIDs { get; set; }
        /// <summary>
        /// 商品字符串
        /// </summary>
        public string SkuCodeStr { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public string AuthorizedTime { get; set; }
        /// <summary>
        /// 授权过期后提示的文本
        /// </summary>
        public string AuthorizedHintFormat { get; set; }

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
}
