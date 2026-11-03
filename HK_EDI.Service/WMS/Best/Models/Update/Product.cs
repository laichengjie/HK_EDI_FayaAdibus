using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models.Update
{
    [XmlType(TypeName = "product")]
    public class Product
    {
        public string skuCode { get; set; }
        public string normalQuantity { get; set; } //良品数量
        public string defectiveQuantity { get; set; } //不良品数量

    }
}
