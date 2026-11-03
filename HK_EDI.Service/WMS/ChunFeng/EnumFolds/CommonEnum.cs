using HK_EDI.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.ChunFeng.EnumFolds
{
   public enum OrderOperationStatusContrastEnum
    {
        /// <summary>
        /// 已导⼊系统
        /// </summary>
        [EnumResource("已导⼊系统")]
        RECVFAILED = 1,
        /// <summary>
        /// 已分配
        /// </summary>
        [EnumResource("已分配")]
        RECVSUCCESS = 2,
        /// <summary>
        /// 已拣选
        /// </summary>
        [EnumResource("已拣选")]
        PICK = 3,
        /// <summary>
        /// 已打包
        /// </summary>
        [EnumResource("已打包")]
        PACKAGE = 4,
        /// <summary>
        /// 已集货
        /// </summary>
        [EnumResource("已集货")]
        CHECK = 5,
        /// <summary>
        /// 发货完成
        /// </summary>
        [EnumResource("发货完成")]
        SHIP = 6,
        /// <summary>
        ///  取消
        /// </summary>
        [EnumResource("已取消")]
        CancelOrder = 9,
        
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
        /// 已接收仓内调入回传
        /// </summary>
        [EnumResource("已接收仓内调入回传")]
        ReceiveAdjustWriteback = -94
    }
}
