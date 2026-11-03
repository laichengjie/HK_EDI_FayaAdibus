using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HK_EDI.Core;
using HK_EDI.Service.Common;
using HK_EDI.Core.Infrastructure;
using System.Text;
using System.Xml.Serialization;
//using System.Net.Http.Formatting;
using System.Net.Http;
using System.Collections.Generic;
using System.Web;
using HK_EDI.Service.WMS.Best.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test1 = "service=OrderStatus&custId=00000008&content=<OrderStatus>  <OrderCode>2017081692614</OrderCode>  <OrderType>销售出货</OrderType>  <WMSOrderID>34821169</WMSOrderID>  <TmsCode>ZTO</TmsCode>  <TmsName>中通快递</TmsName>  <MailNo>535926889414</MailNo>  <OrderTime>2017-08-16 19:12:09</OrderTime>  <Items>    <Item>      <SkuCode>A990127-3M</SkuCode>      <Qty>1</Qty>    </Item>  </Items></OrderStatus>&sign=4F883BE643ADC4D64E2D2AA01A3B3C0F";
            
            //var data = XmlHelper.XmlSerialize(request, Encoding.UTF8);
            string data = "%3c%3fxml+version%3d%221.0%22+encoding%3d%22utf-8%22+%3f%3e%3cReceiptsWriteback%3e%3corderCode%3e2015092410861%3c%2forderCode%3e%3cownerCode%3eMMG%3c%2fownerCode%3e%3creceiptDate%3e2015%2f9%2f24+16%3a53%3a20%3c%2freceiptDate%3e%3cwarehouseCode%3eBymWhId%3c%2fwarehouseCode%3e%3creceiptType%3eReturn%3c%2freceiptType%3e%3clogisticProviderId%3e%3c%2flogisticProviderId%3e%3coperator%3etest104%3c%2foperator%3e%3cnote%3e%e4%bb%8e%e5%8d%95%e6%8d%ae%3aRQ150924HKH01-003%e5%a4%8d%e5%88%b6%e4%ba%a7%e7%94%9f%3c%2fnote%3e%3cproducts%3e%3cproduct%3e%3cskuCode%3eredR2%3c%2fskuCode%3e%3citemName%3eredR2%3c%2fitemName%3e%3citemCount%3e100%3c%2fitemCount%3e%3clotNumber%3e150924000030%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3eB133401ACHR23-%3c%2fskuCode%3e%3citemName%3eredR2%3c%2fitemName%3e%3citemCount%3e100%3c%2fitemCount%3e%3clotNumber%3e150924000029%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3eB133405-PUP22-%3c%2fskuCode%3e%3citemName%3eredR2%3c%2fitemName%3e%3citemCount%3e100%3c%2fitemCount%3e%3clotNumber%3e150924000028%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3eB133302-PUP22-%3c%2fskuCode%3e%3citemName%3eredR2%3c%2fitemName%3e%3citemCount%3e100%3c%2fitemCount%3e%3clotNumber%3e150924000027%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e001000315x05%3c%2fskuCode%3e%3citemName%3eredR1%3c%2fitemName%3e%3citemCount%3e50%3c%2fitemCount%3e%3clotNumber%3e150924000026%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e0010001x08%3c%2fskuCode%3e%3citemName%3eredR1%3c%2fitemName%3e%3citemCount%3e50%3c%2fitemCount%3e%3clotNumber%3e150924000025%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e0010001x09%3c%2fskuCode%3e%3citemName%3eredR1%3c%2fitemName%3e%3citemCount%3e50%3c%2fitemCount%3e%3clotNumber%3e150924000024%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e001000315x06%3c%2fskuCode%3e%3citemName%3eredR1%3c%2fitemName%3e%3citemCount%3e50%3c%2fitemCount%3e%3clotNumber%3e150924000023%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e20150813001x06%3c%2fskuCode%3e%3citemName%3e20150813001%3c%2fitemName%3e%3citemCount%3e20%3c%2fitemCount%3e%3clotNumber%3e150924000022%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e201508311311x09%3c%2fskuCode%3e%3citemName%3e201508311311%3c%2fitemName%3e%3citemCount%3e500%3c%2fitemCount%3e%3clotNumber%3e150924000021%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e201508311311x08%3c%2fskuCode%3e%3citemName%3e201508311311%3c%2fitemName%3e%3citemCount%3e500%3c%2fitemCount%3e%3clotNumber%3e150924000020%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e201508311311x07%3c%2fskuCode%3e%3citemName%3e201508311311%3c%2fitemName%3e%3citemCount%3e500%3c%2fitemCount%3e%3clotNumber%3e150924000019%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3cproduct%3e%3cskuCode%3e201508311311x06%3c%2fskuCode%3e%3citemName%3e201508311311%3c%2fitemName%3e%3citemCount%3e500%3c%2fitemCount%3e%3clotNumber%3e150924000018%3c%2flotNumber%3e%3cremark%3e%3c%2fremark%3e%3c%2fproduct%3e%3c%2fproducts%3e%3c%2fReceiptsWriteback%3e";
            string url = "http://localhost:4080/Api/WMSTH/UpdateOrderStatus";
            HK_EDI.Service.Common.WebClient wc = new HK_EDI.Service.Common.WebClient();
            HK_EDI.Service.Common.MultipartForm mf = new MultipartForm();
            mf.AddString("logistics_interface", data);
            mf.AddString("data_digest", "KE0oU+cYD7YDYzLF8tq20g==1");
            //  mf.AddString("data_digest", System.Web.HttpUtility.UrlEncode("KE0oU+cYD7YDYzLF8tq20g==1"));
            string result = wc.Post(url, test1);
        }
    }
}