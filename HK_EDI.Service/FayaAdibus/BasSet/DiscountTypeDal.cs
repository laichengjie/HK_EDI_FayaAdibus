using HK_EDI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.BasSet
{
    public class DiscountTypeDal
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="DiscountType">折扣类型</param>
        /// <returns></returns>
        public DataTable QueryDiscountType(string DiscountType)
        {
            DataTable dt = new DataTable();
            try
            {
                var sql = "SELECT ROW_NUMBER() OVER (ORDER BY id ASC) AS Xuhao ,ID,DiscountType,Discount,Checker,ModifyDTM FROM HK_FayaAdibus_DiscountType ";
                if (!string.IsNullOrEmpty(DiscountType))
                {
                    sql += "where AliCardCode like'" + DiscountType + "%'";
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
        public void UpdateDiscountType(string ID, string DiscountType, decimal Discount, string Checker, string ModifyDTM)
        {
            try
            {
            
                //string CardTypeCode=System.Configuration.ConfigurationManager.AppSettings["CardTypeCode"];
                DataTable dt = DBHelper.ExecuteDataTable("SELECT top 1 1 FROM	HK_FayaAdibus_DiscountType where ID='" + ID + "'");
                if (dt.Rows.Count < 1)
                {
                    var sqlselect = string.Format(@"INSERT INTO [dbo].[HK_FayaAdibus_DiscountType]
                                                                            ([DiscountType]
                                                                            ,[Discount]
                                                                            ,[Checker]
                                                                            ,[ModifyDTM])
                                                                          
                                                                      VALUES
                                                                            ('{0}'
                                                                            ,'{1}'
                                                                            ,'{2}'
                                                                            ,'{3}')", DiscountType, Discount, Checker, ModifyDTM);
                    DBHelper.ExecuteScalar(sqlselect);


                }
                else
                {
                    var sql = string.Format(@" UPDATE [dbo].[HK_FayaAdibus_DiscountType]
                                                               SET [DiscountType] = '{0}'
                                                                  ,[Discount] = '{1}'
                                                                  ,[Checker] = '{2}'
                                                                  ,[ModifyDTM] ='{3}'
                                                                  where ID='" + ID + "'", DiscountType, Discount, Checker, ModifyDTM);
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
        public void DeleteDiscountType(string ID, string DiscountType)
        {
            try
            {
                var sql = string.Format(@" Delete [dbo].[HK_FayaAdibus_DiscountType] where ID='" + ID + "' and DiscountType = '"+ DiscountType + "'");
                DBHelper.ExecuteScalar(sql);
            }
            catch (Exception e)
            {

            }
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
    }
}
