using HK_EDI.Core.Domain.EDI_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
    public class lockExtend
    {
        private static List<lockObjects> lockArray = new List<lockObjects>();

        private static object lockobject = new object();

        #region  取得要锁的对象
        public  lockObjects GetPostSkuLock(SD_Adibus_Interface config, string Name)
        {
            lock (lockobject)
            { //保证这个锁队列是1个1个执行的.
                if (!lockArray.Any(t => t.Key == config.OSInterfaceID && t.Name == Name))
                {
                    lockArray.Add(new lockObjects() { Key = config.OSInterfaceID, Name = Name });
                }
                return lockArray.First(t => t.Key == config.OSInterfaceID && t.Name == Name);
            }
        }
        #endregion
    }

    public class lockObjects
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物流ID
        /// </summary>
        public string Key { get; set; }
    }
}
