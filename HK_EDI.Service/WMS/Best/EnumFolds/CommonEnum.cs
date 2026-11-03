using HK_EDI.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.EnumFolds
{
    public enum OrderOperationStatusContrastEnum
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [EnumResource("未处理")]
        NoProcessing = -100,
        /// <summary>
        /// 发送成功
        /// </summary>
        [EnumResource("发送成功")]
        SendSuccess = -99,
        /// <summary>
        /// 发送失败
        /// </summary>
        [EnumResource("发送失败")]
        SendFail = -98,
        /// <summary>
        /// 已接收回执
        /// </summary>
        [EnumResource("已接收回执数据")]
        ReceiveReturnReceipt = -97,
        /// <summary>
        /// 已处理完成
        /// </summary>
        [EnumResource("已处理完成")]
        ReceiveProcessedComplete = -96,
        /// <summary>
        /// 处理失败
        /// </summary>
        [EnumResource("处理失败")]
        ReceiveProcessedFail = -95,
        /// <summary>
        /// 已取消
        /// </summary>
        [EnumResource("已取消")]
        CancelOrder=1
    }
}
