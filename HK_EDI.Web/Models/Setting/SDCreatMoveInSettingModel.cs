using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class SDCreatMoveInSettingModel : BaseModel
    {
        public string CompanyId { get; set; }
        /// <summary>
        /// 调用接口查询的店铺代码
        /// </summary>
        public string SelectShopCode { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        public DateTime BeginDate { get; set; }
    }
}