using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.CommonModels
{
    public class NormalOrderModel : BaseEntity
    {
        public string CompanyID { get; set; }
        public string BillNo { get; set; }
        public string SourcebillNo { get; set; }
        public string BillTypeID { get; set; }
        public string StockID { get; set; }
        public string CustomerCode { get; set; }

    }

    public class NormalOrderDetailModel : BaseEntity
    {
        public string Sequence { get; set; }
        public string SourceBillNo { get; set; }
        public string BarCode { get; set; }
        public string MaterialCode { get; set; }
        public string SKUCode { get; set; } //货号加尺码
        public string MaterialShortName { get; set; }
        public decimal Qty { get; set; }
        public string Remark { get; set; }
        public decimal? Price { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string CardName { get; set; }
        public string CompanyID { get; set; }
        public decimal ActualQuantity { get; set; }
    }

    public class CancelOrderGetModel : BaseEntity
    {
        public string CompanyID { get; set; }
        public string BillNo { get; set; }
        public string TranState { get; set; }
        public string OrderType { get; set; }
        public string SignCode { get; set; }
        public int PId { get; set; }
    }

    public class CompleteOrderGetModel : BaseEntity
    {
        public string BillNo { get; set; }
        public string CompanyID { get; set; }
        public string SaleType { get; set; }
        public string Content { get; set; }
        public string SignCode { get; set; }

        public string BarCode { get; set; }
        public string SKUCode { get; set; }
    }

    public class vwSD_VendorManerageModel : BaseEntity
    {
        public string VendCustID { get; set; }
        public string VendCustCode { get; set; }
        public string VendCustName { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
    }

    public class productModel
    {
        public string SKUCode { get; set; }
        public string Qty { get; set; }
        public string lotNumber { get; set; }
        public string uniqueId { get; set; }
    }

    public class StockQueryModel:BaseEntity
    {
        public decimal Qty { get; set; }
        public string BarCode { get; set; }
    }
}
