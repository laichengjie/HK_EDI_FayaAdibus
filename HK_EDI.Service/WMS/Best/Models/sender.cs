using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    /// <summary>
    /// 发货人信息
    /// </summary>
    public class sender
    {
        /// <summary>
        /// 省
        /// </summary>
        [XmlElementAttribute("province")]
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [XmlElementAttribute("city")]
        public string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [XmlElementAttribute("shippingAddress")]
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElementAttribute("name")]
        public string Contact_Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [XmlElementAttribute("phoneNumber")]
        public string TelPhone { get; set; }
    }
}
