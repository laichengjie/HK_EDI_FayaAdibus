using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.ChunFeng
{
     public class Utils
    {
         /// <summary>
         /// json序列化
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="_products"></param>
         /// <returns></returns>
       public static string SerializeData<T>(T products)
       {
           return JsonConvert.SerializeObject(products);
       }
        
         /// <summary>
         /// json反序列化
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="data"></param>
         /// <returns></returns>
       public static T DeserializeObject<T>(string data)
       {
           return JsonConvert.DeserializeObject<T>(data);
       }

       public class ChinaDateTimeConverter : DateTimeConverterBase
       {
           private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

           public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
           {
               return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
           }

           public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
           {
               dtConverter.WriteJson(writer, value, serializer);
           }
       }


       /// <summary>
       /// 将List转换成DataTable
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="data"></param>
       /// <returns></returns>
       public static DataTable ToDataTable<T>(List<T> data)
       {
           PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
           DataTable dt = new DataTable();
           for (int i = 0; i < properties.Count; i++)
           {
               PropertyDescriptor property = properties[i];
               dt.Columns.Add(property.Name, property.PropertyType);
           }
           object[] values = new object[properties.Count];
           foreach (T item in data)
           {
               for (int i = 0; i < values.Length; i++)
               {
                   values[i] = properties[i].GetValue(item);
               }
               dt.Rows.Add(values);
           }
           return dt;
       }
       public static int BatchInert(string connectionString, string desTable, DataTable dt, int batchSize = 500)
       {
           using (var sbc = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction)
           {
               BulkCopyTimeout = 300,
               NotifyAfter = dt.Rows.Count,
               BatchSize = batchSize,
               DestinationTableName = desTable
           })
           {
               foreach (DataColumn column in dt.Columns)
                   sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName);
               sbc.WriteToServer(dt);
           }

           return dt.Rows.Count;
       }
       /// <summary> 
       /// 批量更新数据(每批次5000) 
       /// </summary> 
       /// <param name="connString">数据库链接字符串</param> 
       /// <param name="table"></param> 
       public static void Update(string connString, DataTable table)
       {
           SqlConnection conn = new SqlConnection(connString);
           SqlCommand comm = conn.CreateCommand();
           comm.CommandType = CommandType.Text;
           SqlDataAdapter adapter = new SqlDataAdapter(comm);
           SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
           commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
           try
           {
               conn.Open();
               //设置批量更新的每次处理条数 
               adapter.UpdateBatchSize = 5000;
               adapter.SelectCommand.Transaction = conn.BeginTransaction();/////////////////开始事务 
               if (table.ExtendedProperties["SQL"] != null)
               {
                   adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
               }
               adapter.Update(table);
               adapter.SelectCommand.Transaction.Commit();/////提交事务 
           }
           catch (Exception ex)
           {
               if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
               {
                   adapter.SelectCommand.Transaction.Rollback();
               }
               throw ex;
           }
           finally
           {
               conn.Close();
               conn.Dispose();
           }
       } 
    }
}
