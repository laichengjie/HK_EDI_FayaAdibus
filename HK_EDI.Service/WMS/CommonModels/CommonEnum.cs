using HK_EDI.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.CommonModels
{
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 获取Adibus预计发货单
        /// </summary>
        [EnumResource("获取Adibus预计发货单")]
        GETESTIMATEDSHIPMENT = 1,
        /// <summary>
        /// 生成采购订单
        /// </summary>
        [EnumResource("生成采购订单")]
        CREATECGORDER = 2,
        /// <summary>
        /// 获取Adibus装箱单
        /// </summary>
        [EnumResource("获取Adibus装箱单")]
        GETPACKINGLIST = 3,
        /// <summary>
        /// 生成采购到货
        /// </summary>
        [EnumResource("生成采购到货")]
        CREATERECEIPT = 4,

        /// <summary>
        /// 获取Adibus补货单
        /// </summary>
        [EnumResource("获取Adibus补货单")]
        GETREPLENISHMENTORDER = 5,
        /// <summary>
        /// 生成补货单
        /// </summary>
        [EnumResource("生成补货单")]
        CREATEREPLENISHMENT = 6,
    }
}
