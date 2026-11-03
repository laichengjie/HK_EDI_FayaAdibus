using HK_EDI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.BasSet
{
    public class StockSetDal
    {
        #region erp仓


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="DiscountType">折扣类型</param>
        /// <returns></returns>
        public DataTable QueryErpStock()
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = @"SELECT ROW_NUMBER() OVER (ORDER BY id ASC) AS Xuhao ,ID,ErpStockID,IsNull(IsDemorderForType,0) IsDemorderForType ,CASE IsNull(IsDemorderForType,0)
                                WHEN '0' THEN '否'
                                WHEN '1' THEN '是' END IsDemorderForTypeName, ModifyDTM FROM HK_FayaAdibus_ErpStockSet ";
               
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
        public void UpdateErpStock(string ID, string ErpStockID,int IsDemorderForType, string ModifyDTM)
        {
            try
            {

                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT top 1 ID FROM	HK_FayaAdibus_ErpStockSet where ErpStockID='" + ErpStockID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_FayaAdibus_ErpStockSet]
                                                                            ([ErpStockID]
                                                                            ,[IsDemorderForType],[ModifyDTM])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}','{2}')", ErpStockID, IsDemorderForType, ModifyDTM);
                    DBHelper.ExecuteScalar(sqlselect);


                }
                else
                {
                    var sql = string.Format(@" UPDATE [dbo].[HK_FayaAdibus_ErpStockSet]
                                                               SET [ErpStockID] = '{0}'
                                                                   ,[IsDemorderForType] ='{1}'
                                                                  ,[ModifyDTM] ='{2}'
                                                                  where ID='" + ID + "'", ErpStockID, IsDemorderForType, ModifyDTM);
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
        public void DeleteErpStock(string ID, string ErpStockID)
        {
            try
            {
                var sql = string.Format(@" Delete [dbo].[HK_FayaAdibus_ErpStockSet] where ID='" + ID + "' and ErpStockID = '" + ErpStockID + "'");
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
        public DataTable QueryCorStock(string erpStocID)
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = " SELECT ROW_NUMBER() OVER(ORDER BY id ASC) AS Xuhao, ID, ErpStockID, CorStockCode, CorStockName,Checker, ModifyDTM FROM HK_FayaAdibus_ErpCorStockSet ";
                if (!string.IsNullOrEmpty(erpStocID))
                {
                    sql += "where ErpStockID ='" + erpStocID + "'";
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
        public void UpdateCorStock(string ID, string ErpStockID, string CorStockCode, string CorStockName, string Checker, string ModifyDTM)
        {
            try
            {

                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT top 1 1 FROM	HK_FayaAdibus_ErpCorStockSet where ID='" + ID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_FayaAdibus_ErpCorStockSet]
                                                                            ([ErpStockID]
                                                                            ,[CorStockCode]
                                                                            ,[CorStockName]
                                                                            ,[Checker]   
                                                                            ,[ModifyDTM])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}'
                                                                            ,'{2}'
                                                                            ,'{3}'
                                                                            ,'{4}')", ErpStockID, CorStockCode, CorStockName, Checker,ModifyDTM);
                    DBHelper.ExecuteScalar(sqlselect);


                }
                else
                {
                    var sql = string.Format(@" UPDATE [dbo].[HK_FayaAdibus_ErpCorStockSet]
                                                               SET [ErpStockID] = '{0}'
                                                                  ,[CorStockCode] ='{1}'
                                                                  ,[CorStockName] ='{2}'
                                                                  ,[Checker] ='{3}'
                                                                  ,[ModifyDTM] ='{4}'
                                                                  where ID='" + ID + "'", ErpStockID, CorStockCode, CorStockName, Checker, ModifyDTM);
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
        public void DeleteCorStock(string ID, string CorStockCode)
        {
            try
            {
                var sql = string.Format(@" Delete [dbo].[HK_FayaAdibus_ErpCorStockSet] where ID='" + ID + "' and CorStockCode = '" + CorStockCode + "'");
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
        public DataTable GetStockName(string CompanyId, string StockID)
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = string.Format(@" SELECT StockID,StockCode,StockName   FROM    dbo.Bas_Stock
                            WHERE   AllowUsed =1 AND CompanyID  IN ('00000000', '{0}')", CompanyId);
                if (!string.IsNullOrEmpty(StockID))
                {
                    sql += "and StockID='" + StockID + "'";
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
        //public DataTable GetStockName(string CompanyId, string StockID)
        //{

        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        var sql = string.Format(@" SELECT StockID,StockCode,StockName   FROM    dbo.Bas_Stock
        //                    WHERE   AllowUsed =1 AND CompanyID  IN ('00000000', '{0}')", CompanyId);
        //        if (StockID != "")
        //        {
        //            sql += "and StockID='" + StockID + "'";
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
