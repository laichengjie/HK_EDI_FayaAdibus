using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.EDI_Config
{
    public class SD_Adibus_Interface : BaseEntity
    {
        public string CompanyID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string OSInterfaceID { get; set; }

        /// <summary>
        /// 接口代码
        /// </summary>
        public string OSInterfaceCode { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>

        public string OSInterfaceName { get; set; }

        /// <summary>
        /// 合作系统
        /// </summary>
        public string CooperationSys { get; set; }

        /// <summary>
        /// 合作系统授权码
        /// </summary>
        public string CSysAccreditID { get; set; }

        /// <summary>
        /// 作业频率
        /// </summary>
        public string JobFrequency { get; set; }
        
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerviceURL { get; set; }
        

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 允许使用
        /// </summary>
        public bool? AllowUsed { get; set; }

        public DateTime? ModifyDTM { get; set; }

        public SD_Adibus_InterfaceDetail[] SD_Adibus_InterfaceDetail { get; set; }
    }
}
