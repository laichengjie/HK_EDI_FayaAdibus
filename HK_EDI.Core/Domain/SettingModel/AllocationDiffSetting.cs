using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Configuration;

namespace HK_EDI.Core.Domain.SettingModel
{
    /// <summary>
    /// 收发差异接口
    /// </summary>
    public class AllocationDiffSetting : ISettings
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWorld { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string Companyid { get; set; }

        /// <summary>
        /// POS移动类型
        /// </summary>
        public string BEWART { get; set; }

        /// <summary>
        /// 消息变式
        /// </summary>
        public string MESCOD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OSInterfaceID { get; set; }
    }
}
