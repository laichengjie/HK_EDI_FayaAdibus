using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.Models
{
    class RequestBase
    {
        //授权用户
        public string PartnerID { get; set; }

        //业务数据
        public virtual string BizData { get; set; }

        //消息ID
        public string MessageID { get; set; }

        //消息类型
        public string MessageType { get; set; }

        //服务类型
        public string ServiceType { get; set; }

        //服务版本
        public string ServiceVersion { get; set; }

        //通知回调地址
        public string NotifyUrl { get; set; }
        //签名
        public string Sign { get; set; }
        //授权码
        public string PartnerKey { get;set; }

        private static string SignData(string PartnerID, string BizData, string PartnerKey, string MessageID, string MessageType, string ServiceType, string NotifyUrl)
        {
            string sign = @"partnerId={0}&bizData={1}&partnerKey={2}&msgId={3}&msgType={4}&serviceType={5}&serviceVersion=1.0&notifyUrl={6}";
            sign = string.Format(sign, PartnerID, BizData, PartnerKey, MessageID, MessageType, ServiceType, NotifyUrl);
            return Utils.Convert2HexMD5(sign, System.Text.Encoding.UTF8);
        }
        public RequestBase(string PartnerID,string PartnerKey, string BizData, string MessageType, string ServiceType)
        {
            this.PartnerID = PartnerID;
            this.BizData = BizData;
            this.MessageID = Guid.NewGuid().ToString();
            this.MessageType = MessageType;
            this.ServiceType = ServiceType;
            this.NotifyUrl = string.Empty;
            this.PartnerKey = PartnerKey;
            this.Sign = SignData(PartnerID, BizData,PartnerKey, MessageID, MessageType, ServiceType, NotifyUrl);
            
        }
        public override string ToString()
        {
            string parameter = @"partnerId={0}&bizData={1}&partnerKey={2}&msgId={3}&msgType={4}&serviceType={5}&serviceVersion=1.0&sign={6}&notifyUrl={7}";
            parameter = string.Format(parameter, PartnerID, System.Web.HttpUtility.UrlEncode(BizData), "", MessageID, MessageType, ServiceType, Sign, NotifyUrl);
            return parameter;
        }
    }
}
