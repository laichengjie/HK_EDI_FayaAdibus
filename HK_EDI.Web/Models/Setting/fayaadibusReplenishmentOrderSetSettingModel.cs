using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class fayaadibusReplenishmentOrderSetSettingModel : BaseModel
    {
        public string CompanyId { get; set; }
        public string CheckerID { get; set; }
        public string SERVICECODE { get; set; } 
        /// <summary>
        /// 补货单获取地址
        /// </summary>
        public string GetReplenishmentOrderUrl { get; set; }
        public int GetDate { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
  
        /// <summary>
        /// 发货仓编号
        /// </summary>
        public string DeparturePOSCode  { get; set; }
         
        /// <summary>
        /// 收货店铺编号
        /// </summary>
        public string DestinationPOSCode { get; set; }


        /// <summary>
        /// FRS确认补货单数量 地址
        /// </summary>
        public string PushReplenishmentOrderUrl { get; set; }
        /// <summary>
        /// FRS确认补货单数量 接口
        /// </summary>
        public string PUSSERVICECODE { get; set; }
    }
}