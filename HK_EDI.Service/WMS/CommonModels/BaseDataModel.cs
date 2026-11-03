using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.CommonModels
{
    public class BaseDataModel : BaseEntity
    {
        public string SKUCode { get; set; }
        public string ActionType { get; set; }
        public string MaterialShortName { get; set; }
        public string MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string SizeName { get; set; }
        public string BarCode { get; set; }
        public string ColorName { get; set; }
        public string StyleCode { get; set; }
        public string SizeName1 { get; set; }
        public string SyncProductInfo_Master { get; set; }
        public decimal WeightNo { get; set; }
        public decimal Volume { get; set; }
        public decimal ProRetailPrice { get; set; }
        public string UnitName { get; set; }
        public int isError { get; set; }
        public string OSInterfaceID { get; set; }
        public string KindName { get; set; }
        public string CardName { get; set; }
        public string SeasonName { get; set; }
        public int YearNo { get; set; }
        public string Element { get; set; }
        public bool? UniqueCode { get; set; }
        public string ModelName { get; set; }

    }

    public class SettingModel
    {
        public string CustID { get; set; }
        public string CompID { get; set; }
        public string VendeeCode { get; set; }
        public string StockID { get; set; }
        public string PlanID { get; set; }
        public string Url { get; set; }
        public string ServerName { get; set; }
        public string jsCustID { get; set; }
        public bool IsSign { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
        public string Sku_GroupID { get; set; }
        public string Sku_TypeID { get; set; }
        public string CompanyIds { get; set; }

        public string ownerCode { get; set; }
        public string warehouseCode { get; set; }
        public string user_id { get; set; }

        public string owner_code { get; set; }
        public string oms_warehouse_code { get; set; }
        public string wms_warehouse_code { get; set; }
        public string appkey { get; set; }
        public bool IsPostInformation { get; set; }

        public string IssuedID { get; set; }
        public bool Ishuan { get; set; }

        public bool IsFlow { get; set; }

        public bool IsWarehouse { get; set; }

    }

    public class CommonReturnModel
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string Cust_OrderCode { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string OrderStatu { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string MessageStr { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
