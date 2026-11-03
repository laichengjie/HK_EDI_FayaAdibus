using BC.LogicPub;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Service.WMS;
using HK_EDI.Service.WMS.CommonModels;
using HK_EDI.Service.WMS.SKU360.Models;
using LogicPub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IOrderHandle
    {
        /// <summary>
        /// 取货品档案
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <param name="syncKey">输出参数 同步key</param>
        /// <param name="taskTime">输出参数 同步时间</param>
        /// <returns></returns>
        IList<BaseDataModel> GetProductData(Core.Domain.EDI_Config.SD_Adibus_Interface config, string key, ref string syncKey, ref TaskRunTime taskTime);

        /// <summary>
        /// 取订单主表
        /// </summary>
        /// <param name="config"></param>
        /// <param name="OpertionType">0出库 1入库</param>
        /// <returns></returns>
        IList<NormalOrderModel> GetOrderMasterData(Core.Domain.EDI_Config.SD_Adibus_Interface config, int OpertionType);

        /// <summary>
        /// 取订单明细
        /// </summary>
        /// <param name="config"></param>
        /// <param name="normalOrder">主单实体</param>
        /// <param name="OpertionType">0出库 1入库</param>
        /// <param name="orderTrace"></param>
        /// <returns></returns>
        IList<NormalOrderDetailModel> GetOrderDetailData(Core.Domain.EDI_Config.SD_Adibus_Interface config, NormalOrderModel normalOrder, int OpertionType, ref JobOrderTrace orderTrace, ref int orderType);

        /// <summary>
        /// 取待处理订单
        /// </summary>
        /// <param name="config"></param>
        /// <param name="InterfaceType">状态暂时无用</param>
        /// <returns></returns>
        IList<CompleteOrderGetModel> GetCompleteOrder(Core.Domain.EDI_Config.SD_Adibus_Interface config, int InterfaceType);

        /// <summary>
        /// 处理订单
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con">网店零售订单必须 连接字符串</param>
        /// <param name="Model"></param>
        /// <param name="jobtrace"></param>
        /// <param name="result"></param>
        /// <param name="settingModel">网店零售订单必须 </param>
        void ProcessOmsOrder<T>(string con, CompleteOrderGetModel Model, JobOrderTrace jobtrace, IRequestBase<T> result, SettingModel settingModel, bool uploadState = true) where T : IProductBase;

        /// <summary>
        /// 作废取消的订单
        /// </summary>
        /// <param name="billNo"></param>
        void BillCancel(string billNo);

        /// <summary>
        /// 货品档案上传记录
        /// </summary>
        /// <param name="dt"></param>
        void UpdatePostSkuErrorRecords(DataTable dt);

        /// <summary>
        /// 取的日志状态记录表的订单类型
        /// </summary>
        /// <param name="billTypeId">类型ID</param>
        /// <returns></returns>
        int GetOrderType(string billTypeId);

        /// <summary>
        /// 增加订单信息
        /// </summary>
        /// <param name="orderTrace"></param>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        JobOrderTrace AddOrderTrace(JobOrderTrace orderTrace, NormalOrderModel model,  int orderType);

        /// <summary>
        /// 增加订单信息
        /// </summary>
        /// <param name="orderTrace"></param>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        JobOrderTrace AddOrderTrace(JobOrderTrace orderTrace, NormalOrderModel model, string OSInterfaceID, int orderType,string ShopID, string StockID, DateTime BillDate);
        /// <summary>
        /// 初始化table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="detail"></param>
        void InitializeDataTable(DataTable dt, DataTable detail);

        /// <summary>
        /// 添加日志明细
        /// </summary>
        /// <param name="jobtrace"></param>
        /// <param name="ex"></param>
        void AddJobOrderTraceDetail(JobOrderTrace jobtrace, string ex);

        IList<OrderValidityResult> CheckOrderValidity(string billNo,string ossStockID);
    }
}
