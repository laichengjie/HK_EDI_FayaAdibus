using HK_EDI.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.SettingModel
{
    /// <summary>
    /// 折扣配置
    /// </summary>
    public class DiscountTypeSet : ISettings
    {
        public string ID { get; set; }
        /// <summary>
        /// 折扣类型
        /// </summary>
        public string DiscountType { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? ModifyDTM { get; set; }
    }
}
