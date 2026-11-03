using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.CommonModels
{
    public enum ContentType_OperationEnum
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        CompleteOrder_Success=-1,
        /// <summary>
        /// 处理失败
        /// </summary>
        CompleteOrder_Fail=-2,
        /// <summary>
        /// 科捷推送订单
        /// </summary>
        keijie_PostOrder = 0,
        /// <summary>
        /// 科捷推送退换货订单
        /// </summary>
        keijie_PostRefundOrder = 1,
        /// <summary>
        /// 科捷查询订单
        /// </summary>
        keijie_GetOrderStatus = 2,
        /// <summary>
        /// 科捷完成订单
        /// </summary>
        keije_CompleteOrder = 3,
        /// <summary>
        /// 科捷撤销订单
        /// </summary>
        keije_CancelOrder=4,        
        /// <summary>
        /// 推送出库订单给中邮
        /// </summary>
        zhongyou_PostOutOrder=20,
        /// <summary>
        /// 推送入库订单给中邮
        /// </summary>
        zhongyou_PostInOrder = 21,
        /// <summary>
        /// 中邮入库反馈
        /// </summary>
        zhongyou_ReceiptsWriteback=22,
        /// <summary>
        /// 中邮出库反馈
        /// </summary>
        zhongyou_OrdersWriteback=23,
        /// <summary>
        ///中邮撤销订单
        /// </summary>
        zhongyou_CancelOrder =24,
        /// <summary>
        /// 中邮盘点出入库回传
        /// </summary>
         zhongyou_AdjustWriteback=25,
        /// <summary>
        /// 中邮完成订单
        /// </summary>
        zhongyou_CompleteOrder=26,
        /// <summary>
        /// 中邮盘点处理完成
        /// </summary>
        zhongyou_CompleteAdjustSuccess=27,
        /// <summary>
        /// 中邮盘点处理失败
        /// </summary>
        zhongyou_CompleteAdjustFail = 28,
        /// <summary>
        /// 推送出库订单给华胜
        /// </summary>
        Huasheng_PostOutOrder = 100,
        /// <summary>
        /// 推送入库订单给华胜
        /// </summary>
        Huasheng_PostInOrder=101,
        /// <summary>
        /// 华胜推送入库
        /// </summary>
        Huasheng_RequestReceiveInfo=102,
        /// <summary>
        /// 华胜推送出库
        /// </summary>
        Huasheng_RequestOrderStatusInfo=103,
        /// <summary>
        /// 华胜反馈的订单状态
        /// </summary>
        Huasheng_WmsOrderStatusUpdate=104,
        /// <summary>
        /// 撤销订单给华胜
        /// </summary>
        Huasheng_CancelOrder=105,
        /// <summary>
        /// 华胜完成订单
        /// </summary>
        Huasheng_CompleteOrder =106,
        /// <summary>
        /// 华胜盘点数据
        /// </summary>
        Huasheng_AdjustWriteback=107,
        /// <summary>
        /// 华胜盘点处理完成
        /// </summary>
        Huasheng_CompleteAdjustSuccess = 108,
        /// <summary>
        /// 华胜盘点处理失败
        /// </summary>
        Huasheng_CompleteAdjustFail = 109,
        /// <summary>
        /// 威吾德推送订单
        /// </summary>
        SKU360_pushOrder=201,
        /// <summary>
        /// 威吾德接收回执
        /// </summary>
        SKU360_Confirm = 202,
        /// <summary>
        /// 威吾德处理订单
        /// </summary>
        SKU360_CompleteOrder = 203,
        /// <summary>
        ///威吾德取消订单
        /// </summary>
        SKU360_CancelOrder = 204,
        /// <summary>
        ///威吾德仓内调入确认
        /// </summary>
        SKU360_TransferInto = 205,
        /// <summary>
        ///威吾德仓内调入确认成功
        /// </summary>
        SKU360_TransferIntoSuccess = 206,
        /// <summary>
        ///威吾德仓内调入确认失败
        /// </summary>
        SKU360_TransferIntoFail = 207,
        /// <summary>
        /// 百世推送订单
        /// </summary>
        Best_SyncSalesOrderInfo=301,
        /// <summary>
        /// 百世接收回执
        /// </summary>
        Best_Confirm = 302,
        /// <summary>
        /// 百世推送O2O订单
        /// </summary>
        Best_SyncExpressOrder = 303,
        /// <summary>
        /// 天图推送订单
        /// </summary>
        TianTu_SyncSalesOrderInfo = 401,
        /// <summary>
        /// 天图接收回执
        /// </summary>
        TianTu_Confirm = 402,
        /// <summary>
        /// 快尚推送订单
        /// </summary>
        KuaiShang_PushOrder = 501,
        /// <summary>
        /// 快尚接收回执
        /// </summary>
        KuaiShang_Confirm = 502,
        /// <summary>
        /// 快尚接收盘点单
        /// </summary>
        KuaiShang_AdjustConfim = 503,
        /// <summary>
        /// 快尚盘点处理完成
        /// </summary>
        KuaiShang_CompleteAdjustSuccess = 504,
        /// <summary>
        /// 快尚盘点处理失败
        /// </summary>
        KuaiShang_CompleteAdjustFail = 505,
        /// <summary>
        /// UR接收回执
        /// </summary>
        UR_Confirm = 601,
        /// <summary>
        /// UR处理订单
        /// </summary>
        UR_CompleteOrder = 602,
        /// <summary>
        /// UR接收销售回执
        /// </summary>
        UR_SaleConfirm = 603,
        /// <summary>
        /// UR接收退货回执
        /// </summary>
        UR_ReturnConfirm = 604,
        /// <summary>
        /// UR推送订单
        /// </summary>
        UR_SyncSalesOrderInfo=605,
        /// <summary>
        /// UR接受盘点回执
        /// </summary>
        UR_AdjustConfim = 606,
        /// <summary>
        /// 向准时达推送入库订单
        /// </summary>
        ZhunShiDa_PushInOrder = 701,
        /// <summary>
        /// 向准时达推送出库订单
        /// </summary>
        ZhunShiDa_PushOutOrder = 702,
        /// <summary>
        /// 准时达入库反馈
        /// </summary>
        ZhunShiDa_ReceiptInOrder = 703,
        /// <summary>
        /// 准时达出库反馈
        /// </summary>
        ZhunShiDa_ReceiptOutOrder = 704,
        /// <summary>
        /// 准时达撤销订单
        /// </summary>
        ZhunShiDa_CancelOrder=705,
        /// <summary>
        /// 准时达 接收订单物流状态反馈
        /// </summary>
        ZhunShiDa_ReceiptLogisticsStatus = 706,
        /// <summary>
        /// 准时达 推送退换货订单
        /// </summary>
        ZhunShiDa_PushRefundOrder = 707,
        /// <summary>
        /// 春风物流仓能调出
        /// </summary>
        ChunFeng_TransferOut = 708,
        /// <summary>
        /// 春风物流仓能调出成功
        /// </summary>
        ChunFeng_TransferOutSuccess = 709,
        /// <summary>
        /// 春风物流仓能调出失败
        /// </summary>
        ChunFeng_TransferOutFail = 710
    }
}
