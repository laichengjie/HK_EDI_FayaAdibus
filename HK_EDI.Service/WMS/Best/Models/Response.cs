using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class Response
    {
        /// <summary>
        /// 应答xml中的request节点
        /// </summary>
        [XmlElementAttribute("request")]
        public ResponseRequest XRequest { get; set; }

        /// <summary>
        /// 应答xml中的response节点
        /// </summary>
        [XmlElementAttribute("response")]
        public ResponseResponse XResponse { get; set; }
    }

    /// <summary>
    /// 百世物流－包含在应答中的请求信息
    /// </summary>
    public class ResponseRequest
    {
        #region 公开属性

        /// <summary>
        /// partnerId
        /// </summary>
        [XmlElementAttribute("partnerId")]
        public string PartnerID { get; set; }

        /// <summary>
        /// msgId
        /// </summary>
        [XmlElementAttribute("msgId")]
        public string MessageID { get; set; }

        /// <summary>
        /// msgType
        /// </summary>
        [XmlElementAttribute("msgType")]
        public string MessageType { get; set; }

        /// <summary>
        /// serviceType
        /// </summary>
        [XmlElementAttribute("serviceType")]
        public string ServiceType { get; set; }

        #endregion
    }
    /// <summary>
    /// 百世物流－包含在应答中的应答信息
    /// </summary>
    public class ResponseResponse
    {
        #region 公开属性

        /// <summary>
        /// HTTP请求响应标志，为SUCCESS或者FAILURE或者INPROCESS
        /// </summary>
        [XmlElementAttribute("flag")]
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 数据有收到，签名也是通过，但里面的字段不正确
        /// </summary>
        [XmlElementAttribute("bizData")]
        public string BizData { get; set; }

        private string _decodedBizData = string.Empty;

        /// <summary>
        /// 解码后的BizData
        /// </summary>
        public string DecodedBizData
        {
            get
            {
                if (string.IsNullOrEmpty(_decodedBizData)) _decodedBizData = System.Web.HttpUtility.UrlDecode(BizData);

                return _decodedBizData;
            }
        }

        private BizError _bizError = null;
        /// <summary>
        /// 具体错误
        /// </summary>
        public BizError BizError
        {
            get
            {
                if (_bizError == null) _bizError = Utils.Serialize4Best<BizError>(DecodedBizData,System.Text.Encoding.UTF8);
                return _bizError;
            }
        }

        #endregion
    }


    /// <summary>
    /// 百世物流－上传接口的具体错误，具体到字段
    /// </summary>
    [XmlType("error")]
    public class Error
    {
        #region 公开属性

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElementAttribute("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [XmlElementAttribute("errorDescription")]
        public string ErrorDiscription { get; set; }

        #endregion
    }
    public class BizError
    {
        #region 公开属性

        /// <summary>
        /// SUCCESS-成功
        /// FAILURE-失败
        /// </summary>
        [XmlElementAttribute("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("note")]
        public string Note { get; set; }

        [XmlArray("errors")]
        public List<Error> Errors { get; set; }

        #endregion

        #region 公开方法

        /*
        /// <summary>
        /// 从返回的字符串中创建
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static BizError Create(string xml)
        {
            //--<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            //--<loms:AsnInfo xmlns:loms="http://loms.800best.com" xmlns:ns3="http://www.w3.org/2001/XMLSchema-instance" ns3:schemaLocation="http://loms.800best.com /AsnInfo.xsd">
            //--    <flag>FAILURE</flag>
            //--    <errors>
            //--        <error>
            //--            <errorCode>ASN_ACTION_TYPE_INVALID</errorCode>
            //--            <errorDescription>入库单操作类型[var]不对</errorDescription>
            //--        </error>
            //--    </errors>
            //--</loms:AsnInfo>
            if (string.IsNullOrEmpty(xml)) return null;
            BizError biz = new BizError();
            XDocument xdoc = XDocument.Parse(xml);

            XElement xel = null;
            xel = xdoc.Root.Element("flag");
            if (xel != null) biz.Flag = xel.Value;
            xel = xdoc.Root.Element("note");
            if (xel != null) biz.Note = xel.Value;
            xel = xdoc.Root.Element("errors");
            if (xel != null)
            {
                System.Collections.Generic.IEnumerable<XElement> xErrors = xel.Elements("error");
                System.Collections.Generic.List<Error> __Errors = new System.Collections.Generic.List<Error>();

                foreach (XElement __xel in xErrors)
                {
                    Error __error = new Error();
                    XElement subEl = null;
                    subEl = __xel.Element("errorCode");
                    if (subEl != null) __error.ErrorCode = subEl.Value;
                    subEl = __xel.Element("errorDescription");
                    if (subEl != null) __error.ErrorDiscription = subEl.Value;

                    __Errors.Add(__error);
                }

                biz.Errors = __Errors.ToArray();
            }


            return biz;
        }
        */
        #endregion
    }
}

