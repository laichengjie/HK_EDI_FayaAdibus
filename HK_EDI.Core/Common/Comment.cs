using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Common
{
    public class Comment
    {
        #region 时间戳判断
        private static Int64 RequestMaxAgeInSeconds = 1800;  //30分钟
        /// <summary>
        /// 判断时间戳是否小于5分钟（是否重复请求）
        /// </summary>
        /// <param name="nonce">key</param>
        /// <param name="requestTimeStamp">请求时间戳</param>
        /// <returns></returns>
        public bool isRePalyRequest(string requestTimeStamp)
        {
            DateTime epochStart = new DateTime(1970,01,01,0,0,0,0,DateTimeKind.Utc);
            TimeSpan currentTs = DateTime.UtcNow - epochStart;
            var serverTotalSeconds = Convert.ToInt64(currentTs.TotalSeconds);
            var requestTotalSeconds = Convert.ToInt64(requestTimeStamp);
            var scale = serverTotalSeconds - requestTotalSeconds;
            if (scale > RequestMaxAgeInSeconds)
            {
                return true;
            }
           
            return false;

        }

        #endregion
    }
}
