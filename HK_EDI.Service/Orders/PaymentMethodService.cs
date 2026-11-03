using HK_EDI.Core;
using HK_EDI.Core.Common;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Data;
using HK_EDI.Service.Orders;
using HK_EDI.Service.WMS;
using HK_EDI.Service.WMS.Best.EnumFolds;
using HK_EDI.Service.WMS.CommonModels;
using HK_EDI.Service.XmlNodeClass;
using HK_EDI.Services.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HK_EDI.Service.Orders
{
    public class PaymentMethodService
    {
        public DataTable GetReceiptType()
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = @"  SELECT ROW_NUMBER() OVER(ORDER BY a.CompanyID,a.ReceiptTypeID) ID,
                               a.CompanyID,b.CompanyCode,a.ReceiptTypeID,a.ReceiptTypeCode, a.ReceiptTypeName,
                               ISNULL(C.AdibusSystemDef,'') AdibusSystemDef,IsNull(IsDiscount,0) IsDiscount ,CASE IsNull(IsDiscount,0)
                                WHEN '0' THEN '否'
                                WHEN '1' THEN '是' END      IsDiscountName
							    FROM Bas_ReceiptType A    
	                           LEFT JOIN dbo.Bas_Company B ON B.CompanyID = A.CompanyID
	                           LEFT JOIN HK_AdibusReceiptTypeMap C ON C.CompanyID = A.CompanyID AND C.ReceiptTypeID = A.ReceiptTypeID
		                       WHERE a.AllowUsed = 1";

                dt = DBHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {

            }
            return dt;
        }
        public void UpdateReceiptType( string CompanyId,  string ReceiptTypeID, string AdibusSystemDef, int IsDiscount = 0)
        {
            try
            {
                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT TOP 1 1  FROM HK_AdibusReceiptTypeMap WHERE CompanyID = '"+ CompanyId + "'  AND ReceiptTypeID ='"+ ReceiptTypeID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_AdibusReceiptTypeMap]
                                                                            ([CompanyID]
                                                                            ,[ReceiptTypeID]
                                                                            ,[AdibusSystemDef]
                                                                            ,[ModifyDTM] 
                                                                            ,[IsDiscount])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}'
                                                                            ,'{2}'
                                                                            ,'{3}'
                                                                            ,'{4}')", CompanyId, ReceiptTypeID, AdibusSystemDef, DateTime.Now,IsDiscount);
                    DBHelper.ExecuteScalar(sqlselect);
                }
                else
                {
                    var sql = string.Format(@"  UPDATE [dbo].[HK_AdibusReceiptTypeMap] SET [AdibusSystemDef] = '{0}',
                                                                                        [ModifyDTM] ='{1}' ,
                                                                                        [IsDiscount] ='{2}' 
                                                                                        WHERE CompanyID = '" + CompanyId + "'  AND ReceiptTypeID ='" + ReceiptTypeID + "'", AdibusSystemDef, DateTime.Now, IsDiscount);
                    DBHelper.ExecuteScalar(sql);
                }

            }
            catch (Exception e)
            {

            }
        }

    }
}
