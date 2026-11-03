using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.XmlNodeClass
{
   public  class ZWPUWBW01
    {
        public IDOC IDOC { get; set; }
    }


    //public class AdibusInvReceiveModel : BaseEntity
    //{
    //    public string RCVPRN { get; set; }
    //    public string MESCOD { get; set; }
    //    public string BELEGDATUM { get; set; }
    //    public string BONNUMMER { get; set; }
    //    public string STORE1 { get; set; }
    //    public string STORE2 { get; set; }
    //    public string REMARK1 { get; set; }
    //    public string QUALVORG { get; set; }
    //    public string REFERENZNR { get; set; }
    //    public Nullable<long> LITEM { get; set; }
    //    public string QUALARTNR { get; set; }
    //    public string ARTNR { get; set; }
    //    public string BEWART { get; set; }

    //    //public Nullable<decimal> MENGE { get; set; }
    //    public string MENGE { get; set; }
    //    public string CompanyID { get; set; }

    //    public string MoveType { get; set; }
    //}

    ///// <summary>
    ///// 调拨明细
    ///// </summary>
    //public class AdibusInvMoveModel : BaseEntity
    //{
    //    public string BillNo { get; set; }
    //    public string ManualBillNo { get; set; }
    //    public string Sequence { get; set; }
    //    public string UnitName { get; set; }
    //    public string CheckDate { get; set; }
    //    public string OUTStockCode { get; set; }



    //    public string InStockCode { get; set; }
    //    public string BarCode { get; set; }
    //    public decimal Qty { get; set; }
    //}
    ///// <summary>
    ///// 调拨单
    ///// </summary>
    //public class AdibusMaster : BaseEntity
    //{
    //    public string BillNo { get; set; }
    //    public string CompanyID { get; set; }
        
    //    public string BillTypeID { get; set; }
    //    /// <summary>
    //    /// 2.店调拨,3.出货,4.回货
    //    /// </summary>
    //    public string MoveType { get; set; }
    //    public string OutStockCode { get; set; }
    //    public string InStockCode { get; set; }
    //    public DateTime InCheckDate { get; set; }
    //    public DateTime OutCheckDate { get; set; }
    //    public string OutShopCode { get; set; }
    //    public string InShopCode { get; set; }
    //    public string OutShopID { get; set; }
    //    public string InShopID { get; set; }
    //}

    //public class AdibusInvQTckModel : BaseEntity
    //{

    //    public string BONNUMMER { get; set; }
    //    public string REFERENZNR { get; set; }
    //    public string REMARK1 { get; set; }
    //    public string LITEM { get; set; }
    //    public string QUALVORG { get; set; }
    //    public string QUALARTNR { get; set; }
    //    public string ARTNR { get; set; }
    //    public string BEWART { get; set; }
    //    public Nullable<decimal> MENGE { get; set; }
    //    public string KOSTL { get; set; }

    //    public string CompanyID { get; set; }

    //    public string BillTypeID { get; set; }


    //}
}
