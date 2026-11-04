using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.GetReplenishmentOrder
{
    /// <summary>
    /// 获取FRS补货单（MW FRS Replenishment order）响应报文
    /// </summary>
    public class GetReplenishmentOrderBase
    {
        public List<GetReplenishmentOrderBaseHEAD> HEAD { get; set; }
        public List<GetReplenishmentOrderBaseBODY> BODY { get; set; }
    }

    public class GetReplenishmentOrderBaseHEAD
    {
        /// <summary>
        /// 状态编码（200 成功）
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDesc { get; set; }
    }

    /// <summary>
    /// 补货单明细，字段与接口响应报文体定义保持一致
    /// </summary>
    public class GetReplenishmentOrderBaseBODY
    {
        /// <summary>
        /// 单据类型（REPL）
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// 单据号（补货单号）
        /// </summary>
        public string DocNo { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public string Branch { get; set; }
        /// <summary>
        /// 发货仓编号
        /// </summary>
        public string DeparturePOSCode { get; set; }
        /// <summary>
        /// 收货店铺编号
        /// </summary>
        public string DestinationPOSCode { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 建议补货量
        /// </summary>
        public string ProposedQTY { get; set; }
        /// <summary>
        /// 备用字段1
        /// </summary>
        public string BackupField1 { get; set; }
        /// <summary>
        /// 备用字段2
        /// </summary>
        public string BackupField2 { get; set; }
        /// <summary>
        /// 备用字段3
        /// </summary>
        public string BackupField3 { get; set; }
        /// <summary>
        /// 备用字段4
        /// </summary>
        public string BackupField4 { get; set; }
        /// <summary>
        /// 备用字段5
        /// </summary>
        public string BackupField5 { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public string CurrentPage { get; set; }
        /// <summary>
        /// 总页码
        /// </summary>
        public string TotalPage { get; set; }
    }

    /// <summary>
    /// 请求报文头
    /// </summary>
    public class PostHeader
    {
        public string SERVICE_CODE { get; set; }
        public string CONSUMER_SYSCODE { get; set; }
        public string VERSION { get; set; }
        public string MESSAGE_ID { get; set; }
        public string DTSEND { get; set; }
    }

    /// <summary>
    /// 请求报文 DATA 节点
    /// </summary>
    public class PostDATA
    {
        public PostHEAD HEAD { get; set; }
    }

    /// <summary>
    /// 请求报文 DATA.HEAD 节点
    /// </summary>
    public class PostHEAD
    {
        /// <summary>
        /// KA调用方标识
        /// </summary>
        public string KAName { get; set; }
        /// <summary>
        /// 单据类型（REPL）
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// 发货仓编号（多个用“|”分隔，可不填）
        /// </summary>
        public string DeparturePOSCode { get; set; }
        /// <summary>
        /// 收货店铺编号（多个用“|”分隔，可不填）
        /// </summary>
        public string DestinationPOSCode { get; set; }
        /// <summary>
        /// 补货单生成时间（返回该日期生成的补货单）
        /// </summary>
        public string DocDate { get; set; }
        /// <summary>
        /// 获取时间（避免一天多次请求返回相同数据）
        /// </summary>
        public string GetDate { get; set; }
        /// <summary>
        /// 当前页码（初始值1，单页最多3000行）
        /// </summary>
        public string CurrentPage { get; set; }
    }
}
