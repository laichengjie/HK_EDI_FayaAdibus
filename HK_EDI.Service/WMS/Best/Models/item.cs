using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    /// <summary>
    /// 明细信息
    /// </summary>
    public class item
    {
        [XmlElementAttribute("itemSkuCode")]
        public string BarCode { get; set; }

        [XmlElementAttribute("itemQuantity")]
        public string Qty { get; set; }

        [XmlElementAttribute("fixStatusCode")]
        public string FixStatusCode { get; set; }

        [XmlElementAttribute("udf2")]
        public string CardName { get; set; }
        [XmlElementAttribute("udfFlag")]
        private bool _udfFlag;

        [XmlElementAttribute("lotAtt01")]
        public string CompanyID { get; set; }

        public bool UdfFlag
        {
            get
            {
                return true;
            }
            set
            {
                _udfFlag = value;
            }
        }
    }
}
