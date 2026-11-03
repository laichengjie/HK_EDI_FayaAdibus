using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class BaseModel
    {
    }



    public class RequestAbnormalOrderQuery
    {

        /// <summary>
        /// 货主 
        /// </summary>
        public string ownerCode { get; set; }
        /// <summary>
        /// 仓库号
        /// </summary>
        public string warehouseCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string fromDatetime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string toDatetime { get; set; }
    }


    public class Response
    {
        public string success { get; set; }
        public string reason { get; set; }
        public string orderCodes { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime toDatetime { get; set; }
        public string warehouseCode { get; set; }
        public List<item> items { get; set; }
    }

    public class item
    {
        public string itemSkuCode { get; set; }

        public string itemQuantity { get; set; }

        public string orderedQuantity { get; set; }

        public string lackQuantity { get; set; }
    }
}