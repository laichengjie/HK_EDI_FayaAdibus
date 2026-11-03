using HK_EDI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.BasSet
{
    public class VendCustSetDal
    {
        #region erp仓


        /// <summary>
        /// 查询
        /// </summary>
        public DataTable QueryErpVendCust()
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = @"SELECT ROW_NUMBER() OVER (ORDER BY id ASC) AS Xuhao ,ID,VendCustID,OperationType ,CASE OperationType
                                WHEN 'GetEstimatedShipment' THEN '期货采购订单'
                                WHEN 'GetPacking' THEN '采购到货单' END OperationTypeName,
                                BillState ,CASE BillState
                                WHEN '0' THEN '未送审'
                                WHEN '1' THEN '已生效' END BillStateName,ModifyDTM FROM HK_FayaAdibus_VendCustSet ";
               
                dt = DBHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {

            }
            return dt;
        }
        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ErpVendCustID"></param>
        /// <param name="OperationType"></param>
        /// <param name="BillState">0，未送审；1，已生效</param>
        /// <param name="ModifyDTM"></param>
        public void UpdateErpVendCust(string ID, string ErpVendCustID, string OperationType, string BillState, string ModifyDTM)
        {
            try
            {
                if(string.IsNullOrEmpty(OperationType))
                {
                    OperationType = "GetEstimatedShipment";
                }
                if (string.IsNullOrEmpty(BillState))
                {
                    BillState = "0";
                }



                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT top 1 ID FROM	HK_FayaAdibus_VendCustSet where VendCustID='" + ErpVendCustID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_FayaAdibus_VendCustSet]
                                                                            ([VendCustID]
                                                                            ,[OperationType],[BillState],[ModifyDTM])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}','{2}','{3}')", ErpVendCustID, OperationType,BillState, ModifyDTM);
                    DBHelper.ExecuteScalar(sqlselect);


                }
                else
                {
                    var sql = string.Format(@" UPDATE [dbo].[HK_FayaAdibus_VendCustSet]
                                                               SET [VendCustID] = '{0}'
                                                                  ,[OperationType] ='{1}'
                                                                  ,[BillState] ='{2}'
                                                                  ,[ModifyDTM] ='{3}'
                                                                  where ID='" + ID + "'", ErpVendCustID, OperationType, BillState, ModifyDTM);
                    DBHelper.ExecuteScalar(sql);
                }

            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="DiscountType"></param>
        public void DeleteErpVendCust(string ID, string ErpVendCustID)
        {
            try
            {
                var sql = string.Format(@" Delete [dbo].[HK_FayaAdibus_VendCustSet] where ID='" + ID + "' and VendCustID = '" + ErpVendCustID + "'");
                DBHelper.ExecuteScalar(sql);
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region 对应仓
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="DiscountType">折扣类型</param>
        /// <returns></returns>
        public DataTable QueryCorVendCust(string erpStocID)
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = " SELECT ROW_NUMBER() OVER(ORDER BY id ASC) AS Xuhao, ID, VendCustID, CorVendCustCode, CorVendCustName,Checker, ModifyDTM FROM HK_FayaAdibus_CorVendCustSet ";
                if (!string.IsNullOrEmpty(erpStocID))
                {
                    sql += "where VendCustID ='" + erpStocID + "'";
                }
                dt = DBHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="DiscountType"></param>
        /// <param name="Discount"></param>
        /// <param name="Checker"></param>
        /// <param name="ModifyDTM"></param>
        public void UpdateCorVendCust(string ID, string ErpVendCustID, string CorVendCustCode, string CorVendCustName, string Checker, string ModifyDTM)
        {
            try
            {

                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT top 1 1 FROM	HK_FayaAdibus_CorVendCustSet where ID='" + ID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_FayaAdibus_CorVendCustSet]
                                                                            ([VendCustID]
                                                                            ,[CorVendCustCode]
                                                                            ,[CorVendCustName]
                                                                            ,[Checker]   
                                                                            ,[ModifyDTM])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}'
                                                                            ,'{2}'
                                                                            ,'{3}'
                                                                            ,'{4}')", ErpVendCustID, CorVendCustCode, CorVendCustName, Checker,ModifyDTM);
                    DBHelper.ExecuteScalar(sqlselect);


                }
                else
                {
                    var sql = string.Format(@" UPDATE [dbo].[HK_FayaAdibus_CorVendCustSet]
                                                               SET [VendCustID] = '{0}'
                                                                  ,[CorVendCustCode] ='{1}'
                                                                  ,[CorVendCustName] ='{2}'
                                                                  ,[Checker] ='{3}'
                                                                  ,[ModifyDTM] ='{4}'
                                                                  where ID='" + ID + "'", ErpVendCustID, CorVendCustCode, CorVendCustName, Checker, ModifyDTM);
                    DBHelper.ExecuteScalar(sql);
                }

            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="DiscountType"></param>
        public void DeleteCorVendCust(string ID, string CorVendCustCode)
        {
            try
            {
                var sql = string.Format(@" Delete [dbo].[HK_FayaAdibus_CorVendCustSet] where ID='" + ID + "' and CorVendCustCode = '" + CorVendCustCode + "'");
                DBHelper.ExecuteScalar(sql);
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        /// <summary>
        /// 查询仓库名称
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetVendCustName(string CompanyId, string VendCustID)
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = string.Format(@" SELECT VendCustID,VendCustCode,VendCustName   FROM    dbo.vwSD_VendorManerage
                            WHERE   AllowUsed =1 AND CompanyID  IN ('00000000', '{0}')", CompanyId);
                if (!string.IsNullOrEmpty(VendCustID))
                {
                    sql += "and VendCustID='" + VendCustID + "'";
                }
                dt = DBHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        /// <summary>
        /// 查询操作员
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetUserName(string CompanyId, string UserID)
        {

            DataTable dt = new DataTable();
            try
            {
                var sql = string.Format(@" SELECT TOP 1 username   FROM dbo.Sys_User  where AllowUsed =1 AND CompanyID  IN ('00000000', '{0}') and CardTypeID='{1}'", CompanyId, UserID);

                dt = DBHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {

            }
            return dt;
        }
        //public DataTable GetVendCustName(string CompanyId, string VendCustID)
        //{

        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        var sql = string.Format(@" SELECT VendCustID,VendCustCode,VendCustName   FROM    dbo.vwSD_VendorManerage
        //                    WHERE   AllowUsed =1 AND CompanyID  IN ('00000000', '{0}')", CompanyId);
        //        if (VendCustID != "")
        //        {
        //            sql += "and VendCustID='" + VendCustID + "'";
        //        }
        //        dt = DBHelper.ExecuteDataTable(sql);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return dt;
        //}
    }

}
