using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.OrderTrace
{
    public class AbnormalOrderModel
    {
        public string createdDate { get; set; }
        public string toDatetime { get; set; }
        public string warehouseCode { get; set; }
        public string orderCodes { get; set; }

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